using System.Collections;
using UnityEngine;

public class EnableAfterDelay : MonoBehaviour
{
	[SerializeField]
	private MonoBehaviour _component;

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		_component.enabled = true;
	}
}
