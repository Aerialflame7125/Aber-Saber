using System.Collections;
using UnityEngine;

public class FadeInOnStart : MonoBehaviour
{
	[SerializeField]
	private Ease01 _ease01;

	private IEnumerator Start()
	{
		_ease01.FadeOutInstant();
		yield return new WaitForEndOfFrame();
		_ease01.FadeIn();
	}
}
