using UnityEngine;

public class ResetLocalLeaderboardOnKey : MonoBehaviour
{
	[SerializeField]
	private KeyCode _keyCode = KeyCode.F9;

	private void Update()
	{
		if (Input.GetKeyDown(_keyCode))
		{
			PersistentSingleton<LocalLeaderboardsModel>.instance.ClearAllLeaderboards(true);
		}
	}
}
