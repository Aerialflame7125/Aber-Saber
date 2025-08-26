using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class EventsSelectedTool : MonoBehaviour
{
	[SerializeField]
	private Text _valueText;

	public ObstacleType selectedType { get; private set; }

	public int value { get; private set; }

	private void Awake()
	{
		value = 0;
		_valueText.text = "0";
	}

	public void IncreaseValue()
	{
		value++;
		value = Mathf.Clamp(value, 0, 7);
		_valueText.text = value.ToString();
	}

	public void DecreaseValue()
	{
		value--;
		value = Mathf.Clamp(value, 0, 7);
		_valueText.text = value.ToString();
	}
}
