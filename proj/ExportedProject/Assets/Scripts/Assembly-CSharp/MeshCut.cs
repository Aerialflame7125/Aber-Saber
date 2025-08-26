using System.Collections.Generic;
using UnityEngine;

public class MeshCut
{
	private static Plane _blade;

	private static Mesh _victimMesh;

	private static bool[] _sides = new bool[3];

	private static List<int>[] _leftGatherSubIndices = new List<int>[2]
	{
		new List<int>(),
		new List<int>()
	};

	private static List<int>[] _rightGatherSubIndices = new List<int>[2]
	{
		new List<int>(),
		new List<int>()
	};

	private static List<Vector3>[] _leftGatherAddedPoints = new List<Vector3>[2]
	{
		new List<Vector3>(),
		new List<Vector3>()
	};

	private static List<Vector2>[] _leftGatherAddedUVs = new List<Vector2>[2]
	{
		new List<Vector2>(),
		new List<Vector2>()
	};

	private static List<Vector3>[] _leftGatherAddedNormals = new List<Vector3>[2]
	{
		new List<Vector3>(),
		new List<Vector3>()
	};

	private static List<Vector3>[] _rightGatherAddedPoints = new List<Vector3>[2]
	{
		new List<Vector3>(),
		new List<Vector3>()
	};

	private static List<Vector2>[] _rightGatherAddedUVs = new List<Vector2>[2]
	{
		new List<Vector2>(),
		new List<Vector2>()
	};

	private static List<Vector3>[] _rightGatherAddedNormals = new List<Vector3>[2]
	{
		new List<Vector3>(),
		new List<Vector3>()
	};

	private static Vector3 _leftPoint1 = Vector3.zero;

	private static Vector3 _leftPoint2 = Vector3.zero;

	private static Vector3 _rightPoint1 = Vector3.zero;

	private static Vector3 _rightPoint2 = Vector3.zero;

	private static Vector2 _leftUV1 = Vector3.zero;

	private static Vector2 _leftUV2 = Vector3.zero;

	private static Vector2 _rightUV1 = Vector3.zero;

	private static Vector2 _rightUV2 = Vector3.zero;

	private static Vector3 _leftNormal1 = Vector3.zero;

	private static Vector3 _leftNormal2 = Vector3.zero;

	private static Vector3 _rightNormal1 = Vector3.zero;

	private static Vector3 _rightNormal2 = Vector3.zero;

	private static List<int>[] _leftFinalSubIndices = new List<int>[2]
	{
		new List<int>(),
		new List<int>()
	};

	private static List<Vector3> _leftFinalVertices = new List<Vector3>();

	private static List<Vector3> _leftFinalNormals = new List<Vector3>();

	private static List<Vector2> _leftFinalUVs = new List<Vector2>();

	private static List<int>[] _rightFinalSubIndices = new List<int>[2]
	{
		new List<int>(),
		new List<int>()
	};

	private static List<Vector3> _rightFinalVertices = new List<Vector3>();

	private static List<Vector3> _rightFinalNormals = new List<Vector3>();

	private static List<Vector2> _rightFinalUVs = new List<Vector2>();

	private static List<Vector3> _createdVertexPoints = new List<Vector3>();

	private static List<Vector3> capVertTracker = new List<Vector3>();

	private static List<Vector3> capVertpolygon = new List<Vector3>();

	public static void Cut(Mesh mesh, Mesh leftHalfMesh, Mesh rightHalfMesh, Transform meshTransform, Vector3 anchorPoint, Vector3 normalDirection)
	{
		_blade = new Plane(meshTransform.InverseTransformDirection(-normalDirection), meshTransform.InverseTransformPoint(anchorPoint));
		_victimMesh = mesh;
		_victimMesh.subMeshCount = 2;
		ResetGatheringValues();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		_sides = new bool[3];
		int num4 = 0;
		int[] triangles = _victimMesh.triangles;
		int[] indices = _victimMesh.GetIndices(1);
		for (int i = 0; i < triangles.Length; i += 3)
		{
			num = triangles[i];
			num2 = triangles[i + 1];
			num3 = triangles[i + 2];
			_sides[0] = _blade.GetSide(_victimMesh.vertices[num]);
			_sides[1] = _blade.GetSide(_victimMesh.vertices[num2]);
			_sides[2] = _blade.GetSide(_victimMesh.vertices[num3]);
			num4 = 0;
			for (int j = 0; j < indices.Length; j++)
			{
				if (indices[j] == num)
				{
					num4 = 1;
					break;
				}
			}
			if (_sides[0] == _sides[1] && _sides[0] == _sides[2])
			{
				if (_sides[0])
				{
					_leftGatherSubIndices[num4].Add(num);
					_leftGatherSubIndices[num4].Add(num2);
					_leftGatherSubIndices[num4].Add(num3);
				}
				else
				{
					_rightGatherSubIndices[num4].Add(num);
					_rightGatherSubIndices[num4].Add(num2);
					_rightGatherSubIndices[num4].Add(num3);
				}
			}
			else
			{
				ResetFaceCuttingTemps();
				CutThisFace(num4, num, num2, num3);
			}
		}
		ResetFinalArrays();
		SetFinalArraysWithOriginals();
		AddNewTrianglesToFinalArrays();
		MakeCaps();
		leftHalfMesh.Clear();
		leftHalfMesh.vertices = _leftFinalVertices.ToArray();
		leftHalfMesh.subMeshCount = 2;
		leftHalfMesh.SetIndices(_leftFinalSubIndices[0].ToArray(), MeshTopology.Triangles, 0);
		leftHalfMesh.SetIndices(_leftFinalSubIndices[1].ToArray(), MeshTopology.Triangles, 1);
		leftHalfMesh.normals = _leftFinalNormals.ToArray();
		leftHalfMesh.uv = _leftFinalUVs.ToArray();
		rightHalfMesh.Clear();
		rightHalfMesh.vertices = _rightFinalVertices.ToArray();
		rightHalfMesh.subMeshCount = 2;
		rightHalfMesh.SetIndices(_rightFinalSubIndices[0].ToArray(), MeshTopology.Triangles, 0);
		rightHalfMesh.SetIndices(_rightFinalSubIndices[1].ToArray(), MeshTopology.Triangles, 1);
		rightHalfMesh.normals = _rightFinalNormals.ToArray();
		rightHalfMesh.uv = _rightFinalUVs.ToArray();
	}

	private static void ResetGatheringValues()
	{
		_leftGatherSubIndices[0].Clear();
		_leftGatherSubIndices[1].Clear();
		_leftGatherAddedPoints[0].Clear();
		_leftGatherAddedPoints[1].Clear();
		_leftGatherAddedUVs[0].Clear();
		_leftGatherAddedUVs[1].Clear();
		_leftGatherAddedNormals[0].Clear();
		_leftGatherAddedNormals[1].Clear();
		_rightGatherSubIndices[0].Clear();
		_rightGatherSubIndices[1].Clear();
		_rightGatherAddedPoints[0].Clear();
		_rightGatherAddedPoints[1].Clear();
		_rightGatherAddedUVs[0].Clear();
		_rightGatherAddedUVs[1].Clear();
		_rightGatherAddedNormals[0].Clear();
		_rightGatherAddedNormals[1].Clear();
		_createdVertexPoints.Clear();
	}

	private static void ResetFaceCuttingTemps()
	{
		_leftPoint1 = Vector3.zero;
		_leftPoint2 = Vector3.zero;
		_rightPoint1 = Vector3.zero;
		_rightPoint2 = Vector3.zero;
		_leftUV1 = Vector3.zero;
		_leftUV2 = Vector3.zero;
		_rightUV1 = Vector3.zero;
		_rightUV2 = Vector3.zero;
		_leftNormal1 = Vector3.zero;
		_leftNormal2 = Vector3.zero;
		_rightNormal1 = Vector3.zero;
		_rightNormal2 = Vector3.zero;
	}

	private static void CutThisFace(int submesh, int index1, int index2, int index3)
	{
		int num = index1;
		for (int i = 0; i < 3; i++)
		{
			switch (i)
			{
			case 0:
				num = index1;
				break;
			case 1:
				num = index2;
				break;
			case 2:
				num = index3;
				break;
			}
			if (_sides[i])
			{
				if (_leftPoint1 == Vector3.zero)
				{
					_leftPoint1 = _victimMesh.vertices[num];
					_leftPoint2 = _leftPoint1;
					_leftUV1 = _victimMesh.uv[num];
					_leftUV2 = _leftUV1;
					_leftNormal1 = _victimMesh.normals[num];
					_leftNormal2 = _leftNormal1;
				}
				else
				{
					_leftPoint2 = _victimMesh.vertices[num];
					_leftUV2 = _victimMesh.uv[num];
					_leftNormal2 = _victimMesh.normals[num];
				}
			}
			else if (_rightPoint1 == Vector3.zero)
			{
				_rightPoint1 = _victimMesh.vertices[num];
				_rightPoint2 = _rightPoint1;
				_rightUV1 = _victimMesh.uv[num];
				_rightUV2 = _rightUV1;
				_rightNormal1 = _victimMesh.normals[num];
				_rightNormal2 = _rightNormal1;
			}
			else
			{
				_rightPoint2 = _victimMesh.vertices[num];
				_rightUV2 = _victimMesh.uv[num];
				_rightNormal2 = _victimMesh.normals[num];
			}
		}
		float num2 = 0f;
		float enter = 0f;
		_blade.Raycast(new Ray(_leftPoint1, (_rightPoint1 - _leftPoint1).normalized), out enter);
		num2 = enter / (_rightPoint1 - _leftPoint1).magnitude;
		Vector3 vector = Vector3.Lerp(_leftPoint1, _rightPoint1, num2);
		Vector2 vector2 = Vector2.Lerp(_leftUV1, _rightUV1, num2);
		Vector3 vector3 = Vector3.Lerp(_leftNormal1, _rightNormal1, num2);
		_createdVertexPoints.Add(vector);
		_blade.Raycast(new Ray(_leftPoint2, (_rightPoint2 - _leftPoint2).normalized), out enter);
		num2 = enter / (_rightPoint2 - _leftPoint2).magnitude;
		Vector3 vector4 = Vector3.Lerp(_leftPoint2, _rightPoint2, num2);
		Vector2 vector5 = Vector2.Lerp(_leftUV2, _rightUV2, num2);
		Vector3 vector6 = Vector3.Lerp(_leftNormal2, _rightNormal2, num2);
		_createdVertexPoints.Add(vector4);
		AddLeftTriangle(submesh, vector3, new Vector3[3] { _leftPoint1, vector, vector4 }, new Vector2[3] { _leftUV1, vector2, vector5 }, new Vector3[3] { _leftNormal1, vector3, vector6 });
		AddLeftTriangle(submesh, vector6, new Vector3[3] { _leftPoint1, _leftPoint2, vector4 }, new Vector2[3] { _leftUV1, _leftUV2, vector5 }, new Vector3[3] { _leftNormal1, _leftNormal2, vector6 });
		AddRightTriangle(submesh, vector3, new Vector3[3] { _rightPoint1, vector, vector4 }, new Vector2[3] { _rightUV1, vector2, vector5 }, new Vector3[3] { _rightNormal1, vector3, vector6 });
		AddRightTriangle(submesh, vector6, new Vector3[3] { _rightPoint1, _rightPoint2, vector4 }, new Vector2[3] { _rightUV1, _rightUV2, vector5 }, new Vector3[3] { _rightNormal1, _rightNormal2, vector6 });
	}

	private static void AddLeftTriangle(int submesh, Vector3 faceNormal, Vector3[] points, Vector2[] uvs, Vector3[] normals)
	{
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		Vector3 lhs = Vector3.Cross((points[1] - points[0]).normalized, (points[2] - points[0]).normalized);
		if (Vector3.Dot(lhs, faceNormal) < 0f)
		{
			num = 2;
			num2 = 1;
			num3 = 0;
		}
		_leftGatherAddedPoints[submesh].Add(points[num]);
		_leftGatherAddedPoints[submesh].Add(points[num2]);
		_leftGatherAddedPoints[submesh].Add(points[num3]);
		_leftGatherAddedUVs[submesh].Add(uvs[num]);
		_leftGatherAddedUVs[submesh].Add(uvs[num2]);
		_leftGatherAddedUVs[submesh].Add(uvs[num3]);
		_leftGatherAddedNormals[submesh].Add(normals[num]);
		_leftGatherAddedNormals[submesh].Add(normals[num2]);
		_leftGatherAddedNormals[submesh].Add(normals[num3]);
	}

	private static void AddRightTriangle(int submesh, Vector3 faceNormal, Vector3[] points, Vector2[] uvs, Vector3[] normals)
	{
		int num = 0;
		int num2 = 1;
		int num3 = 2;
		Vector3 lhs = Vector3.Cross((points[1] - points[0]).normalized, (points[2] - points[0]).normalized);
		if (Vector3.Dot(lhs, faceNormal) < 0f)
		{
			num = 2;
			num2 = 1;
			num3 = 0;
		}
		_rightGatherAddedPoints[submesh].Add(points[num]);
		_rightGatherAddedPoints[submesh].Add(points[num2]);
		_rightGatherAddedPoints[submesh].Add(points[num3]);
		_rightGatherAddedUVs[submesh].Add(uvs[num]);
		_rightGatherAddedUVs[submesh].Add(uvs[num2]);
		_rightGatherAddedUVs[submesh].Add(uvs[num3]);
		_rightGatherAddedNormals[submesh].Add(normals[num]);
		_rightGatherAddedNormals[submesh].Add(normals[num2]);
		_rightGatherAddedNormals[submesh].Add(normals[num3]);
	}

	private static void ResetFinalArrays()
	{
		_leftFinalSubIndices[0].Clear();
		_leftFinalSubIndices[1].Clear();
		_leftFinalVertices.Clear();
		_leftFinalNormals.Clear();
		_leftFinalUVs.Clear();
		_rightFinalSubIndices[0].Clear();
		_rightFinalSubIndices[1].Clear();
		_rightFinalVertices.Clear();
		_rightFinalNormals.Clear();
		_rightFinalUVs.Clear();
	}

	private static void SetFinalArraysWithOriginals()
	{
		int num = 0;
		for (int i = 0; i < 2; i++)
		{
			for (int j = 0; j < _leftGatherSubIndices[i].Count; j++)
			{
				num = _leftGatherSubIndices[i][j];
				_leftFinalVertices.Add(_victimMesh.vertices[num]);
				_leftFinalSubIndices[i].Add(_leftFinalVertices.Count - 1);
				_leftFinalNormals.Add(_victimMesh.normals[num]);
				_leftFinalUVs.Add(_victimMesh.uv[num]);
			}
			for (int k = 0; k < _rightGatherSubIndices[i].Count; k++)
			{
				num = _rightGatherSubIndices[i][k];
				_rightFinalVertices.Add(_victimMesh.vertices[num]);
				_rightFinalSubIndices[i].Add(_rightFinalVertices.Count - 1);
				_rightFinalNormals.Add(_victimMesh.normals[num]);
				_rightFinalUVs.Add(_victimMesh.uv[num]);
			}
		}
	}

	private static void AddNewTrianglesToFinalArrays()
	{
		for (int i = 0; i < 2; i++)
		{
			int count = _leftFinalVertices.Count;
			for (int j = 0; j < _leftGatherAddedPoints[i].Count; j++)
			{
				_leftFinalVertices.Add(_leftGatherAddedPoints[i][j]);
				_leftFinalSubIndices[i].Add(j + count);
				_leftFinalUVs.Add(_leftGatherAddedUVs[i][j]);
				_leftFinalNormals.Add(_leftGatherAddedNormals[i][j]);
			}
			count = _rightFinalVertices.Count;
			for (int k = 0; k < _rightGatherAddedPoints[i].Count; k++)
			{
				_rightFinalVertices.Add(_rightGatherAddedPoints[i][k]);
				_rightFinalSubIndices[i].Add(k + count);
				_rightFinalUVs.Add(_rightGatherAddedUVs[i][k]);
				_rightFinalNormals.Add(_rightGatherAddedNormals[i][k]);
			}
		}
	}

	private static void MakeCaps()
	{
		capVertTracker.Clear();
		for (int i = 0; i < _createdVertexPoints.Count; i++)
		{
			if (capVertTracker.Contains(_createdVertexPoints[i]))
			{
				continue;
			}
			capVertpolygon.Clear();
			capVertpolygon.Add(_createdVertexPoints[i]);
			capVertpolygon.Add(_createdVertexPoints[i + 1]);
			capVertTracker.Add(_createdVertexPoints[i]);
			capVertTracker.Add(_createdVertexPoints[i + 1]);
			bool flag = false;
			while (!flag)
			{
				flag = true;
				for (int j = 0; j < _createdVertexPoints.Count; j += 2)
				{
					if (_createdVertexPoints[j] == capVertpolygon[capVertpolygon.Count - 1] && !capVertTracker.Contains(_createdVertexPoints[j + 1]))
					{
						flag = false;
						capVertpolygon.Add(_createdVertexPoints[j + 1]);
						capVertTracker.Add(_createdVertexPoints[j + 1]);
					}
					else if (_createdVertexPoints[j + 1] == capVertpolygon[capVertpolygon.Count - 1] && !capVertTracker.Contains(_createdVertexPoints[j]))
					{
						flag = false;
						capVertpolygon.Add(_createdVertexPoints[j]);
						capVertTracker.Add(_createdVertexPoints[j]);
					}
				}
			}
			FillCap(capVertpolygon);
		}
	}

	private static void FillCap(List<Vector3> vertices)
	{
		List<int> list = new List<int>();
		List<Vector2> list2 = new List<Vector2>();
		List<Vector3> list3 = new List<Vector3>();
		Vector3 zero = Vector3.zero;
		foreach (Vector3 vertex in vertices)
		{
			zero += vertex;
		}
		zero /= (float)vertices.Count;
		Vector3 zero2 = Vector3.zero;
		zero2.x = _blade.normal.y;
		zero2.y = 0f - _blade.normal.x;
		zero2.z = _blade.normal.z;
		Vector3 rhs = Vector3.Cross(_blade.normal, zero2);
		Vector3 zero3 = Vector3.zero;
		Vector3 zero4 = Vector3.zero;
		for (int i = 0; i < vertices.Count; i++)
		{
			zero3 = vertices[i] - zero;
			zero4 = Vector3.zero;
			zero4.x = 0.5f + Vector3.Dot(zero3, rhs);
			zero4.y = 0.5f + Vector3.Dot(zero3, zero2);
			zero4.z = 0.5f + Vector3.Dot(zero3, _blade.normal);
			list2.Add(new Vector2(zero4.x, zero4.y));
			list3.Add(_blade.normal);
		}
		vertices.Add(zero);
		list3.Add(_blade.normal);
		list2.Add(new Vector2(0.5f, 0.5f));
		Vector3 zero5 = Vector3.zero;
		int num = 0;
		for (int j = 0; j < vertices.Count; j++)
		{
			num = (j + 1) % (vertices.Count - 1);
			zero5 = Vector3.Cross((vertices[num] - vertices[j]).normalized, (vertices[vertices.Count - 1] - vertices[j]).normalized);
			if (Vector3.Dot(zero5, _blade.normal) < 0f)
			{
				list.Add(vertices.Count - 1);
				list.Add(num);
				list.Add(j);
			}
			else
			{
				list.Add(j);
				list.Add(num);
				list.Add(vertices.Count - 1);
			}
		}
		int num2 = 0;
		for (int k = 0; k < list.Count; k++)
		{
			num2 = list[k];
			_rightFinalVertices.Add(vertices[num2]);
			_rightFinalSubIndices[1].Add(_rightFinalVertices.Count - 1);
			_rightFinalNormals.Add(list3[num2]);
			_rightFinalUVs.Add(list2[num2]);
		}
		for (int l = 0; l < list3.Count; l++)
		{
			list3[l] = -list3[l];
		}
		for (int m = 0; m < list.Count; m += 3)
		{
			int value = list[m + 2];
			int value2 = list[m];
			list[m] = value;
			list[m + 2] = value2;
		}
		for (int n = 0; n < list.Count; n++)
		{
			num2 = list[n];
			_leftFinalVertices.Add(vertices[num2]);
			_leftFinalSubIndices[1].Add(_leftFinalVertices.Count - 1);
			_leftFinalNormals.Add(list3[num2]);
			_leftFinalUVs.Add(list2[num2]);
		}
	}
}
