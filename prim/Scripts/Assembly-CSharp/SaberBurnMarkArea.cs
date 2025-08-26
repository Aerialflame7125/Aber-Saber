using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class SaberBurnMarkArea : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private GameObject _sparkleParticleSystemPrefab;

	[SerializeField]
	private GameObject _burnMarkCenterParticleSystemPrefab;

	[SerializeField]
	private float _blackMarkLineWidth = 0.5f;

	[SerializeField]
	private float _blackMarkLineRandomOffset = 0.001f;

	[SerializeField]
	private Material _blackMarkLineMaterial;

	[SerializeField]
	private int _textureWidth = 1024;

	[SerializeField]
	private int _textureHeight = 512;

	[SerializeField]
	private float _burnMarksFadeOutStrength = 0.3f;

	private Renderer _renderer;

	private int _fadeOutStrengthShaderPropertyID;

	private Saber[] _sabers;

	private Plane _plane;

	private Vector3[] _prevBurnMarkPos;

	private bool[] _prevBurnMarkPosValid;

	private LineRenderer[] _lineRenderers;

	private Camera _camera;

	private Vector3[] _linePoints;

	private RenderTexture[] _renderTextures;

	private ParticleSystem[] _sparkleParticleSystems;

	private ParticleSystem[] _burnMarkCenterParticleSystems;

	private ParticleSystem.EmitParams _emitParams;

	private Transform _particleSystemsTranform;

	private Material _fadeOutMaterial;

	private void Awake()
	{
		_emitParams = default(ParticleSystem.EmitParams);
		_sabers = new Saber[2];
		_prevBurnMarkPos = new Vector3[2];
		_prevBurnMarkPosValid = new bool[2];
		_sparkleParticleSystems = new ParticleSystem[2];
		_burnMarkCenterParticleSystems = new ParticleSystem[2];
		_renderer = GetComponent<Renderer>();
		_renderer.enabled = true;
		_plane = new Plane(base.transform.up, base.transform.position);
		_linePoints = new Vector3[100];
		_fadeOutStrengthShaderPropertyID = Shader.PropertyToID("_FadeOutStrength");
		_lineRenderers = new LineRenderer[2];
		int num = 31;
		for (int i = 0; i < 2; i++)
		{
			GameObject gameObject = new GameObject();
			gameObject.transform.parent = base.transform;
			gameObject.layer = num;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			_lineRenderers[i] = gameObject.AddComponent<LineRenderer>();
			_lineRenderers[i].material = _blackMarkLineMaterial;
			_lineRenderers[i].startColor = Color.red;
			_lineRenderers[i].endColor = Color.red;
			_lineRenderers[i].widthMultiplier = _blackMarkLineWidth;
			_lineRenderers[i].numCapVertices = 2;
			_lineRenderers[i].alignment = LineAlignment.Local;
			_lineRenderers[i].textureMode = LineTextureMode.Tile;
			_lineRenderers[i].positionCount = 2;
			GameObject gameObject2 = Object.Instantiate(_sparkleParticleSystemPrefab);
			Transform transform = gameObject2.transform;
			transform.parent = base.transform;
			transform.position = Vector3.zero;
			transform.eulerAngles = new Vector3(-90f, 0f, 0f);
			_sparkleParticleSystems[i] = gameObject2.GetComponent<ParticleSystem>();
			gameObject2 = Object.Instantiate(_burnMarkCenterParticleSystemPrefab);
			transform = gameObject2.transform;
			transform.parent = base.transform;
			transform.position = Vector3.zero;
			transform.eulerAngles = new Vector3(-90f, 0f, 0f);
			_burnMarkCenterParticleSystems[i] = gameObject2.GetComponent<ParticleSystem>();
			_particleSystemsTranform = transform;
			_prevBurnMarkPosValid[i] = false;
		}
		_renderTextures = new RenderTexture[2];
		_renderTextures[0] = new RenderTexture(_textureWidth, _textureHeight, 0, RenderTextureFormat.ARGBFloat);
		_renderTextures[0].hideFlags = HideFlags.DontSave;
		_renderTextures[1] = new RenderTexture(_textureWidth, _textureHeight, 0, RenderTextureFormat.ARGBFloat);
		_renderTextures[1].hideFlags = HideFlags.DontSave;
		GameObject gameObject3 = new GameObject();
		gameObject3.hideFlags = HideFlags.HideAndDontSave;
		_camera = gameObject3.AddComponent<Camera>();
		_camera.orthographic = true;
		_camera.orthographicSize = 1f;
		_camera.nearClipPlane = 0f;
		_camera.farClipPlane = 1f;
		_camera.clearFlags = CameraClearFlags.Nothing;
		_camera.backgroundColor = Color.black;
		_camera.cullingMask = 1 << num;
		_camera.targetTexture = _renderTextures[0];
		_camera.allowMSAA = false;
		_camera.allowHDR = false;
		_camera.enabled = false;
		_renderer.sharedMaterial.mainTexture = _renderTextures[1];
		_fadeOutMaterial = new Material(Shader.Find("Custom/FadeOutTexture"));
		_fadeOutMaterial.hideFlags = HideFlags.HideAndDontSave;
		_fadeOutMaterial.mainTexture = _renderTextures[0];
	}

	private void Start()
	{
		PlayerController providedObject = _playerControllerProvider.GetProvidedObject<PlayerController>();
		_sabers[0] = providedObject.leftSaber;
		_sabers[1] = providedObject.rightSaber;
	}

	private void OnDestroy()
	{
		Object.Destroy(_camera.gameObject);
		Object.Destroy(_lineRenderers[0].gameObject);
		Object.Destroy(_lineRenderers[1].gameObject);
	}

	private bool GetBurnMarkPos(Vector3 bladeBottomPos, Vector3 bladeTopPos, out Vector3 burnMarkPos)
	{
		float num = Vector3.Distance(bladeBottomPos, bladeTopPos);
		Vector3 vector = (bladeTopPos - bladeBottomPos) / num;
		if (_plane.Raycast(new Ray(bladeBottomPos, vector), out var enter) && enter <= num)
		{
			burnMarkPos = bladeBottomPos + vector * enter;
			Bounds bounds = _renderer.bounds;
			return bounds.min.x < burnMarkPos.x && bounds.max.x > burnMarkPos.x && bounds.min.z < burnMarkPos.z && bounds.max.z > burnMarkPos.z;
		}
		burnMarkPos = Vector3.zero;
		return false;
	}

	private Vector3 WorldToCameraBurnMarkPos(Vector3 pos)
	{
		pos = base.transform.InverseTransformPoint(pos);
		Bounds bounds = _renderer.bounds;
		Vector3 localScale = base.transform.localScale;
		return new Vector3((0f - pos.x) * localScale.x / bounds.extents.x * (float)_textureWidth / (float)_textureHeight, (0f - pos.z) * localScale.z / bounds.extents.z, 0f);
	}

	private void Update()
	{
		if (_sabers[0] == null)
		{
			return;
		}
		for (int i = 0; i < 2; i++)
		{
			Vector3 burnMarkPos = Vector3.zero;
			bool flag = _sabers[i].isActiveAndEnabled && GetBurnMarkPos(_sabers[i].saberBladeBottomPos, _sabers[i].saberBladeTopPos, out burnMarkPos);
			if (flag && _prevBurnMarkPosValid[i])
			{
				Vector3 vector = burnMarkPos - _prevBurnMarkPos[i];
				float magnitude = vector.magnitude;
				float num = 0.007f;
				int num2 = (int)(magnitude / num);
				int num3 = ((num2 <= 0) ? 1 : num2);
				for (int j = 0; j <= num2; j++)
				{
					Vector3 position = _prevBurnMarkPos[i] + vector * j / num3;
					_emitParams.applyShapeToPosition = true;
					_emitParams.position = _particleSystemsTranform.InverseTransformPoint(position);
					_burnMarkCenterParticleSystems[i].Emit(_emitParams, 1);
				}
				num = 0.05f;
				num2 = (int)(magnitude / num);
				num3 = ((num2 <= 0) ? 1 : num2);
				for (int k = 0; k <= num2; k++)
				{
					Vector3 position = _prevBurnMarkPos[i] + vector * k / num3;
					_emitParams.applyShapeToPosition = true;
					_emitParams.position = _particleSystemsTranform.InverseTransformPoint(position);
					_sparkleParticleSystems[i].Emit(_emitParams, 1);
				}
				num = 0.01f;
				num2 = (int)(magnitude / num);
				num3 = ((num2 <= 0) ? 1 : num2);
				Vector3 normalized = new Vector3(vector.z, 0f, 0f - vector.x).normalized;
				for (int l = 0; l <= num3 && l < _linePoints.Length; l++)
				{
					Vector3 position = _prevBurnMarkPos[i] + vector * l / num3;
					position += normalized * Random.Range(0f - _blackMarkLineRandomOffset, _blackMarkLineRandomOffset);
					ref Vector3 reference = ref _linePoints[l];
					reference = WorldToCameraBurnMarkPos(position);
				}
				_lineRenderers[i].positionCount = num3 + 1;
				_lineRenderers[i].SetPositions(_linePoints);
				_lineRenderers[i].enabled = true;
			}
			else
			{
				_lineRenderers[i].enabled = false;
			}
			_prevBurnMarkPosValid[i] = flag;
			_prevBurnMarkPos[i] = burnMarkPos;
		}
		if (_lineRenderers[0].enabled || _lineRenderers[1].enabled)
		{
			_camera.Render();
		}
		_camera.targetTexture = _renderTextures[1];
		_renderer.sharedMaterial.mainTexture = _renderTextures[1];
		_fadeOutMaterial.mainTexture = _renderTextures[0];
		_fadeOutMaterial.SetFloat(_fadeOutStrengthShaderPropertyID, Mathf.Max(0f, 1f - Time.deltaTime * _burnMarksFadeOutStrength));
		Graphics.Blit(_renderTextures[0], _renderTextures[1], _fadeOutMaterial);
		RenderTexture renderTexture = _renderTextures[0];
		_renderTextures[0] = _renderTextures[1];
		_renderTextures[1] = renderTexture;
	}
}
