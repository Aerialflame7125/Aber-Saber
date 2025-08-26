using UnityEngine;

public class MultiplierIncreasedEffectSpawner : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private FlyingTextEffect _textEffectPrefab;

	[SerializeField]
	private Transform _textEffectEndPos;

	[SerializeField]
	private float _textEffectDuration = 1f;

	private ScoreController _scoreController;

	private int _lastMultiplier;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_textEffectPrefab.CreatePool(10);
		_scoreController.multiplierDidChangeEvent += HandleMultiplierDidChange;
		_lastMultiplier = 1;
	}

	private void OnDestroy()
	{
		if (_scoreController != null)
		{
			_scoreController.multiplierDidChangeEvent -= HandleMultiplierDidChange;
		}
	}

	private void HandleMultiplierDidChange(int multiplier, float progress)
	{
		if (_lastMultiplier >= multiplier)
		{
			_lastMultiplier = multiplier;
			return;
		}
		_lastMultiplier = multiplier;
		FlyingTextEffect flyingTextEffect = _textEffectPrefab.Spawn(base.transform.position, Quaternion.identity);
		flyingTextEffect.InitAndPresent("Multiplier\nincreased", _textEffectDuration, _textEffectEndPos.position, Color.white, 1.5f, shake: false);
	}
}
