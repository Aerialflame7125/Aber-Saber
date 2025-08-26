using LIV.SDK.Unity;
using UnityEngine;

public class LIVBS : MonoBehaviour
{
	private static LIVBS livbs;

	public LIV.SDK.Unity.LIV liv;

	public static void Init(Camera camera)
	{
		if (Application.isPlaying && !(livbs != null))
		{
			GameObject gameObject = new GameObject("LIV_MIXED_REALITY");
			gameObject.SetActive(value: false);
			livbs = gameObject.AddComponent<LIVBS>();
			livbs.liv = gameObject.AddComponent<LIV.SDK.Unity.LIV>();
			livbs.liv.TrackedSpaceOrigin = camera.transform.parent;
			livbs.liv.HMDCamera = camera;
			gameObject.SetActive(value: true);
		}
	}

	private void OnDestroy()
	{
		if (livbs == this)
		{
			livbs = null;
		}
	}
}
