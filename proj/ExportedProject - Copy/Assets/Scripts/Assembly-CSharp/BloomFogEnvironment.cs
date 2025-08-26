using UnityEngine;

public class BloomFogEnvironment : MonoBehaviour
{
	[SerializeField]
	private BloomFog _bloomFog;

	[SerializeField]
	private BloomFogEnvironmentParams _fog0Params;

	[SerializeField]
	private BloomFogEnvironmentParams _fog1Params;

	private void Awake()
	{
		_bloomFog.transition = 0f;
		_bloomFog.fog0Params = _fog0Params;
		_bloomFog.fog1Params = _fog1Params;
	}
}
