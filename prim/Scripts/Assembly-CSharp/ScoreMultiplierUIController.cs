using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMultiplierUIController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private TextMeshProUGUI[] _multiplierTexts;

	[SerializeField]
	private Image _multiplierProgressImage;

	[SerializeField]
	private Animator _multiplierAnimator;

	private ScoreController _scoreController;

	private int _prevMultiplier;

	private int _multiplierIncreasedTriggerId;

	private float _progressTarget;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_scoreController.multiplierDidChangeEvent += HandleMultiplierDidChange;
		_prevMultiplier = 1;
		for (int i = 0; i < _multiplierTexts.Length; i++)
		{
			_multiplierTexts[i].text = "1";
		}
		_multiplierProgressImage.fillAmount = 0f;
		_multiplierIncreasedTriggerId = Animator.StringToHash("MultiplierIncreased");
	}

	private void OnDestroy()
	{
		if ((bool)_scoreController)
		{
			_scoreController.multiplierDidChangeEvent -= HandleMultiplierDidChange;
		}
	}

	private void Update()
	{
		if (Mathf.Abs(_progressTarget - _multiplierProgressImage.fillAmount) > 0.001f)
		{
			_multiplierProgressImage.fillAmount = Mathf.Lerp(_multiplierProgressImage.fillAmount, _progressTarget, Time.deltaTime * 4f);
		}
	}

	private void HandleMultiplierDidChange(int multiplier, float progress)
	{
		if (_prevMultiplier < multiplier)
		{
			_multiplierAnimator.SetTrigger(_multiplierIncreasedTriggerId);
			_multiplierProgressImage.fillAmount = 0f;
		}
		_prevMultiplier = multiplier;
		string text = multiplier.ToString();
		for (int i = 0; i < _multiplierTexts.Length; i++)
		{
			_multiplierTexts[i].text = text;
		}
		_progressTarget = progress;
	}
}
