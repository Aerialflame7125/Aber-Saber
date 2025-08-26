namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewDataErrorEventArgs : DataGridViewCellCancelEventArgs
{
	private Exception exception;

	private DataGridViewDataErrorContexts context;

	private bool throwException;

	/// <summary>Gets details about the state of the <see cref="T:System.Windows.Forms.DataGridView" /> when the error occurred.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the error occurred.</returns>
	/// <filterpriority>1</filterpriority>
	public DataGridViewDataErrorContexts Context => context;

	/// <summary>Gets the exception that represents the error.</summary>
	/// <returns>An <see cref="T:System.Exception" /> that represents the error.</returns>
	/// <filterpriority>1</filterpriority>
	public Exception Exception => exception;

	/// <summary>Gets or sets a value indicating whether to throw the exception after the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventHandler" /> delegate is finished with it.</summary>
	/// <returns>true if the exception should be thrown; otherwise, false. The default is false.</returns>
	/// <exception cref="T:System.ArgumentException">When setting this property to true, the <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.Exception" /> property value is null.</exception>
	/// <filterpriority>1</filterpriority>
	public bool ThrowException
	{
		get
		{
			return throwException;
		}
		set
		{
			throwException = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> class.</summary>
	/// <param name="exception">The exception that occurred.</param>
	/// <param name="columnIndex">The column index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
	/// <param name="rowIndex">The row index of the cell that raised the <see cref="E:System.Windows.Forms.DataGridView.DataError" />.</param>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values indicating the context in which the error occurred. </param>
	public DataGridViewDataErrorEventArgs(Exception exception, int columnIndex, int rowIndex, DataGridViewDataErrorContexts context)
		: base(columnIndex, rowIndex)
	{
		this.exception = exception;
		this.context = context;
		throwException = false;
	}
}
