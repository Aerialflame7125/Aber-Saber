namespace System.Windows.Forms.Design;

internal class WndProcRouter : IWindowTarget, IDisposable
{
	private IWindowTarget _oldTarget;

	private IMessageReceiver _receiver;

	private Control _control;

	public Control Control => _control;

	public IWindowTarget OldWindowTarget => _oldTarget;

	public WndProcRouter(Control control, IMessageReceiver receiver)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		if (receiver == null)
		{
			throw new ArgumentNullException("receiver");
		}
		_oldTarget = control.WindowTarget;
		_control = control;
		_receiver = receiver;
	}

	public void ToControl(ref Message m)
	{
		if (_oldTarget != null)
		{
			_oldTarget.OnMessage(ref m);
		}
	}

	public void ToSystem(ref Message m)
	{
		Native.DefWndProc(ref m);
	}

	void IWindowTarget.OnHandleChange(IntPtr newHandle)
	{
		if (_oldTarget != null)
		{
			_oldTarget.OnHandleChange(newHandle);
		}
	}

	void IWindowTarget.OnMessage(ref Message m)
	{
		if (_receiver != null)
		{
			_receiver.WndProc(ref m);
		}
		else
		{
			ToControl(ref m);
		}
	}

	public void Dispose()
	{
		if (_control != null)
		{
			_control.WindowTarget = _oldTarget;
		}
		_control = null;
		_oldTarget = null;
	}
}
