using UnityEngine;

public class Snap : MonoBehaviour
{
	public Vector3 snap = new Vector3(1f, 1f, 1f);

	public Vector3 offset = new Vector3(0f, 0f, 0f);

	public void SnapPosition()
	{
		if (!Application.isPlaying && snap.x != 0f && snap.y != 0f && snap.z != 0f)
		{
			Vector3 position = base.transform.position;
			position.x = Mathf.Round(position.x / snap.x) * snap.x;
			position.y = Mathf.Round(position.y / snap.y) * snap.y;
			position.z = Mathf.Round(position.z / snap.z) * snap.z;
			base.transform.position = position + offset;
		}
	}
}
