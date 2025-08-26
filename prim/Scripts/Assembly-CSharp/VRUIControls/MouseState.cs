using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace VRUIControls;

public class MouseState
{
	private List<ButtonState> m_TrackedButtons = new List<ButtonState>();

	public bool AnyPressesThisFrame()
	{
		for (int i = 0; i < m_TrackedButtons.Count; i++)
		{
			if (m_TrackedButtons[i].eventData.PressedThisFrame())
			{
				return true;
			}
		}
		return false;
	}

	public bool AnyReleasesThisFrame()
	{
		for (int i = 0; i < m_TrackedButtons.Count; i++)
		{
			if (m_TrackedButtons[i].eventData.ReleasedThisFrame())
			{
				return true;
			}
		}
		return false;
	}

	public ButtonState GetButtonState(PointerEventData.InputButton button)
	{
		ButtonState buttonState = null;
		for (int i = 0; i < m_TrackedButtons.Count; i++)
		{
			if (m_TrackedButtons[i].button == button)
			{
				buttonState = m_TrackedButtons[i];
				break;
			}
		}
		if (buttonState == null)
		{
			ButtonState buttonState2 = new ButtonState();
			buttonState2.button = button;
			buttonState2.eventData = new MouseButtonEventData();
			buttonState2.pressedValue = 0f;
			buttonState = buttonState2;
			m_TrackedButtons.Add(buttonState);
		}
		return buttonState;
	}

	public void SetButtonState(PointerEventData.InputButton button, PointerEventData.FramePressState stateForMouseButton, PointerEventData data)
	{
		ButtonState buttonState = GetButtonState(button);
		buttonState.eventData.buttonState = stateForMouseButton;
		buttonState.eventData.buttonData = data;
	}
}
