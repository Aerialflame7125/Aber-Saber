using UnityEngine;
using UnityEngine.UI;

public class GameEventOnUIButtonClick : MonoBehaviour
{
	[SerializeField]
	[EventSender]
	private GameEvent _buttonClickedEvent;

	[SerializeField]
	private Button _button;

	private void OnReset()
	{
		_button = GetComponent<Button>();
	}

	private void Start()
	{
		_button.onClick.AddListener(_buttonClickedEvent.Raise);
	}

	private void OnDestroy()
	{
		if ((bool)_button)
		{
			_button.onClick.RemoveListener(_buttonClickedEvent.Raise);
		}
	}
}
