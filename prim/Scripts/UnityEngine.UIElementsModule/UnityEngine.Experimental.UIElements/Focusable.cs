namespace UnityEngine.Experimental.UIElements;

public abstract class Focusable : CallbackEventHandler
{
	private int m_FocusIndex;

	public abstract FocusController focusController { get; }

	public int focusIndex
	{
		get
		{
			return m_FocusIndex;
		}
		set
		{
			m_FocusIndex = value;
		}
	}

	public virtual bool canGrabFocus => m_FocusIndex >= 0;

	protected Focusable()
	{
		m_FocusIndex = 0;
	}

	public virtual void Focus()
	{
		if (focusController != null)
		{
			focusController.SwitchFocus((!canGrabFocus) ? null : this);
		}
	}

	public virtual void Blur()
	{
		if (focusController != null && focusController.focusedElement == this)
		{
			focusController.SwitchFocus(null);
		}
	}

	protected internal override void ExecuteDefaultAction(EventBase evt)
	{
		base.ExecuteDefaultAction(evt);
		if (evt.GetEventTypeId() == EventBase<MouseDownEvent>.TypeId())
		{
			Focus();
		}
		if (focusController != null)
		{
			focusController.SwitchFocusOnEvent(evt);
		}
	}
}
