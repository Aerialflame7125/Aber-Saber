using UnityEngine;

public class NoteData : BeatmapObjectData
{
	public NoteType noteType { get; private set; }

	public NoteCutDirection cutDirection { get; private set; }

	public NoteLineLayer noteLineLayer { get; private set; }

	public NoteLineLayer startNoteLineLayer { get; private set; }

	public int flipLineIndex { get; private set; }

	public float flipYSide { get; private set; }

	public NoteData(int id, float time, int lineIndex, NoteLineLayer noteLineLayer, NoteLineLayer startNoteLineLayer, NoteType noteType, NoteCutDirection cutDirection)
	{
		base.beatmapObjectType = BeatmapObjectType.Note;
		base.id = id;
		base.time = time;
		base.lineIndex = lineIndex;
		this.noteLineLayer = noteLineLayer;
		this.startNoteLineLayer = startNoteLineLayer;
		this.noteType = noteType;
		this.cutDirection = cutDirection;
		flipLineIndex = lineIndex;
		flipYSide = 0f;
	}

	public override BeatmapObjectData GetCopy()
	{
		NoteData noteData = new NoteData(base.id, base.time, base.lineIndex, noteLineLayer, startNoteLineLayer, noteType, cutDirection);
		noteData.flipLineIndex = flipLineIndex;
		noteData.flipYSide = flipYSide;
		return noteData;
	}

	public void SetNoteFlipToNote(NoteData targetNote)
	{
		flipLineIndex = targetNote.lineIndex;
		if (noteLineLayer == targetNote.noteLineLayer)
		{
			flipYSide = ((base.lineIndex > targetNote.lineIndex) ? 1 : (-1));
		}
	}

	public void SwitchNoteType()
	{
		if (noteType == NoteType.NoteA)
		{
			noteType = NoteType.NoteB;
		}
		else if (noteType == NoteType.NoteB)
		{
			noteType = NoteType.NoteA;
		}
	}

	public void MirrorTransformCutDirection()
	{
		if (cutDirection == NoteCutDirection.Left)
		{
			cutDirection = NoteCutDirection.Right;
		}
		else if (cutDirection == NoteCutDirection.Right)
		{
			cutDirection = NoteCutDirection.Left;
		}
		else if (cutDirection == NoteCutDirection.UpLeft)
		{
			cutDirection = NoteCutDirection.UpRight;
		}
		else if (cutDirection == NoteCutDirection.UpRight)
		{
			cutDirection = NoteCutDirection.UpLeft;
		}
		else if (cutDirection == NoteCutDirection.DownLeft)
		{
			cutDirection = NoteCutDirection.DownRight;
		}
		else if (cutDirection == NoteCutDirection.DownRight)
		{
			cutDirection = NoteCutDirection.DownLeft;
		}
	}

	public void SetNoteToAnyCutDirection()
	{
		cutDirection = NoteCutDirection.Any;
	}

	public void TransformNoteAOrBToRandomType()
	{
		if (noteType == NoteType.NoteA || noteType == NoteType.NoteB)
		{
			if (Random.Range(0f, 1f) > 0.6f)
			{
				noteType = ((noteType == NoteType.NoteA) ? NoteType.NoteB : NoteType.NoteA);
			}
			flipLineIndex = base.lineIndex;
			flipYSide = 0f;
		}
	}

	public override void MirrorLineIndex(int lineCount)
	{
		base.lineIndex = lineCount - 1 - base.lineIndex;
		flipLineIndex = lineCount - 1 - flipLineIndex;
	}
}
