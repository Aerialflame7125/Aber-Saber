namespace System.Threading;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
public class ThreadExceptionEventArgs : EventArgs
{
	private Exception exception;

	/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
	/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
	public Exception Exception => exception;

	/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
	/// <param name="t">The <see cref="T:System.Exception" /> that occurred.</param>
	public ThreadExceptionEventArgs(Exception t)
	{
		exception = t;
	}
}
