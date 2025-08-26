using UnityEngine;

[CreateAssetMenu(fileName = "ColorManager", menuName = "BS/Others/ColorManager")]
public class ColorManager : ScriptableObject
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private SimpleColorSO _colorA;

	[SerializeField]
	private SimpleColorSO _colorB;

	[SerializeField]
	private Color _multiplierNoteColor;

	public Color colorA => _mainSettingsModel.swapColors ? _colorB : _colorA;

	public Color colorB => _mainSettingsModel.swapColors ? _colorA : _colorB;

	private Color ColorForType(NoteType type)
	{
		if (type == NoteType.NoteB)
		{
			return colorB;
		}
		return colorA;
	}

	public Color ColorForNoteType(NoteType type)
	{
		Color color = ((type != NoteType.NoteB) ? colorA : colorB);
		return color * _multiplierNoteColor;
	}

	public Color ColorForSaberType(Saber.SaberType type)
	{
		if (type == Saber.SaberType.SaberB)
		{
			return colorB;
		}
		return colorA;
	}
}
