using UnityEngine;

[ExecuteInEditMode]
public class SteamVR_Stats : MonoBehaviour
{
	private void Awake()
	{
		Debug.Log("SteamVR_Stats is deprecated in Unity 2017.2 - REMOVING");
		Object.DestroyImmediate(this);
	}
}
