namespace UnityEngine.Experimental.UIElements;

public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
{
	public MouseEnterWindowEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Cancellable;
	}
}
