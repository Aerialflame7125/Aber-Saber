using UnityEngine;

public class TutorialSongController : SongController
{
	public class TutorialObjectSpawnData
	{
		public GameEvent gameEvent;

		public int beatOffset;

		public int firstTimeBeatOffset;

		public TutorialObjectSpawnData(GameEvent gameEvent, int firstTimeBeatOffset, int beatOffset)
		{
			this.gameEvent = gameEvent;
			this.firstTimeBeatOffset = firstTimeBeatOffset;
			this.beatOffset = beatOffset;
		}
	}

	public class TutorialNoteSpawnData : TutorialObjectSpawnData
	{
		public int lineIndex;

		public NoteLineLayer noteLineLayer;

		public NoteCutDirection cutDirection;

		public NoteType noteType;

		public TutorialNoteSpawnData(GameEvent gameEvent, int firstTimeBeatOffset, int beatOffset, int line, NoteLineLayer noteLineLayer, NoteCutDirection cutDirection, NoteType noteType)
			: base(gameEvent, firstTimeBeatOffset, beatOffset)
		{
			base.gameEvent = gameEvent;
			this.noteLineLayer = noteLineLayer;
			lineIndex = line;
			this.cutDirection = cutDirection;
			this.noteType = noteType;
		}
	}

	public class TutorialObstacleSpawnData : TutorialObjectSpawnData
	{
		public int lineIndex;

		public int width;

		public ObstacleType obstacleType;

		public TutorialObstacleSpawnData(GameEvent gameEvent, int firstTimeBeatOffset, int beatOffset, int lineIndex, int width, ObstacleType obstacleType)
			: base(gameEvent, firstTimeBeatOffset, beatOffset)
		{
			base.gameEvent = gameEvent;
			this.lineIndex = lineIndex;
			this.width = width;
			this.obstacleType = obstacleType;
		}
	}

	[SerializeField]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	[SerializeField]
	private BeatmapDataModel _beatmapDataModel;

	[SerializeField]
	private AudioClip _audioClip;

	[SerializeField]
	private AudioTimeSyncController _audioTimeSyncController;

	[SerializeField]
	private float _songBPM = 1f;

	[SerializeField]
	private int _startWaitTimeInBeats = 8;

	[SerializeField]
	private int _numberOfBeatsToSnap = 8;

	[SerializeField]
	private int _obstacleDurationInBeats = 2;

	[Space]
	[SerializeField]
	[EventSender]
	private GameEvent noteCuttingTutorialPartDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteCuttingInAnyDirectionDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent bombCuttingTutorialPartDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent leftObstacleTutorialPartDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent rightObstacleTutorialPartDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent topObstacleTutorialPartDidStartEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteWasCutOKEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteWasCutTooSoonEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteWasCutWithWrongColorEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteWasCutFromDifferentDirectionEvent;

	[SerializeField]
	[EventSender]
	private GameEvent noteWasCutWithSlowSpeedEvent;

	[SerializeField]
	[EventSender]
	private GameEvent bombWasCutEvent;

	private int _tutorialBeatmapObjectIndex;

	private int _prevSpawnedBeatmapObjectIndex = -1;

	private int _nextBeatmapObjectId;

	private TutorialObjectSpawnData[] _normalModeTutorialObjectsSpawnData;

	private TutorialObjectSpawnData[] _specialModeTutorialObjectsSpawnData;

	public bool specialTutorialMode { get; set; }

	protected void Awake()
	{
		_normalModeTutorialObjectsSpawnData = new TutorialObjectSpawnData[13]
		{
			new TutorialNoteSpawnData(noteCuttingTutorialPartDidStartEvent, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Down, NoteType.NoteB),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Down, NoteType.NoteA),
			new TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Right, NoteType.NoteB),
			new TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Upper, NoteCutDirection.Right, NoteType.NoteA),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Upper, NoteCutDirection.Left, NoteType.NoteB),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Up, NoteType.NoteA),
			new TutorialNoteSpawnData(noteCuttingInAnyDirectionDidStartEvent, 9, 9, 2, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteB),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialNoteSpawnData(bombCuttingTutorialPartDidStartEvent, 17, 9, 2, NoteLineLayer.Base, NoteCutDirection.None, NoteType.Bomb),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.None, NoteType.Bomb),
			new TutorialObstacleSpawnData(rightObstacleTutorialPartDidStartEvent, 9, 9, 2, 1, ObstacleType.FullHeight),
			new TutorialObstacleSpawnData(leftObstacleTutorialPartDidStartEvent, 9, 9, 1, 1, ObstacleType.FullHeight),
			new TutorialObstacleSpawnData(topObstacleTutorialPartDidStartEvent, 9, 9, 0, 4, ObstacleType.Top)
		};
		_specialModeTutorialObjectsSpawnData = new TutorialObjectSpawnData[4]
		{
			new TutorialNoteSpawnData(noteCuttingInAnyDirectionDidStartEvent, 9, 9, 2, NoteLineLayer.Base, NoteCutDirection.Any, NoteType.NoteB),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Base, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialNoteSpawnData(null, 9, 9, 1, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteA),
			new TutorialNoteSpawnData(null, 9, 9, 2, NoteLineLayer.Top, NoteCutDirection.Any, NoteType.NoteB)
		};
	}

	private void Start()
	{
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCutEvent;
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissed;
		_beatmapObjectSpawnController.obstacleDidPassThreeQuartersOfMove2Event += HandleObstacleDidPassThreeQuartersOfMove2;
		_audioTimeSyncController.Init(_audioClip, 0f, 0f);
		_beatmapObjectSpawnController.Init(_songBPM, 4, 10f);
		BeatmapData beatmapData = new BeatmapData(CreateBeatmapLines(1, 100), new BeatmapEventData[0]);
		_beatmapDataModel.beatmapData = beatmapData;
	}

	protected void OnDestroy()
	{
		_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCutEvent;
		_beatmapObjectSpawnController.noteWasMissedEvent -= HandleNoteWasMissed;
		_beatmapObjectSpawnController.obstacleDidPassThreeQuartersOfMove2Event -= HandleObstacleDidPassThreeQuartersOfMove2;
	}

	public override void StartSong()
	{
		float num = 60f / _songBPM;
		UpdateBeatmapData(num * (float)_startWaitTimeInBeats);
		_audioTimeSyncController.StartSong();
	}

	public override void StopSong()
	{
		_audioTimeSyncController.StopSong();
	}

	public override void PauseSong()
	{
		_audioTimeSyncController.Pause();
	}

	public override void ResumeSong()
	{
		_audioTimeSyncController.Resume();
	}

	private void HandleNoteWasCutEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		if (noteController.noteData.noteType == NoteType.Bomb)
		{
			bombWasCutEvent.Raise();
			UpdateBeatmapData(-1f);
			return;
		}
		if (specialTutorialMode)
		{
			if (!noteCutInfo.allExceptSaberTypeIsOK || noteCutInfo.saberTypeOK)
			{
				UpdateBeatmapData(-1f);
				return;
			}
		}
		else if (!noteCutInfo.allIsOK)
		{
			if (noteCutInfo.wasCutTooSoon)
			{
				noteWasCutTooSoonEvent.Raise();
			}
			else if (!noteCutInfo.saberTypeOK)
			{
				noteWasCutWithWrongColorEvent.Raise();
			}
			else if (!noteCutInfo.speedOK)
			{
				noteWasCutWithSlowSpeedEvent.Raise();
			}
			else if (!noteCutInfo.directionOK)
			{
				noteWasCutFromDifferentDirectionEvent.Raise();
			}
			UpdateBeatmapData(-1f);
			return;
		}
		noteWasCutOKEvent.Raise();
		_tutorialBeatmapObjectIndex++;
		UpdateBeatmapData(-1f);
	}

	private void HandleNoteWasMissed(BeatmapObjectSpawnController noteSpawnController, NoteController noteController)
	{
		if (noteController.noteData.noteType == NoteType.NoteA || noteController.noteData.noteType == NoteType.NoteB)
		{
			UpdateBeatmapData(-1f);
		}
		else if (noteController.noteData.noteType == NoteType.Bomb)
		{
			noteWasCutOKEvent.Raise();
			_tutorialBeatmapObjectIndex++;
			UpdateBeatmapData(-1f);
		}
	}

	private void HandleObstacleDidPassThreeQuartersOfMove2(BeatmapObjectSpawnController spawnController, ObstacleController obstacleController)
	{
		_tutorialBeatmapObjectIndex++;
		UpdateBeatmapData(-1f);
	}

	private void UpdateBeatmapData(float noteTime)
	{
		TutorialObjectSpawnData[] array = ((!specialTutorialMode) ? _normalModeTutorialObjectsSpawnData : _specialModeTutorialObjectsSpawnData);
		if (_tutorialBeatmapObjectIndex >= array.Length)
		{
			SendSongDidFinishEvent();
			return;
		}
		BeatmapLineData[] beatmapLinesData = null;
		TutorialObjectSpawnData tutorialObjectSpawnData = array[_tutorialBeatmapObjectIndex];
		bool flag = _prevSpawnedBeatmapObjectIndex != _tutorialBeatmapObjectIndex;
		if (flag && tutorialObjectSpawnData.gameEvent != null)
		{
			tutorialObjectSpawnData.gameEvent.Raise();
		}
		float time = ((!(noteTime > 0f)) ? GetNextBeatmapObjectTime((!flag) ? tutorialObjectSpawnData.beatOffset : tutorialObjectSpawnData.firstTimeBeatOffset) : noteTime);
		if (tutorialObjectSpawnData is TutorialNoteSpawnData)
		{
			if (specialTutorialMode)
			{
				int num = _nextBeatmapObjectId % 4;
				tutorialObjectSpawnData = _specialModeTutorialObjectsSpawnData[num];
			}
			beatmapLinesData = CreateBeatmapLinesData(time, tutorialObjectSpawnData as TutorialNoteSpawnData);
		}
		else if (tutorialObjectSpawnData is TutorialObstacleSpawnData)
		{
			beatmapLinesData = CreateBeatmapLinesData(time, tutorialObjectSpawnData as TutorialObstacleSpawnData);
		}
		BeatmapData beatmapData = new BeatmapData(beatmapLinesData, new BeatmapEventData[0]);
		_beatmapDataModel.beatmapData = beatmapData;
		_prevSpawnedBeatmapObjectIndex = _tutorialBeatmapObjectIndex;
	}

	private float GetNextBeatmapObjectTime(int beatOffset)
	{
		float num = 60f / _songBPM;
		int num2 = (int)(_audioTimeSyncController.songTime / (num * (float)_numberOfBeatsToSnap) + 0.5f) * _numberOfBeatsToSnap + 1;
		num2 += beatOffset - 1;
		return (float)(num2 - 1) * num;
	}

	private BeatmapLineData[] CreateBeatmapLinesData(float time, TutorialObstacleSpawnData tutorialObstacleSpawnData)
	{
		float num = 60f / _songBPM;
		int lineIndex = tutorialObstacleSpawnData.lineIndex;
		BeatmapLineData[] array = CreateBeatmapLines(4, lineIndex);
		array[lineIndex].beatmapObjectsData[0] = new ObstacleData(_nextBeatmapObjectId++, time, lineIndex, tutorialObstacleSpawnData.obstacleType, (float)_obstacleDurationInBeats * num, tutorialObstacleSpawnData.width);
		return array;
	}

	private BeatmapLineData[] CreateBeatmapLinesData(float time, TutorialNoteSpawnData tutorialNoteSpawnData)
	{
		int lineIndex = tutorialNoteSpawnData.lineIndex;
		NoteLineLayer noteLineLayer = tutorialNoteSpawnData.noteLineLayer;
		BeatmapLineData[] array = CreateBeatmapLines(4, lineIndex);
		array[lineIndex].beatmapObjectsData[0] = new NoteData(_nextBeatmapObjectId++, time, lineIndex, noteLineLayer, NoteLineLayer.Base, tutorialNoteSpawnData.noteType, tutorialNoteSpawnData.cutDirection);
		return array;
	}

	private BeatmapLineData[] CreateBeatmapLines(int lineCount, int activeLineIndex)
	{
		BeatmapLineData[] array = new BeatmapLineData[lineCount];
		for (int i = 0; i < lineCount; i++)
		{
			array[i] = new BeatmapLineData();
			bool flag = activeLineIndex == i || activeLineIndex == -1;
			array[i].beatmapObjectsData = new BeatmapObjectData[flag ? 1 : 0];
		}
		return array;
	}
}
