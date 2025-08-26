using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BL.Authentication
{
    public class CurrentToken
    {
        public string token { get; private set; }

        public void SetToken(string tokenval)
        {
            if (tokenval == null)
            {
                return;
            }
            else
            {
                token = tokenval;
                Debug.LogError(tokenval);
            }
        }
    }
}

