using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ASMP
{
    public class PlayerResolver : MonoBehaviour
    {
        public static string FindPlayerNameByID(int pid)
        {
            // Placeholder
            if (pid == -1)
            {
                return "Concreteeater";
            }
            else
            {
                return "Unknown";
            }
        }
    }
}