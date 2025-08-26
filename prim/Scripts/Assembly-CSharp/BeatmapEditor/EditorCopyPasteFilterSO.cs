using System;
using UnityEngine;

namespace BeatmapEditor;

public class EditorCopyPasteFilterSO : ScriptableObject
{
	[NonSerialized]
	public bool copyBaseNotes;

	[NonSerialized]
	public bool copyUpperNotes;

	[NonSerialized]
	public bool copyTopNotes;

	[NonSerialized]
	public bool copyEvents;

	[NonSerialized]
	public bool copyObstacles;

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		copyBaseNotes = true;
	}
}
