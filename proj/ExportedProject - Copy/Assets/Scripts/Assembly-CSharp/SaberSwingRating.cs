using UnityEngine;

public class SaberSwingRating
{
	public const float kMaxNormalAngleDiff = 90f;

	public const float kToleranceNormalAngleDiff = 75f;

	public const float kMaxBeforeCutSwingDuration = 0.4f;

	public const float kMaxAfterCutSwingDuration = 0.4f;

	private const float kBeforeCutAngleFor1Rating = 90f;

	private const float kAfterCutAngleFor1Rating = 60f;

	private static float NormalRating(float normalDiff)
	{
		return 1f - Mathf.Clamp((normalDiff - 75f) / 15f, 0f, 1f);
	}

	public static float BeforeCutStepRating(float angleDiff, float normalDiff)
	{
		return angleDiff * NormalRating(normalDiff) / 90f;
	}

	public static float AfterCutStepRating(float angleDiff, float normalDiff)
	{
		return angleDiff * NormalRating(normalDiff) / 60f;
	}
}
