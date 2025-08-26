using UnityEngine;

public class MoveBackWall : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private float _fadeInRegion = 0.5f;

	[SerializeField]
	private MeshRenderer _meshRenderer;

	private PlayerController _playerController;

	private float _thisZ;

	private bool _isVisible;

	private Material _material;

	private void Start()
	{
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_thisZ = base.transform.position.z;
		_material = _meshRenderer.sharedMaterial;
		_meshRenderer.enabled = false;
	}

	private void Update()
	{
		float num = Mathf.Abs(_playerController.headPos.z - _thisZ);
		if (num < _fadeInRegion && !_isVisible)
		{
			_isVisible = true;
			_meshRenderer.enabled = true;
		}
		else if (num > _fadeInRegion && _isVisible)
		{
			_isVisible = false;
			_meshRenderer.enabled = false;
		}
		if (_isVisible)
		{
			_material.color = new Color(1f, 1f, 1f, 1f - num / _fadeInRegion);
		}
	}
}
