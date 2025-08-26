using System;

public class AfterCutScoreBuffer : HMAutoincrementedRequestId
{
	public Action<AfterCutScoreBuffer> didFinishEvent;

	private int _afterCutScoreWithMultiplier;

	private int _multiplier;

	private NoteCutInfo _noteCutInfo;

	public int scoreWithMultiplier
	{
		get
		{
			return _afterCutScoreWithMultiplier;
		}
	}

	public AfterCutScoreBuffer(NoteCutInfo noteCutInfo, SaberAfterCutSwingRatingCounter afterCutSwingRatingCounter)
	{
		_noteCutInfo = noteCutInfo;
		afterCutSwingRatingCounter.didChangeEvent = (SaberAfterCutSwingRatingCounter.DidChangeDelegate)Delegate.Combine(afterCutSwingRatingCounter.didChangeEvent, new SaberAfterCutSwingRatingCounter.DidChangeDelegate(HandleAfterCutSwingRatingCounterDidChangeEvent));
		afterCutSwingRatingCounter.didFinishEvent = (SaberAfterCutSwingRatingCounter.DidFinishDelegate)Delegate.Combine(afterCutSwingRatingCounter.didFinishEvent, new SaberAfterCutSwingRatingCounter.DidFinishDelegate(HandleAfterCutSwingRatingCounterDidFinishEvent));
	}

	private void HandleAfterCutSwingRatingCounterDidChangeEvent(SaberAfterCutSwingRatingCounter afterCutRating, float rating)
	{
		int beforeCutScore;
		int afterCutScore;
		ScoreController.ScoreWithoutMultiplier(_noteCutInfo, afterCutRating, out beforeCutScore, out afterCutScore);
		_afterCutScoreWithMultiplier = afterCutScore * _multiplier;
	}

	private void HandleAfterCutSwingRatingCounterDidFinishEvent(SaberAfterCutSwingRatingCounter afterCutSwingRatingCounter)
	{
		afterCutSwingRatingCounter.didChangeEvent = (SaberAfterCutSwingRatingCounter.DidChangeDelegate)Delegate.Remove(afterCutSwingRatingCounter.didChangeEvent, new SaberAfterCutSwingRatingCounter.DidChangeDelegate(HandleAfterCutSwingRatingCounterDidChangeEvent));
		afterCutSwingRatingCounter.didFinishEvent = (SaberAfterCutSwingRatingCounter.DidFinishDelegate)Delegate.Remove(afterCutSwingRatingCounter.didFinishEvent, new SaberAfterCutSwingRatingCounter.DidFinishDelegate(HandleAfterCutSwingRatingCounterDidFinishEvent));
		if (didFinishEvent != null)
		{
			didFinishEvent(this);
		}
	}
}
