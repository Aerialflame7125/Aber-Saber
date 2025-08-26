using System.Collections;
using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Serves as the base class for all data source view classes, which define the capabilities of data source controls.</summary>
public abstract class DataSourceView
{
	private string viewName = string.Empty;

	private EventHandlerList eventsList;

	private static readonly object EventDataSourceViewChanged = new object();

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The base class implementation returns <see langword="false" />.</returns>
	public virtual bool CanDelete => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The base class implementation returns <see langword="false" />.</returns>
	public virtual bool CanInsert => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The base class implementation returns <see langword="false" />.</returns>
	public virtual bool CanPage => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports retrieving the total number of data rows, instead of the data.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The base class implementation returns <see langword="false" />.</returns>
	public virtual bool CanRetrieveTotalRowCount => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports a sorted view on the underlying data source.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The default implementation returns <see langword="false" />.</returns>
	public virtual bool CanSort => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the operation is supported; otherwise, <see langword="false" />. The default implementation returns <see langword="false" />.</returns>
	public virtual bool CanUpdate => false;

	/// <summary>Gets a list of event-handler delegates for the data source view.</summary>
	/// <returns>The list of event-handler delegates.</returns>
	protected EventHandlerList Events
	{
		get
		{
			if (eventsList == null)
			{
				eventsList = new EventHandlerList();
			}
			return eventsList;
		}
	}

	/// <summary>Gets the name of the data source view.</summary>
	/// <returns>The name of the <see cref="T:System.Web.UI.DataSourceView" />, if it has one. The default value is <see cref="F:System.String.Empty" />.</returns>
	public string Name => viewName;

	/// <summary>Occurs when the data source view has changed.</summary>
	public event EventHandler DataSourceViewChanged
	{
		add
		{
			Events.AddHandler(EventDataSourceViewChanged, value);
		}
		remove
		{
			Events.RemoveHandler(EventDataSourceViewChanged, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceView" /> class.</summary>
	/// <param name="owner">The data source control that the <see cref="T:System.Web.UI.DataSourceView" /> is associated with.</param>
	/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> object.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="owner" /> is <see langword="null" />.- or -
	///         <paramref name="viewName" /> is <see langword="null" />.</exception>
	protected DataSourceView(IDataSource owner, string viewName)
	{
		if (owner == null)
		{
			throw new ArgumentNullException("owner");
		}
		this.viewName = viewName;
		owner.DataSourceChanged += OnDataSourceChanged;
	}

	private void OnDataSourceChanged(object sender, EventArgs e)
	{
		OnDataSourceViewChanged(EventArgs.Empty);
	}

	/// <summary>Performs an asynchronous delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
	/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is <see langword="null" />. </exception>
	public virtual void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
	{
		if (callback == null)
		{
			throw new ArgumentNullException("callBack");
		}
		int affectedRecords;
		try
		{
			affectedRecords = ExecuteDelete(keys, oldValues);
		}
		catch (Exception ex)
		{
			if (!callback(0, ex))
			{
				throw;
			}
			return;
		}
		callback(affectedRecords, null);
	}

	/// <summary>Performs a delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
	/// <returns>The number of items that were deleted from the underlying data storage.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
	protected virtual int ExecuteDelete(IDictionary keys, IDictionary oldValues)
	{
		throw new NotSupportedException();
	}

	/// <summary>Performs an insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
	/// <returns>The number of items that were inserted into the underlying data storage.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
	protected virtual int ExecuteInsert(IDictionary values)
	{
		throw new NotSupportedException();
	}

	/// <summary>Gets a list of data from the underlying data storage.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data from the underlying data storage.</returns>
	protected internal abstract IEnumerable ExecuteSelect(DataSourceSelectArguments arguments);

	/// <summary>Performs an update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
	/// <returns>The number of items that were updated in the underlying data storage.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
	protected virtual int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
	{
		throw new NotSupportedException();
	}

	/// <summary>Performs an asynchronous insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
	/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete. </param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is <see langword="null" />.</exception>
	public virtual void Insert(IDictionary values, DataSourceViewOperationCallback callback)
	{
		if (callback == null)
		{
			throw new ArgumentNullException("callback");
		}
		int affectedRecords;
		try
		{
			affectedRecords = ExecuteInsert(values);
		}
		catch (Exception ex)
		{
			if (!callback(0, ex))
			{
				throw;
			}
			return;
		}
		callback(affectedRecords, null);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
	protected virtual void OnDataSourceViewChanged(EventArgs e)
	{
		if (eventsList != null && eventsList[EventDataSourceViewChanged] is EventHandler eventHandler)
		{
			eventHandler(this, e);
		}
	}

	/// <summary>Called by the <see cref="M:System.Web.UI.DataSourceSelectArguments.RaiseUnsupportedCapabilitiesError(System.Web.UI.DataSourceView)" /> method to compare the capabilities requested for an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the view supports.</summary>
	/// <param name="capability">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values that is compared against the capabilities that the view supports.</param>
	/// <exception cref="T:System.NotSupportedException">The data source view does not support the data source capability specified.</exception>
	protected internal virtual void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
	{
		if ((capability & DataSourceCapabilities.Sort) != 0 && !CanSort)
		{
			throw new NotSupportedException("Sort Capabilites");
		}
		if ((capability & DataSourceCapabilities.Page) != 0 && !CanPage)
		{
			throw new NotSupportedException("Page Capabilites");
		}
		if ((capability & DataSourceCapabilities.RetrieveTotalRowCount) != 0 && !CanRetrieveTotalRowCount)
		{
			throw new NotSupportedException("RetrieveTotalRowCount Capabilites");
		}
	}

	/// <summary>Gets a list of data asynchronously from the underlying data storage.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
	/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> supplied is <see langword="null" />.</exception>
	public virtual void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
	{
		if (callback == null)
		{
			throw new ArgumentNullException("callBack");
		}
		arguments.RaiseUnsupportedCapabilitiesError(this);
		IEnumerable data = ExecuteSelect(arguments);
		callback(data);
	}

	/// <summary>Performs an asynchronous update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
	/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
	/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is <see langword="null" />.</exception>
	public virtual void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
	{
		if (callback == null)
		{
			throw new ArgumentNullException("callback");
		}
		int affectedRecords;
		try
		{
			affectedRecords = ExecuteUpdate(keys, values, oldValues);
		}
		catch (Exception ex)
		{
			if (!callback(0, ex))
			{
				throw;
			}
			return;
		}
		callback(affectedRecords, null);
	}

	internal bool HasEvents()
	{
		return eventsList != null;
	}
}
