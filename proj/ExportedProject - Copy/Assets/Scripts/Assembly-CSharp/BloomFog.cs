using UnityEngine;

[CreateAssetMenu(fileName = "BloomFog", menuName = "BS/Rendering/BloomFog")]
public class BloomFog : ScriptableObject
{
	public BloomFogEnvironmentParams fog0Params;

	public BloomFogEnvironmentParams fog1Params;

	private bool _bloomFogEnabled = true;

	private float _transition;

	private int _customFogColorID;

	private int _customFogColorMultiplierID;

	private int _customFogAttenuationID;

	private int _customFogOffsetID;

	private int _bloomFogEnabledID;

	public float transition
	{
		get
		{
			return _transition;
		}
		set
		{
			if (value != _transition)
			{
				_transition = value;
				UpdateShaderParams();
			}
		}
	}

	public bool bloomFogEnabled
	{
		get
		{
			return _bloomFogEnabled;
		}
		set
		{
			if (value != _bloomFogEnabled)
			{
				if (value)
				{
					Shader.EnableKeyword("_ENABLE_BLOOM_FOG");
				}
				else
				{
					Shader.DisableKeyword("_ENABLE_BLOOM_FOG");
				}
				_bloomFogEnabled = value;
			}
		}
	}

	private void OnEnable()
	{
		_customFogColorID = Shader.PropertyToID("_CustomFogColor");
		_customFogColorMultiplierID = Shader.PropertyToID("_CustomFogColorMultiplier");
		_customFogAttenuationID = Shader.PropertyToID("_CustomFogAttenuation");
		_customFogOffsetID = Shader.PropertyToID("_CustomFogOffset");
		UpdateShaderParams();
	}

	public void UpdateShaderParams()
	{
		if (!(fog0Params == null))
		{
			if (fog1Params == null || _transition <= Mathf.Epsilon)
			{
				SetParams(fog0Params.color, fog0Params.colorMultiplier, fog0Params.attenuation, fog0Params.offset);
				return;
			}
			if (_transition == 1f)
			{
				SetParams(fog1Params.color, fog1Params.colorMultiplier, fog1Params.attenuation, fog1Params.offset);
				return;
			}
			Color color = Color.Lerp(fog0Params.color, fog1Params.color, _transition);
			float colorMultiplier = Mathf.Lerp(fog0Params.colorMultiplier, fog1Params.colorMultiplier, _transition);
			float attenuation = Mathf.Lerp(fog0Params.attenuation, fog1Params.attenuation, _transition);
			float offset = Mathf.Lerp(fog0Params.offset, fog1Params.offset, _transition);
			SetParams(color, colorMultiplier, attenuation, offset);
		}
	}

	private void SetParams(Color color, float colorMultiplier, float attenuation, float offset)
	{
		Shader.SetGlobalColor(_customFogColorID, color);
		Shader.SetGlobalFloat(_customFogColorMultiplierID, colorMultiplier);
		Shader.SetGlobalFloat(_customFogAttenuationID, attenuation);
		Shader.SetGlobalFloat(_customFogOffsetID, offset);
	}
}
