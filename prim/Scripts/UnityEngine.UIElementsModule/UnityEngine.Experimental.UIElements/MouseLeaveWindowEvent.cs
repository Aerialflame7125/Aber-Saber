namespace UnityEngine.Experimental.UIElements;

public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
{
	public MouseLeaveWindowEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Cancellable;
	}
}
