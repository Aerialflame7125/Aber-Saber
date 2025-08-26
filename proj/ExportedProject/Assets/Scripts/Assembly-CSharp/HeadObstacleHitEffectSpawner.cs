using UnityEngine;

public class HeadObstacleHitEffectSpawner : MonoBehaviour
{
	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	[SerializeField]
	private FlyingTextSpawner _flyingTextSpawner;

	[SerializeField]
	private float _minSpawnInterval = 0.2f;

	private float _prevSpawnTime;

	private bool _prevWasInObstacle;

	private void Update()
	{
		bool flag = _playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0;
		if (_prevWasInObstacle != flag)
		{
			_prevWasInObstacle = flag;
			float timeSinceLevelLoad = Time.timeSinceLevelLoad;
			if (flag && timeSinceLevelLoad - _prevSpawnTime > _minSpawnInterval)
			{
				_prevSpawnTime = timeSinceLevelLoad;
				Vector3 headPos = _playerHeadAndObstacleInteraction.headPos;
				headPos.y = 0.5f;
				_flyingTextSpawner.SpawnText(headPos, "Head\nin obstacle");
			}
		}
	}
}
