using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks; // This is for System.Threading.Tasks.Task
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Steamworks; // This is for SteamManager, SteamUser, SteamClient
using Oculus.Platform; // This is for Oculus.Platform.Request, etc.
using Oculus.Platform.Models;
using Steamworks.Data; // VERY IMPORTANT: This using directive is likely missing or you need it!
using BL.Auth;

public class TestLogin : MonoBehaviour
{
    private void Start()
    {
        // Example: Set the platform (e.g., based on user choice or auto-detection)
        Authentication.SetPlatform(Authentication.AuthPlatform.OculusPC);

        // Call the login flow
        CoroutineRunner.Instance.StartCoroutine(Authentication.EnsureLoggedIn(loggedin, error));
    }

    private static void loggedin()
    {
        Debug.LogError("Logged In");
    }

    private static void error(string error)
    {
        Debug.LogError(error);
    }
}
