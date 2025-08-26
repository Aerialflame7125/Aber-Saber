using UnityEngine;

namespace BeatmapEditor;

public class EditorSelectedNoteCutDirectionSO : ObservableVariableSO<NoteCutDirection>
{
	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		value = NoteCutDirection.Up;
	}
}
