namespace UnityEngine.Experimental.UIElements;

public class WheelEvent : MouseEventBase<WheelEvent>
{
	public Vector3 delta { get; private set; }

	public WheelEvent()
	{
		Init();
	}

	public new static WheelEvent GetPooled(Event systemEvent)
	{
		WheelEvent pooled = MouseEventBase<WheelEvent>.GetPooled(systemEvent);
		pooled.imguiEvent = systemEvent;
		if (systemEvent != null)
		{
			pooled.delta = systemEvent.delta;
		}
		return pooled;
	}

	protected override void Init()
	{
		base.Init();
		delta = Vector3.zero;
	}
}
