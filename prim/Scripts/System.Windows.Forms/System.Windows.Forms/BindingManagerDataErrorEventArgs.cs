namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event. </summary>
public class BindingManagerDataErrorEventArgs : EventArgs
{
	private Exception exception;

	/// <summary>Gets the <see cref="T:System.Exception" /> caught in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</summary>
	/// <returns>The <see cref="T:System.Exception" /> that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised. </returns>
	public Exception Exception => exception;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingManagerDataErrorEventArgs" /> class. </summary>
	/// <param name="exception">The <see cref="T:System.Exception" /> that occurred in the binding process that caused the <see cref="E:System.Windows.Forms.BindingManagerBase.DataError" /> event to be raised.</param>
	public BindingManagerDataErrorEventArgs(Exception exception)
	{
		this.exception = exception;
	}
}
