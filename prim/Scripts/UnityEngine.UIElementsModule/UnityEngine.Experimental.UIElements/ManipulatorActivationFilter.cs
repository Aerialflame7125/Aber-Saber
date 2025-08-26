namespace UnityEngine.Experimental.UIElements;

public struct ManipulatorActivationFilter
{
	public MouseButton button;

	public EventModifiers modifiers;

	public int clickCount;

	public bool Matches(IMouseEvent e)
	{
		bool flag = clickCount == 0 || e.clickCount >= clickCount;
		return button == (MouseButton)e.button && HasModifiers(e) && flag;
	}

	private bool HasModifiers(IMouseEvent e)
	{
		if (((modifiers & EventModifiers.Alt) != 0 && !e.altKey) || ((modifiers & EventModifiers.Alt) == 0 && e.altKey))
		{
			return false;
		}
		if (((modifiers & EventModifiers.Control) != 0 && !e.ctrlKey) || ((modifiers & EventModifiers.Control) == 0 && e.ctrlKey))
		{
			return false;
		}
		if (((modifiers & EventModifiers.Shift) != 0 && !e.shiftKey) || ((modifiers & EventModifiers.Shift) == 0 && e.shiftKey))
		{
			return false;
		}
		return ((modifiers & EventModifiers.Command) == 0 || e.commandKey) && ((modifiers & EventModifiers.Command) != 0 || !e.commandKey);
	}
}
