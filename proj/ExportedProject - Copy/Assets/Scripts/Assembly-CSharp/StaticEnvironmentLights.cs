using UnityEngine;

public class StaticEnvironmentLights : MonoBehaviour
{
	[SerializeField]
	private Color[] _lightColors;

	[SerializeField]
	private Material[] _materials;

	private void Awake()
	{
		for (int i = 0; i < _materials.Length; i++)
		{
			_materials[i].color = _lightColors[i];
		}
	}
}
