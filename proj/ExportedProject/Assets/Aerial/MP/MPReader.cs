using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using ASMP;

namespace ASMP
{
    public class MPReader : MonoBehaviour
    {
        [System.Serializable]
        public class ReturnedData
        {
            public int p;
            public float c1x;
            public float c1y;
            public float c1z;

            public float c2x;
            public float c2y;
            public float c2z;

            public int score;
            //public int percent;
        }
        
        void Start()
        {
            StartCoroutine(callSignIn());
        }
        IEnumerator callSignIn()
        {
            UnityWebRequest uwr = UnityWebRequest.Get("http://localhost:4000/signin");
            yield return uwr.SendWebRequest();
        
            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                string jsonText = uwr.downloadHandler.text;
                ReturnedData data = JsonUtility.FromJson<ReturnedData>(jsonText);

                if (data != null)
                {
                    Debug.Log($"Recieved controller data and score for player {data.p}. Left Controller: x: {data.c1x}, y: {data.c1y}, z: {data.c1z}. Right Controller: x: {data.c2x}, y: {data.c2y}, z: {data.c2z}. Score: {data.score}");
                    Math.FromLocalScore(data.score, data.p);
                }
                else
                {
                    Debug.LogError("Failed to deserialize JSON.");
                }
            }
        }
    }
}

