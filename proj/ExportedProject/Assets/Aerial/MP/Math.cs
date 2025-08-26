using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASMP;

namespace ASMP
{
    
    public class Math : MonoBehaviour
    {
        public static int FromLocalScore(int score, int player, int localScore = 250115)
        {
            Debug.Log($"FromLocalScore called with {score}, {player}");

            int diff = score - localScore;
            if (localScore == 250115)
            {
                Debug.Log($"Distance from player {ASMP.PlayerResolver.FindPlayerNameByID(player)} from fake score: {diff}");
                return diff;
            }
            else
            {
                Debug.Log($"Distance from player {ASMP.PlayerResolver.FindPlayerNameByID(player)}: {diff}");
                return diff;
            }
        }
    }
}