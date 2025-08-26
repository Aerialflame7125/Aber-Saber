using UnityEngine;

public class PersistentScriptableObject : ScriptableObject
{
	protected void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}
}
