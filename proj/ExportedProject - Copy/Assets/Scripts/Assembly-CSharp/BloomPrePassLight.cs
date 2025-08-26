using System.Collections.Generic;
using UnityEngine;

public abstract class BloomPrePassLight : MonoBehaviour
{
	[SerializeField]
	private int _ID = -1;

	private static List<BloomPrePassLight> _lightList = new List<BloomPrePassLight>();

	public int ID
	{
		get
		{
			return _ID;
		}
	}

	public abstract Color color { get; set; }

	public static List<BloomPrePassLight> lightList
	{
		get
		{
			return _lightList;
		}
	}

	protected virtual void OnEnable()
	{
		_lightList.Add(this);
	}

	protected virtual void OnDisable()
	{
		_lightList.Remove(this);
	}

	public abstract void FillMeshData(int lightNum, Vector3[] vertices, Color32[] colors32, Vector2[] uv2, Vector2[] uv3, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, float lineWidth);
}
