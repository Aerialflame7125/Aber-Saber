namespace UnityEngine.Experimental.UIElements;

public class PostLayoutEvent : EventBase<PostLayoutEvent>, IPropagatableEvent
{
	public bool hasNewLayout { get; private set; }

	public Rect oldRect { get; private set; }

	public Rect newRect { get; private set; }

	public PostLayoutEvent()
	{
		Init();
	}

	public static PostLayoutEvent GetPooled(bool hasNewLayout, Rect oldRect, Rect newRect)
	{
		PostLayoutEvent pooled = EventBase<PostLayoutEvent>.GetPooled();
		pooled.hasNewLayout = hasNewLayout;
		pooled.oldRect = oldRect;
		pooled.newRect = newRect;
		return pooled;
	}

	protected override void Init()
	{
		base.Init();
		hasNewLayout = false;
	}
}
