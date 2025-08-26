using UnityEngine;

public class XWeaponTrailRenderer : MonoBehaviour
{
	[SerializeField]
	private MeshRenderer _meshRenderer;

	[SerializeField]
	private MeshFilter _meshFilter;

	private Mesh _mesh;

	public Mesh mesh
	{
		get
		{
			if (_mesh == null)
			{
				_mesh = new Mesh();
				_meshFilter.mesh = _mesh;
			}
			return _mesh;
		}
	}

	private void OnValidate()
	{
		if (_meshFilter == null)
		{
			_meshFilter = GetComponent<MeshFilter>();
		}
		if (_meshRenderer == null)
		{
			_meshRenderer = GetComponent<MeshRenderer>();
		}
	}

	private void OnEnable()
	{
		_meshRenderer.enabled = true;
	}

	private void OnDisable()
	{
		if ((bool)_meshRenderer)
		{
			_meshRenderer.enabled = false;
		}
	}
}
