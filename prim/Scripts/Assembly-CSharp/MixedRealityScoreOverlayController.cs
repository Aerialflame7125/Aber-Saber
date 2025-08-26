using TMPro;
using UnityEngine;

public class MixedRealityScoreOverlayController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private TextMeshProUGUI _highScoreText;

	private ScoreController _scoreController;

	private void Start()
	{
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_scoreController.scoreDidChangeEvent += HandleScoreDidChangeRealtime;
		HandleScoreDidChangeRealtime(0);
	}

	private void OnEnable()
	{
		if (_scoreController != null)
		{
			_scoreController.scoreDidChangeEvent -= HandleScoreDidChangeRealtime;
			_scoreController.scoreDidChangeEvent += HandleScoreDidChangeRealtime;
		}
	}

	private void OnDestroy()
	{
		if (_scoreController != null)
		{
			_scoreController.scoreDidChangeEvent -= HandleScoreDidChangeRealtime;
		}
	}

	private void HandleScoreDidChangeRealtime(int score)
	{
		_highScoreText.text = $"{score:000 000}";
	}
}
