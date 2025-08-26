using UnityEngine;

public class MaterialPropertyBlockController : MonoBehaviour
{
	[SerializeField]
	private Renderer[] _renderers;

	private MaterialPropertyBlock _materialPropertyBlock;

	private bool _shouldUpdate;

	public MaterialPropertyBlock materialPropertyBlock
	{
		get
		{
			return _materialPropertyBlock;
		}
	}

	private void Awake()
	{
		_materialPropertyBlock = new MaterialPropertyBlock();
	}

	public void ApplyChanges()
	{
		_shouldUpdate = true;
	}

	private void LateUpdate()
	{
		if (_shouldUpdate)
		{
			_shouldUpdate = false;
			for (int i = 0; i < _renderers.Length; i++)
			{
				_renderers[i].SetPropertyBlock(_materialPropertyBlock);
			}
		}
	}
}
