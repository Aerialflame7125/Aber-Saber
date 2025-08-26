using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureArray : MonoBehaviour
{
    // Textures
    public Texture2D[] textures;

    // Called on prefab enable
    public void OnEnable()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material.SetTexture("_MainTex", textures[Random.Range(0, textures.Length)]);
    }
}