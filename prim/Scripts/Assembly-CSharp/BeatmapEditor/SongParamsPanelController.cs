using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class SongParamsPanelController : MonoBehaviour
{
	[SerializeField]
	private ObservableFloatSO _beatsPerMinute;

	[SerializeField]
	private ObservableFloatSO _songTimeOffset;

	[SerializeField]
	private ObservableFloatSO _shuffleStrength;

	[Space]
	[SerializeField]
	private InputField _beatsPerMinuteInputField;

	[SerializeField]
	private InputField _songTimeOffsetInputField;

	[SerializeField]
	private InputField _shuffleStrengthInputField;

	private InputFieldDataBinder _binder;

	private void Awake()
	{
		_binder = new InputFieldDataBinder();
		_binder.AddBindings(new List<Tuple<InputField, ObservableFloatSO, Func<string, float>, Func<float, string>>>
		{
			{
				_beatsPerMinuteInputField,
				_beatsPerMinute,
				(Func<string, float>)((string s) => ConvertAndClamp(s, _beatsPerMinute, 30f, 300f, 1f)),
				(Func<float, string>)((float f) => f.ToString())
			},
			{
				_songTimeOffsetInputField,
				_songTimeOffset,
				(Func<string, float>)((string s) => ConvertAndClamp(s, _songTimeOffset, 0f, 100f, 0.001f)),
				(Func<float, string>)((float f) => (f * 1000f).ToString())
			},
			{
				_shuffleStrengthInputField,
				_shuffleStrength,
				(Func<string, float>)((string s) => ConvertAndClamp(s, _shuffleStrength, -1000f, 1000f, 0.001f)),
				(Func<float, string>)((float f) => Mathf.FloorToInt(f * 1000f + 0.5f).ToString())
			}
		});
	}

	private float ConvertAndClamp(string s, float originalValue, float min, float max, float factor)
	{
		if (float.TryParse(s, out var result))
		{
			return Mathf.Clamp(result, min, max) * factor;
		}
		return originalValue;
	}

	private void OnDestroy()
	{
		if (_binder != null)
		{
			_binder.ClearBindings();
		}
	}
}
