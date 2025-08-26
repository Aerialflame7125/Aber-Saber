import websockets
import asyncio
import json
import threading
from flask import Flask, jsonify, request
import time
import os
import requests as r
from collections import Counter


last_seen = {}  # key = username, value = last activity timestamp (epoch)
playercontrollers = []
players = []
pauth = []
expiringusers = []
tempuser = None
tempuauth = None
tempx1 = 0
tempy1 = 0
tempz1 = 0
tempx2 = 0
tempy2 = 0
tempz2 = 0
templeftcontroller = None
temprightcontroller = None
response_data = {}
scores = []
player_map_urls = {}     # username -> download URL
player_cover_urls = {}   # username -> cover URL


app = Flask(__name__)

@app.route('/signin', methods=['POST'])
def SignIn():
    name = request.args.get('name')
    auth = request.args.get('auth')
    status = request.args.get('status')

    if status == "connecting":
        if name not in players:
            players.append(name)
            pauth.append(auth)
            playercontrollers.append("(0, 0, 0), (0, 0, 0)")
            scores.append(0)
        expiringusers.append(name)
        last_seen[name] = time.time()
        return jsonify({"status": "OK", "do": "set", "expire": "2"}), 200
    else:
        return jsonify({"status": "KO"}), 403

@app.route('/selectlevel', methods=['PUT'])
def SelectLevel():
    mapid = request.args.get('mapCode')
    username = request.args.get('name')
    auth = request.args.get('auth')

    if checkauth(username, auth):
        res = r.get(f"https://api.beatsaver.com/maps/id/{mapid}")
        mapinfo = res.json()
        version = mapinfo['versions'][0]

        player_map_urls[username] = version.get('downloadURL')
        player_cover_urls[username] = version.get('coverURL')

        return jsonify({"url": player_map_urls[username], "cover": player_cover_urls[username]})
    else:
        return jsonify({"status": "KO"}), 403


def starthttpserver():
    # Disable debug mode when running Flask in a separate thread
    app.run(host='0.0.0.0', port=3000, debug=False)



def checkauth(player, auth):
    global tempuser, pauth, players
    print()
    print("checkauth called for player and auth " + player + " " + auth)
    print("pauth: " + str(pauth))
    print("players: " + str(players))
    for i in range(len(players)):
        if players[i] == player:
            print(f"players[{i}] == {player}")
            if pauth[i] == auth:
                print(f"pauth[{i}] == {auth}")
                return True
            
def getplayerindex(player, auth):
    for i in range(len(players)):
        if players[i] == player and pauth[i] == auth:
            return i
    return None

def get_controller_data(data):
    return {
        "c1x": data.get("c1x", "0"),
        "c1y": data.get("c1y", "0"),
        "c1z": data.get("c1z", "0"),
        "c2x": data.get("c2x", "0"),
        "c2y": data.get("c2y", "0"),
        "c2z": data.get("c2z", "0"),
    }

def print_changes_table(old, new):
    print("\n=== Controller Update ===")
    print(f"{'Axis':<6} | {'Old':<10} | {'New':<10} | {'Changed?'}")
    print("-" * 40)
    for key in old.keys():
        o = old[key]
        n = new[key]
        changed = "✅ Yes" if o != n else "❌ No"
        print(f"{key:<6} | {o:<10} | {n:<10} | {changed}")
    print("-" * 40 + "\n")

async def set(websocket):
    global players, playercontrollers, tempuser, tempuauth, pauth, tempx1, tempy1, tempz1, tempx2, tempy2, tempz2, templeftcontroller, temprightcontroller, tempmap, tempmapcover
    
    player = None
    async for message in websocket:
        print(f"Received: {message}")
        try:
            pairs = message.split(',')
            data = {}
            for pair in pairs:
                if ':' in pair:
                    key, value = pair.split(':', 1)
                    data[key.strip()] = value.strip()

            print(f"Parsed data: {data}")

            player = data.get("p")
            if player:
                last_seen[player] = time.time()
            auth = data.get("auth")
            score = data.get("score")

            if not player or not auth:
                await websocket.send("KO")
                continue

            if not checkauth(player, auth):
                await websocket.send("KO")
                continue

            # Store auth and user
            tempuser = player
            tempuauth = auth

            if tempuser in expiringusers:
                expiringusers.remove(tempuser)

            # Update controllers
            tempx1 = data.get("c1x", "0")
            tempy1 = data.get("c1y", "0")
            tempz1 = data.get("c1z", "0")

            tempx2 = data.get("c2x", "0")
            tempy2 = data.get("c2y", "0")
            tempz2 = data.get("c2z", "0")

            templeftcontroller = f"({tempx1}, {tempy1}, {tempz1})"
            temprightcontroller = f"({tempx2}, {tempy2}, {tempz2})"
            bothcontrollers = f"({templeftcontroller}, {temprightcontroller})"

            # Extract new controller data
            new_data = get_controller_data(data)

            # Get previous controller data
            index = getplayerindex(player, auth)
            if index is not None:
                # Parse the old values from the stored string (e.g. "((x1, y1, z1), (x2, y2, z2))")
                try:
                    raw = playercontrollers[index]
                    raw = raw.replace("(", "").replace(")", "").replace(" ", "")
                    nums = raw.split(",")
                    old_data = {
                        "c1x": nums[0], "c1y": nums[1], "c1z": nums[2],
                        "c2x": nums[3], "c2y": nums[4], "c2z": nums[5]
                    }
                except Exception as e:
                    print(f"Failed to parse old controller data: {e}")
                    old_data = get_controller_data({})  # fallback to zeros

                # Compare and print change table
                print_changes_table(old_data, new_data)

                # Store new controller data
                templeftcontroller = f"({new_data['c1x']}, {new_data['c1y']}, {new_data['c1z']})"
                temprightcontroller = f"({new_data['c2x']}, {new_data['c2y']}, {new_data['c2z']})"
                bothcontrollers = f"({templeftcontroller}, {temprightcontroller})"
                playercontrollers[index] = bothcontrollers

                # Build response excluding the sender
                response_data = {}
                for i in range(len(players)):
                    other = players[i]
                    if other != tempuser:
                        lc, rc = playercontrollers[i].split("), (")
                        response_data[other] = {
                            "left_controller": lc.replace("(", ""),
                            "right_controller": rc.replace(")", ""),
                            "map_url": player_map_urls.get(other, ""),
                            "cover_url": player_cover_urls.get(other, "")
                        }

                for i in range(len(players)):
                    if players[i] != tempuser:
                        response_data[players[i]] = playercontrollers[i]
                        
                map_counts = Counter(player_map_urls.values())
                most_common_map, count = map_counts.most_common(1)[0] if map_counts else (None, 0)
                majority_threshold = len(players) // 2 + 1 if players else 0

                response_payload = {
                    "status": "OK",
                    "others": response_data
                }

                if most_common_map and count >= majority_threshold:
                    # Find the corresponding cover
                    for user, url in player_map_urls.items():
                        if url == most_common_map:
                            cover = player_cover_urls.get(user, "")
                            response_payload["majority_map_cover"] = cover
                            break

                await websocket.send(json.dumps(response_payload))
            else:
                await websocket.send("KO")

        except Exception as e:
            print(f"Error: {e}")
            await websocket.send("KO")

async def expire_users_loop():
    while True:
        now = time.time()
        expired = []

        for user in list(expiringusers):
            if user in last_seen:
                if now - last_seen[user] > 120:  # 2 minutes
                    expired.append(user)

        for user in expired:
            try:
                idx = players.index(user)
                print(f"[Expire] Removing inactive user: {user}")
                del players[idx]
                del pauth[idx]
                del playercontrollers[idx]
            except ValueError:
                pass  # User might have already been removed

            expiringusers.remove(user)
            last_seen.pop(user, None)
            player_map_urls.pop(user, None)
            player_cover_urls.pop(user, None)

        await asyncio.sleep(10)  # Check every 10 seconds


async def main():
    await asyncio.gather(
        websockets.serve(set, "0.0.0.0", 6000),
        expire_users_loop()
    )
    async with websockets.serve(set, "0.0.0.0", 6000):
        await asyncio.Future()

if __name__ == "__main__":
    thread = threading.Thread(target=starthttpserver)
    thread.start()
    asyncio.run(main())