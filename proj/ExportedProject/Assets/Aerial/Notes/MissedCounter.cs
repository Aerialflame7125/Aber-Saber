using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Aerial.Note
{
    public class MissedCounter : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;

        [SerializeField]
        private GameObject textObject;

        private int notes = 0;
        private void Start()
        {
            notes = 0;
            textObject.SetActive(false);
        }

        public void newMiss()
        {
            notes++;
            textObject.SetActive(true);
            text.text = notes.ToString();
        }
    }
}

