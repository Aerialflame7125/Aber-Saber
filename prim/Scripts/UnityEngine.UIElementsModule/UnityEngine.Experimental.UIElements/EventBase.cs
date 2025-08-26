using System;

namespace UnityEngine.Experimental.UIElements;

public abstract class EventBase : IDisposable
{
	[Flags]
	public enum EventFlags
	{
		None = 0,
		Bubbles = 1,
		Capturable = 2,
		Cancellable = 4,
		Pooled = 0x100
	}

	private static long s_LastTypeId = 0L;

	protected IEventHandler m_CurrentTarget;

	private Event m_ImguiEvent;

	private Vector2 m_OriginalMousePosition;

	public long timestamp { get; private set; }

	protected EventFlags flags { get; set; }

	public bool bubbles => (flags & EventFlags.Bubbles) != 0;

	public bool capturable => (flags & EventFlags.Capturable) != 0;

	public IEventHandler target { get; internal set; }

	public bool isPropagationStopped { get; private set; }

	public bool isImmediatePropagationStopped { get; private set; }

	public bool isDefaultPrevented { get; private set; }

	public PropagationPhase propagationPhase { get; internal set; }

	public virtual IEventHandler currentTarget
	{
		get
		{
			return m_CurrentTarget;
		}
		internal set
		{
			m_CurrentTarget = value;
			if (imguiEvent != null && currentTarget is VisualElement ele)
			{
				imguiEvent.mousePosition = ele.WorldToLocal(m_OriginalMousePosition);
			}
		}
	}

	public bool dispatch { get; internal set; }

	public Event imguiEvent
	{
		get
		{
			return m_ImguiEvent;
		}
		protected set
		{
			m_ImguiEvent = value;
			if (m_ImguiEvent != null)
			{
				originalMousePosition = value.mousePosition;
			}
		}
	}

	public Vector2 originalMousePosition
	{
		get
		{
			return m_OriginalMousePosition;
		}
		private set
		{
			m_OriginalMousePosition = value;
		}
	}

	protected EventBase()
	{
		Init();
	}

	protected static long RegisterEventType()
	{
		return ++s_LastTypeId;
	}

	public abstract long GetEventTypeId();

	public void StopPropagation()
	{
		isPropagationStopped = true;
	}

	public void StopImmediatePropagation()
	{
		isPropagationStopped = true;
		isImmediatePropagationStopped = true;
	}

	public void PreventDefault()
	{
		if ((flags & EventFlags.Cancellable) == EventFlags.Cancellable)
		{
			isDefaultPrevented = true;
		}
	}

	protected virtual void Init()
	{
		timestamp = DateTime.Now.Ticks;
		flags = EventFlags.None;
		target = null;
		isPropagationStopped = false;
		isImmediatePropagationStopped = false;
		isDefaultPrevented = false;
		propagationPhase = PropagationPhase.None;
		m_CurrentTarget = null;
		dispatch = false;
		imguiEvent = null;
		originalMousePosition = Vector2.zero;
	}

	public abstract void Dispose();
}
public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
{
	private static readonly long s_TypeId = EventBase.RegisterEventType();

	private static readonly EventPool<T> s_Pool = new EventPool<T>();

	public static long TypeId()
	{
		return s_TypeId;
	}

	public static T GetPooled()
	{
		T result = s_Pool.Get();
		result.Init();
		result.flags |= EventFlags.Pooled;
		return result;
	}

	protected static void ReleasePooled(T evt)
	{
		if ((evt.flags & EventFlags.Pooled) == EventFlags.Pooled)
		{
			s_Pool.Release(evt);
			evt.flags &= ~EventFlags.Pooled;
			evt.target = null;
		}
	}

	public override void Dispose()
	{
		ReleasePooled((T)this);
	}

	public override long GetEventTypeId()
	{
		return s_TypeId;
	}
}
