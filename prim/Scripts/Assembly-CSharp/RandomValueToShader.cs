using UnityEngine;

public class RandomValueToShader : ScriptableObject
{
	private int _randomValueID;

	private int _lastFrameNum = -1;

	private void Awake()
	{
		_randomValueID = Shader.PropertyToID("_GlobalRandomValue");
	}

	public void SetRandomValueToShaders()
	{
		int frameCount = Time.frameCount;
		if (_lastFrameNum != frameCount)
		{
			Shader.SetGlobalFloat(_randomValueID, Random.value);
			_lastFrameNum = frameCount;
		}
	}
}
