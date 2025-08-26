using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Add this if you want TextMeshPro instead of TextMesh (uncomment the following line and install TMP package)
// using TMPro;

public class SaberClonerMP : MonoBehaviour
{
    [Tooltip("Fallback prefab if Saber components are not found.")]
    public GameObject fallbackSaberPrefab;

    // Class to hold each player's ghost sabers and score text
    private class GhostSaberPair
    {
        public Transform left;
        public Transform right;
        public TextMeshProUGUI scoreText; // Change to TextMeshPro if using TMP
    }

    private Dictionary<string, GhostSaberPair> ghostObjects = new Dictionary<string, GhostSaberPair>();

    /// <summary>
    /// Call this to initialize/update the list of player ghosts.
    /// It will add new players and remove players no longer present.
    /// </summary>
    public void UpdateGhostPlayers(List<string> currentPlayerNames)
    {
        // Remove ghosts of players no longer in the list
        List<string> toRemove = new List<string>();
        foreach (var kvp in ghostObjects)
        {
            if (!currentPlayerNames.Contains(kvp.Key))
                toRemove.Add(kvp.Key);
        }
        foreach (string playerToRemove in toRemove)
        {
            RemoveGhost(playerToRemove);
        }

        // Add ghosts for new players
        foreach (string playerName in currentPlayerNames)
        {
            if (!ghostObjects.ContainsKey(playerName))
            {
                CreateGhost(playerName);
            }
        }
    }

    private void RemoveGhost(string playerName)
    {
        if (!ghostObjects.ContainsKey(playerName)) return;

        GhostSaberPair pair = ghostObjects[playerName];

        if (pair.left) Destroy(pair.left.gameObject);
        if (pair.right) Destroy(pair.right.gameObject);
        if (pair.scoreText) Destroy(pair.scoreText.gameObject);

        ghostObjects.Remove(playerName);
        Debug.Log($"Removed ghosts for player {playerName}");
    }

    private void CreateGhost(string playerName)
    {
        Saber[] sabers = FindObjectsOfType<Saber>();
        GameObject leftBase = null;
        GameObject rightBase = null;

        if (sabers.Length >= 2)
        {
            leftBase = sabers[0].gameObject;
            rightBase = sabers[1].gameObject;
        }
        else if (fallbackSaberPrefab != null)
        {
            leftBase = fallbackSaberPrefab;
            rightBase = fallbackSaberPrefab;
        }
        else
        {
            Debug.LogWarning("No Saber components found and no fallback prefab set.");
            return;
        }

        GhostSaberPair pair = new GhostSaberPair();

        // Instantiate left saber clone
        GameObject leftClone = Instantiate(leftBase, Vector3.zero, Quaternion.identity);
        leftClone.name = $"GhostSaber_L_{playerName}";
        MakeTransparent(leftClone);
        pair.left = leftClone.transform;

        // Instantiate right saber clone
        GameObject rightClone = Instantiate(rightBase, Vector3.zero, Quaternion.identity);
        rightClone.name = $"GhostSaber_R_{playerName}";
        MakeTransparent(rightClone);
        pair.right = rightClone.transform;

        // Create score text
        GameObject scoreObj = new GameObject($"Score_{playerName}");
        TextMeshProUGUI scoreText = scoreObj.AddComponent<TextMeshProUGUI>();
        scoreText.text = $"Score for {playerName}: 0";
        scoreText.fontSize = 32;
        scoreText.color = Color.cyan;
        pair.scoreText = scoreText;

        ghostObjects[playerName] = pair;

        Debug.Log($"Created ghosts for player {playerName}");
    }

    /// <summary>
    /// Update the position of the player's ghost sabers and the score text.
    /// </summary>
    public void UpdateGhost(string playerName, Vector3 leftPos, Vector3 rightPos, int score)
    {
        if (!ghostObjects.ContainsKey(playerName)) return;

        GhostSaberPair pair = ghostObjects[playerName];

        if (pair.left != null)
            pair.left.position = leftPos;

        if (pair.right != null)
            pair.right.position = rightPos;

        if (pair.scoreText != null)
        {
            pair.scoreText.text = $"Score: {score}";
            pair.scoreText.transform.position = (leftPos + rightPos) * 0.5f + Vector3.up * 0.5f;
            pair.scoreText.transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward); // face camera
        }
    }

    /// <summary>
    /// Makes the given GameObject and its children transparent (30% opacity).
    /// </summary>
    private void MakeTransparent(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                Color color = mat.color;
                color.a = 0.3f;
                mat.color = color;

                // Setup material for transparency
                mat.SetFloat("_Mode", 2);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;
            }
        }
    }
}
