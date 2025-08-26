using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class OnWillRenderObjectTrigger : MonoBehaviour
{
	private Material _material;

	private Mesh _mesh;

	private MeshFilter _meshFilter;

	private MeshRenderer _meshRenderer;

	private void OnEnable()
	{
		if (_material == null)
		{
			_material = new Material(Shader.Find("Diffuse"));
			_material.renderQueue = 0;
		}
		if (_mesh == null)
		{
			_mesh = new Mesh();
			_mesh.name = "Huge Mesh";
			_mesh.hideFlags = HideFlags.HideAndDontSave;
			_mesh.bounds = new Bounds(Vector3.zero, new Vector3(9999999f, 9999999f, 9999999f));
		}
		_meshRenderer = GetComponent<MeshRenderer>();
		if (_meshRenderer == null)
		{
			_meshRenderer = base.gameObject.AddComponent<MeshRenderer>();
		}
		_meshRenderer.hideFlags = HideFlags.None;
		_meshRenderer.sharedMaterial = _material;
		_meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
		_meshRenderer.receiveShadows = false;
		_meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
		_meshRenderer.allowOcclusionWhenDynamic = false;
		_meshRenderer.lightProbeUsage = LightProbeUsage.Off;
		_meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
		_meshFilter = GetComponent<MeshFilter>();
		if (_meshFilter == null)
		{
			_meshFilter = base.gameObject.AddComponent<MeshFilter>();
		}
		_meshFilter.hideFlags = HideFlags.None;
		_meshFilter.sharedMesh = _mesh;
	}

	private void OnDisable()
	{
		EssentialHelpers.SafeDestroy(_material);
		EssentialHelpers.SafeDestroy(_mesh);
	}
}
