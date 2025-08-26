namespace UnityEngine.Experimental.UIElements;

public class MouseLeaveEvent : MouseEventBase<MouseLeaveEvent>
{
	public MouseLeaveEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Capturable;
	}
}
