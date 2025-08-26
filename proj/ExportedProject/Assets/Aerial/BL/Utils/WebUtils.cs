using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using BL.Auth; 

namespace BL.Utils
{
    public class WebUtils : MonoBehaviour
    {
        // --- Singleton Pattern ---
        private static WebUtils _instance;

        public static WebUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    // Create a new GameObject to host the WebUtils script
                    GameObject go = new GameObject("WebUtils");
                    _instance = go.AddComponent<WebUtils>();
                    DontDestroyOnLoad(go); // Keep it alive between scene changes
                }
                return _instance;
            }
        }

        private void Awake()
        {
            // Ensure there's only one instance
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Sends a web request.
        /// </summary>
        /// <param name="url">The target URL.</param>
        /// <param name="method">The HTTP method (e.g., "GET", "POST"). Case-insensitive.</param>
        /// <param name="onSuccess">Callback invoked with the response body on success.</param>
        /// <param name="onError">Callback invoked with the error message on failure.</param>
        /// <param name="postData">Optional string data for POST requests.</param>
        public void SendRequest(string url, string method, Action<string> onSuccess, Action<string, long> onError, bool getCodeOnly = false, string postData = null)
        {
            UnityWebRequest request;
            string methodUpper = method.ToUpper();

            switch (methodUpper)
            {
                case "POST":
                    // NOTE: A proper POST request needs data.
                    request = new UnityWebRequest(url, "POST");
                    if (!string.IsNullOrEmpty(postData))
                    {
                        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(postData);
                        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    }
                    request.downloadHandler = new DownloadHandlerBuffer();
                    request.SetRequestHeader("Content-Type", "application/json");
                    break;

                case "PUT":
                    // NOTE: A proper PUT request also needs data.
                    request = UnityWebRequest.Put(url, postData ?? "");
                    break;

                case "GET":
                default:
                    request = UnityWebRequest.Get(url);
                    break;
            }

            // Start the coroutine on this MonoBehaviour instance
            StartCoroutine(SendRequestCoroutine(request, onSuccess, onError, getCodeOnly));
        }

        private IEnumerator SendRequestCoroutine(UnityWebRequest request, Action<string> onSuccess, Action<string, long> onError, bool getCodeOnly)
        {
            // --- Set Headers ---
            // Example headers. Modify as needed.
            Auth.Authentication.CookieManager.ApplyCookies(request);
            request.SetRequestHeader("Accept", "*/*");
            request.SetRequestHeader("User-Agent", "Beat Saber/0.11.1 (Aber Saber)");

            // --- Send Request ---
            yield return request.SendWebRequest();

            // --- Process Response ---
            string responseBody = request.downloadHandler?.text;
            long responseCode = request.responseCode;
            
            if (request.isNetworkError || request.isHttpError)
            {
                if (getCodeOnly)
                {
                    onError?.Invoke("", responseCode);
                }
                else
                {
                    string errorMessage = $"URL: {request.url}\nResponse Code: {responseCode}\nError: {request.error}\nResponse: {responseBody}";
                    Debug.LogError($"Web Request Failed!\n{errorMessage}");

                    // Invoke the error callback with the details
                    onError?.Invoke(errorMessage, responseCode);
                }
            }
            else
            {
                if (getCodeOnly)
                {
                    onSuccess?.Invoke(responseCode.ToString());
                }
                else
                {
                    Debug.Log($"Web Request Succeeded!\nURL: {request.url}\nResponse Code: {responseCode}\nResponse: {responseBody}");

                    onSuccess?.Invoke(responseBody);
                }
            }
            request.Dispose();
        }
    }
}