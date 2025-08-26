using System;
using UnityEngine;

public class GameEnergyCounter : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(BeatmapObjectSpawnController))]
	private ObjectProvider _beatmapObjectSpawnControllerProvider;

	[SerializeField]
	private PlayerHeadAndObstacleInteraction _playerHeadAndObstacleInteraction;

	[SerializeField]
	private float _badNoteEnergyDrain = 0.1f;

	[SerializeField]
	private float _missNoteEnergyDrain = 0.1f;

	[SerializeField]
	private float _hitBombEnergyDrain = 0.15f;

	[SerializeField]
	private float _goodNoteEnergyCharge = 0.05f;

	[SerializeField]
	private float _obstacleEnergyDrainPerSecond = 0.1f;

	private float _energy = 0.5f;

	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private bool _cannotFail;

	public float energy => _energy;

	public bool pause { get; set; }

	public event Action gameEnergyDidReach0Event;

	public event Action<float> gameEnergyDidChangeEvent;

	private void Start()
	{
		_beatmapObjectSpawnController = _beatmapObjectSpawnControllerProvider.GetProvidedObject<BeatmapObjectSpawnController>();
		_beatmapObjectSpawnController.noteWasCutEvent += HandleNoteWasCutEvent;
		_beatmapObjectSpawnController.noteWasMissedEvent += HandleNoteWasMissedEvent;
	}

	private void OnDestroy()
	{
		if (_beatmapObjectSpawnController != null)
		{
			_beatmapObjectSpawnController.noteWasCutEvent -= HandleNoteWasCutEvent;
			_beatmapObjectSpawnController.noteWasMissedEvent -= HandleNoteWasMissedEvent;
		}
	}

	private void Update()
	{
		if (!pause && _playerHeadAndObstacleInteraction.intersectingObstacles.Count > 0)
		{
			AddEnergy(Time.deltaTime * (0f - _obstacleEnergyDrainPerSecond));
		}
	}

	private void HandleNoteWasCutEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController, NoteCutInfo noteCutInfo)
	{
		NoteType noteType = noteController.noteData.noteType;
		if (noteType == NoteType.Bomb || noteType == NoteType.NoteA || noteType == NoteType.NoteB)
		{
			if (noteCutInfo.allIsOK)
			{
				AddEnergy(_goodNoteEnergyCharge);
			}
			else if (noteType == NoteType.Bomb)
			{
				AddEnergy(0f - _hitBombEnergyDrain);
			}
			else
			{
				AddEnergy(0f - _badNoteEnergyDrain);
			}
		}
	}

	private void HandleNoteWasMissedEvent(BeatmapObjectSpawnController noteSpawnController, NoteController noteController)
	{
		NoteType noteType = noteController.noteData.noteType;
		if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
		{
			AddEnergy(0f - _missNoteEnergyDrain);
		}
	}

	private void AddEnergy(float value)
	{
		if (value < 0f)
		{
			if (_energy <= 0f)
			{
				return;
			}
			_energy += value;
			if (_energy <= 1E-05f)
			{
				_energy = 0f;
				if (this.gameEnergyDidReach0Event != null && !_cannotFail)
				{
					this.gameEnergyDidReach0Event();
				}
			}
		}
		else
		{
			if (_energy >= 1f)
			{
				return;
			}
			_energy += value;
			if (_energy >= 1f)
			{
				_energy = 1f;
			}
		}
		if (this.gameEnergyDidChangeEvent != null)
		{
			this.gameEnergyDidChangeEvent(_energy);
		}
	}

	public void Init(bool cannotFail)
	{
		_cannotFail = cannotFail;
	}
}
