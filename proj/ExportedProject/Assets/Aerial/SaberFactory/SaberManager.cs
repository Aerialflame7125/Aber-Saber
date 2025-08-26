using UnityEngine;
using System.Collections.Generic;
using System.IO;

namespace Aerial.Sabers
{
    [System.Serializable]
    public class SaberSelection
    {
        public string selectedSaberPath;
    }

    public class SaberManager : MonoBehaviour
    {

        public string currentPath;
        public List<Sprite> saberIcons = new List<Sprite>();
        public List<string> saberPaths = new List<string>();

        private string saveFile => Path.Combine(Application.persistentDataPath, "saberSelection.txt");

        public void LoadSabersFromFolder(string folderPath)
        {
            currentPath = File.ReadAllText(saveFile);
            if (currentPath == null || currentPath == "")
            {
                File.WriteAllText(saveFile, "none");
            }
            var files = Directory.GetFiles(folderPath, "*.saber");
            foreach (var file in files)
            {
                var bundle = AssetBundle.LoadFromFile(file);
                if (bundle == null) continue;

                // Load only lightweight previews
                var sprite = bundle.LoadAsset<Sprite>("SaberIcon");
                if (sprite != null)
                {
                    saberIcons.Add(sprite);
                    saberPaths.Add(file);
                }
                else
                {
                    // fallback: maybe load the first sprite/texture
                    var textures = bundle.LoadAllAssets<Texture2D>();
                    if (textures.Length > 0)
                    {
                        var tex = textures[0];
                        var spr = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
                        saberIcons.Add(spr);
                        saberPaths.Add(file);
                    }
                }

                bundle.Unload(false); // free memory, but keep loaded sprites/textures
            }
        }

        // Call this whenever a new saber is selected
        public void SetCurrentSaber(string newSaberPath)
        {
            currentPath = newSaberPath;
            WriteSaberPathToFile(currentPath);
            Debug.Log("Saber path updated: " + currentPath);
        }

        private void WriteSaberPathToFile(string path)
        {
            try
            {
                string filePath = Path.Combine(Application.persistentDataPath, currentPath);
                File.WriteAllText(filePath, path);
                Debug.Log("Saber path written to: " + filePath);
            }
            catch (IOException e)
            {
                Debug.LogError("Failed to write saber path: " + e.Message);
            }
        }
    }
}

