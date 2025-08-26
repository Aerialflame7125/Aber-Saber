namespace UnityEngine.Experimental.UIElements;

public class FocusOutEvent : FocusEventBase<FocusOutEvent>
{
	public FocusOutEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Bubbles | EventFlags.Capturable;
	}
}
