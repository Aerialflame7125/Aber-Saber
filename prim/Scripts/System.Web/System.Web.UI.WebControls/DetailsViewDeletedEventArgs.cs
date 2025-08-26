using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleted" /> event.</summary>
public class DetailsViewDeletedEventArgs : EventArgs
{
	private int rowsAffected;

	private Exception e;

	private bool exceptionHandled;

	private IOrderedDictionary keys;

	private IOrderedDictionary values;

	/// <summary>Gets the number of rows affected by the delete operation.</summary>
	/// <returns>The number of rows affected by the delete operation.</returns>
	public int AffectedRows => rowsAffected;

	/// <summary>Gets the exception (if any) that was raised during the delete operation.</summary>
	/// <returns>An <see cref="T:System.Exception" /> that represents the exception that was raised during the delete operation.</returns>
	public Exception Exception => e;

	/// <summary>Gets or sets a value indicating whether an exception that was raised during the delete operation was handled in the event handler.</summary>
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

	/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeletedEventArgs" /> class.</summary>
	/// <param name="affectedRows">The number of rows affected by the delete operation.</param>
	/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the delete operation was performed. If no exception is raised, use <see langword="null" /> for this parameter.</param>
	public DetailsViewDeletedEventArgs(int affectedRows, Exception e)
	{
		rowsAffected = affectedRows;
		this.e = e;
		exceptionHandled = false;
	}

	internal DetailsViewDeletedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary values)
		: this(affectedRows, e)
	{
		this.keys = keys;
		this.values = values;
	}
}
