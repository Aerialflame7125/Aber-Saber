using UnityEngine;

[AddComponentMenu("AVPro Live Camera/IMGUI Display")]
public class AVProLiveCameraGUIDisplay : MonoBehaviour
{
	public AVProLiveCamera _liveCamera;

	public ScaleMode _scaleMode = ScaleMode.ScaleToFit;

	public Color _color = Color.white;

	public int _depth;

	public bool _fullScreen = true;

	public float _x;

	public float _y;

	public float _width = 1f;

	public float _height = 1f;

	public bool _flipX;

	public bool _flipY;

	private static int _propApplyGamma;

	private static int _propFlip;

	private static Shader _shaderGammaConversion;

	private Material _material;

	private void Awake()
	{
		if (_propApplyGamma == 0)
		{
			_propApplyGamma = Shader.PropertyToID("_ApplyGamma");
			_propFlip = Shader.PropertyToID("_Flip");
		}
	}

	private void Start()
	{
		base.useGUILayout = false;
		if (_shaderGammaConversion == null)
		{
			_shaderGammaConversion = Shader.Find("Hidden/AVProLiveCamera/IMGUI");
		}
	}

	private void OnDestroy()
	{
		if (_material != null)
		{
			Object.Destroy(_material);
			_material = null;
		}
	}

	private static Shader GetRequiredShader()
	{
		Shader result = null;
		if (QualitySettings.activeColorSpace == ColorSpace.Linear)
		{
			result = _shaderGammaConversion;
		}
		return result;
	}

	private void Update()
	{
		Shader shader = null;
		if (_material != null)
		{
			shader = _material.shader;
		}
		Shader requiredShader = GetRequiredShader();
		if (!(shader != requiredShader))
		{
			return;
		}
		if (_material != null)
		{
			Object.Destroy(_material);
			_material = null;
		}
		if (!(requiredShader != null))
		{
			return;
		}
		_material = new Material(requiredShader);
		if (_material.HasProperty(_propApplyGamma))
		{
			if (QualitySettings.activeColorSpace == ColorSpace.Linear)
			{
				_material.EnableKeyword("APPLY_GAMMA");
			}
			else
			{
				_material.DisableKeyword("APPLY_GAMMA");
			}
		}
	}

	public void OnGUI()
	{
		if (_liveCamera == null)
		{
			return;
		}
		_x = Mathf.Clamp01(_x);
		_y = Mathf.Clamp01(_y);
		_width = Mathf.Clamp01(_width);
		_height = Mathf.Clamp01(_height);
		if (!(_liveCamera.OutputTexture != null))
		{
			return;
		}
		GUI.depth = _depth;
		GUI.color = _color;
		Rect rect = ((!_fullScreen) ? new Rect(_x * (float)(Screen.width - 1), _y * (float)(Screen.height - 1), _width * (float)Screen.width, _height * (float)Screen.height) : new Rect(0f, 0f, Screen.width, Screen.height));
		if (_material != null)
		{
			Vector2 one = Vector2.one;
			if (_flipX)
			{
				one.x = -1f;
			}
			if (_flipY)
			{
				one.y = -1f;
			}
			_material.SetVector(_propFlip, one);
			DrawTexture(rect, _liveCamera.OutputTexture, _scaleMode, _material);
			return;
		}
		if (_flipX || _flipY)
		{
			Vector2 pivotPoint = new Vector2(rect.x + rect.width / 2f, rect.y + rect.height / 2f);
			Vector2 one2 = Vector2.one;
			if (_flipX)
			{
				one2.x = -1f;
			}
			if (_flipY)
			{
				one2.y = -1f;
			}
			GUIUtility.ScaleAroundPivot(one2, pivotPoint);
		}
		GUI.DrawTexture(rect, _liveCamera.OutputTexture, _scaleMode, false);
	}

	private static void DrawTexture(Rect screenRect, Texture texture, ScaleMode scaleMode, Material material)
	{
		if (Event.current.type != EventType.Repaint)
		{
			return;
		}
		float num = texture.width;
		float num2 = texture.height;
		float num3 = num / num2;
		Rect sourceRect = new Rect(0f, 0f, 1f, 1f);
		switch (scaleMode)
		{
		case ScaleMode.ScaleAndCrop:
		{
			float num7 = screenRect.width / screenRect.height;
			if (num7 > num3)
			{
				float num8 = num3 / num7;
				sourceRect = new Rect(0f, (1f - num8) * 0.5f, 1f, num8);
			}
			else
			{
				float num9 = num7 / num3;
				sourceRect = new Rect(0.5f - num9 * 0.5f, 0f, num9, 1f);
			}
			break;
		}
		case ScaleMode.ScaleToFit:
		{
			float num4 = screenRect.width / screenRect.height;
			if (num4 > num3)
			{
				float num5 = num3 / num4;
				screenRect = new Rect(screenRect.xMin + screenRect.width * (1f - num5) * 0.5f, screenRect.yMin, num5 * screenRect.width, screenRect.height);
			}
			else
			{
				float num6 = num4 / num3;
				screenRect = new Rect(screenRect.xMin, screenRect.yMin + screenRect.height * (1f - num6) * 0.5f, screenRect.width, num6 * screenRect.height);
			}
			break;
		}
		}
		Graphics.DrawTexture(screenRect, texture, sourceRect, 0, 0, 0, 0, GUI.color, material);
	}
}
