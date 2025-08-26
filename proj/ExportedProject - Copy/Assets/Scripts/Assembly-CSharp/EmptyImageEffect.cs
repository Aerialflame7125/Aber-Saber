using UnityEngine;

public class EmptyImageEffect : MonoBehaviour
{
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination);
	}
}
