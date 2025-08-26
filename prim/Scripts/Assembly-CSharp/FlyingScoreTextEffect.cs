using System;
using UnityEngine;

public class FlyingScoreTextEffect : FlyingTextEffect
{
	[SerializeField]
	private float _duration = 1.2f;

	private NoteCutInfo _noteCutInfo;

	private int _multiplier;

	private SaberAfterCutSwingRatingCounter _saberAfterCutSwingRatingCounter;

	public void InitAndPresent(NoteCutInfo noteCutInfo, int multiplier, Vector3 targetPos, Color color, SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter)
	{
		_noteCutInfo = noteCutInfo;
		_multiplier = multiplier;
		_saberAfterCutSwingRatingCounter = saberAfterCutSwingRatingCounter;
		SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter2 = _saberAfterCutSwingRatingCounter;
		saberAfterCutSwingRatingCounter2.didChangeEvent = (SaberAfterCutSwingRatingCounter.DidChangeDelegate)Delegate.Combine(saberAfterCutSwingRatingCounter2.didChangeEvent, new SaberAfterCutSwingRatingCounter.DidChangeDelegate(HandleSaberAfterCutSwingRatingCounterDidChangeEvent));
		ScoreController.ScoreWithoutMultiplier(noteCutInfo, saberAfterCutSwingRatingCounter, out var beforeCutScore, out var afterCutScore);
		InitAndPresent(GetScoreText(beforeCutScore + afterCutScore, multiplier), _duration, targetPos, color, 2f, shake: false);
	}

	private void OnDisable()
	{
		if (_saberAfterCutSwingRatingCounter != null)
		{
			SaberAfterCutSwingRatingCounter saberAfterCutSwingRatingCounter = _saberAfterCutSwingRatingCounter;
			saberAfterCutSwingRatingCounter.didChangeEvent = (SaberAfterCutSwingRatingCounter.DidChangeDelegate)Delegate.Remove(saberAfterCutSwingRatingCounter.didChangeEvent, new SaberAfterCutSwingRatingCounter.DidChangeDelegate(HandleSaberAfterCutSwingRatingCounterDidChangeEvent));
		}
	}

	private void HandleSaberAfterCutSwingRatingCounterDidChangeEvent(SaberAfterCutSwingRatingCounter afterCutRating, float rating)
	{
		ScoreController.ScoreWithoutMultiplier(_noteCutInfo, _saberAfterCutSwingRatingCounter, out var beforeCutScore, out var afterCutScore);
		base.text = GetScoreText(beforeCutScore + afterCutScore, _multiplier);
	}

	private string GetScoreText(int score, int multiplier)
	{
		return score.ToString();
	}
}
