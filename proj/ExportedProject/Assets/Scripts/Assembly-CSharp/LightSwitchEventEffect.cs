using UnityEngine;

public class LightSwitchEventEffect : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectCallbackController))]
	private ObjectProvider _beatmapObjectCallbackControllerProvider;

	[SerializeField]
	private ColorSO _lightColor0;

	[SerializeField]
	private ColorSO _lightColor1;

	[SerializeField]
	private ColorSO _highlightColor0;

	[SerializeField]
	private ColorSO _highlightColor1;

	[SerializeField]
	private bool _lightOnStart;

	[SerializeField]
	private int _lightsID;

	[SerializeField]
	private BeatmapEventType _event;

	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private bool _lightIsOn;

	private Color _offColor = new Color(0f, 0f, 0f, 0f);

	private float _highlightValue;

	private Color _afterHighlightColor;

	private Color _highlightColor;

	private float kFadeSpeed = 2f;

	private BloomPrePassLight[] _lights;

	public int LightsID
	{
		get
		{
			return _lightsID;
		}
	}

	private void Start()
	{
		_beatmapObjectCallbackController = _beatmapObjectCallbackControllerProvider.GetProvidedObject<BeatmapObjectCallbackController>();
		_beatmapObjectCallbackController.beatmapEventDidTriggerEvent += HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		_lights = BloomPrePass.GetLightsWithID(_lightsID);
		_lightIsOn = _lightOnStart;
		SetColor((!_lightIsOn) ? _offColor : ((Color)_lightColor0));
		base.enabled = false;
	}

	private void OnDestroy()
	{
		if ((bool)_beatmapObjectCallbackController)
		{
			_beatmapObjectCallbackController.beatmapEventDidTriggerEvent -= HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger;
		}
	}

	private void Update()
	{
		if (_lightIsOn || _highlightValue != 0f)
		{
			SetColor(Color.Lerp(_afterHighlightColor, _highlightColor, _highlightValue));
			_highlightValue = Mathf.Lerp(_highlightValue, 0f, Time.deltaTime * kFadeSpeed);
			if (_highlightValue < 0.0001f)
			{
				_highlightValue = 0f;
				SetColor(_afterHighlightColor);
				base.enabled = false;
			}
		}
	}

	private void HandleBeatmapObjectCallbackControllerBeatmapEventDidTrigger(BeatmapEventData beatmapEventData)
	{
		if (beatmapEventData.type == _event)
		{
			if (beatmapEventData.value == 0)
			{
				_lightIsOn = false;
				_highlightValue = 0f;
				base.enabled = false;
				SetColor(_offColor);
			}
			else if (beatmapEventData.value == 1 || beatmapEventData.value == 5)
			{
				_lightIsOn = true;
				_highlightValue = 0f;
				base.enabled = false;
				SetColor((beatmapEventData.value != 1) ? _lightColor1 : _lightColor0);
			}
			else if (beatmapEventData.value == 2 || beatmapEventData.value == 6)
			{
				_lightIsOn = true;
				_highlightValue = 1f;
				base.enabled = true;
				_highlightColor = ((beatmapEventData.value != 2) ? _highlightColor1 : _highlightColor0);
				SetColor(_highlightColor);
				_afterHighlightColor = ((beatmapEventData.value != 2) ? _lightColor1 : _lightColor0);
			}
			else if (beatmapEventData.value == 3 || beatmapEventData.value == 7 || beatmapEventData.value == -1)
			{
				_lightIsOn = true;
				_highlightValue = 1f;
				base.enabled = true;
				_highlightColor = ((beatmapEventData.value != 3) ? _highlightColor1 : _highlightColor0);
				SetColor(_highlightColor);
				_afterHighlightColor = _offColor;
			}
		}
	}

	private void SetColor(Color color)
	{
		for (int i = 0; i < _lights.Length; i++)
		{
			_lights[i].color = color;
		}
	}
}
