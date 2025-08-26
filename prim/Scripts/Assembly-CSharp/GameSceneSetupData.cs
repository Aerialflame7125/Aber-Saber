using UnityEngine;

public abstract class GameSceneSetupData : ScriptableObject
{
	[SerializeField]
	private SceneInfo _sceneInfo;

	private bool _wasUsedInLastTransition;

	public SceneInfo sceneInfo => _sceneInfo;

	public bool wasUsedInLastTransition => _wasUsedInLastTransition;

	private void Awake()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public void WillBeUsedInTransition()
	{
		_wasUsedInLastTransition = true;
	}

	public void ResetWasUsedInTransition()
	{
		_wasUsedInLastTransition = false;
	}

	public void TransitionToScene(float minDuration)
	{
		_sceneInfo.gameScenesManager.TransitionToScene(this, minDuration);
	}
}
