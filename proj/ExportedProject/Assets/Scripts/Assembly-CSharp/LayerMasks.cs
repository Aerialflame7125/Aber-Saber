using UnityEngine;

public class LayerMasks : MonoBehaviour
{
	private static LayerMask _saberLayerMask;

	private static LayerMask _noteLayerMask;

	public static LayerMask saberLayerMask
	{
		get
		{
			if (_saberLayerMask.value == 0)
			{
				_saberLayerMask = GetLayerMask("Saber");
			}
			return _saberLayerMask;
		}
	}

	public static LayerMask noteLayerMask
	{
		get
		{
			if (_noteLayerMask.value == 0)
			{
				_noteLayerMask = GetLayerMask("Note");
			}
			return _noteLayerMask;
		}
	}

	private static LayerMask GetLayerMask(string layerName)
	{
		int num = LayerMask.NameToLayer(layerName);
		LayerMask result = default(LayerMask);
		result.value = 1 << num;
		return result;
	}
}
