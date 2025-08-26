using UnityEngine;

public class SmokeEnabler : MonoBehaviour
{
	[SerializeField]
	private SmokeParams _smokeParams;

	private void Start()
	{
		base.gameObject.SetActive(_smokeParams.smokeEnabled);
	}
}
