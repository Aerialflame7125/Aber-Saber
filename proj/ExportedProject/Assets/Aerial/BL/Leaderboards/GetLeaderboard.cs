using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic; // Make sure to include this for List<T>
using TMPro;

namespace BL.Leaderboards
{
    public class GetLeaderboard : MonoBehaviour
    {
        // Top-level class for BeatLeader API response
        [System.Serializable]
        public class BLResponse
        {
            public BLMetadata metadata;
            public List<BLData> data; // Changed from 'string' to 'List<BLData>' to match JSON array
        }

        // Metadata class
        [System.Serializable]
        public class BLMetadata
        {
            public int itemsPerPage; // Changed to int, as it's a number
            public int page;         // Changed to int
            public int total;        // Changed to int
        }

        // Data class for each score entry
        [System.Serializable]
        public class BLData
        {
            public string headsetName;
            public string controllerName;
            public long id; // Changed to long to match potential large ID numbers
            public int baseScore;
            public int modifiedScore;
            public float accuracy; // Changed to float for decimal values
            public string playerId;
            public int pp;
            public int bonusPp;
            public int passPP;
            public int accPP;
            public int techPP;
            public int rank;
            public int responseRank;
            public string country;
            public float fcAccuracy; // Changed to float
            public int fcPp;
            public int weight;
            public string replay;
            public string modifiers;
            public int badCuts;
            public int missedNotes;
            public int bombCuts;
            public int wallsHit;
            public int pauses;
            public bool fullCombo; // Changed to bool
            public string platform;
            public int maxCombo;
            public int? maxStreak; // Nullable int for potential null values
            public int hmd;
            public int controller;
            public string leaderboardId;
            public string timeset; // Consider long if this is a Unix timestamp
            public long timepost; // Changed to long
            public int replaysWatched;
            public int playCount;
            public int lastTryTime;
            public int priority;
            public int originalId;
            public Player player; // Nested Player object
            public ScoreImprovement scoreImprovement; // Nested ScoreImprovement object
            public object rankVoting; // Use object if structure varies or you don't need to parse it
            public object metadata;   // Use object
            public object offsets;    // Use object
            public int sotwNominations;
            public int status;
        }

        // Player class
        [System.Serializable]
        public class Player
        {
            public string id;
            public string name;
            public string platform;
            public string avatar;
            public string country;
            public string alias;
            public bool bot;
            public float pp; // Changed to float
            public int rank;
            public int countryRank;
            public int level;
            public int experience;
            public int prestige;
            public string role;
            public List<Social> socials; // List of Social objects
            public object contextExtensions;
            public object patreonFeatures;
            public ProfileSettings profileSettings; // Nested ProfileSettings object
            public string clanOrder;
            public List<Clan> clans; // List of Clan objects
        }

        // Social class
        [System.Serializable]
        public class Social
        {
            public int id;
            public string service;
            public string link;
            public string user;
            public string userId;
            public string playerId;
            public bool hidden;
        }

        // ProfileSettings class
        [System.Serializable]
        public class ProfileSettings
        {
            public int id;
            public string bio;
            public string message;
            public string effectName;
            public string profileAppearance;
            public int hue;
            public int saturation;
            public object leftSaberColor; // Can be null
            public object rightSaberColor; // Can be null
            public string profileCover;
            public string starredFriends;
            public bool horizontalRichBio;
            public string rankedMapperSort; // Can be "null" string or actual value
            public bool showBots;
            public bool showAllRatings;
            public bool showExplicitCovers;
            public bool showStatsPublic;
            public bool showStatsPublicPinned;
        }

        // Clan class
        [System.Serializable]
        public class Clan
        {
            public int id;
            public string tag;
            public string color;
            public object name; // Can be null
        }

        // ScoreImprovement class
        [System.Serializable]
        public class ScoreImprovement
        {
            public long id; // Changed to long
            public string timeset; // Consider long if Unix timestamp
            public int score;
            public float accuracy; // Changed to float
            public int pp;
            public int bonusPp;
            public int rank;
            public float accRight; // Changed to float
            public float accLeft;  // Changed to float
            public int averageRankedAccuracy;
            public int totalPp;
            public int totalRank;
            public int badCuts;
            public int missedNotes;
            public int bombCuts;
            public int wallsHit;
            public int pauses;
            public string modifiers;
        }


        // BSData classes (from your original request, assuming BeatSaver API)
        [System.Serializable]
        public class BSData
        {
            public string id;
            public Uploader uploader;
            public List<BeatSaverVersion> versions; // Changed to List<BeatSaverVersion> to match typical BeatSaver API response
            // BeatSaver API typically has a hash within the 'versions' array, not a direct 'bshash' field.
            // We'll adjust for this in the parsing logic.
        }

        [System.Serializable]
        public class Uploader
        {
            public string id;
            public string name;
            public string avatar;
        }

        [System.Serializable]
        public class BeatSaverVersion
        {
            public string hash; // The hash is usually within a 'versions' array
            public string state;
            public string createdAt;
            public int sageScore;
            public List<Diff> diffs; // List of difficulties
            public string downloadURL;
            public string coverURL;
            public string previewURL;
        }

        [System.Serializable]
        public class Diff
        {
            public float njs;
            public float offset;
            public int notes;
            public int bombs;
            public int walls;
            public int difficulty; // This might be an enum or string in practice
            public float characteristic; // This might be an enum or string in practice
            public float events;
            public float chroma;
            public float me; // Mapping Extensions
            public float ne; // Noodle Extensions
            public float seconds;
            public string parity;
            public float stars;
            public int maxScore;
            public float environmentName; // This might be a string
            public float label; // This might be a string
        }

        // New class to represent the desired output format for each score entry
        [System.Serializable]
        public class CustomBeatLeaderScore
        {
            public string headsetName;
            public string controllerName;
            public int Place;
            public string Player;
            public int Score;
            public float Accuracy;
            public float fcAccuracy;
            public int fcPp;
            public string modifiers;
            public int missedNotes;
            public int bombCuts;
            public int wallsHit;
            public int pauses;
            public bool fullCombo;
            public int maxCombo;
            public string platform;
            public string avatar;
            public string country;
            public int playerRank;
            public int playerCountryRank;
            public List<string> playerClan; // Changed to List<string> to hold clan tags
        }


        IEnumerator Get(string uri, System.Action<string> callback)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                yield return webRequest.SendWebRequest();

                // Corrected error check for older Unity versions
                if (webRequest.isNetworkError || webRequest.isHttpError)
                {
                    Debug.LogError("Error: " + webRequest.error);
                    callback?.Invoke(null); // Indicate an error or no data
                }
                else
                {
                    callback?.Invoke(webRequest.downloadHandler.text); // Invoke the callback with the result
                }
            }
        }

        public IEnumerator ByIDGlobal(string mapID, string difficulty, string page, System.Action<string> callback) // Changed to IEnumerator and added a callback
        {
            List<CustomBeatLeaderScore> customScores = new List<CustomBeatLeaderScore>();

            // First, get data from BeatSaver to obtain the hash
            string beatSaverUri = $"https://api.beatsaver.com/maps/id/{mapID}";
            string bsJson = null;
            BSData beatSaverData;
            string mapHash = null;

            string mapcode = mapID;

            // OST maps (skip BeatSaver call)
            List<string> ostmaps = new List<string>{"TurnMeOn", "Escape", "100Bills", "Legend", "CountryRounds", "CommercialPumping", "Breezer", "BeatSaber", "LvlInsane", "BalearicPumping", "AngelVoices"};
            if (ostmaps.Contains(mapcode))
            {
                Debug.Log("Map is OST, Skipping BeatSaver.");
                mapHash = mapcode;
            }
            else
            {
                yield return Get(beatSaverUri, (json) => bsJson = json);

                if (string.IsNullOrEmpty(bsJson))
                {
                    Debug.LogError("Error fetching BeatSaver data.");
                    callback?.Invoke(null); // Indicate error
                    yield break;
                }

                beatSaverData = JsonUtility.FromJson<BSData>(bsJson);

                if (beatSaverData != null && beatSaverData.versions != null && beatSaverData.versions.Count > 0)
                {
                    mapHash = beatSaverData.versions[0].hash;
                    Debug.LogError(mapHash);
                }

                if (string.IsNullOrEmpty(mapHash))
                {
                    Debug.LogError("Could not find map hash from BeatSaver.");
                    callback?.Invoke(null); // Indicate error
                    yield break;
                }
            }

            // Now, use the hash to get data from BeatLeader

            string beatLeaderUri = $"https://api.beatleader.com/v3/scores/{mapHash}/{difficulty}/Standard/1/10/{page}";
            string blJson = null;

            yield return Get(beatLeaderUri, (json) => blJson = json);

            Debug.LogError(blJson);
            if (string.IsNullOrEmpty(blJson))
            {
                Debug.LogError("Error fetching BeatLeader data.");
                callback?.Invoke(null); // Indicate error
                yield break;
            }

            BLResponse beatLeaderResponse = JsonUtility.FromJson<BLResponse>(blJson);

            if (beatLeaderResponse != null && beatLeaderResponse.data != null)
            {
                foreach (var scoreData in beatLeaderResponse.data)
                {
                    CustomBeatLeaderScore customScore = new CustomBeatLeaderScore
                    {
                        headsetName = scoreData.headsetName,
                        controllerName = scoreData.controllerName,
                        Place = scoreData.rank,
                        Score = scoreData.modifiedScore,
                        Accuracy = scoreData.accuracy * 100f, // Multiply by 100 for percentage
                        fcAccuracy = scoreData.fcAccuracy * 100f, // Multiply by 100 for percentage
                        fcPp = scoreData.fcPp,
                        modifiers = scoreData.modifiers,
                        missedNotes = scoreData.missedNotes,
                        bombCuts = scoreData.bombCuts,
                        wallsHit = scoreData.wallsHit,
                        pauses = scoreData.pauses,
                        fullCombo = scoreData.fullCombo,
                        maxCombo = scoreData.maxCombo,
                    };

                    // Populate player-specific data if player object exists
                    if (scoreData.player != null)
                    {
                        customScore.Player = scoreData.player.name;
                        customScore.platform = scoreData.player.platform;
                        customScore.avatar = scoreData.player.avatar;
                        customScore.country = scoreData.player.country;
                        customScore.playerRank = scoreData.player.rank;
                        customScore.playerCountryRank = scoreData.player.countryRank;

                        // Extract clan tags
                        customScore.playerClan = new List<string>();
                        if (scoreData.player.clans != null)
                        {
                            foreach (var clan in scoreData.player.clans)
                            {
                                if (!string.IsNullOrEmpty(clan.tag))
                                {
                                    customScore.playerClan.Add(clan.tag);
                                }
                            }
                        }
                    }

                    customScores.Add(customScore);
                }

                // Convert the list of custom scores to a JSON array string
                // Unity's JsonUtility does not directly support serializing a List<T> at the root level.
                // We need a wrapper class for that.
                Wrapper<CustomBeatLeaderScore> wrapper = new Wrapper<CustomBeatLeaderScore>();
                wrapper.Items = customScores;
                string finalJson = JsonUtility.ToJson(wrapper, true); // 'true' for pretty printing

                callback?.Invoke(finalJson); // Send back the formatted JSON string
            }
            else
            {
                Debug.LogWarning("No BeatLeader scores found or error parsing data.");
                callback?.Invoke("[]"); // Return an empty JSON array if no data
            }
        }

        public IEnumerator ByIDAround(string mapID, string difficulty, string page, string playerid, System.Action<string> callback) // Changed to IEnumerator and added a callback
        {
            List<CustomBeatLeaderScore> customScores = new List<CustomBeatLeaderScore>();

            // First, get data from BeatSaver to obtain the hash
            string beatSaverUri = $"https://api.beatsaver.com/maps/id/{mapID}";
            string bsJson = null;
            BSData beatSaverData;
            string mapHash = null;

            string mapcode = mapID;

            // OST maps (skip BeatSaver call)
            List<string> ostmaps = new List<string> { "TurnMeOn", "Escape", "100Bills", "Legend", "CountryRounds", "CommercialPumping", "Breezer", "BeatSaber", "LvlInsane", "BalearicPumping", "AngelVoices" };
            if (ostmaps.Contains(mapcode))
            {
                Debug.Log("Map is OST, Skipping BeatSaver.");
                mapHash = mapcode;
            }
            else
            {
                yield return Get(beatSaverUri, (json) => bsJson = json);

                if (string.IsNullOrEmpty(bsJson))
                {
                    Debug.LogError("Error fetching BeatSaver data.");
                    callback?.Invoke(null); // Indicate error
                    yield break;
                }

                beatSaverData = JsonUtility.FromJson<BSData>(bsJson);

                if (beatSaverData != null && beatSaverData.versions != null && beatSaverData.versions.Count > 0)
                {
                    mapHash = beatSaverData.versions[0].hash;
                }

                if (string.IsNullOrEmpty(mapHash))
                {
                    Debug.LogError("Could not find map hash from BeatSaver.");
                    callback?.Invoke(null); // Indicate error
                    yield break;
                }
            }

            // Now, use the hash to get data from BeatLeader
            string beatLeaderUri = $"https://api.beatleader.com/v3/scores/{mapHash}/{difficulty}/Standard/1/1/around?player={playerid}&count=10";
            string blJson = null;

            yield return Get(beatLeaderUri, (json) => blJson = json);

            if (string.IsNullOrEmpty(blJson))
            {
                Debug.LogError("Error fetching BeatLeader data.");
                callback?.Invoke(null); // Indicate error
                yield break;
            }

            BLResponse beatLeaderResponse = JsonUtility.FromJson<BLResponse>(blJson);

            if (beatLeaderResponse != null && beatLeaderResponse.data != null)
            {
                foreach (var scoreData in beatLeaderResponse.data)
                {
                    CustomBeatLeaderScore customScore = new CustomBeatLeaderScore
                    {
                        headsetName = scoreData.headsetName,
                        controllerName = scoreData.controllerName,
                        Place = scoreData.rank,
                        Score = scoreData.modifiedScore,
                        Accuracy = scoreData.accuracy * 100f, // Multiply by 100 for percentage
                        fcAccuracy = scoreData.fcAccuracy * 100f, // Multiply by 100 for percentage
                        fcPp = scoreData.fcPp,
                        modifiers = scoreData.modifiers,
                        missedNotes = scoreData.missedNotes,
                        bombCuts = scoreData.bombCuts,
                        wallsHit = scoreData.wallsHit,
                        pauses = scoreData.pauses,
                        fullCombo = scoreData.fullCombo,
                        maxCombo = scoreData.maxCombo,
                    };

                    // Populate player-specific data if player object exists
                    if (scoreData.player != null)
                    {
                        customScore.Player = scoreData.player.name;
                        customScore.platform = scoreData.player.platform;
                        customScore.avatar = scoreData.player.avatar;
                        customScore.country = scoreData.player.country;
                        customScore.playerRank = scoreData.player.rank;
                        customScore.playerCountryRank = scoreData.player.countryRank;

                        // Extract clan tags
                        customScore.playerClan = new List<string>();
                        if (scoreData.player.clans != null)
                        {
                            foreach (var clan in scoreData.player.clans)
                            {
                                if (!string.IsNullOrEmpty(clan.tag))
                                {
                                    customScore.playerClan.Add(clan.tag);
                                }
                            }
                        }
                    }

                    customScores.Add(customScore);
                }

                // Convert the list of custom scores to a JSON array string
                // Unity's JsonUtility does not directly support serializing a List<T> at the root level.
                // We need a wrapper class for that.
                Wrapper<CustomBeatLeaderScore> wrapper = new Wrapper<CustomBeatLeaderScore>();
                wrapper.Items = customScores;
                string finalJson = JsonUtility.ToJson(wrapper, true); // 'true' for pretty printing

                callback?.Invoke(finalJson); // Send back the formatted JSON string
            }
            else
            {
                Debug.LogWarning("No BeatLeader scores found or error parsing data.");
                callback?.Invoke("[]"); // Return an empty JSON array if no data
            }
        }
    }

    // Helper wrapper class for serializing a List<T> as a root JSON array
    [System.Serializable]
    public class Wrapper<T>
    {
        public List<T> Items;
    }
}