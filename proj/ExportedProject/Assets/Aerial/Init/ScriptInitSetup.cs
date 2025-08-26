using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SongLoader;
using BL.Auth;

public class ScriptInitSetup : MonoBehaviour
{
    float timer = 0f;
    bool Ichecked = false;

    void Awake()
    {
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f && Ichecked == false)
        {
            timer = 0f;
            Init();
        }
    }
    public void Init()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName != "HealthAndSafetyWarning") { return; }
        else if (sceneName == "HealthAndSafetyWarning")
        {
            Debug.Log("Player is in HealthAndSafetyWarning.");
            Ichecked = true;
            //Find buttons
            GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();
            GameObject OkButton = null;
            GameObject PleaseWait = null;
            GameObject FillBar = null;
            foreach (GameObject obj in objects)
            {
                if (obj.name == "OkButton")
                {
                    OkButton = obj;
                } 
                else if (obj.name == "FillBar")
                {
                    Transform parent = obj.transform.parent;
                    if (parent != null && parent.name == "PleaseWait")
                    {
                        FillBar = obj;
                        HMUI.Image Image = FillBar.GetComponent<HMUI.Image>();
                        Image.fillAmount = 0.03f;
                        PleaseWait = obj.transform.parent.gameObject;
                    }
                }
            }

            //song loader
            MainSongLoader[] mainSongLoader = FindObjectsOfType<MainSongLoader>();
            mainSongLoader[0].StartLoadingAndMerge(true);
            if (FillBar != null)
            {
                HMUI.Image Image = FillBar.GetComponent<HMUI.Image>();
                Image.fillAmount = 0.5f;
            }

            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
            DestroyImmediate(gameObject);
            if (FillBar != null)
            {
                HMUI.Image Image = FillBar.GetComponent<HMUI.Image>();
                Image.fillAmount = 1f;
                CallAfterDelay.Create(6f, () => {
                    PleaseWait.SetActive(false);
                    OkButton.SetActive(true);
                });
            }

        }
    }

}
