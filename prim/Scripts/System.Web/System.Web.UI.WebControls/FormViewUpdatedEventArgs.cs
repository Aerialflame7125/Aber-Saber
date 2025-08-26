using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdated" /> event.</summary>
public class FormViewUpdatedEventArgs : EventArgs
{
	private int rowsAffected;

	private Exception e;

	private bool exceptionHandled;

	private bool keepEditMode;

	private IOrderedDictionary keys;

	private IOrderedDictionary oldValues;

	private IOrderedDictionary newValues;

	/// <summary>Gets the number of rows affected by the update operation.</summary>
	/// <returns>The number of rows affected by the update operation.</returns>
	public int AffectedRows => rowsAffected;

	/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
	/// <returns>An <see cref="T:System.Exception" /> object that represents the exception that was raised during the update operation.</returns>
	public Exception Exception => e;

	/// <summary>Gets or sets a value indicating whether an exception that was raised during the update operation was handled in the event handler.</summary>
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

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.FormView" /> control should remain in edit mode after an update operation.</summary>
	/// <returns>
	///     <see langword="true" /> to remain in edit mode after an update operation; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool KeepInEditMode
	{
		get
		{
			return keepEditMode;
		}
		set
		{
			keepEditMode = value;
		}
	}

	/// <summary>Gets a dictionary that contains the original key field name/value pairs for the updated record.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the updated record.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the updated record.</returns>
	public IOrderedDictionary NewValues => newValues;

	/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the updated record.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the updated record.</returns>
	public IOrderedDictionary OldValues => oldValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs" /> class.</summary>
	/// <param name="affectedRows">The number of rows affected by the update operation.</param>
	/// <param name="e">An <see cref="T:System.Exception" /> that represents the exception raised when the update operation was performed. If no exception is raised, use <see langword="null" /> for this parameter.</param>
	public FormViewUpdatedEventArgs(int affectedRows, Exception e)
	{
		rowsAffected = affectedRows;
		this.e = e;
		exceptionHandled = false;
		keepEditMode = false;
	}

	internal FormViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		: this(affectedRows, e)
	{
		this.keys = keys;
		this.oldValues = oldValues;
		this.newValues = newValues;
	}
}
