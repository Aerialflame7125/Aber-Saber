using UnityEngine;
using UnityEngine.XR;

[CreateAssetMenu(fileName = "MirrorRenderer", menuName = "BS/Rendering/MirrorRenderer")]
public class MirrorRenderer : PersistentScriptableObject
{
	[SerializeField]
	private LayerMask _reflectLayers = -1;

	[SerializeField]
	private int _stereoTextureWidth = 2048;

	[SerializeField]
	private int _stereoTextureHeight = 1024;

	[SerializeField]
	private int _monoTextureWidth = 256;

	[SerializeField]
	private int _monoTextureHeight = 256;

	[SerializeField]
	private bool _disablePixelLights = true;

	[SerializeField]
	private int _maxAntiAliasing = 1;

	[SerializeField]
	private bool _disableDepthTexture;

	private RenderTexture _activeRenderTexture;

	private RenderTexture _stereoMirrorTexture;

	private RenderTexture _monoMirrorTexture;

	private Camera _mirrorCamera;

	private Vector3 _prevCameraPos;

	private Quaternion _prevCameraRotation;

	private float _prevCameraFOV;

	private int _prevFrameNum;

	private int _antialiasing;

	private Texture2D _emptyTexture;

	private readonly Rect kLeftRect = new Rect(0f, 0f, 0.5f, 1f);

	private readonly Rect kRightRect = new Rect(0.5f, 0f, 0.5f, 1f);

	private readonly Rect kFullRect = new Rect(0f, 0f, 1f, 1f);

	private void OnValidate()
	{
		ValidateParams();
	}

	private void Awake()
	{
		ValidateParams();
	}

	private void ValidateParams()
	{
		if (_maxAntiAliasing != 1 && _maxAntiAliasing != 2 && _maxAntiAliasing != 4 && _maxAntiAliasing != 8)
		{
			_maxAntiAliasing = 1;
		}
		_antialiasing = Mathf.Min(QualitySettings.antiAliasing, _maxAntiAliasing);
		if (_antialiasing != 1 && _antialiasing != 2 && _antialiasing != 4 && _antialiasing != 8)
		{
			_antialiasing = 1;
		}
	}

	public void InitFromPreset(MirrorRendererGraphicsSettingsPresets.Preset preset)
	{
		_reflectLayers = preset.reflectLayers;
		_stereoTextureWidth = preset.stereoTextureWidth;
		_stereoTextureHeight = preset.stereoTextureHeight;
		_monoTextureWidth = preset.monoTextureWidth;
		_monoTextureHeight = preset.monoTextureHeight;
		_maxAntiAliasing = preset.maxAntiAliasing;
	}

	public Texture GetMirrorTexture(Vector3 position, Vector3 normal)
	{
		if ((int)_reflectLayers == 0)
		{
			if (_emptyTexture == null)
			{
				_emptyTexture = new Texture2D(1, 1, TextureFormat.ARGB32, mipmap: false);
				_emptyTexture.SetPixel(0, 0, Color.clear);
				_emptyTexture.Apply();
			}
			return _emptyTexture;
		}
		Camera current = Camera.current;
		if (!current || current == _mirrorCamera)
		{
			return null;
		}
		if (_prevFrameNum == Time.frameCount && _prevCameraPos == current.transform.position && _prevCameraRotation == current.transform.rotation && _prevCameraFOV == current.fieldOfView)
		{
			return _activeRenderTexture;
		}
		Transform transform = current.transform;
		Vector3 position2 = transform.position;
		if (new Plane(normal, position).GetDistanceToPoint(position2) <= Mathf.Epsilon || (current.orthographic && Mathf.Abs(Vector3.Dot(transform.forward, normal)) <= Mathf.Epsilon))
		{
			return _activeRenderTexture;
		}
		_prevFrameNum = Time.frameCount;
		_prevCameraPos = position2;
		_prevCameraRotation = current.transform.rotation;
		_prevCameraFOV = current.fieldOfView;
		CreateOrUpdateMirrorCamera(current);
		int pixelLightCount = QualitySettings.pixelLightCount;
		if (_disablePixelLights)
		{
			QualitySettings.pixelLightCount = 0;
		}
		Graphics.SetRenderTarget(_activeRenderTexture);
		GL.Clear(clearDepth: true, clearColor: true, Color.clear);
		if (current.stereoEnabled)
		{
			if (current.stereoTargetEye == StereoTargetEyeMask.Both || current.stereoTargetEye == StereoTargetEyeMask.Right)
			{
				Vector3 vector = InputTracking.GetLocalPosition(XRNode.RightEye);
				Quaternion quaternion = InputTracking.GetLocalRotation(XRNode.RightEye);
				Transform parent = current.transform.parent;
				if ((bool)parent)
				{
					vector = parent.TransformPoint(vector);
					quaternion = parent.rotation * quaternion;
				}
				Matrix4x4 stereoProjectionMatrix = current.GetStereoProjectionMatrix(Camera.StereoscopicEye.Right);
				RenderMirror(vector, quaternion, stereoProjectionMatrix, kRightRect, position, normal);
			}
			if (current.stereoTargetEye == StereoTargetEyeMask.Both || current.stereoTargetEye == StereoTargetEyeMask.Left)
			{
				Vector3 vector2 = InputTracking.GetLocalPosition(XRNode.LeftEye);
				Quaternion quaternion2 = InputTracking.GetLocalRotation(XRNode.LeftEye);
				Transform parent2 = current.transform.parent;
				if ((bool)parent2)
				{
					vector2 = parent2.TransformPoint(vector2);
					quaternion2 = parent2.rotation * quaternion2;
				}
				Matrix4x4 stereoProjectionMatrix2 = current.GetStereoProjectionMatrix(Camera.StereoscopicEye.Left);
				RenderMirror(vector2, quaternion2, stereoProjectionMatrix2, kLeftRect, position, normal);
			}
		}
		else
		{
			RenderMirror(current.transform.position, current.transform.rotation, current.projectionMatrix, kFullRect, position, normal);
		}
		if (_disablePixelLights)
		{
			QualitySettings.pixelLightCount = pixelLightCount;
		}
		return _activeRenderTexture;
	}

	private void RenderMirror(Vector3 camPosition, Quaternion camRotation, Matrix4x4 camProjectionMatrix, Rect screenRect, Vector3 position, Vector3 normal)
	{
		_mirrorCamera.ResetWorldToCameraMatrix();
		_mirrorCamera.transform.SetPositionAndRotation(camPosition, camRotation);
		_mirrorCamera.projectionMatrix = camProjectionMatrix;
		Vector4 plane = Plane(position, normal);
		_mirrorCamera.worldToCameraMatrix *= CalculateReflectionMatrix(plane);
		Vector4 clipPlane = CameraSpacePlane(_mirrorCamera, position, normal);
		_mirrorCamera.projectionMatrix = _mirrorCamera.CalculateObliqueMatrix(clipPlane);
		bool invertCulling = GL.invertCulling;
		GL.invertCulling = !invertCulling;
		_mirrorCamera.rect = screenRect;
		try
		{
			_mirrorCamera.Render();
		}
		catch
		{
		}
		GL.invertCulling = invertCulling;
	}

	private void OnDisable()
	{
		if ((bool)_mirrorCamera)
		{
			Object.DestroyImmediate(_mirrorCamera.gameObject);
			_mirrorCamera = null;
		}
		if ((bool)_stereoMirrorTexture)
		{
			Object.DestroyImmediate(_stereoMirrorTexture);
			_stereoMirrorTexture = null;
		}
		if ((bool)_monoMirrorTexture)
		{
			Object.DestroyImmediate(_monoMirrorTexture);
			_monoMirrorTexture = null;
		}
	}

	private void CreateOrUpdateMirrorCamera(Camera src)
	{
		if (!_mirrorCamera)
		{
			GameObject gameObject = new GameObject("MirrorCam" + GetInstanceID(), typeof(Camera));
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			_mirrorCamera = gameObject.GetComponent<Camera>();
			_mirrorCamera.enabled = false;
		}
		if (_stereoMirrorTexture == null || _stereoMirrorTexture.width != _stereoTextureWidth || _stereoMirrorTexture.height != _stereoTextureHeight || _stereoMirrorTexture.antiAliasing != _antialiasing)
		{
			if (_stereoMirrorTexture != null)
			{
				Object.DestroyImmediate(_stereoMirrorTexture);
			}
			_stereoMirrorTexture = new RenderTexture(_stereoTextureWidth, _stereoTextureHeight, 24, (!src.allowHDR) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
			_stereoMirrorTexture.name = "__MirrorReflectionStereo" + GetInstanceID();
			_stereoMirrorTexture.hideFlags = HideFlags.HideAndDontSave;
			_stereoMirrorTexture.antiAliasing = _antialiasing;
		}
		if (_monoMirrorTexture == null || _monoMirrorTexture.width != _monoTextureWidth || _monoMirrorTexture.height != _monoTextureHeight || _monoMirrorTexture.antiAliasing != _antialiasing)
		{
			if (_monoMirrorTexture != null)
			{
				Object.DestroyImmediate(_monoMirrorTexture);
			}
			_monoMirrorTexture = new RenderTexture(_monoTextureWidth, _monoTextureHeight, 24, (!src.allowHDR) ? RenderTextureFormat.Default : RenderTextureFormat.DefaultHDR);
			_monoMirrorTexture.name = "__MirrorReflectionMono" + GetInstanceID();
			_monoMirrorTexture.hideFlags = HideFlags.HideAndDontSave;
			_monoMirrorTexture.antiAliasing = _antialiasing;
		}
		_activeRenderTexture = ((!src.stereoEnabled) ? _monoMirrorTexture : _stereoMirrorTexture);
		_mirrorCamera.CopyFrom(src);
		_mirrorCamera.targetTexture = _activeRenderTexture;
		if (_disableDepthTexture)
		{
			_mirrorCamera.depthTextureMode = DepthTextureMode.None;
		}
		_mirrorCamera.cullingMask = -17 & _reflectLayers.value;
		_mirrorCamera.clearFlags = CameraClearFlags.Nothing;
	}

	private static Vector4 Plane(Vector3 pos, Vector3 normal)
	{
		return new Vector4(normal.x, normal.y, normal.z, 0f - Vector3.Dot(pos, normal));
	}

	private static Vector4 CameraSpacePlane(Camera cam, Vector3 pos, Vector3 normal)
	{
		Matrix4x4 worldToCameraMatrix = cam.worldToCameraMatrix;
		Vector3 pos2 = worldToCameraMatrix.MultiplyPoint(pos);
		Vector3 normalized = worldToCameraMatrix.MultiplyVector(normal).normalized;
		return Plane(pos2, normalized);
	}

	private static Matrix4x4 CalculateReflectionMatrix(Vector4 plane)
	{
		Matrix4x4 identity = Matrix4x4.identity;
		identity.m00 = 1f - 2f * plane[0] * plane[0];
		identity.m01 = -2f * plane[0] * plane[1];
		identity.m02 = -2f * plane[0] * plane[2];
		identity.m03 = -2f * plane[3] * plane[0];
		identity.m10 = -2f * plane[1] * plane[0];
		identity.m11 = 1f - 2f * plane[1] * plane[1];
		identity.m12 = -2f * plane[1] * plane[2];
		identity.m13 = -2f * plane[3] * plane[1];
		identity.m20 = -2f * plane[2] * plane[0];
		identity.m21 = -2f * plane[2] * plane[1];
		identity.m22 = 1f - 2f * plane[2] * plane[2];
		identity.m23 = -2f * plane[3] * plane[2];
		identity.m30 = 0f;
		identity.m31 = 0f;
		identity.m32 = 0f;
		identity.m33 = 1f;
		return identity;
	}
}
