using UnityEngine;

public class FrameCounter : MonoBehaviour
{
	private int _numberOfFrames;

	private void Update()
	{
		_numberOfFrames++;
	}

	private void OnDestroy()
	{
		Debug.Log("NUMBER OF FRAMES: " + _numberOfFrames);
	}
}
