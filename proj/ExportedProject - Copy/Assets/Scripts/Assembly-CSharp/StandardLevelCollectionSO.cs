using UnityEngine;

public class StandardLevelCollectionSO : ScriptableObject
{
	[SerializeField]
	private StandardLevelSO[] _levels;

	public StandardLevelSO[] levels
	{
		get
		{
			return _levels;
		}
		set
		{
			_levels = value;
		}
	}
}
