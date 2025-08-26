using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class ShufflePanelController : MonoBehaviour
{
	[SerializeField]
	private ObservableFloatSO _shufflePeriod;

	[SerializeField]
	private Text _shufflePeriodText;

	private void Start()
	{
		RefreshUI();
		_shufflePeriod.didChangeEvent += HandleShufflePeriodDidChange;
	}

	private void OnDestroy()
	{
		if (_shufflePeriod != null)
		{
			_shufflePeriod.didChangeEvent -= HandleShufflePeriodDidChange;
		}
	}

	private void RefreshUI()
	{
		_shufflePeriodText.text = Mathf.RoundToInt(4f / (float)_shufflePeriod).ToString();
	}

	private void HandleShufflePeriodDidChange()
	{
		RefreshUI();
	}

	public void ShufflePeriodMul(float mul)
	{
		float num = (float)_shufflePeriod * mul;
		if (num > 0.5f)
		{
			_shufflePeriod.value = 0.5f;
		}
		else if (num < 0.125f)
		{
			_shufflePeriod.value = 0.125f;
		}
		else
		{
			_shufflePeriod.value = num;
		}
	}
}
