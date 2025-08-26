using System.Collections.Generic;
using UnityEngine;

public class RenderTextureGraphDrawer
{
	private static Material _lineMaterial;

	private static RenderTexture _renderTexture;

	public static Texture2D Draw(int texWidth, int texHeight, List<Vector2> data, float lineSize, Color lineColor, Color bgColor, Texture lineTexture)
	{
		if (!_lineMaterial)
		{
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			_lineMaterial = new Material(shader);
			_lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			_lineMaterial.SetInt("_SrcBlend", 5);
			_lineMaterial.SetInt("_DstBlend", 10);
			_lineMaterial.SetInt("_Cull", 0);
			_lineMaterial.SetInt("_ZWrite", 0);
		}
		if (lineTexture != null)
		{
			_lineMaterial.SetTexture("_MainTex", lineTexture);
		}
		_lineMaterial.SetPass(0);
		if (_renderTexture == null || _renderTexture.width != texWidth || _renderTexture.height != texHeight)
		{
			_renderTexture = new RenderTexture(texWidth, texHeight, 0, RenderTextureFormat.ARGB32);
		}
		RenderTexture active = RenderTexture.active;
		RenderTexture.active = _renderTexture;
		GameObject gameObject = new GameObject();
		gameObject.layer = 31;
		LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
		lineRenderer.material = _lineMaterial;
		Vector3[] array = new Vector3[data.Count];
		for (int i = 0; i < data.Count; i++)
		{
			ref Vector3 reference = ref array[i];
			reference = new Vector3((data[i].x * 2f - 1f) * (float)texWidth / (float)texHeight, data[i].y * 2f - 1f, 0f);
		}
		lineRenderer.startColor = lineColor;
		lineRenderer.endColor = lineColor;
		lineRenderer.widthMultiplier = lineSize / (float)texHeight;
		lineRenderer.numCornerVertices = 3;
		lineRenderer.alignment = LineAlignment.Local;
		lineRenderer.textureMode = LineTextureMode.Stretch;
		lineRenderer.positionCount = data.Count;
		lineRenderer.SetPositions(array);
		GameObject gameObject2 = new GameObject();
		Camera camera = gameObject2.AddComponent<Camera>();
		camera.orthographic = true;
		camera.orthographicSize = 1f;
		camera.nearClipPlane = 0f;
		camera.farClipPlane = 1f;
		camera.clearFlags = CameraClearFlags.Color;
		camera.backgroundColor = bgColor;
		camera.cullingMask = 1 << gameObject.layer;
		camera.targetTexture = _renderTexture;
		camera.enabled = false;
		camera.Render();
		Texture2D texture2D = new Texture2D(texWidth, texHeight, TextureFormat.ARGB32, mipmap: false);
		texture2D.wrapMode = TextureWrapMode.Clamp;
		texture2D.ReadPixels(new Rect(0f, 0f, texWidth, texHeight), 0, 0);
		texture2D.Apply();
		RenderTexture.active = active;
		Object.DestroyImmediate(gameObject2);
		Object.DestroyImmediate(gameObject);
		return texture2D;
	}

	private void OnDestroy()
	{
		EssentialHelpers.SafeDestroy(_lineMaterial);
	}
}
