using UnityEngine;

public class SpectrogramColumns : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BasicSpectrogramData))]
	private ObjectProvider _basicSpectrogramDataProvider;

	[SerializeField]
	private GameObject _columnPrefab;

	[SerializeField]
	private Vector3 _separator = new Vector3(0f, 0f, 1f);

	[SerializeField]
	private float _minHeight = 1f;

	[SerializeField]
	private float _maxHeight = 10f;

	[SerializeField]
	private float _columnWidth = 1f;

	[SerializeField]
	private float _columnDepth = 1f;

	private Transform[] _columnTransforms;

	private BasicSpectrogramData _spectrogramData;

	private void Start()
	{
		_spectrogramData = _basicSpectrogramDataProvider.GetProvidedObject<BasicSpectrogramData>();
		CreateColums();
	}

	private void Update()
	{
		float[] processedSamples = _spectrogramData.ProcessedSamples;
		for (int i = 0; i < processedSamples.Length; i++)
		{
			float num = processedSamples[i] * (5f + (float)i * 0.07f);
			if (num > 1f)
			{
				num = 1f;
			}
			num = Mathf.Pow(num, 2f);
			_columnTransforms[i].localScale = new Vector3(_columnWidth, Mathf.Lerp(_minHeight, _maxHeight, num) + (float)i * 0.1f, _columnDepth);
			_columnTransforms[i + 64].localScale = new Vector3(_columnWidth, Mathf.Lerp(_minHeight, _maxHeight, num), _columnDepth);
		}
	}

	private void CreateColums()
	{
		_columnTransforms = new Transform[128];
		for (int i = 0; i < 64; i++)
		{
			_columnTransforms[i] = CreateColumn(_separator * i);
			_columnTransforms[i + 64] = CreateColumn(-_separator * (i + 1));
		}
	}

	private Transform CreateColumn(Vector3 pos)
	{
		GameObject gameObject = Object.Instantiate(_columnPrefab, base.transform);
		gameObject.transform.localPosition = pos;
		gameObject.transform.localScale = new Vector3(_columnWidth, _minHeight, _columnDepth);
		return gameObject.transform;
	}
}
