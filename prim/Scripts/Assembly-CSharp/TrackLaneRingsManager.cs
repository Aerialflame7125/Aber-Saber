using UnityEngine;

public class TrackLaneRingsManager : MonoBehaviour
{
	[Header("Rings visuals")]
	[SerializeField]
	private TrackLaneRing _trackLaneRingPrefab;

	[SerializeField]
	private int _ringCount = 10;

	[SerializeField]
	private float _ringPositionStep = 2f;

	private TrackLaneRing[] _rings;

	public float ringPositionStep => _ringPositionStep;

	public TrackLaneRing[] Rings => _rings;

	private void Awake()
	{
		_rings = new TrackLaneRing[_ringCount];
		for (int i = 0; i < _rings.Length; i++)
		{
			_rings[i] = Object.Instantiate(_trackLaneRingPrefab);
			Vector3 position = new Vector3(0f, 0f, (float)i * _ringPositionStep);
			_rings[i].Init(position, base.transform.position);
		}
	}

	private void Update()
	{
		for (int i = 0; i < _rings.Length; i++)
		{
			_rings[i].UpdateRing(Time.deltaTime);
		}
	}
}
