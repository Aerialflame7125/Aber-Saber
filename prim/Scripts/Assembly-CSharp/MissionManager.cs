using UnityEngine;

public class MissionManager : MonoBehaviour
{
	[SerializeField]
	private MissionsData _missionsData;

	private MissionSuccessChecker _missionSuccessChecker;

	private void Awake()
	{
		_missionSuccessChecker = new MissionSuccessChecker();
	}

	public int GetNumberOfMissions()
	{
		return _missionsData.missionsData.Length;
	}

	public MissionData GetMissionData(int missionIndex)
	{
		if (missionIndex < 0 || missionIndex >= _missionsData.missionsData.Length)
		{
			return null;
		}
		return _missionsData.missionsData[missionIndex];
	}

	public MissionData GetMissionData(string missionId)
	{
		MissionData[] missionsData = _missionsData.missionsData;
		foreach (MissionData missionData in missionsData)
		{
			if (missionData.missionId == missionId)
			{
				return missionData;
			}
		}
		return null;
	}

	public MissionData GetMissionData(string missionId, out int missionIndex)
	{
		for (missionIndex = 0; missionIndex < _missionsData.missionsData.Length; missionIndex++)
		{
			MissionData missionData = _missionsData.missionsData[missionIndex];
			if (missionData.missionId == missionId)
			{
				return missionData;
			}
		}
		return null;
	}

	public bool GetMissionSuccess(string missionId, LevelCompletionResults levelCompletionResults)
	{
		MissionData missionData = GetMissionData(missionId);
		if (missionData == null)
		{
			return false;
		}
		return _missionSuccessChecker.GetMissionSuccess(missionData.formula, levelCompletionResults);
	}
}
