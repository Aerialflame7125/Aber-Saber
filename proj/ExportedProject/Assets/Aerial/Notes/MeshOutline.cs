using UnityEngine;
using System.Collections.Generic;
// No need for UnityEditor namespace unless you specifically use editor-only APIs
// (like PrefabUtility.IsPartOfPrefabAsset in non-play mode checks, which are optional here)

[RequireComponent(typeof(MeshRenderer))] 
[RequireComponent(typeof(MeshFilter))]   
public class MeshOutline : MonoBehaviour
{
    [Header("Outline Settings")]
    public Material outlineMaterial;
    [Range(0.0f, 0.1f)]
    public float outlineThickness = 0.01f;
    public Color outlineColor = Color.white;

    private GameObject _outlineObject;
    private MeshRenderer _outlineRenderer;
    private Transform _mainMeshTransform; 

    void Awake()
    {
        _mainMeshTransform = GetComponent<Transform>(); 
        SetupOutline(); // This will now be guarded
    }

    void OnEnable()
    {
        if (_outlineRenderer != null)
        {
            _outlineRenderer.enabled = true;
        }
        else
        {
            // If _outlineObject was somehow destroyed or not setup (e.g., due to an earlier error in Awake)
            // try to set up again. This ensures robustness if component is re-enabled.
            SetupOutline(); 
        }
    }

    void OnDisable()
    {
        if (_outlineRenderer != null)
        {
            _outlineRenderer.enabled = false;
        }
    }

    void OnDestroy()
    {
        if (_outlineObject != null)
        {
            Destroy(_outlineObject);
        }
    }

    void OnValidate()
    {
        // OnValidate can run in the editor, even on prefab assets.
        // We only want to apply material settings and ensure the outline object is positioned correctly IF it already exists.
        // We DO NOT want to call SetupOutline() here if this is a Prefab Asset, to prevent the parenting error.

        if (_outlineObject == null) // If outline object doesn't exist yet, it's likely running in prefab asset context
        {
            // If in editor and not playing, and this is part of a prefab asset, return.
            // We only want to create the outline object when instantiated in a scene.
            #if UNITY_EDITOR
            if (!Application.isPlaying && UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
            {
                return; // Prevent errors in prefab asset view
            }
            #endif
            SetupOutline(); // If it's a scene object or playing, try to set up
        }

        if (_outlineRenderer != null && _outlineRenderer.sharedMaterial != null)
        {
            _outlineRenderer.sharedMaterial.SetColor("_OutlineColor", outlineColor);
            _outlineRenderer.sharedMaterial.SetFloat("_OutlineThickness", outlineThickness);
            SetOutlineRenderQueue();
            
            if (_outlineObject != null) // Ensure _outlineObject exists before modifying its transform
            {
                _outlineObject.transform.localPosition = Vector3.zero;
                _outlineObject.transform.localRotation = Quaternion.identity;
                _outlineObject.transform.localScale = Vector3.one;
            }
        }
    }

    void SetupOutline()
    {
        if (_outlineObject != null) return; // Already setup

        // === CRITICAL GUARD: Only run setup if this GameObject is part of a loaded scene ===
        // This prevents the "Setting parent of a transform which resides in a Prefab Asset" error.
        if (!gameObject.scene.isLoaded)
        {
            // For extra robustness in editor, explicitly check if it's a prefab asset and if not playing
            #if UNITY_EDITOR
            if (!Application.isPlaying && UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
            {
                //Debug.Log("MeshOutline: Skipping setup on Prefab Asset: " + gameObject.name); // Optional: for debugging
                return; 
            }
            #endif
        }

        if (_mainMeshTransform == null)
        {
            _mainMeshTransform = GetComponent<Transform>();
            if (_mainMeshTransform == null)
            {
                Debug.LogError("MeshOutline: Could not find Transform for main object.", this);
                enabled = false;
                return;
            }
        }

        _outlineObject = new GameObject("Outline_" + gameObject.name);
        _outlineObject.transform.SetParent(_mainMeshTransform); 
        
        _outlineObject.transform.localPosition = Vector3.zero; 
        _outlineObject.transform.localRotation = Quaternion.identity;
        _outlineObject.transform.localScale = Vector3.one; 

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        // Use sharedMesh when accessing the mesh data from a Prefab
        if (meshFilter == null || meshFilter.sharedMesh == null) 
        {
            Debug.LogError("MeshOutline: No MeshFilter or sharedMesh found on parent GameObject.", this); 
            Destroy(_outlineObject);
            _outlineObject = null;
            return;
        }

        MeshFilter outlineMeshFilter = _outlineObject.AddComponent<MeshFilter>();
        outlineMeshFilter.sharedMesh = meshFilter.sharedMesh; // Assign sharedMesh

        _outlineRenderer = _outlineObject.AddComponent<MeshRenderer>();

        if (outlineMaterial == null)
        {
            Debug.LogError("Outline Material is not assigned! Please assign 'OutlineMaterial' in the inspector.", this);
            Destroy(_outlineObject);
            _outlineObject = null;
            return;
        }

        _outlineRenderer.sharedMaterial = outlineMaterial;

        _outlineRenderer.sharedMaterial.SetColor("_OutlineColor", outlineColor);
        _outlineRenderer.sharedMaterial.SetFloat("_OutlineThickness", outlineThickness);

        SetOutlineRenderQueue();
    }

    // LateUpdate is now completely empty of transform logic, as parenting handles it.
    void LateUpdate()
    {
        // No explicit transform assignments needed here.
        // The parent-child relationship handles it automatically.
    }

    private void SetOutlineRenderQueue()
    {
        if (_outlineRenderer != null && _outlineRenderer.sharedMaterial != null)
        {
            _outlineRenderer.sharedMaterial.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry - 1; 
        }
    }

    public void SetOutlineEnabled(bool enabled)
    {
        if (_outlineRenderer != null)
        {
            _outlineRenderer.enabled = enabled;
        }
    }

    public void SetOutlineColor(Color color)
    {
        outlineColor = color;
        if (_outlineRenderer != null && _outlineRenderer.sharedMaterial != null)
        {
            _outlineRenderer.sharedMaterial.SetColor("_OutlineColor", outlineColor);
        }
    }

    public void SetOutlineThickness(float thickness)
    {
        thickness = Mathf.Max(0, thickness);
        outlineThickness = thickness;
        if (_outlineRenderer != null && _outlineRenderer.sharedMaterial != null)
        {
            _outlineRenderer.sharedMaterial.SetFloat("_OutlineThickness", thickness);
        }
    }
}