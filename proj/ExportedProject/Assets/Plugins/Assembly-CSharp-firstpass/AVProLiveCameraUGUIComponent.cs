using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("AVPro Live Camera/uGUI Component")]
public class AVProLiveCameraUGUIComponent : MaskableGraphic
{
	[SerializeField]
	public AVProLiveCamera m_liveCamera;

	[SerializeField]
	public Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

	[SerializeField]
	public bool _setNativeSize;

	[SerializeField]
	public bool _keepAspectRatio = true;

	[SerializeField]
	public Texture _defaultTexture;

	private int _lastWidth;

	private int _lastHeight;

	public override Texture mainTexture
	{
		get
		{
			Texture result = Texture2D.whiteTexture;
			if (HasValidTexture())
			{
				result = m_liveCamera.OutputTexture;
			}
			else if (_defaultTexture != null)
			{
				result = _defaultTexture;
			}
			return result;
		}
	}

	public AVProLiveCamera source
	{
		get
		{
			return m_liveCamera;
		}
		set
		{
			if (!(m_liveCamera == value))
			{
				m_liveCamera = value;
				SetMaterialDirty();
			}
		}
	}

	public Rect uvRect
	{
		get
		{
			return m_UVRect;
		}
		set
		{
			if (!(m_UVRect == value))
			{
				m_UVRect = value;
				SetVerticesDirty();
			}
		}
	}

	protected AVProLiveCameraUGUIComponent()
	{
	}

	public bool HasValidTexture()
	{
		return m_liveCamera != null && m_liveCamera.OutputTexture != null;
	}

	private void Update()
	{
		if (!(mainTexture == null))
		{
			if (_setNativeSize)
			{
				SetNativeSize();
			}
			if (HasValidTexture() && (mainTexture.width != _lastWidth || mainTexture.height != _lastHeight))
			{
				_lastWidth = mainTexture.width;
				_lastHeight = mainTexture.height;
				SetVerticesDirty();
			}
			SetMaterialDirty();
		}
	}

	[ContextMenu("Set Native Size")]
	public override void SetNativeSize()
	{
		Texture texture = mainTexture;
		if (texture != null)
		{
			int num = Mathf.RoundToInt((float)texture.width * uvRect.width);
			int num2 = Mathf.RoundToInt((float)texture.height * uvRect.height);
			base.rectTransform.anchorMax = base.rectTransform.anchorMin;
			base.rectTransform.sizeDelta = new Vector2(num, num2);
		}
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		List<UIVertex> list = new List<UIVertex>();
		_OnFillVBO(list);
		List<int> indices = new List<int>(new int[6] { 0, 1, 2, 2, 3, 0 });
		vh.AddUIVertexStream(list, indices);
	}

	protected void _OnFillVBO(List<UIVertex> vbo)
	{
		Vector4 drawingDimensions = GetDrawingDimensions(_keepAspectRatio);
		vbo.Clear();
		UIVertex simpleVert = UIVertex.simpleVert;
		simpleVert.position = new Vector2(drawingDimensions.x, drawingDimensions.y);
		simpleVert.uv0 = new Vector2(m_UVRect.xMin, m_UVRect.yMin);
		simpleVert.color = color;
		vbo.Add(simpleVert);
		simpleVert.position = new Vector2(drawingDimensions.x, drawingDimensions.w);
		simpleVert.uv0 = new Vector2(m_UVRect.xMin, m_UVRect.yMax);
		simpleVert.color = color;
		vbo.Add(simpleVert);
		simpleVert.position = new Vector2(drawingDimensions.z, drawingDimensions.w);
		simpleVert.uv0 = new Vector2(m_UVRect.xMax, m_UVRect.yMax);
		simpleVert.color = color;
		vbo.Add(simpleVert);
		simpleVert.position = new Vector2(drawingDimensions.z, drawingDimensions.y);
		simpleVert.uv0 = new Vector2(m_UVRect.xMax, m_UVRect.yMin);
		simpleVert.color = color;
		vbo.Add(simpleVert);
	}

	private Vector4 GetDrawingDimensions(bool shouldPreserveAspect)
	{
		Vector4 result = Vector4.zero;
		if ((bool)mainTexture)
		{
			Vector4 zero = Vector4.zero;
			Vector2 vector = new Vector2(mainTexture.width, mainTexture.height);
			Rect pixelAdjustedRect = GetPixelAdjustedRect();
			int num = Mathf.RoundToInt(vector.x);
			int num2 = Mathf.RoundToInt(vector.y);
			Vector4 vector2 = new Vector4(zero.x / (float)num, zero.y / (float)num2, ((float)num - zero.z) / (float)num, ((float)num2 - zero.w) / (float)num2);
			if (shouldPreserveAspect && vector.sqrMagnitude > 0f)
			{
				float num3 = vector.x / vector.y;
				float num4 = pixelAdjustedRect.width / pixelAdjustedRect.height;
				if (num3 > num4)
				{
					float height = pixelAdjustedRect.height;
					pixelAdjustedRect.height = pixelAdjustedRect.width * (1f / num3);
					pixelAdjustedRect.y += (height - pixelAdjustedRect.height) * base.rectTransform.pivot.y;
				}
				else
				{
					float width = pixelAdjustedRect.width;
					pixelAdjustedRect.width = pixelAdjustedRect.height * num3;
					pixelAdjustedRect.x += (width - pixelAdjustedRect.width) * base.rectTransform.pivot.x;
				}
			}
			result = new Vector4(pixelAdjustedRect.x + pixelAdjustedRect.width * vector2.x, pixelAdjustedRect.y + pixelAdjustedRect.height * vector2.y, pixelAdjustedRect.x + pixelAdjustedRect.width * vector2.z, pixelAdjustedRect.y + pixelAdjustedRect.height * vector2.w);
		}
		return result;
	}
}
