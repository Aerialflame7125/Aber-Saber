using UnityEngine;

public class FlyingScoreSpawner : MonoBehaviour
{
	[SerializeField]
	private FlyingScoreTextEffect _flyingScoreTextEffectPrefab;

	private const int kMaxLineCount = 4;

	private const int kMaxSlotCount = 1;

	private const float kMinSlotTakenTime = 0.4f;

	private float[,] lineSlotSpawnTimes = new float[4, 1];

	private void Start()
	{
		_flyingScoreTextEffectPrefab.CreatePool(20);
	}

	public void SpawnFlyingScore(NoteCutInfo noteCutInfo, int noteLineIndex, Vector3 pos, Color color, SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter)
	{
		if (noteLineIndex >= lineSlotSpawnTimes.GetLength(0))
		{
			float[,] array = new float[noteLineIndex, 1];
			for (int i = 0; i < lineSlotSpawnTimes.GetLength(0); i++)
			{
				for (int j = 0; j < 1; j++)
				{
					array[i, j] = lineSlotSpawnTimes[i, j];
				}
			}
			lineSlotSpawnTimes = array;
		}
		int num = 0;
		for (num = 0; num < 0 && !(lineSlotSpawnTimes[noteLineIndex, num] + 0.4f < Time.timeSinceLevelLoad); num++)
		{
		}
		lineSlotSpawnTimes[noteLineIndex, num] = Time.timeSinceLevelLoad;
		FlyingScoreTextEffect flyingScoreTextEffect = _flyingScoreTextEffectPrefab.Spawn(pos, Quaternion.identity);
		pos.z = 0f;
		pos.y = -0.24f;
		Vector3 targetPos = pos + new Vector3(0f, -0.23f * (float)num, 7.55f);
		flyingScoreTextEffect.InitAndPresent(noteCutInfo, targetPos, color, saberAfterCutSwingRatingCounter);
	}
}
