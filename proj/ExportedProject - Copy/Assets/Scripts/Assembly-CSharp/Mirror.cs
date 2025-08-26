using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class Mirror : MonoBehaviour
{
	[SerializeField]
	private MirrorRenderer _mirrorRenderer;

	private Renderer _renderer;

	private int _texturePropertyID;

	private void Awake()
	{
		_renderer = GetComponent<Renderer>();
		_texturePropertyID = Shader.PropertyToID("_ReflectionTex");
	}

	private void OnWillRenderObject()
	{
		if (base.enabled && (bool)_renderer && _renderer.enabled && !(_mirrorRenderer == null))
		{
			Texture mirrorTexture = _mirrorRenderer.GetMirrorTexture(base.transform.position, base.transform.up);
			_renderer.sharedMaterial.SetTexture(_texturePropertyID, mirrorTexture);
		}
	}
}
