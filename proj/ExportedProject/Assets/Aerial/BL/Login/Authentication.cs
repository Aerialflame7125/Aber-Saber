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

namespace BL.Auth
{
    public class Authentication
    {
        #region AuthPlatform

        private static Task<string> _loginTask;
        private static string _cachedAuthToken;

        [Serializable]
        public class SerializableCookie
        {
            public string name;
            public string value;
            public string domain;
            public string path;
        }

        public static class CookieManager
        {
            private static string cookieFile => Path.Combine(UnityEngine.Application.persistentDataPath, "cookies.json");
            public static void ClearStoredCookies()
            {
                if (File.Exists(cookieFile))
                    File.Delete(cookieFile);
            }

            public static void SaveCookies(UnityWebRequest request)
            {
                if (request.GetResponseHeaders() != null &&
                    request.GetResponseHeaders().TryGetValue("Set-Cookie", out string rawCookies))
                {
                    var cookies = ParseCookies(rawCookies);
                    string json = JsonConvert.SerializeObject(cookies, Formatting.Indented);
                    File.WriteAllText(cookieFile, json);

                }
            }

            public static void ApplyCookies(UnityWebRequest request)
            {
                if (!File.Exists(cookieFile)) return;

                string json = File.ReadAllText(cookieFile);
                var cookies = JsonConvert.DeserializeObject<List<SerializableCookie>>(json);

                if (cookies != null && cookies.Count > 0)
                {
                    var cookieHeader = string.Join("; ", cookies.Select(c => $"{c.name}={c.value}"));
                    request.SetRequestHeader("Cookie", cookieHeader);
                }
            }

            private static List<SerializableCookie> ParseCookies(string raw)
            {
                var parts = raw.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                var cookies = new List<SerializableCookie>();

                foreach (var part in parts)
                {
                    var semicolonSplit = part.Split(';');
                    var keyValue = semicolonSplit[0].Split('=');
                    if (keyValue.Length >= 2)
                    {
                        cookies.Add(new SerializableCookie
                        {
                            name = keyValue[0].Trim(),
                            value = keyValue[1].Trim(),
                            path = "/",
                            domain = "beatleader.net" // customize if needed
                        });
                    }
                }

                return cookies;
            }
        }


        public static AuthPlatform Platform { get; private set; }

        public static void SetPlatform(AuthPlatform platform)
        {
            Platform = platform;
            if (platform == AuthPlatform.OculusPC)
            {
                Core.AsyncInitialize();
            }
        }

        public enum AuthPlatform
        {
            Undefined,
            Steam,
            OculusPC,
            LocalAuthFile
        }

        #endregion

        #region Ticket

        [Serializable]
        public class AuthConfig
        {
            public string LoginUsername { get; set; }
            public string LoginPassword { get; set; }
            public string CustomSteamTicket { get; set; }
            public string CustomOculusTicket { get; set; }
        }

        private static void OnLoginSuccess()
        {
            Debug.Log("Login successful! Welcome to BeatLeader!");
            // Proceed to game scene, enable features, etc.
        }

        private static void OnLoginFailure(string errorMessage)
        {
            Debug.LogError("Login failed: " + errorMessage);
            // Display error message to user, offer retry, etc.
        }

        public static async Task<string> PlatformTicket()
        {
            CookieManager.ClearStoredCookies();
            /*
            string steamTicket = null;
            #if ENABLE_STEAMWORKS_NET // Define this compilation symbol in Player Settings -> Other Settings -> Scripting Define Symbols
            if (SteamClient.IsValid) // Use SteamClient.IsInitialized for Facepunch.Steamworks
            {
                try
                {
                    // The correct way to get the auth session ticket with Facepunch.Steamworks
                    // This call doesn't require any arguments unless you're using a specific overload
                    // for network identity, which is rare for simple client auth tickets.
                    AuthTicket ticket = await SteamUser.GetAuthSessionTicketAsync(default(NetIdentity), 10.0f); // 10-second timeout

                    // Check if the ticket is valid by examining its Data (byte array)
                    if (ticket.Data != null && ticket.Data.Length > 0)
                    {
                        // Convert ticket.Data to hex string
                        steamTicket = BitConverter.ToString(ticket.Data).Replace("-", "").ToLower();
                        UnityEngine.Debug.LogError("Steam Ticket (Debug): " + steamTicket);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("Failed to get Steam auth session ticket: Ticket data is empty or null.");
                        steamTicket = null;
                    }

                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError("Failed to get Steam ticket: " + ex.Message);
                }
            }
            else
            {
                UnityEngine.Debug.LogError("SteamAPI is not initialized (SteamClient.IsInitialized is false). Cannot get Steam ticket.");
            }
            #else
            UnityEngine.Debug.LogError("Steamworks.NET is not enabled/integrated (ENABLE_STEAMWORKS_NET define missing). Cannot check SteamAPI.");
            #endif
            */
            string oculusTicket = null;
            if (Oculus.Platform.Core.IsInitialized())
            {
                try
                {
                    var tcs = new TaskCompletionSource<string>();

                    Oculus.Platform.Users.GetAccessToken().OnComplete(message =>
                    {
                        if (message.IsError)
                        {
                            UnityEngine.Debug.LogError("Failed to get Oculus access token: " + message.GetError().Message);
                            tcs.SetException(new Exception(message.GetError().Message));
                        }
                        else
                        {
                            oculusTicket = message.Data;
                            tcs.SetResult(oculusTicket);
                            //CoroutineRunner.Instance.StartCoroutine(DoLoginInternal(OnLoginSuccess, OnLoginFailure));
                            // THIS IS A FUCKING LOOP THAT STOPS YOUR REQUESTS FROM THE API ^^ WTF IS WRONG WITH YOU
                        }
                    });

                    await tcs.Task;
                    oculusTicket = tcs.Task.Result;

                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError("Error during Oculus ticket acquisition: " + ex.Message);
                    oculusTicket = null;
                }
            }
            else
            {
                UnityEngine.Debug.LogError("Oculus Platform SDK is not initialized.");
                Core.AsyncInitialize();
            }

            if (string.IsNullOrEmpty(oculusTicket))
            {
                Debug.LogError("Oculus ticket returned Null or Empty. Cannot login.");
            }

            string authJsonPath = Path.Combine(UnityEngine.Application.dataPath, "..", "Auth.json");
            AuthConfig authConfig = null;

            /*
            if (File.Exists(authJsonPath))
            {
                try
                {
                    string jsonContent = File.ReadAllText(authJsonPath);
                    authConfig = JsonConvert.DeserializeObject<AuthConfig>(jsonContent);
                    UnityEngine.Debug.Log("Successfully read Auth.json from " + authJsonPath);

                    if (!string.IsNullOrEmpty(authConfig.CustomSteamTicket))
                    {
                        UnityEngine.Debug.LogError("Auth.json Custom Steam Ticket (Debug): " + authConfig.CustomSteamTicket);
                    }
                    if (!string.IsNullOrEmpty(authConfig.CustomOculusTicket))
                    {
                        UnityEngine.Debug.LogError("Auth.json Custom Oculus Ticket (Debug): " + authConfig.CustomOculusTicket);
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError("Error reading or parsing Auth.json: " + ex.Message);
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning("Auth.json not found at " + authJsonPath + ". Skipping local authentication file.");
            }
            */
            string finalTicket = null;
            switch (Platform)
            {
                /*
                case AuthPlatform.Steam:
                    finalTicket = steamTicket ?? authConfig?.CustomSteamTicket;
                    break
                */
                case AuthPlatform.OculusPC:
                    finalTicket = oculusTicket ?? authConfig?.CustomOculusTicket;
                    break;
                /*
                case AuthPlatform.LocalAuthFile:
                    finalTicket = "LOCAL_AUTH_FILE_PRESENT";
                    break*/
                default:
                    UnityEngine.Debug.LogError("PlatformTicket called with Undefined platform.");
                    finalTicket = oculusTicket ?? authConfig?.CustomOculusTicket;
                    break;
            }
            return finalTicket;
        }

        #endregion

        #region Login

        private static bool _locked;
        private static bool _signedIn;

        public static void ResetLogin()
        {
            UnityWebRequest.ClearCookieCache(new Uri(BLConstants.BEATLEADER_API_URL));
            _signedIn = false;
            UnityEngine.Debug.Log("Login state reset.");
        }

        public static IEnumerator EnsureLoggedIn(Action onSuccess, Action<string> onFail)
        {
            while (true)
            {
                if (!_locked)
                {
                    _locked = true;
                    break;
                }

                yield return null;
            }

            try
            {
                if (_signedIn && !string.IsNullOrEmpty(_cachedAuthToken))
                {
                    onSuccess?.Invoke();
                    yield break;
                }

                // If login is already in progress, wait for it
                if (_loginTask != null && !_loginTask.IsCompleted)
                {
                    yield return new WaitUntil(() => _loginTask.IsCompleted);
                }
                else
                {
                    _loginTask = PlatformTicket(); // this includes the call to DoLoginInternal indirectly
                    yield return new WaitUntil(() => _loginTask.IsCompleted);
                }

                if (_loginTask.IsFaulted)
                {
                    onFail?.Invoke("Failed to get platform ticket.");
                    yield break;
                }

                string ticket = _loginTask.Result;
                _cachedAuthToken = ticket;

                // Call DoLoginInternal once per ticket
                CoroutineRunner.Instance.StartCoroutine(DoLoginInternal(() =>
                {
                    _signedIn = true;
                    onSuccess?.Invoke();
                }, onFail));
            }
            finally
            {
                _locked = false;
            }
        }


        public static IEnumerator DoLoginInternal(Action onSuccess, Action<string> onFail)
        {
            /*
            if (Platform == AuthPlatform.LocalAuthFile)
            {
                string authJsonPath = Path.Combine(UnityEngine.Application.dataPath, "..", "Auth.json");
                AuthConfig authConfig = null;

                try
                {
                    if (File.Exists(authJsonPath))
                    {
                        string jsonContent = File.ReadAllText(authJsonPath);
                        authConfig = JsonConvert.DeserializeObject<AuthConfig>(jsonContent);
                    }
                    else
                    {
                        UnityEngine.Debug.LogError("Attempted LocalAuthFile login, but Auth.json not found at " + authJsonPath);
                        onFail("Auth.json not found for local login.");
                        yield break;
                    }
                }
                catch (Exception ex)
                {
                    UnityEngine.Debug.LogError("Error reading or parsing Auth.json for local login: " + ex.Message);
                    onFail("Local Auth Error: " + ex.Message);
                    yield break;
                }

                if (authConfig != null && !string.IsNullOrEmpty(authConfig.LoginUsername) && !string.IsNullOrEmpty(authConfig.LoginPassword))
                {
                    yield return LoginWithCredentials(authConfig.LoginUsername, authConfig.LoginPassword, onSuccess, onFail);
                    yield break;
                }
                else
                {
                    UnityEngine.Debug.LogError("Auth.json found, but LoginUsername or LoginPassword are empty for LocalAuthFile platform.");
                    onFail("Auth.json incomplete for local login.");
                }
            }
            */

            string provider = null;
            if (!TryGetPlatformProvider(Platform, out provider))
            {
                UnityEngine.Debug.LogError("Login failed! Unknown platform or provider not applicable for direct ticket login.");
                onFail("Unknown platform");
                yield break;
            }

            Task<string> ticketTask = PlatformTicket();
            while (!ticketTask.IsCompleted)
            {
                yield return null;
            }

            if (ticketTask.IsFaulted)
            {
                UnityEngine.Debug.LogError($"Login failed! Platform ticket task faulted. Exception: {ticketTask.Exception?.InnerException?.Message ?? ticketTask.Exception?.Message ?? "Unknown error"}");
                onFail($"Failed to get platform ticket: {ticketTask.Exception?.InnerException?.Message ?? ticketTask.Exception?.Message ?? "Unknown error"}");
                yield break;
            }

            string authToken = ticketTask.Result;
            if (string.IsNullOrEmpty(authToken) || authToken == "LOCAL_AUTH_FILE_PRESENT") // Check for empty or placeholder token
            {
                UnityEngine.Debug.LogError($"Login failed! No valid auth token received from platform ticket. (Auth token: {(string.IsNullOrEmpty(authToken) ? "null or empty" : authToken)})");
                onFail("No valid auth token from platform.");
                yield break;
            }

            List<IMultipartFormSection> form = new List<IMultipartFormSection> {
            new MultipartFormDataSection("ticket", authToken),
            new MultipartFormDataSection("provider", "oculusTicket"),
            new MultipartFormDataSection("returnUrl", "/")
        };

            UnityWebRequest request = UnityWebRequest.Post(BLConstants.SIGNIN_WITH_TICKET, form);
            request.SetRequestHeader("Accept", "*/*");
            request.SetRequestHeader("User-Agent", "Beat Saber/0.11.1 (Aber Saber)");

            UnityEngine.Debug.Log($"Attempting login to BeatLeader with ticket. URL: {BLConstants.SIGNIN_WITH_TICKET}, Provider: {provider}, Ticket (first 10 chars): {authToken.Substring(0, Mathf.Min(authToken.Length, 10))}...");
            CookieManager.ApplyCookies(request);
            yield return request.SendWebRequest();

            // --- START OF CORRECTED LOGGING FOR UNITY 2019 ---

            string responseBody = request.downloadHandler?.text;
            long responseCode = request.responseCode;

            if (request.isNetworkError || request.isHttpError) // Use isNetworkError and isHttpError for Unity 2019
            {
                // Handle network or HTTP errors
                UnityEngine.Debug.LogError($"Platform ticket login failed! " +
                                           $"URL: {request.url}, " +
                                           $"Response Code: {responseCode}, " +
                                           $"Error: {request.error}, " +
                                           $"Response Body: {responseBody}");

                if (responseCode == BLConstants.MaintenanceStatus)
                {
                    UnityEngine.Debug.LogWarning("BeatLeader Login: Server is in Maintenance mode.");
                    onFail("Maintenance");
                }
                else if (responseCode == BLConstants.Unauthorized) // Example: Handle specific 401 Unauthorized
                {
                    UnityEngine.Debug.LogError("BeatLeader Login: Unauthorized - Invalid ticket or credentials.");
                    onFail("Unauthorized: " + (string.IsNullOrEmpty(responseBody) ? request.error : responseBody));
                }
                else
                {
                    UnityEngine.Debug.LogError($"BeatLeader Login: Network or HTTP Error: {responseCode}. Details: {request.error}");
                    onFail($"Network Error ({responseCode}): {request.error}");
                }
            }
            else // This covers a successful request in Unity 2019 (no network or http error)
            {
                // Login successful
                UnityEngine.Debug.Log($"BeatLeader Login Successful! " +
                                       $"URL: {request.url}, " +
                                       $"Response Code: {responseCode}, " +
                                       $"Response Body: {responseBody}");
                CookieManager.SaveCookies(request);
                onSuccess();
            }

            // --- END OF CORRECTED LOGGING FOR UNITY 2019 ---
        }

        public static IEnumerator LoginWithCredentials(string username, string password, Action onSuccess, Action<string> onFail)
        {
            UnityEngine.Debug.Log("Attempting to perform login with username/password...");

            string loginUrl = BLConstants.BEATLEADER_API_URL + "signin";

            List<IMultipartFormSection> formData = new List<IMultipartFormSection>
        {
            new MultipartFormDataSection("ticket", "login"),
            new MultipartFormDataSection("login", username),
            new MultipartFormDataSection("password", password)
        };

            UnityWebRequest request = UnityWebRequest.Post(loginUrl, formData);

            request.SetRequestHeader("Accept", "*/*");
            request.SetRequestHeader("User-Agent", BLConstants.USER_AGENT);

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                if (request.responseCode == BLConstants.MaintenanceStatus)
                {
                    UnityEngine.Debug.LogWarning("Username/Password login failed! Maintenance.");
                    onFail?.Invoke("Maintenance");
                }
                else if (request.responseCode == BLConstants.Unauthorized)
                {
                    UnityEngine.Debug.LogError("Username/Password login failed: Invalid credentials. Status: " + request.responseCode + ", Error: " + request.error);
                    onFail?.Invoke("Invalid username or password.");
                }
                else
                {
                    UnityEngine.Debug.LogError("Username/Password login failed! Status: " + request.responseCode + ", Error: " + request.error);
                    onFail?.Invoke("NetworkError: " + request.responseCode + " - " + request.error);
                }
            }
            else
            {
                UnityEngine.Debug.Log("Username/Password login successful! Response: " + request.downloadHandler.text);
                onSuccess?.Invoke();
            }
        }

        private static bool TryGetPlatformProvider(AuthPlatform platform, out string provider)
        {
            switch (platform)
            {
                case AuthPlatform.Steam:
                    provider = "steamTicket";
                    return true;
                case AuthPlatform.OculusPC:
                    provider = "oculusTicket";
                    return true;
                case AuthPlatform.Undefined:
                case AuthPlatform.LocalAuthFile:
                default:
                    provider = null;
                    return false;
            }
        }

        #endregion
    }
}
