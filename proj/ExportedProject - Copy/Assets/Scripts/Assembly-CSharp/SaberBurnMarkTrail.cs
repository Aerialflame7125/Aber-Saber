using UnityEngine;

public class SaberBurnMarkTrail : MonoBehaviour
{
	[SerializeField]
	private float _fadeoutDuration = 0.3f;

	[SerializeField]
	private TrailRenderer _trailRenderer;

	private float _fadeoutTime = -1f;

	private void Awake()
	{
		_trailRenderer.sortingOrder = 10;
	}

	private void Update()
	{
		if (_fadeoutTime >= 0f)
		{
			_fadeoutTime += Time.deltaTime;
			if (_fadeoutTime > _fadeoutDuration)
			{
				_fadeoutTime = -1f;
				base.gameObject.Recycle();
			}
		}
	}

	public void Fadeout()
	{
		_fadeoutTime = 0f;
	}
}
