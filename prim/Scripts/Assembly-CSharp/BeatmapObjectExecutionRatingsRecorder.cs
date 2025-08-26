using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapObjectExecutionRatingsRecorder : MonoBehaviour
{
	private class AfterCutScoreHandler
	{
		public delegate void DidFinishCallback(AfterCutScoreHandler afterCutScoreHandler);

		private DidFinishCallback _finishCallback;

		private NoteExecutionRating _noteExecutionRating;

		private NoteCutInfo _noteCutInfo;

		public AfterCutScoreHandler(DidFinishCallback finishCallback)
		{
			_finishCallback = finishCallback;
		}

		public void Set(NoteCutInfo noteCutInfo, NoteExecutionRating noteExecutionRating, SaberAfterCutSwingRatingCounter afterCutRating)
		{
			_noteCutInfo = noteCutInfo;
			_noteExecutionRating = noteExecutionRating;
			afterCutRating.didFinishEvent = (SaberAfterCutSwingRatingCounter.DidFinishDelegate)Delegate.Combine(afterCutRating.didFinishEvent, new SaberAfterCutSwingRatingCounter.DidFinishDelegate(HandleAfterCutSwingRatingCounterDidFinishEvent));
		}

		private void HandleAfterCutSwingRatingCounterDidFinishEvent(SaberAfterCutSwingRatingCounter afterCutRating)
		{
			ScoreController.ScoreWithoutMultiplier(_noteCutInfo, afterCutRating, out var _, out var afterCutScore);
			_noteExecutionRating.cutScore += afterCutScore;
			afterCutRating.didFinishEvent = (SaberAfterCutSwingRatingCounter.DidFinishDelegate)Delegate.Remove(afterCutRating.didFinishEvent, new SaberAfterCutSwingRatingCounter.DidFinishDelegate(HandleAfterCutSwingRatingCounterDidFinishEvent));
			if (_finishCallback != null)
			{
				_finishCallback(this);
			}
		}
	}

	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private ScoreController _scoreController;

	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	[SerializeField]
	private FloatVariable _songTime;

	private List<BeatmapObjectExecutionRating> _beatmapObjectExecutionRatings;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private HashSet<int> _hitObstacles;

	private List<ObstacleController> _prevIntersectingObstacles;

	private List<AfterCutScoreHandler> _afterCutScoreHandlers;

	private List<AfterCutScoreHandler> _unusedAfterCutScoreHandlers;

	public List<BeatmapObjectExecutionRating> beatmapObjectExecutionRatings => _beatmapObjectExecutionRatings;

	private void Start()
	{
		_scoreController.noteWasCutEvent += HandleScoreControllerNoteWasCut;
		_scoreController.noteWasMissedEvent += HandleScoreControllerNoteWasMissed;
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.obstacleDidPassAvoidedMarkEvent += HandleBeatmapObjectSpawnControllerObstacleDidPassAvoidedMark;
		_beatmapObjectExecutionRatings = new List<BeatmapObjectExecutionRating>(500);
		_afterCutScoreHandlers = new List<AfterCutScoreHandler>(10);
		_unusedAfterCutScoreHandlers = new List<AfterCutScoreHandler>(10);
		for (int i = 0; i < _unusedAfterCutScoreHandlers.Capacity; i++)
		{
			_unusedAfterCutScoreHandlers.Add(new AfterCutScoreHandler(HandleAfterCutScoreHandlerDidFinish));
		}
		_hitObstacles = new HashSet<int>();
		_prevIntersectingObstacles = new List<ObstacleController>(10);
	}

	private void OnDestroy()
	{
		if (_scoreController != null)
		{
			_scoreController.noteWasCutEvent -= HandleScoreControllerNoteWasCut;
			_scoreController.noteWasMissedEvent -= HandleScoreControllerNoteWasMissed;
		}
		if (_beatmapObjectSpawnController != null)
		{
			_beatmapObjectSpawnController.obstacleDidPassAvoidedMarkEvent -= HandleBeatmapObjectSpawnControllerObstacleDidPassAvoidedMark;
		}
	}

	private void Update()
	{
		List<ObstacleController> intersectingObstacles = _playerHeadAndObstacleInteraction.intersectingObstacles;
		for (int i = 0; i < intersectingObstacles.Count; i++)
		{
			bool flag = false;
			for (int j = 0; j < _prevIntersectingObstacles.Count; j++)
			{
				if (intersectingObstacles[i] == _prevIntersectingObstacles[j])
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				int id = intersectingObstacles[i].obstacleData.id;
				_hitObstacles.Add(id);
				ObstacleExecutionRating item = new ObstacleExecutionRating(_songTime.value, ObstacleExecutionRating.Rating.NotGood);
				_beatmapObjectExecutionRatings.Add(item);
			}
		}
		_prevIntersectingObstacles.Clear();
		_prevIntersectingObstacles.AddRange(intersectingObstacles);
	}

	private void HandleAfterCutScoreHandlerDidFinish(AfterCutScoreHandler afterCutScoreHandler)
	{
		_unusedAfterCutScoreHandlers.Add(afterCutScoreHandler);
		_afterCutScoreHandlers.Remove(afterCutScoreHandler);
	}

	private void HandleScoreControllerNoteWasCut(NoteData noteData, NoteCutInfo noteCutInfo, int multiplier)
	{
		if (noteData.noteType == NoteType.Bomb)
		{
			BombExecutionRating item = new BombExecutionRating(noteData.time, BombExecutionRating.Rating.NotGood);
			_beatmapObjectExecutionRatings.Add(item);
		}
		else
		{
			if (noteData.noteType != 0 && noteData.noteType != NoteType.NoteB)
			{
				return;
			}
			NoteExecutionRating.Rating rating = ((!noteCutInfo.allIsOK) ? NoteExecutionRating.Rating.BadCut : NoteExecutionRating.Rating.GoodCut);
			ScoreController.ScoreWithoutMultiplier(noteCutInfo, null, out var beforeCutScore, out var _);
			NoteExecutionRating noteExecutionRating = new NoteExecutionRating(noteData.time, rating, beforeCutScore, noteCutInfo.timeDeviation, noteCutInfo.cutDirDeviation);
			_beatmapObjectExecutionRatings.Add(noteExecutionRating);
			if (noteCutInfo.allIsOK)
			{
				AfterCutScoreHandler afterCutScoreHandler = null;
				if (_unusedAfterCutScoreHandlers.Count > 0)
				{
					afterCutScoreHandler = _unusedAfterCutScoreHandlers[0];
					_unusedAfterCutScoreHandlers.RemoveAt(0);
				}
				else
				{
					afterCutScoreHandler = new AfterCutScoreHandler(HandleAfterCutScoreHandlerDidFinish);
				}
				afterCutScoreHandler.Set(noteCutInfo, noteExecutionRating, noteCutInfo.afterCutSwingRatingCounter);
				_afterCutScoreHandlers.Add(afterCutScoreHandler);
			}
		}
	}

	private void HandleScoreControllerNoteWasMissed(NoteData noteData, int multiplier)
	{
		if (noteData.noteType == NoteType.Bomb)
		{
			BombExecutionRating item = new BombExecutionRating(noteData.time, BombExecutionRating.Rating.OK);
			_beatmapObjectExecutionRatings.Add(item);
		}
		else if (noteData.noteType == NoteType.NoteA || noteData.noteType == NoteType.NoteB)
		{
			NoteExecutionRating item2 = new NoteExecutionRating(noteData.time, NoteExecutionRating.Rating.Missed, 0, 0f, 0f);
			_beatmapObjectExecutionRatings.Add(item2);
		}
	}

	private void HandleBeatmapObjectSpawnControllerObstacleDidPassAvoidedMark(BeatmapObjectSpawnController beatmapObjectSpawnController, ObstacleController obstacleController)
	{
		if (_hitObstacles.Contains(obstacleController.obstacleData.id))
		{
			_hitObstacles.Remove(obstacleController.obstacleData.id);
			return;
		}
		ObstacleExecutionRating item = new ObstacleExecutionRating(obstacleController.obstacleData.time + obstacleController.obstacleData.duration, ObstacleExecutionRating.Rating.OK);
		_beatmapObjectExecutionRatings.Add(item);
	}
}
