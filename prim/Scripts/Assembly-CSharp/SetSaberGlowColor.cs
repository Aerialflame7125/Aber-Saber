using UnityEngine;

public class SetSaberGlowColor : MonoBehaviour
{
	[SerializeField]
	private SaberTypeObject _saber;

	[SerializeField]
	private ColorManager _colorManager;

	[SerializeField]
	private Color _multiplierSaberColor = new Color(1f, 1f, 1f, 1f);

	[SerializeField]
	private MeshRenderer _meshRenderer;

	private void Start()
	{
		MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
		int num = Shader.PropertyToID("_Color");
		materialPropertyBlock.SetColor(num, _colorManager.ColorForSaberType(_saber.saberType) * _multiplierSaberColor);
		_meshRenderer.SetPropertyBlock(materialPropertyBlock);
	}
}
