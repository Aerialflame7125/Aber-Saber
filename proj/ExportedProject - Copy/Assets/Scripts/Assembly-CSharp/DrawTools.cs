using UnityEngine;

public class DrawTools
{
	public static void DrawTexture(Texture texture, float x, float y, float w, float h, Material mat, float sx = 0f, float sy = 0f, float sw = 1f, float sh = 1f)
	{
		x /= (float)Screen.width;
		w /= (float)Screen.width;
		y /= (float)Screen.height;
		h /= (float)Screen.height;
		GL.PushMatrix();
		GL.LoadOrtho();
		mat.mainTexture = texture;
		mat.SetPass(0);
		GL.Begin(7);
		GL.TexCoord2(sx, sy);
		GL.Vertex3(x, y, 0f);
		GL.TexCoord2(sx + sw, sy);
		GL.Vertex3(x + w, y, 0f);
		GL.TexCoord2(sx + sw, sy + sw);
		GL.Vertex3(x + w, y + h, 0f);
		GL.TexCoord2(sx, sy + sw);
		GL.Vertex3(x, y + h, 0f);
		GL.End();
		GL.PopMatrix();
	}
}
