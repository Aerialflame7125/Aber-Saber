using UnityEngine;

public class BasicSpectrogramData : MonoBehaviour
{
	[SerializeField]
	private AudioSource _audioSource;

	public const int kNumberOfSamples = 64;

	private bool _hasData;

	private bool _hasProcessedData;

	private float[] _samples = new float[64];

	private float[] _processedSamples = new float[64];

	public float[] Samples
	{
		get
		{
			if (!_hasData && (bool)_audioSource)
			{
				_audioSource.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
				_hasData = true;
			}
			return _samples;
		}
	}

	public float[] ProcessedSamples
	{
		get
		{
			if (!_hasProcessedData)
			{
				ProcessSamples(Samples, _processedSamples);
				_hasProcessedData = true;
			}
			return _processedSamples;
		}
	}

	private void Awake()
	{
		for (int i = 0; i < _processedSamples.Length; i++)
		{
			_processedSamples[i] = 0f;
		}
	}

	private void LateUpdate()
	{
		_hasData = false;
		_hasProcessedData = false;
	}

	private void ProcessSamples(float[] sourceSamples, float[] processedSamples)
	{
		float deltaTime = Time.deltaTime;
		for (int i = 0; i < sourceSamples.Length; i++)
		{
			float f = 10000f * sourceSamples[i];
			float num = 0.05f * Mathf.Log(Mathf.Abs(f) + 1f, 10f);
			if (processedSamples[i] < num)
			{
				processedSamples[i] = Mathf.Lerp(processedSamples[i], num, deltaTime * 8f);
			}
			else
			{
				processedSamples[i] = Mathf.Lerp(processedSamples[i], num, deltaTime * 1.5f);
			}
		}
	}
}
