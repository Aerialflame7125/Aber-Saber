using UnityEngine;

public class ObstacleDissolve : MonoBehaviour
{
	[SerializeField]
	private ObstacleController _obstacleController;

	[SerializeField]
	private CutoutAnimateEffect _cutoutAnimateEffect;

	private void Awake()
	{
		_obstacleController.didStartDissolvingEvent += HandleObcstacleDidStartDissolvingEvent;
		_obstacleController.didInitEvent += HandleObstacleDidInitEvent;
	}

	private void OnDestroy()
	{
		if ((bool)_obstacleController)
		{
			_obstacleController.didStartDissolvingEvent -= HandleObcstacleDidStartDissolvingEvent;
			_obstacleController.didInitEvent -= HandleObstacleDidInitEvent;
		}
	}

	private void HandleObstacleDidInitEvent(ObstacleController obstacleController)
	{
		_cutoutAnimateEffect.ResetEffect();
	}

	private void HandleObcstacleDidStartDissolvingEvent(ObstacleController obstacleController, float duration)
	{
		_cutoutAnimateEffect.AnimateCutout(0f, 1f, duration);
	}
}
