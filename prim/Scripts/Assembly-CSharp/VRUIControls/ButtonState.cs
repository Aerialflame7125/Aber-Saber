using UnityEngine.EventSystems;

namespace VRUIControls;

public class ButtonState
{
	private PointerEventData.InputButton _button;

	private MouseButtonEventData _eventData;

	private float _pressedValue;

	public MouseButtonEventData eventData
	{
		get
		{
			return _eventData;
		}
		set
		{
			_eventData = value;
		}
	}

	public PointerEventData.InputButton button
	{
		get
		{
			return _button;
		}
		set
		{
			_button = value;
		}
	}

	public float pressedValue
	{
		get
		{
			return _pressedValue;
		}
		set
		{
			_pressedValue = value;
		}
	}
}
