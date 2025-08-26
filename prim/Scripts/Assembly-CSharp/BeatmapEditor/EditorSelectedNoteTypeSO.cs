using UnityEngine;

namespace BeatmapEditor;

public class EditorSelectedNoteTypeSO : ObservableVariableSO<NoteType>
{
	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		value = NoteType.NoteA;
	}
}
