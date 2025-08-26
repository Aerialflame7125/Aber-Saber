using UnityEngine;

[CreateAssetMenu(fileName = "MissionsData", menuName = "BS/Data/MissionsData")]
public class MissionsData : ScriptableObject
{
	[SerializeField]
	private MissionData[] _missionsData;

	public MissionData[] missionsData => _missionsData;
}
