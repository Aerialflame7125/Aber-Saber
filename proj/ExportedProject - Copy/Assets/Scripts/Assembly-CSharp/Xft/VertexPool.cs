using UnityEngine;

namespace Xft
{
	public class VertexPool
	{
		public class VertexSegment
		{
			public int VertStart;

			public int IndexStart;

			public int VertCount;

			public int IndexCount;

			public VertexPool Pool;

			public VertexSegment(int start, int count, int istart, int icount, VertexPool pool)
			{
				VertStart = start;
				VertCount = count;
				IndexCount = icount;
				IndexStart = istart;
				Pool = pool;
			}

			public void ClearIndices()
			{
				for (int i = IndexStart; i < IndexStart + IndexCount; i++)
				{
					Pool.Indices[i] = 0;
				}
				Pool.IndiceChanged = true;
			}
		}

		public Vector3[] Vertices;

		public int[] Indices;

		public Vector2[] UVs;

		public Color[] Colors;

		public bool IndiceChanged;

		public bool ColorChanged;

		public bool UVChanged;

		public bool VertChanged;

		public bool UV2Changed;

		private bool _firstUpdate = true;

		private const int _blockSize = 108;

		private float _boundsScheduleTime = 1f;

		private float _elapsedTime;

		private int _vertexTotal;

		private int _vertexUsed;

		private int _indexTotal;

		private int _indexUsed;

		private bool _vertCountChanged;

		private Mesh _mesh;

		public VertexPool(Mesh mesh)
		{
			_mesh = mesh;
			_vertexTotal = (_vertexUsed = 0);
			_vertCountChanged = false;
			Vertices = new Vector3[4];
			UVs = new Vector2[4];
			Colors = new Color[4];
			Indices = new int[6];
			_vertexTotal = 4;
			_indexTotal = 6;
			IndiceChanged = (ColorChanged = (UVChanged = (UV2Changed = (VertChanged = true))));
		}

		public VertexSegment GetVertices(int vcount, int icount)
		{
			int num = 0;
			int num2 = 0;
			if (_vertexUsed + vcount >= _vertexTotal)
			{
				num = (vcount / 108 + 1) * 108;
			}
			if (_indexUsed + icount >= _indexTotal)
			{
				num2 = (icount / 108 + 1) * 108;
			}
			_vertexUsed += vcount;
			_indexUsed += icount;
			if (num != 0 || num2 != 0)
			{
				EnlargeArrays(num, num2);
				_vertexTotal += num;
				_indexTotal += num2;
			}
			return new VertexSegment(_vertexUsed - vcount, vcount, _indexUsed - icount, icount, this);
		}

		public void EnlargeArrays(int count, int icount)
		{
			Vector3[] vertices = Vertices;
			Vertices = new Vector3[Vertices.Length + count];
			vertices.CopyTo(Vertices, 0);
			Vector2[] uVs = UVs;
			UVs = new Vector2[UVs.Length + count];
			uVs.CopyTo(UVs, 0);
			Color[] colors = Colors;
			Colors = new Color[Colors.Length + count];
			colors.CopyTo(Colors, 0);
			int[] indices = Indices;
			Indices = new int[Indices.Length + icount];
			indices.CopyTo(Indices, 0);
			_vertCountChanged = true;
			IndiceChanged = true;
			ColorChanged = true;
			UVChanged = true;
			VertChanged = true;
			UV2Changed = true;
		}

		public void ManualUpdate(float deltaTime)
		{
			if (_vertCountChanged)
			{
				_mesh.Clear();
			}
			_mesh.vertices = Vertices;
			if (UVChanged)
			{
				_mesh.uv = UVs;
			}
			if (ColorChanged)
			{
				_mesh.colors = Colors;
			}
			if (IndiceChanged)
			{
				_mesh.triangles = Indices;
			}
			_elapsedTime += deltaTime;
			if (_elapsedTime > _boundsScheduleTime || _firstUpdate)
			{
				_mesh.RecalculateBounds();
				_elapsedTime = 0f;
			}
			if (_elapsedTime > _boundsScheduleTime)
			{
				_firstUpdate = false;
			}
			_vertCountChanged = false;
			IndiceChanged = false;
			ColorChanged = false;
			UVChanged = false;
			UV2Changed = false;
			VertChanged = false;
		}
	}
}
