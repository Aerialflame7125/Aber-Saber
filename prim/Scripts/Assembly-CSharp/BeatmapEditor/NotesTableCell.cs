using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class NotesTableCell : BeatmapEditorTableCell
{
	[SerializeField]
	private Image[] _backgrounds;

	[SerializeField]
	private Image[] _icons;

	[SerializeField]
	private Sprite _arrowNoteSprite;

	[SerializeField]
	private Sprite _ghostNoteSprite;

	[SerializeField]
	private Sprite _bombNoteSprite;

	private RectTransform[] _iconTransforms;

	private void Awake()
	{
		_iconTransforms = new RectTransform[4];
		for (int i = 0; i < 4; i++)
		{
			_iconTransforms[i] = _icons[i].rectTransform;
		}
	}

	public void SetLineActive(int lineIndex, bool active)
	{
		_backgrounds[lineIndex].gameObject.SetActive(active);
	}

	public void SetLineType(int noteIdx, NoteType type, NoteCutDirection dir)
	{
		if (type == NoteType.NoteA || type == NoteType.NoteB)
		{
			if (!_backgrounds[noteIdx].enabled)
			{
				_backgrounds[noteIdx].enabled = true;
			}
			_backgrounds[noteIdx].color = ((type != 0) ? Color.blue : Color.red);
			_icons[noteIdx].sprite = _arrowNoteSprite;
			float z = 0f;
			switch (dir)
			{
			case NoteCutDirection.Up:
				z = 0f;
				break;
			case NoteCutDirection.Down:
				z = 180f;
				break;
			case NoteCutDirection.Left:
				z = 90f;
				break;
			case NoteCutDirection.Right:
				z = -90f;
				break;
			case NoteCutDirection.UpLeft:
				z = 45f;
				break;
			case NoteCutDirection.UpRight:
				z = -45f;
				break;
			case NoteCutDirection.DownLeft:
				z = 135f;
				break;
			case NoteCutDirection.DownRight:
				z = -135f;
				break;
			}
			if (dir == NoteCutDirection.Any)
			{
				z = 0f;
				if (_icons[noteIdx].enabled)
				{
					_icons[noteIdx].enabled = false;
				}
			}
			else if (!_icons[noteIdx].enabled)
			{
				_icons[noteIdx].enabled = true;
			}
			_iconTransforms[noteIdx].localEulerAngles = new Vector3(0f, 0f, z);
		}
		else
		{
			if (_backgrounds[noteIdx].enabled)
			{
				_backgrounds[noteIdx].enabled = false;
			}
			if (!_icons[noteIdx].enabled)
			{
				_icons[noteIdx].enabled = true;
			}
			switch (type)
			{
			case NoteType.GhostNote:
				_icons[noteIdx].sprite = _ghostNoteSprite;
				break;
			case NoteType.Bomb:
				_icons[noteIdx].sprite = _bombNoteSprite;
				break;
			}
			_iconTransforms[noteIdx].localEulerAngles = new Vector3(0f, 0f, 0f);
		}
	}
}
