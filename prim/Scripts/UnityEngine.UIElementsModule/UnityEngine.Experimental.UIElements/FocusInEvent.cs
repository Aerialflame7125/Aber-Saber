namespace UnityEngine.Experimental.UIElements;

public class FocusInEvent : FocusEventBase<FocusInEvent>
{
	public FocusInEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Bubbles | EventFlags.Capturable;
	}
}
