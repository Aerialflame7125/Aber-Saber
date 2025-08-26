using UnityEngine;

public class GlobalCutoutZParams : MonoBehaviour
{
	[SerializeField]
	private float _defaultGlobalCutoutZOffset = -1f;

	[SerializeField]
	private float _defaultGlobalCutoutZWidth = 3f;

	private int _globalCutoutZOffsetID;

	private int _globalCutoutZWidthID;

	private void Start()
	{
		_globalCutoutZOffsetID = Shader.PropertyToID("_GlobalCutoutZOffset");
		_globalCutoutZWidthID = Shader.PropertyToID("_GlobalCutoutZWidth");
		Shader.SetGlobalFloat(_globalCutoutZOffsetID, _defaultGlobalCutoutZOffset);
		Shader.SetGlobalFloat(_globalCutoutZWidthID, _defaultGlobalCutoutZWidth);
	}
}
