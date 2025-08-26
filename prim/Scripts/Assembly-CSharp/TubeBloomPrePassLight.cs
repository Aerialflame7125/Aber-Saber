using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class TubeBloomPrePassLight : BloomPrePassLight
{
	[SerializeField]
	private float _width = 0.5f;

	[SerializeField]
	private float _length = 1f;

	[SerializeField]
	[Range(0f, 1f)]
	private float _center = 0.5f;

	[SerializeField]
	private Color _color;

	private int _colorID;

	private int _sizeParamsID;

	private MeshRenderer _meshRenderer;

	private MeshFilter _meshFilter;

	private MaterialPropertyBlock _materialPropertyBlock;

	private bool _isInitialized;

	private Transform _transform;

	private static Mesh _mesh;

	private const float kMaxWidth = 10f;

	private const float kMaxLength = 1000f;

	public override Color color
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
			UpdateMaterialPropertyBlock();
		}
	}

	private void Awake()
	{
		Init();
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (_mesh == null)
		{
			_mesh = _meshFilter.sharedMesh;
			if (_mesh == null)
			{
				_mesh = CreateMesh();
			}
		}
		_meshFilter.sharedMesh = _mesh;
		UpdateMaterialPropertyBlock();
	}

	private void Init()
	{
		if (!_isInitialized)
		{
			_isInitialized = true;
			_meshFilter = GetComponent<MeshFilter>();
			_meshRenderer = GetComponent<MeshRenderer>();
			_colorID = Shader.PropertyToID("_Color");
			_sizeParamsID = Shader.PropertyToID("_SizeParams");
			_transform = base.transform;
		}
	}

	private void UpdateMaterialPropertyBlock()
	{
		if (_materialPropertyBlock == null)
		{
			_materialPropertyBlock = new MaterialPropertyBlock();
		}
		_materialPropertyBlock.SetColor(_colorID, _color);
		_materialPropertyBlock.SetVector(_sizeParamsID, new Vector3(_width, _length, _center));
		_meshRenderer.SetPropertyBlock(_materialPropertyBlock);
	}

	private Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.name = "Tube";
		Vector3[] vertices = new Vector3[8]
		{
			new Vector3(-1f, 0f, -1f),
			new Vector3(1f, 0f, -1f),
			new Vector3(1f, 1f, -1f),
			new Vector3(-1f, 1f, -1f),
			new Vector3(-1f, 1f, 1f),
			new Vector3(1f, 1f, 1f),
			new Vector3(1f, 0f, 1f),
			new Vector3(-1f, 0f, 1f)
		};
		int[] triangles = new int[36]
		{
			0, 2, 1, 0, 3, 2, 2, 3, 4, 2,
			4, 5, 1, 2, 5, 1, 5, 6, 0, 7,
			4, 0, 4, 3, 5, 4, 7, 5, 7, 6,
			0, 6, 7, 0, 1, 6
		};
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.bounds = new Bounds(new Vector3(0f, 0f, 0f), new Vector3(10f, 2000f, 10f));
		return mesh;
	}

	public override void FillMeshData(int lightNum, Vector3[] vertices, Color32[] colors32, Vector2[] uv2, Vector2[] uv3, Matrix4x4 viewMatrix, Matrix4x4 projectionMatrix, float lineWidth)
	{
		float y = (0f - _length) * _center;
		float y2 = _length * (1f - _center);
		Matrix4x4 localToWorldMatrix = _transform.localToWorldMatrix;
		Vector3 point = localToWorldMatrix.MultiplyPoint3x4(new Vector4(0f, y, 0f));
		Vector3 point2 = localToWorldMatrix.MultiplyPoint3x4(new Vector4(0f, y2, 0f));
		Vector3 vector = viewMatrix.MultiplyPoint3x4(point);
		Vector3 vector2 = viewMatrix.MultiplyPoint3x4(point2);
		Vector4 vector3 = projectionMatrix * new Vector4(vector.x, vector.y, vector.z, 1f);
		Vector4 vector4 = projectionMatrix * new Vector4(vector2.x, vector2.y, vector2.z, 1f);
		bool flag = vector3.x >= 0f - vector3.w;
		bool flag2 = vector4.x >= 0f - vector4.w;
		if (!flag && !flag2)
		{
			for (int i = 0; i < 4; i++)
			{
				ref Vector3 reference = ref vertices[lightNum * 4 + i];
				reference = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num = (0f - vector3.w - vector3.x) / (vector4.x - vector3.x + vector4.w - vector3.w);
			Vector4 vector5 = vector3 + (vector4 - vector3) * num;
			if (flag)
			{
				vector4 = vector5;
			}
			else
			{
				vector3 = vector5;
			}
		}
		flag = vector3.x <= vector3.w;
		flag2 = vector4.x <= vector4.w;
		if (!flag && !flag2)
		{
			for (int j = 0; j < 4; j++)
			{
				ref Vector3 reference2 = ref vertices[lightNum * 4 + j];
				reference2 = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num2 = (vector3.w - vector3.x) / (vector4.x - vector3.x - vector4.w + vector3.w);
			Vector4 vector6 = vector3 + (vector4 - vector3) * num2;
			if (flag)
			{
				vector4 = vector6;
			}
			else
			{
				vector3 = vector6;
			}
		}
		flag = vector3.y >= 0f - vector3.w;
		flag2 = vector4.y >= 0f - vector4.w;
		if (!flag && !flag2)
		{
			for (int k = 0; k < 4; k++)
			{
				ref Vector3 reference3 = ref vertices[lightNum * 4 + k];
				reference3 = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num3 = (0f - vector3.w - vector3.y) / (vector4.y - vector3.y + vector4.w - vector3.w);
			Vector4 vector7 = vector3 + (vector4 - vector3) * num3;
			if (flag)
			{
				vector4 = vector7;
			}
			else
			{
				vector3 = vector7;
			}
		}
		flag = vector3.y <= vector3.w;
		flag2 = vector4.y <= vector4.w;
		if (!flag && !flag2)
		{
			for (int l = 0; l < 4; l++)
			{
				ref Vector3 reference4 = ref vertices[lightNum * 4 + l];
				reference4 = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num4 = (vector3.w - vector3.y) / (vector4.y - vector3.y - vector4.w + vector3.w);
			Vector4 vector8 = vector3 + (vector4 - vector3) * num4;
			if (flag)
			{
				vector4 = vector8;
			}
			else
			{
				vector3 = vector8;
			}
		}
		flag = vector3.z <= vector3.w;
		flag2 = vector4.z <= vector4.w;
		if (!flag && !flag2)
		{
			for (int m = 0; m < 4; m++)
			{
				ref Vector3 reference5 = ref vertices[lightNum * 4 + m];
				reference5 = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num5 = (vector3.w - vector3.z) / (vector4.z - vector3.z - vector4.w + vector3.w);
			Vector4 vector9 = vector3 + (vector4 - vector3) * num5;
			if (flag)
			{
				vector4 = vector9;
			}
			else
			{
				vector3 = vector9;
			}
		}
		float num6 = 0.0001f;
		flag = vector3.z >= 0f - vector3.w - num6;
		flag2 = vector4.z >= 0f - vector4.w - num6;
		if (!flag && !flag2)
		{
			for (int n = 0; n < 4; n++)
			{
				ref Vector3 reference6 = ref vertices[lightNum * 4 + n];
				reference6 = Vector3.zero;
			}
			return;
		}
		if (flag != flag2)
		{
			float num7 = (0f - vector3.w - vector3.z) / (vector4.z - vector3.z + vector4.w - vector3.w);
			Vector4 vector10 = vector3 + (vector4 - vector3) * num7;
			if (flag)
			{
				vector4 = vector10;
			}
			else
			{
				vector3 = vector10;
			}
		}
		Matrix4x4 inverse = projectionMatrix.inverse;
		vector = inverse * vector3;
		vector2 = inverse * vector4;
		Vector3 vector11 = vector3 / vector3.w;
		Vector3 vector12 = vector4 / vector4.w;
		vector11.x = vector11.x * 0.5f + 0.5f;
		vector11.y = vector11.y * 0.5f + 0.5f;
		vector11.z = 0f;
		vector12.x = vector12.x * 0.5f + 0.5f;
		vector12.y = vector12.y * 0.5f + 0.5f;
		vector12.z = 0f;
		Vector3 vector13 = vector12 - vector11;
		Vector3 vector14 = new Vector3(0f - vector13.y, vector13.x, 0f);
		vector14.Normalize();
		vector14 *= lineWidth;
		Vector3 vector15 = new Vector3(vector.x / vector3.w, vector.y / vector3.w, vector.z / vector3.w);
		Vector3 vector16 = new Vector3(1f / vector3.w, 0f, 0f);
		Vector3 vector17 = new Vector3(vector2.x / vector4.w, vector2.y / vector4.w, vector2.z / vector4.w);
		Vector3 vector18 = new Vector3(1f / vector4.w, 0f, 0f);
		int num8 = lightNum * 4;
		ref Vector3 reference7 = ref vertices[num8];
		reference7 = vector11 - vector14;
		ref Vector3 reference8 = ref vertices[num8 + 1];
		reference8 = vector11 + vector14;
		ref Vector3 reference9 = ref vertices[num8 + 2];
		reference9 = vector12 + vector14;
		ref Vector3 reference10 = ref vertices[num8 + 3];
		reference10 = vector12 - vector14;
		Color32 color = _color;
		colors32[num8] = color;
		colors32[num8 + 1] = color;
		colors32[num8 + 2] = color;
		colors32[num8 + 3] = color;
		ref Vector2 reference11 = ref uv2[num8];
		reference11 = vector15;
		ref Vector2 reference12 = ref uv2[num8 + 1];
		reference12 = vector15;
		ref Vector2 reference13 = ref uv2[num8 + 2];
		reference13 = vector17;
		ref Vector2 reference14 = ref uv2[num8 + 3];
		reference14 = vector17;
		ref Vector2 reference15 = ref uv3[num8];
		reference15 = vector16;
		ref Vector2 reference16 = ref uv3[num8 + 1];
		reference16 = vector16;
		ref Vector2 reference17 = ref uv3[num8 + 2];
		reference17 = vector18;
		ref Vector2 reference18 = ref uv3[num8 + 3];
		reference18 = vector18;
	}
}
