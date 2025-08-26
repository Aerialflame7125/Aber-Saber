using System.Text;
using TMPro;
using UnityEngine;

public class ScoreUIController : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(ScoreController))]
	private ObjectProvider _scoreControllerProvider;

	[SerializeField]
	private TextMeshPro[] _scoreTexts;

	private ScoreController _scoreController;

	private StringBuilder _stringBuilder;

	private void Start()
	{
		_stringBuilder = new StringBuilder(16);
		for (int i = 0; i < _scoreTexts.Length; i++)
		{
		}
		_scoreController = _scoreControllerProvider.GetProvidedObject<ScoreController>();
		_scoreController.scoreDidChangeEvent += HandleScoreDidChangeRealtime;
		UpdateScore(0);
	}

	private void OnDestroy()
	{
		if ((bool)_scoreController)
		{
			_scoreController.scoreDidChangeEvent -= HandleScoreDidChangeRealtime;
		}
	}

	private void HandleScoreDidChangeRealtime(int newScore)
	{
		UpdateScore(newScore);
	}

	private void UpdateScore(int score)
	{
		_stringBuilder.Remove(0, _stringBuilder.Length);
		_stringBuilder.AppendNumber(score);
		string text = _stringBuilder.ToString();
		for (int i = 0; i < _scoreTexts.Length; i++)
		{
			_scoreTexts[i].text = text;
		}
	}
}
