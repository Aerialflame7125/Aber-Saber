using System.Collections.Generic;
using UnityEngine;

public class ActiveObstaclesManager : ScriptableObject
{
	private List<ObstacleController> _activeObstacleControllers;

	public List<ObstacleController> activeObstacleControllers => _activeObstacleControllers;

	private void OnEnable()
	{
		_activeObstacleControllers = new List<ObstacleController>();
	}

	public void RegisterObstacle(ObstacleController obstacleController)
	{
		_activeObstacleControllers.Add(obstacleController);
	}

	public void UnregisterObstacle(ObstacleController obstacleController)
	{
		_activeObstacleControllers.Remove(obstacleController);
	}

	public void GetObstaclesCointainingPoint(Vector3 worldPos, List<ObstacleController> obstacleControllers)
	{
		obstacleControllers.Clear();
		foreach (ObstacleController activeObstacleController in _activeObstacleControllers)
		{
			if (!activeObstacleController.hasPassedAvoidedMark)
			{
				Vector3 point = activeObstacleController.transform.InverseTransformPoint(worldPos);
				if (activeObstacleController.bounds.Contains(point))
				{
					obstacleControllers.Add(activeObstacleController);
				}
			}
		}
	}
}
