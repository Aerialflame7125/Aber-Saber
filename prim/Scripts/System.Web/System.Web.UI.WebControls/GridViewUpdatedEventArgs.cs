using System.Collections.Specialized;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdated" /> event.</summary>
public class GridViewUpdatedEventArgs : EventArgs
{
	private int rowsAffected;

	private Exception e;

	private bool exceptionHandled;

	private bool keepEditMode;

	private IOrderedDictionary keys;

	private IOrderedDictionary newValues;

	private IOrderedDictionary oldValues;

	/// <summary>Gets the number of rows that were affected by the update operation.</summary>
	/// <returns>The number of rows that were affected by the update operation.</returns>
	public int AffectedRows => rowsAffected;

	/// <summary>Gets the exception (if any) that was raised during the update operation.</summary>
	/// <returns>The exception that was raised during the update operation. If no exceptions were raised, this property returns <see langword="null" />.</returns>
	public Exception Exception => e;

	/// <summary>Gets or sets a value that indicates whether an exception that was raised during the update operation was handled in the event handler.</summary>
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

	/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.GridView" /> control should remain in edit mode after an update operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the control will remain in edit mode after an update operation; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
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

	/// <summary>Gets a dictionary that contains the key field name/value pairs for the updated record.</summary>
	/// <returns>A dictionary of key field name/value pairs for the updated record.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary that contains the new field name/value pairs for the updated record.</summary>
	/// <returns>A dictionary of the new field name/value pairs for the updated record.</returns>
	public IOrderedDictionary NewValues => newValues;

	/// <summary>Gets a dictionary that contains the original field name/value pairs for the updated record.</summary>
	/// <returns>A dictionary of the original field name/value pairs for the updated record.</returns>
	public IOrderedDictionary OldValues => oldValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdatedEventArgs" /> class.</summary>
	/// <param name="affectedRows">The number of rows that were affected by the update operation.</param>
	/// <param name="e">The exception that was raised when the update operation was performed. If no exception was raised, use <see langword="null" /> for this parameter.</param>
	public GridViewUpdatedEventArgs(int affectedRows, Exception e)
	{
		rowsAffected = affectedRows;
		this.e = e;
		exceptionHandled = false;
		keepEditMode = false;
	}

	internal GridViewUpdatedEventArgs(int affectedRows, Exception e, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		: this(affectedRows, e)
	{
		this.keys = keys;
		this.newValues = newValues;
		this.oldValues = oldValues;
	}
}
