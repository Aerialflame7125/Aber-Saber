namespace UnityEngine.Experimental.UIElements;

public class MouseEnterEvent : MouseEventBase<MouseEnterEvent>
{
	public MouseEnterEvent()
	{
		Init();
	}

	protected override void Init()
	{
		base.Init();
		base.flags = EventFlags.Capturable;
	}
}
