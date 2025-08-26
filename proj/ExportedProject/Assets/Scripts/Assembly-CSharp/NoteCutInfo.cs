using UnityEngine;

public class NoteCutInfo
{
	public bool speedOK { get; private set; }

	public bool directionOK { get; private set; }

	public bool saberTypeOK { get; private set; }

	public bool wasCutTooSoon { get; private set; }

	public float saberSpeed { get; private set; }

	public Vector3 saberDir { get; private set; }

	public Saber.SaberType saberType { get; private set; }

	public float swingRating { get; private set; }

	public float timeDeviation { get; private set; }

	public float cutDirDeviation { get; private set; }

	public Vector3 cutPoint { get; private set; }

	public Vector3 cutNormal { get; private set; }

	public float cutDistanceToCenter { get; private set; }

	public SaberAfterCutSwingRatingCounter afterCutSwingRatingCounter { get; private set; }

	public bool allIsOK
	{
		get
		{
			return speedOK && directionOK && saberTypeOK && !wasCutTooSoon;
		}
	}

	public bool allExceptSaberTypeIsOK
	{
		get
		{
			return speedOK && directionOK && !wasCutTooSoon;
		}
	}

	public string FailText
	{
		get
		{
			if (wasCutTooSoon)
			{
				return "TOO SOON!";
			}
			if (!saberTypeOK)
			{
				return "WRONG\nCOLOR!";
			}
			if (!speedOK)
			{
				return "CUT\nHARDER!";
			}
			if (!directionOK)
			{
				return "WRONG\nDIRECTION!";
			}
			return string.Empty;
		}
	}

	public NoteCutInfo(bool speedOK, bool directionOK, bool saberTypeOK, bool wasCutTooSoon, float saberSpeed, Vector3 saberDir, Saber.SaberType saberType, float swingRating, float timeDeviation, float cutDirDeviation, Vector3 cutCenter, Vector3 cutNormal, SaberAfterCutSwingRatingCounter afterCutSwingRatingCounter, float cutDistanceToCenter)
	{
		this.speedOK = speedOK;
		this.directionOK = directionOK;
		this.saberTypeOK = saberTypeOK;
		this.wasCutTooSoon = wasCutTooSoon;
		this.saberSpeed = saberSpeed;
		this.saberDir = saberDir;
		this.saberType = saberType;
		this.swingRating = swingRating;
		cutPoint = cutCenter;
		this.cutNormal = cutNormal;
		this.timeDeviation = timeDeviation;
		this.cutDirDeviation = cutDirDeviation;
		this.afterCutSwingRatingCounter = afterCutSwingRatingCounter;
		this.cutDistanceToCenter = cutDistanceToCenter;
	}
}
