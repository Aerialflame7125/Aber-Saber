using UnityEngine;

public class GameBuildMode : ScriptableObject
{
	public enum Mode
	{
		Full,
		Demo,
		Arcade
	}

	[SerializeField]
	private Mode _mode;

	public Mode mode => _mode;

	private void Awake()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public void ForceSetMode(Mode mode)
	{
		_mode = mode;
	}
}
