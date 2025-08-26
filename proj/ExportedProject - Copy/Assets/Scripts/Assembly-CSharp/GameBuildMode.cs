using UnityEngine;

public class GameBuildMode : ScriptableObject
{
	public enum Mode
	{
		Full = 0,
		Demo = 1,
		Arcade = 2
	}

	[SerializeField]
	private Mode _mode;

	public Mode mode
	{
		get
		{
			return _mode;
		}
	}

	private void Awake()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public void ForceSetMode(Mode mode)
	{
		_mode = mode;
	}
}
