using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class StretchableCube : MonoBehaviour
{
	private Vector2[] _uvs;

	private Mesh _mesh;

	private MeshRenderer _meshRenderer;

	private void Awake()
	{
		_meshRenderer = GetComponent<MeshRenderer>();
		MeshFilter component = GetComponent<MeshFilter>();
		_mesh = CreateBox();
		component.mesh = _mesh;
	}

	private Mesh CreateBox()
	{
		Mesh mesh = new Mesh();
		float num = 1f;
		float num2 = 1f;
		float num3 = 1f;
		Vector3 vector = new Vector3((0f - num) * 0.5f, (0f - num2) * 0.5f, num3 * 0.5f);
		Vector3 vector2 = new Vector3(num * 0.5f, (0f - num2) * 0.5f, num3 * 0.5f);
		Vector3 vector3 = new Vector3(num * 0.5f, (0f - num2) * 0.5f, (0f - num3) * 0.5f);
		Vector3 vector4 = new Vector3((0f - num) * 0.5f, (0f - num2) * 0.5f, (0f - num3) * 0.5f);
		Vector3 vector5 = new Vector3((0f - num) * 0.5f, num2 * 0.5f, num3 * 0.5f);
		Vector3 vector6 = new Vector3(num * 0.5f, num2 * 0.5f, num3 * 0.5f);
		Vector3 vector7 = new Vector3(num * 0.5f, num2 * 0.5f, (0f - num3) * 0.5f);
		Vector3 vector8 = new Vector3((0f - num) * 0.5f, num2 * 0.5f, (0f - num3) * 0.5f);
		Vector3[] vertices = new Vector3[24]
		{
			vector, vector2, vector3, vector4, vector8, vector5, vector, vector4, vector5, vector6,
			vector2, vector, vector7, vector8, vector4, vector3, vector6, vector7, vector3, vector2,
			vector8, vector7, vector6, vector5
		};
		Vector3 up = Vector3.up;
		Vector3 down = Vector3.down;
		Vector3 forward = Vector3.forward;
		Vector3 back = Vector3.back;
		Vector3 left = Vector3.left;
		Vector3 right = Vector3.right;
		Vector3[] normals = new Vector3[24]
		{
			down, down, down, down, left, left, left, left, forward, forward,
			forward, forward, back, back, back, back, right, right, right, right,
			up, up, up, up
		};
		_uvs = new Vector2[24];
		RecalculateUVs(_uvs);
		int[] triangles = new int[36]
		{
			3, 1, 0, 3, 2, 1, 7, 5, 4, 7,
			6, 5, 11, 9, 8, 11, 10, 9, 15, 13,
			12, 15, 14, 13, 19, 17, 16, 19, 18, 17,
			23, 21, 20, 23, 22, 21
		};
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = _uvs;
		mesh.triangles = triangles;
		mesh.RecalculateBounds();
		return mesh;
	}

	private void RecalculateUVs(Vector2[] uvs)
	{
		Vector3 lossyScale = base.transform.lossyScale;
		Vector2[] array = new Vector2[6]
		{
			new Vector2(lossyScale.x, lossyScale.z),
			new Vector2(lossyScale.z, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.y),
			new Vector2(lossyScale.z, lossyScale.y),
			new Vector2(lossyScale.x, lossyScale.z)
		};
		for (int i = 0; i < array.Length; i++)
		{
			uvs[i * 4] = array[i];
			uvs[i * 4 + 1] = new Vector2(0f, array[i].y);
			uvs[i * 4 + 2] = Vector2.zero;
			uvs[i * 4 + 3] = new Vector2(array[i].x, 0f);
		}
	}

	public void RefreshUVs()
	{
		RecalculateUVs(_uvs);
		_mesh.uv = _uvs;
	}

	public Bounds GetBounds()
	{
		return _meshRenderer.bounds;
	}
}
