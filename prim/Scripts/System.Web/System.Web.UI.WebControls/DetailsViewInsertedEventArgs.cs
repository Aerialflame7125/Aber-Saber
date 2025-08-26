using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserted" /> event.</summary>
public class DetailsViewInsertedEventArgs : EventArgs
{
	private int rowsAffected;

	private Exception e;

	private bool exceptionHandled;

	private bool keepInsertedMode;

	private IOrderedDictionary values;

	/// <summary>Gets the number of rows affected by the insert operation.</summary>
	/// <returns>The number of rows affected by the insert operation.</returns>
	public int AffectedRows => rowsAffected;

	/// <summary>Gets the exception (if any) that was raised during the insert operation.</summary>
	/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the insert operation.</returns>
	public Exception Exception => e;

	/// <summary>Gets or sets a value indicating whether an exception that was raised during the insert operation was handled in the event handler.</summary>
	/// <returns>
	///     <see langword="true" /> if the exception was handled in the event handler; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool ExceptionHandled
	{
		get
		{
			return exceptionHandled;
		}
		set
		{
			exceptionHandled = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control should remain in insert mode after an insert operation.</summary>
	/// <returns>
	///     <see langword="true" /> to remain in insert mode after an insert operation; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool KeepInInsertMode
	{
		get
		{
			return keepInsertedMode;
		}
		set
		{
			keepInsertedMode = value;
		}
	}

	/// <summary>Gets a dictionary that contains the field name/value pairs for the inserted record.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the inserted record.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertedEventArgs" /> class.</summary>
	/// <param name="affectedRows">The number of rows affected by the insert operation.</param>
	/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the insert operation was performed. If no exception was raised, use <see langword="null" /> for this parameter.</param>
	public DetailsViewInsertedEventArgs(int affectedRows, Exception e)
	{
		rowsAffected = affectedRows;
		this.e = e;
		exceptionHandled = false;
		keepInsertedMode = false;
	}

	internal DetailsViewInsertedEventArgs(int affectedRows, Exception e, IOrderedDictionary values)
		: this(affectedRows, e)
	{
		this.values = values;
	}
}
