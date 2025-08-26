using UnityEngine;

public class FeverModeUIPanel : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private RectTransform _feverBGTextRectTransform;

	private ScoreController _scoreController;

	private float _feverTextRectWidth;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_feverTextRectWidth = _feverBGTextRectTransform.sizeDelta.x;
		_scoreController.feverDidStartEvent += HandleFeverModeDidStart;
		_scoreController.feverDidFinishEvent += HandleFeverModeDidFinish;
		_scoreController.feverModeChargeProgressDidChangeEvent += HandleFeverModeChargeProgressDidChange;
		SetProgress(0f);
	}

	private void OnDestroy()
	{
		if ((bool)_scoreController)
		{
			_scoreController.feverDidStartEvent -= HandleFeverModeDidStart;
			_scoreController.feverDidFinishEvent -= HandleFeverModeDidFinish;
			_scoreController.feverModeChargeProgressDidChangeEvent -= HandleFeverModeChargeProgressDidChange;
		}
	}

	private void Update()
	{
		if (_scoreController.feverModeActive)
		{
			SetProgress(1f - _scoreController.feverModeDrainProgress);
		}
	}

	private void SetProgress(float progress)
	{
		_feverBGTextRectTransform.sizeDelta = new Vector2(_feverTextRectWidth * progress, _feverBGTextRectTransform.sizeDelta.y);
	}

	private void HandleFeverModeDidStart()
	{
		SetProgress(0f);
	}

	private void HandleFeverModeDidFinish()
	{
		SetProgress(0f);
	}

	private void HandleFeverModeChargeProgressDidChange(float progress)
	{
		SetProgress(progress);
	}
}
