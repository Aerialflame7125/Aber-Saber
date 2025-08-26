namespace System.Windows.Forms.Design;

/// <summary>Provides a systematic way to manage event handlers for the current document.</summary>
public sealed class EventHandlerService
{
	private readonly Control _focusWnd;

	/// <summary>Gets the control to which event handlers are attached.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Control" /> which was attached through the constructor.</returns>
	public Control FocusWindow => _focusWnd;

	/// <summary>Fires an OnEventHandlerChanged event.</summary>
	public event EventHandler EventHandlerChanged;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.EventHandlerService" /> class.</summary>
	/// <param name="focusWnd">The <see cref="T:System.Windows.Forms.Control" /> which is being designed.</param>
	public EventHandlerService(Control focusWnd)
	{
		_focusWnd = focusWnd;
	}

	/// <summary>Gets the currently active event handler of the specified type.</summary>
	/// <param name="handlerType">The type of the handler to get.</param>
	/// <returns>An instance of the handler, or <see langword="null" /> if there is no handler of the requested type.</returns>
	[System.MonoTODO]
	public object GetHandler(Type handlerType)
	{
		throw new NotImplementedException();
	}

	/// <summary>Pops the given handler off of the stack.</summary>
	/// <param name="handler">The handler to remove from the stack.</param>
	[System.MonoTODO]
	public void PopHandler(object handler)
	{
		throw new NotImplementedException();
	}

	/// <summary>Pushes a new event handler on the stack.</summary>
	/// <param name="handler">The handler to add to the stack.</param>
	[System.MonoTODO]
	public void PushHandler(object handler)
	{
		throw new NotImplementedException();
	}
}
