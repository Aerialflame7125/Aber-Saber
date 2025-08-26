using System;
using UnityEngine;

[Serializable]
public class PlayerMissionStatsData
{
	[SerializeField]
	private string _missionId;

	[SerializeField]
	private bool _cleared;

	public string missionId => _missionId;

	public bool cleared
	{
		get
		{
			return _cleared;
		}
		set
		{
			_cleared = value;
		}
	}

	public PlayerMissionStatsData(string missionId, bool cleared)
	{
		_missionId = missionId;
		_cleared = cleared;
	}
}
