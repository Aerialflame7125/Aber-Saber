using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace VRUIControls
{
	[RequireComponent(typeof(Canvas))]
	public class VRGraphicRaycaster : BaseRaycaster
	{
		private struct TGraphicRaycastResult
		{
			public Graphic graphic;

			public float distance;

			public Vector3 position;

			public Vector2 pointerPosition;

			public override string ToString()
			{
				return string.Format("[{0} - {1}, {2}]", graphic.gameObject.name, distance, pointerPosition);
			}
		}

		private Canvas __canvas;

		private Camera __eventCamera;

		[SerializeField]
		protected LayerMask m_BlockingMask = -1;

		[NonSerialized]
		private readonly List<TGraphicRaycastResult> m_RaycastResults = new List<TGraphicRaycastResult>();

		[NonSerialized]
		private static readonly List<TGraphicRaycastResult> s_SortedGraphics = new List<TGraphicRaycastResult>();

		private Canvas m_canvas
		{
			get
			{
				return __canvas ?? (__canvas = GetComponent<Canvas>());
			}
		}

		public override Camera eventCamera
		{
			get
			{
				return __eventCamera ?? (__eventCamera = ((!(m_canvas.worldCamera != null)) ? Camera.main : m_canvas.worldCamera));
			}
		}

		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			float num = eventCamera.farClipPlane;
			Ray ray = new Ray(eventData.pointerCurrentRaycast.worldPosition, eventData.pointerCurrentRaycast.worldNormal);
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, eventData.pointerCurrentRaycast.depth, m_BlockingMask))
			{
				num = hitInfo.distance;
			}
			RaycastHit2D raycastHit2D = Physics2D.Raycast(ray.origin, ray.direction, eventData.pointerCurrentRaycast.depth, m_BlockingMask);
			if (raycastHit2D.collider != null)
			{
				float num2 = raycastHit2D.fraction * (float)eventData.pointerCurrentRaycast.depth;
				if (num2 < num)
				{
					num = num2;
				}
			}
			m_RaycastResults.Clear();
			Raycast(m_canvas, ray, num, eventCamera, m_RaycastResults);
			for (int i = 0; i < m_RaycastResults.Count; i++)
			{
				GameObject gameObject = m_RaycastResults[i].graphic.gameObject;
				RaycastResult raycastResult = default(RaycastResult);
				raycastResult.gameObject = gameObject;
				raycastResult.module = this;
				raycastResult.distance = m_RaycastResults[i].distance;
				raycastResult.screenPosition = m_RaycastResults[i].pointerPosition;
				raycastResult.worldPosition = m_RaycastResults[i].position;
				raycastResult.index = resultAppendList.Count;
				raycastResult.depth = m_RaycastResults[i].graphic.depth;
				raycastResult.sortingLayer = m_canvas.sortingLayerID;
				raycastResult.sortingOrder = m_canvas.sortingOrder;
				RaycastResult item = raycastResult;
				resultAppendList.Add(item);
			}
		}

		private static void Raycast(Canvas canvas, Ray ray, float hitDistance, Camera eventCamera, List<TGraphicRaycastResult> results)
		{
			IList<Graphic> graphicsForCanvas = GraphicRegistry.GetGraphicsForCanvas(canvas);
			for (int i = 0; i < graphicsForCanvas.Count; i++)
			{
				Graphic graphic = graphicsForCanvas[i];
				if (graphic.depth == -1 || !graphic.raycastTarget)
				{
					continue;
				}
				Transform transform = graphic.transform.transform;
				Vector3 forward = transform.forward;
				float num = Vector3.Dot(forward, transform.position - ray.origin) / Vector3.Dot(forward, ray.direction);
				if (!(num < 0f) && !(num >= hitDistance))
				{
					Vector3 point = ray.GetPoint(num);
					Vector2 vector = eventCamera.WorldToScreenPoint(point);
					if (RectTransformUtility.RectangleContainsScreenPoint(graphic.rectTransform, vector, eventCamera) && graphic.Raycast(vector, eventCamera))
					{
						s_SortedGraphics.Add(new TGraphicRaycastResult
						{
							graphic = graphic,
							distance = num,
							pointerPosition = vector,
							position = point
						});
					}
				}
			}
			s_SortedGraphics.Sort((TGraphicRaycastResult g1, TGraphicRaycastResult g2) => g2.graphic.depth.CompareTo(g1.graphic.depth));
			for (int j = 0; j < s_SortedGraphics.Count; j++)
			{
				results.Add(s_SortedGraphics[j]);
			}
			s_SortedGraphics.Clear();
		}
	}
}
