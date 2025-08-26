using UnityEngine;
using System;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Collections; // Required for IEnumerator
using BL.Utils;
using Aerial;
using TMPro;

namespace BL.Auth
{
    public class LoginManager : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text PlayerName;

        [SerializeField]
        private TMP_Text RankName;

        [SerializeField]
        private TMP_Text CountryName;

        [SerializeField]
        private TMP_Text PpText;

        [SerializeField]
        private HMUI.Image UserPfp;

        [Serializable]
        public class Idfilter
        {
            public string name;
            public string id;
            public string avatar;
        }

        [Serializable]
        public class PlayerDataFilter
        {
            public string id;
            public string name;
            public string platform;
            public string avatar;
            public string country;
            public string alias;
            public bool bot;
            public bool temporary;
            public double pp;
            public long rank;
            public long countryRank;
            public int level;
            public int experience;
            public int prestige;
            public string role;
        }

        // You might want to expose the platform selection in the Inspector for testing
        public Authentication.AuthPlatform desiredPlatform = Authentication.AuthPlatform.OculusPC;

        public void Start()
        {
            // Set the authentication platform when your application starts
            // or based on user selection
            Authentication.SetPlatform(desiredPlatform);

            Debug.Log($"Attempting to log in using {Authentication.Platform} platform...");
            // Start the login coroutine
            StartCoroutine(Authentication.DoLoginInternal(OnLoginSuccess, OnLoginFailure));
        }

        private void OnLoginSuccess()
        {
            WebUtils.Instance.SendRequest("https://api.beatleader.net/oculususer", "GET", OnIDSuccess, OnIDFailure);
        }

        private void OnIDSuccess(string output)
        {
            if (output != "")
            {
                Idfilter idvalues = JsonConvert.DeserializeObject<Idfilter>(output);
                if (idvalues.id != "")
                {
                    Aerial.ImageFromURL urlImage = new Aerial.ImageFromURL();
                    WebUtils.Instance.SendRequest(
                        $"https://api.beatleader.net/player/{idvalues.id}/exists",
                        "GET",
                        (out1) => {
                            if (out1 == "200")
                            {
                                // Whatever you wanted to do instead of continue
                                Debug.Log("Player exists.");
                                WebUtils.Instance.SendRequest(
                                    $"https://api.beatleader.net/player/{idvalues.id}?stats=true&keepOriginalId=false",
                                    "GET",
                                    (out2) =>
                                    {
                                        if (out2 != "")
                                        {
                                            Debug.Log(out2);
                                            PlayerDataFilter filter = JsonConvert.DeserializeObject<PlayerDataFilter>(out2);
                                            string avatarURL = filter.avatar;
                                            string platform = filter.platform;
                                            string country = filter.country;
                                            string name = filter.name;
                                            double pp = filter.pp;
                                            long rank = filter.rank;
                                            long countryRank = filter.countryRank;

                                            double roundedPP = System.Math.Round(pp, 2, System.MidpointRounding.AwayFromZero);

                                            PlayerName.text = name;
                                            RankName.text = $"#{rank.ToString()} Global";
                                            CountryName.text = $"{country}: #{countryRank.ToString()}";
                                            PpText.text = $"{roundedPP.ToString()} PP";
                                            StartCoroutine(urlImage.LoadImage(avatarURL, UserPfp));
                                        }
                                    },
                                    (error1, code) =>
                                    {
                                        if (error1 != "")
                                        {
                                            Debug.LogError($"Grabbing player failed. Error output: {error1}, Code: {code}");
                                            PlayerName.text = "Error grabbing data from Beat Leader!";
                                            PpText.text = $"Error: {error1}, {code}";
                                            CountryName.text = "Null: #0";
                                            PpText.text = "0 PP";
                                        }
                                    }
                                );
                            }
                        },
                        (str, error2) => {
                            if (error2.ToString() != "200")
                            {
                                Debug.LogError("Player ID apparently doesn't exist.");
                                PlayerName.text = "Error grabbing data from Beat Leader!";

                            }
                        },
                        true
                    );
                }
            }
        }

        private void OnIDFailure(string output, long responsecode)
        {
            Debug.LogError($"Grabbing ID failed, Response code {responsecode}, Output: {output}");
        }

        private void OnLoginFailure(string errorMessage)
        {
            Debug.LogError($"Authentication failed: {errorMessage}");
            // Display an error message to the user, prompt for retry, etc.
        }
        // Example of how you might trigger a reset (e.g., from a logout button)
        public void LogoutUser()
        {
            Authentication.ResetLogin();
            Debug.Log("Logged out. Ready for new login.");
        }

        // You could also have a public method to trigger login manually if needed
        public void TryLoginAgain()
        {
            Debug.Log("Retrying login...");
            StartCoroutine(Authentication.EnsureLoggedIn(OnLoginSuccess, OnLoginFailure));
        }
    }
}


