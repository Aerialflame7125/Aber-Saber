using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WebsocketClientMP : MonoBehaviour
{
    public SaberClonerMP ghostManager;

    // Example method to call when you receive a websocket message
    public void OnWebSocketMessage(dynamic response)
    {
        // Assuming 'response.others' is a dictionary or similar
        List<string> currentPlayers = new List<string>();

        foreach (var key in response.others.Keys)
            currentPlayers.Add((string)key);

        ghostManager.UpdateGhostPlayers(currentPlayers);

        foreach (var kvp in response.others)
        {
            string name = kvp.Key;
            var left = kvp.Value.left_controller;
            var right = kvp.Value.right_controller;
            int score = kvp.Value.score;

            Vector3 leftPos = new Vector3(left.x, left.y, left.z);
            Vector3 rightPos = new Vector3(right.x, right.y, right.z);

            ghostManager.UpdateGhost(name, leftPos, rightPos, score);
        }
    }
}
