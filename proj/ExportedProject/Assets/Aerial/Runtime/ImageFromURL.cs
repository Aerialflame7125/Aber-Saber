using UnityEngine;
using UnityEngine.Networking; // Required for UnityWebRequest
using System.Collections; // Required for IEnumerator
using System.IO; // Required for File.Exists and File.ReadAllBytes

// Make sure to reference your custom HMUI namespace
using HMUI; // Add this line!

namespace Aerial
{
    public class ImageFromURL : MonoBehaviour
    {
        public string imagePathOrUrl = "https://upload.wikimedia.org/wikipedia/commons/4/47/PNG_transparency_demonstration_1.png";

        // Reference to custom HMUI.Image component
        public HMUI.Image targetHMUIImage;

        void Start()
        {
            if (targetHMUIImage == null)
            {
                Debug.LogError("HMUI.Image component not assigned. Please assign it in the Inspector.");
                return;
            }

            StartCoroutine(LoadImage(imagePathOrUrl, targetHMUIImage));
        }

        public IEnumerator LoadImage(string pathOrUrl, HMUI.Image HMUIImage)
        {
            Texture2D loadedTexture = null;

            // **YOUR IF STATEMENT GOES HERE (modified for local file check)**
            // First, try to load as a local file
            if (File.Exists(pathOrUrl))
            {
                Debug.Log($"Attempting to load image from local file: {pathOrUrl}");
                try
                {
                    byte[] fileData = File.ReadAllBytes(pathOrUrl);
                    // Create a new Texture2D. LoadImage will automatically resize it.
                    loadedTexture = new Texture2D(2, 2); // Small initial size is fine.
                    if (loadedTexture.LoadImage(fileData)) // LoadImage returns true on success
                    {
                        Debug.Log("Image loaded successfully from local file.");
                    }
                    else
                    {
                        Debug.LogError("Failed to load image data from local file. Is it a valid image format?");
                        loadedTexture = null; // Mark as failed
                    }
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Error reading local file: {pathOrUrl} - {e.Message}");
                    loadedTexture = null;
                }
            }
            else // If not a local file, assume it's a URL and try to download
            {
                Debug.Log($"Local file not found or it's a URL. Attempting to download from: {pathOrUrl}");
                UnityWebRequest www = UnityWebRequestTexture.GetTexture(pathOrUrl);

                // Send the request and wait for it to complete
                yield return www.SendWebRequest();

                // Check for errors based on Unity 2019.4.0f1 API
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogError("Error downloading image from URL: " + www.error);
                }
                else
                {
                    // GetContent directly gives you the Texture2D, properly sized.
                    loadedTexture = DownloadHandlerTexture.GetContent(www);
                    if (loadedTexture != null)
                    {
                        Debug.Log("Image downloaded successfully from URL.");
                    }
                    else
                    {
                        Debug.LogError("Downloaded content is not a valid Texture2D (perhaps not an image?).");
                    }
                }
            }

            // Now, if a texture was successfully loaded (either locally or from URL), apply it
            if (loadedTexture != null)
            {
                // Create the Sprite using the actual dimensions of the loaded texture.
                // Using a central pivot (0.5f, 0.5f) is generally good for UI images.
                Sprite newSprite = Sprite.Create(loadedTexture, new Rect(0, 0, loadedTexture.width, loadedTexture.height), new Vector2(0.5f, 0.5f));

                if (HMUIImage != null)
                {
                    HMUIImage.sprite = newSprite; // Assign to your HMUI.Image component
                    Debug.Log("Image applied to HMUI.Image component successfully!");
                }
                else
                {
                    Debug.LogWarning("Target HMUI.Image component became null. Image loaded but not applied.");
                }
            }
            else
            {
                Debug.LogError("No image could be loaded or downloaded, or it was invalid. " +
                               "HMUI.Image will show default missing sprite.");
            }
        }
    }
}
