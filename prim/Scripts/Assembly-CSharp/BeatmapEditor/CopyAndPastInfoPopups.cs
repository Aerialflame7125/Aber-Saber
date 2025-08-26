using UnityEngine;

namespace BeatmapEditor;

public class CopyAndPastInfoPopups : MonoBehaviour
{
	[SerializeField]
	private CopyAndPasteController _copyAndPasteController;

	[SerializeField]
	private EditorPopUpInfoPanelController _popupInfoPanelController;

	private void Start()
	{
		_copyAndPasteController.dataCopiedEvent += HandleCopyAndPasteControllerDataCopied;
	}

	private void OnDestroy()
	{
		if (_copyAndPasteController != null)
		{
			_copyAndPasteController.dataCopiedEvent -= HandleCopyAndPasteControllerDataCopied;
		}
	}

	private void HandleCopyAndPasteControllerDataCopied(int numberOfBeats)
	{
		if (numberOfBeats > 0)
		{
			_popupInfoPanelController.ShowInfo(numberOfBeats + " beats copied", EditorPopUpInfoPanelController.InfoType.Info);
		}
		else
		{
			_popupInfoPanelController.ShowInfo("Nothing was copied. No columns are selected or there is no data.", EditorPopUpInfoPanelController.InfoType.Warning);
		}
	}
}
