using UnityEngine;

public class FlyingTextSpawner : MonoBehaviour
{
	[SerializeField]
	private FlyingTextEffect _flyingTextEffectPrefab;

	[Space]
	[SerializeField]
	private int _poolSize = 20;

	[SerializeField]
	private float _duration = 1f;

	[SerializeField]
	private float _xSpread = 1.15f;

	[SerializeField]
	private float _targetYPos = 1.3f;

	[SerializeField]
	private float _targetZPos = 10f;

	[SerializeField]
	private Color _color = new Color(0f, 0.75f, 1f);

	[SerializeField]
	private float _fontSize = 5f;

	[SerializeField]
	private bool _shake;

	private void Awake()
	{
		_flyingTextEffectPrefab.CreatePool(_poolSize);
	}

	public void SpawnText(Vector3 pos, string text)
	{
		FlyingTextEffect flyingTextEffect = _flyingTextEffectPrefab.Spawn(pos, Quaternion.identity);
		flyingTextEffect.InitAndPresent(targetPos: new Vector3(pos.x * _xSpread, _targetYPos, _targetZPos), text: text, duration: _duration, color: _color, fontSize: _fontSize, shake: _shake);
	}
}
