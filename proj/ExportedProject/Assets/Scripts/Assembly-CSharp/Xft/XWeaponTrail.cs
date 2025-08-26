using System.Collections.Generic;
using UnityEngine;

namespace Xft
{
	public class XWeaponTrail : MonoBehaviour
	{
		public class Element
		{
			public Vector3 PointStart;

			public Vector3 PointEnd;

			public Vector3 Pos
			{
				get
				{
					return (PointStart + PointEnd) * 0.5f;
				}
			}

			public Element(Vector3 start, Vector3 end)
			{
				PointStart = start;
				PointEnd = end;
			}

			public Element()
			{
			}
		}

		public class ElementPool
		{
			private readonly Stack<Element> _stack = new Stack<Element>();

			public int CountAll { get; private set; }

			public int CountActive
			{
				get
				{
					return CountAll - CountInactive;
				}
			}

			public int CountInactive
			{
				get
				{
					return _stack.Count;
				}
			}

			public ElementPool(int preCount)
			{
				for (int i = 0; i < preCount; i++)
				{
					Element item = new Element();
					_stack.Push(item);
					CountAll++;
				}
			}

			public Element Get()
			{
				Element result;
				if (_stack.Count == 0)
				{
					result = new Element();
					CountAll++;
				}
				else
				{
					result = _stack.Pop();
				}
				return result;
			}

			public void Release(Element element)
			{
				if (_stack.Count > 0 && object.ReferenceEquals(_stack.Peek(), element))
				{
					Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
				}
				_stack.Push(element);
			}
		}

		[SerializeField]
		protected XWeaponTrailRenderer _trailRendererPrefab;

		[SerializeField]
		protected Transform _pointStart;

		[SerializeField]
		protected Transform _pointEnd;

		[SerializeField]
		protected int _maxFrame = 20;

		[SerializeField]
		protected int _granularity = 60;

		[SerializeField]
		protected Color _color = Color.white;

		[SerializeField]
		protected int _skipFirstFrames = 4;

		protected float _trailWidth;

		protected List<Element> _snapshotList = new List<Element>();

		protected ElementPool _elemPool;

		protected Spline _spline = new Spline();

		protected VertexPool _vertexPool;

		protected VertexPool.VertexSegment _vertexSegment;

		protected XWeaponTrailRenderer _trailRenderer;

		protected int _frameNum;

		public Vector3 CurHeadPos
		{
			get
			{
				return (_pointStart.position + _pointEnd.position) / 2f;
			}
		}

		protected virtual Color Color
		{
			get
			{
				return _color;
			}
		}

		private void Start()
		{
			_elemPool = new ElementPool(_maxFrame);
			_trailWidth = (_pointStart.position - _pointEnd.position).magnitude;
			_trailRenderer = Object.Instantiate(_trailRendererPrefab, Vector3.zero, Quaternion.identity);
			_vertexPool = new VertexPool(_trailRenderer.mesh);
			_vertexSegment = _vertexPool.GetVertices(_granularity * 3, (_granularity - 1) * 12);
			UpdateIndices();
			_spline.Granularity = _granularity;
		}

		private void OnEnable()
		{
			_frameNum = 0;
		}

		private void OnDisable()
		{
			if ((bool)_trailRenderer)
			{
				_trailRenderer.enabled = false;
			}
		}

		private void LateUpdate()
		{
			_frameNum++;
			if (_frameNum == _skipFirstFrames + 1)
			{
				if ((bool)_trailRenderer)
				{
					_trailRenderer.enabled = true;
				}
				_spline.Clear();
				for (int i = 0; i < _maxFrame; i++)
				{
					_spline.AddControlPoint(CurHeadPos, _pointStart.position - _pointEnd.position);
				}
				_snapshotList.Clear();
				_snapshotList.Add(new Element(_pointStart.position, _pointEnd.position));
				_snapshotList.Add(new Element(_pointStart.position, _pointEnd.position));
			}
			else if (_frameNum < _skipFirstFrames + 1)
			{
				return;
			}
			UpdateHeadElem();
			RecordCurElem();
			RefreshSpline();
			UpdateVertex();
			_vertexPool.ManualUpdate(Time.deltaTime);
		}

		private void OnDestroy()
		{
			if (_trailRenderer != null)
			{
				Object.Destroy(_trailRenderer.gameObject);
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (!(_pointEnd == null) && !(_pointStart == null))
			{
				float magnitude = (_pointStart.position - _pointEnd.position).magnitude;
				if (!(magnitude < Mathf.Epsilon))
				{
					Gizmos.color = Color.red;
					Gizmos.DrawSphere(_pointStart.position, magnitude * 0.04f);
					Gizmos.color = Color.blue;
					Gizmos.DrawSphere(_pointEnd.position, magnitude * 0.04f);
				}
			}
		}

		private void RefreshSpline()
		{
			for (int i = 0; i < _snapshotList.Count; i++)
			{
				_spline.ControlPoints[i].Position = _snapshotList[i].Pos;
				_spline.ControlPoints[i].Normal = _snapshotList[i].PointEnd - _snapshotList[i].PointStart;
			}
			_spline.RefreshSpline();
		}

		private void UpdateVertex()
		{
			VertexPool pool = _vertexSegment.Pool;
			for (int i = 0; i < _granularity; i++)
			{
				int num = _vertexSegment.VertStart + i * 3;
				float num2 = (float)i / (float)_granularity;
				float tl = num2;
				Vector2 zero = Vector2.zero;
				Vector3 vector = _spline.InterpolateByLen(tl);
				Vector3 vector2 = _spline.InterpolateNormalByLen(tl);
				Vector3 vector3 = vector + vector2.normalized * _trailWidth * 0.5f;
				Vector3 vector4 = vector - vector2.normalized * _trailWidth * 0.5f;
				pool.Vertices[num] = vector3;
				pool.Colors[num] = Color;
				zero.x = 0f;
				zero.y = num2;
				pool.UVs[num] = zero;
				pool.Vertices[num + 1] = vector;
				pool.Colors[num + 1] = Color;
				zero.x = 0.5f;
				zero.y = num2;
				pool.UVs[num + 1] = zero;
				pool.Vertices[num + 2] = vector4;
				pool.Colors[num + 2] = Color;
				zero.x = 1f;
				zero.y = num2;
				pool.UVs[num + 2] = zero;
			}
			_vertexSegment.Pool.UVChanged = true;
			_vertexSegment.Pool.VertChanged = true;
			_vertexSegment.Pool.ColorChanged = true;
		}

		private void UpdateIndices()
		{
			VertexPool pool = _vertexSegment.Pool;
			for (int i = 0; i < _granularity - 1; i++)
			{
				int num = _vertexSegment.VertStart + i * 3;
				int num2 = _vertexSegment.VertStart + (i + 1) * 3;
				int num3 = _vertexSegment.IndexStart + i * 12;
				pool.Indices[num3] = num2;
				pool.Indices[num3 + 1] = num2 + 1;
				pool.Indices[num3 + 2] = num;
				pool.Indices[num3 + 3] = num2 + 1;
				pool.Indices[num3 + 4] = num + 1;
				pool.Indices[num3 + 5] = num;
				pool.Indices[num3 + 6] = num2 + 1;
				pool.Indices[num3 + 7] = num2 + 2;
				pool.Indices[num3 + 8] = num + 1;
				pool.Indices[num3 + 9] = num2 + 2;
				pool.Indices[num3 + 10] = num + 2;
				pool.Indices[num3 + 11] = num + 1;
			}
			pool.IndiceChanged = true;
		}

		private void UpdateHeadElem()
		{
			_snapshotList[0].PointStart = _pointStart.position;
			_snapshotList[0].PointEnd = _pointEnd.position;
		}

		private void RecordCurElem()
		{
			Element element = _elemPool.Get();
			element.PointStart = _pointStart.position;
			element.PointEnd = _pointEnd.position;
			if (_snapshotList.Count < _maxFrame)
			{
				_snapshotList.Insert(1, element);
				return;
			}
			_elemPool.Release(_snapshotList[_snapshotList.Count - 1]);
			_snapshotList.RemoveAt(_snapshotList.Count - 1);
			_snapshotList.Insert(1, element);
		}
	}
}
