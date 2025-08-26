using System.Collections.Generic;
using UnityEngine;

public class PlayerHeadAndObstacleInteraction : ScriptableObject
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private ActiveObstaclesManager _activeObstaclesManager;

	private int _lastFrameNumCheck = -1;

	private List<ObstacleController> _intersectingObstacles = new List<ObstacleController>(10);

	public Vector3 headPos
	{
		get
		{
			return _playerControllerProvider.GetProvidedObject<PlayerController>().headPos;
		}
	}

	public List<ObstacleController> intersectingObstacles
	{
		get
		{
			int frameCount = Time.frameCount;
			if (_lastFrameNumCheck == frameCount)
			{
				return _intersectingObstacles;
			}
			_activeObstaclesManager.GetObstaclesCointainingPoint(_playerControllerProvider.GetProvidedObject<PlayerController>().headPos, _intersectingObstacles);
			_lastFrameNumCheck = frameCount;
			return _intersectingObstacles;
		}
	}
}
