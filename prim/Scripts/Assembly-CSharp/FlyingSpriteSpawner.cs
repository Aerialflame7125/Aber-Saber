using UnityEngine;

public class FlyingSpriteSpawner : MonoBehaviour
{
	[SerializeField]
	private FlyingSpriteEffect _flyingSpriteEffectPrefab;

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
	private bool _shake;

	private void Start()
	{
		_flyingSpriteEffectPrefab.CreatePool(_poolSize);
	}

	public void SpawnFlyingSprite(Vector3 pos)
	{
		FlyingSpriteEffect flyingSpriteEffect = _flyingSpriteEffectPrefab.Spawn(pos, Quaternion.identity);
		flyingSpriteEffect.InitAndPresent(targetPos: new Vector3(pos.x * _xSpread, _targetYPos, _targetZPos), duration: _duration, color: _color, shake: _shake);
	}
}
