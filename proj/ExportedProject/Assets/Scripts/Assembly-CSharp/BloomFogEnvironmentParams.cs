using UnityEngine;

[CreateAssetMenu(fileName = "BloomFogEnvironmentParams", menuName = "BS/Rendering/BloomFogEnvironmentParams")]
public class BloomFogEnvironmentParams : ScriptableObject
{
	public Color color = new Color(0f, 0f, 0f, 0f);

	public float colorMultiplier = 1f;

	public float attenuation = 0.1f;

	public float offset = 10f;
}
