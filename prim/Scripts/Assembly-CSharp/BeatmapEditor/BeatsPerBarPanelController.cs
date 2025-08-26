using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class BeatsPerBarPanelController : MonoBehaviour
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private EditorPopUpInfoPanelController _popUpInfoPanelController;

	[SerializeField]
	private Text _beatsPerBarText;

	private void Start()
	{
		RefreshUI();
		_editorBeatmap.didChangeAllDataEvent += HandleEditorBeatmapDidChangeAllData;
	}

	private void OnDestroy()
	{
		if (_editorBeatmap != null)
		{
			_editorBeatmap.didChangeAllDataEvent -= HandleEditorBeatmapDidChangeAllData;
		}
	}

	private void RefreshUI()
	{
		_beatsPerBarText.text = _editorBeatmap.beatsPerBar.ToString();
	}

	private void HandleEditorBeatmapDidChangeAllData()
	{
		RefreshUI();
	}

	public void Stretch2xButtonPressed()
	{
		if (_editorBeatmap.beatsPerBar < 64)
		{
			_beatsPerBarText.text = _editorBeatmap.beatsPerBar.ToString();
			_editorBeatmap.Stretch2x();
		}
	}

	public void Squish2xButtonPressed(bool forced)
	{
		if (_editorBeatmap.beatsPerBar <= 4)
		{
			return;
		}
		if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
		{
			forced = true;
		}
		if (!forced && !_editorBeatmap.CanSquish2x(out var problematicBeatIndex))
		{
			if (problematicBeatIndex >= 0)
			{
				_popUpInfoPanelController.ShowInfo("Can't squish. Problematic beat at " + problematicBeatIndex / _editorBeatmap.beatsPerBar + ":" + (problematicBeatIndex % _editorBeatmap.beatsPerBar + 1).ToString(), EditorPopUpInfoPanelController.InfoType.Warning);
			}
		}
		else
		{
			_beatsPerBarText.text = _editorBeatmap.beatsPerBar.ToString();
			_editorBeatmap.Squish2x();
		}
	}
}
