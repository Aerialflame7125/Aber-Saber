namespace UnityEngine.Experimental.UIElements;

public abstract class CallbackEventHandler : IEventHandler
{
	private EventCallbackRegistry m_CallbackRegistry;

	public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, Capture useCapture = Capture.NoCapture) where TEventType : EventBase<TEventType>, new()
	{
		if (m_CallbackRegistry == null)
		{
			m_CallbackRegistry = new EventCallbackRegistry();
		}
		m_CallbackRegistry.RegisterCallback(callback, useCapture);
	}

	public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, Capture useCapture = Capture.NoCapture) where TEventType : EventBase<TEventType>, new()
	{
		if (m_CallbackRegistry == null)
		{
			m_CallbackRegistry = new EventCallbackRegistry();
		}
		m_CallbackRegistry.RegisterCallback(callback, userArgs, useCapture);
	}

	public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, Capture useCapture = Capture.NoCapture) where TEventType : EventBase<TEventType>, new()
	{
		if (m_CallbackRegistry != null)
		{
			m_CallbackRegistry.UnregisterCallback(callback, useCapture);
		}
	}

	public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, Capture useCapture = Capture.NoCapture) where TEventType : EventBase<TEventType>, new()
	{
		if (m_CallbackRegistry != null)
		{
			m_CallbackRegistry.UnregisterCallback(callback, useCapture);
		}
	}

	public virtual void HandleEvent(EventBase evt)
	{
		if (evt.propagationPhase != PropagationPhase.DefaultAction)
		{
			if (!evt.isPropagationStopped && m_CallbackRegistry != null)
			{
				m_CallbackRegistry.InvokeCallbacks(evt);
			}
			if (evt.propagationPhase == PropagationPhase.AtTarget && !evt.isDefaultPrevented)
			{
				ExecuteDefaultActionAtTarget(evt);
			}
		}
		else if (!evt.isDefaultPrevented)
		{
			ExecuteDefaultAction(evt);
		}
	}

	public bool HasCaptureHandlers()
	{
		return m_CallbackRegistry != null && m_CallbackRegistry.HasCaptureHandlers();
	}

	public bool HasBubbleHandlers()
	{
		return m_CallbackRegistry != null && m_CallbackRegistry.HasBubbleHandlers();
	}

	protected internal virtual void ExecuteDefaultActionAtTarget(EventBase evt)
	{
	}

	protected internal virtual void ExecuteDefaultAction(EventBase evt)
	{
	}
}
