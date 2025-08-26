using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace System.Web.UI.WebControls;

/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control and provides an interface for data-bound controls to perform data operations with business and data objects.</summary>
public class ObjectDataSourceView : DataSourceView, IStateManager
{
	private ObjectDataSource owner;

	private HttpContext context;

	private Type objectType;

	private Type dataObjectType;

	private bool convertNullToDBNull;

	private bool enablePaging;

	private string dataObjectTypeName;

	private string filterExpression;

	private string maximumRowsParameterName;

	private string oldValuesParameterFormatString;

	private string deleteMethod;

	private string insertMethod;

	private string selectCountMethod;

	private string selectMethod;

	private string sortParameterName;

	private string startRowIndexParameterName;

	private string typeName;

	private string updateMethod;

	private bool isTrackingViewState;

	private ParameterCollection selectParameters;

	private ParameterCollection updateParameters;

	private ParameterCollection deleteParameters;

	private ParameterCollection insertParameters;

	private ParameterCollection filterParameters;

	private static readonly object DeletedEvent;

	private static readonly object DeletingEvent;

	private static readonly object FilteringEvent;

	private static readonly object InsertedEvent;

	private static readonly object InsertingEvent;

	private static readonly object ObjectCreatedEvent;

	private static readonly object ObjectCreatingEvent;

	private static readonly object ObjectDisposingEvent;

	private static readonly object SelectedEvent;

	private static readonly object SelectingEvent;

	private static readonly object UpdatedEvent;

	private static readonly object UpdatingEvent;

	private ConflictOptions conflictDetection;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the delete operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />. Deletion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property is an empty string ("").</returns>
	public override bool CanDelete => DeleteMethod.Length > 0;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the insert operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />. Insertion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property is an empty string.</returns>
	public override bool CanInsert => InsertMethod.Length > 0;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports paging through the retrieved data.</summary>
	/// <returns>
	///     <see langword="true" />, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnablePaging" /> value is set to <see langword="true" />; otherwise, <see langword="false" />. </returns>
	public override bool CanPage => EnablePaging;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports retrieving the total number of data rows, in addition to the set of data.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />.</returns>
	public override bool CanRetrieveTotalRowCount
	{
		get
		{
			if (SelectCountMethod.Length > 0)
			{
				return true;
			}
			return !EnablePaging;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports a sorted view on the underlying data source.</summary>
	/// <returns>
	///     <see langword="true" />.</returns>
	public override bool CanSort => true;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the update operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />. Updating is not supported if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property is an empty string ("").</returns>
	public override bool CanUpdate => UpdateMethod.Length > 0;

	/// <summary>Gets or sets a value that determines how the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs updates and deletes when data in a row in the underlying data storage changes during the time of the operation.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.ConflictOptions" /> values.</exception>
	public ConflictOptions ConflictDetection
	{
		get
		{
			return conflictDetection;
		}
		set
		{
			if (ConflictDetection != value)
			{
				conflictDetection = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from <see langword="null" /> to the <see cref="F:System.DBNull.Value" /> value.</summary>
	/// <returns>
	///     <see langword="true" />, if <see langword="null" /> in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is automatically converted to the <see cref="F:System.DBNull.Value" /> value; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool ConvertNullToDBNull
	{
		get
		{
			return convertNullToDBNull;
		}
		set
		{
			convertNullToDBNull = value;
		}
	}

	/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in a data operation. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses the specified class instead of the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that are in the various parameters collections.</summary>
	/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
	public string DataObjectTypeName
	{
		get
		{
			if (dataObjectTypeName == null)
			{
				return string.Empty;
			}
			return dataObjectTypeName;
		}
		set
		{
			if (!(DataObjectTypeName == value))
			{
				dataObjectTypeName = value;
				dataObjectType = null;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to delete data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to delete data. The default is an empty string ("").</returns>
	public string DeleteMethod
	{
		get
		{
			if (deleteMethod == null)
			{
				return string.Empty;
			}
			return deleteMethod;
		}
		set
		{
			deleteMethod = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property.</returns>
	public ParameterCollection DeleteParameters
	{
		get
		{
			if (deleteParameters == null)
			{
				deleteParameters = new ParameterCollection();
			}
			return deleteParameters;
		}
	}

	/// <summary>Gets or sets a value indicating whether the data source control supports paging through the set of data that it retrieves.</summary>
	/// <returns>
	///     <see langword="true" />, if the data source control supports paging through the data it retrieves; otherwise, <see langword="false" />.</returns>
	public bool EnablePaging
	{
		get
		{
			return enablePaging;
		}
		set
		{
			if (EnablePaging != value)
			{
				enablePaging = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a filtering expression that is applied when the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property is called.</summary>
	/// <returns>A string that represents a filtering expression applied when data is retrieved using the business object method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
	public string FilterExpression
	{
		get
		{
			if (filterExpression == null)
			{
				return string.Empty;
			}
			return filterExpression;
		}
		set
		{
			if (!(FilterExpression == value))
			{
				filterExpression = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> string.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
	public ParameterCollection FilterParameters
	{
		get
		{
			if (filterParameters == null)
			{
				filterParameters = new ParameterCollection();
				filterParameters.ParametersChanged += OnParametersChanged;
				if (IsTrackingViewState)
				{
					((IStateManager)filterParameters).TrackViewState();
				}
			}
			return filterParameters;
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to insert data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to insert data. The default value is an empty string ("").</returns>
	public string InsertMethod
	{
		get
		{
			if (insertMethod == null)
			{
				return string.Empty;
			}
			return insertMethod;
		}
		set
		{
			insertMethod = value;
		}
	}

	/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property.</returns>
	public ParameterCollection InsertParameters
	{
		get
		{
			if (insertParameters == null)
			{
				insertParameters = new ParameterCollection();
			}
			return insertParameters;
		}
	}

	/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
	/// <returns>The name of the method parameter used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
	public string MaximumRowsParameterName
	{
		get
		{
			if (maximumRowsParameterName == null)
			{
				return "maximumRows";
			}
			return maximumRowsParameterName;
		}
		set
		{
			if (!(MaximumRowsParameterName == value))
			{
				maximumRowsParameterName = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the <see langword="Delete" /> or <see langword="Update" /> methods.</summary>
	/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> passed to the <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> method. The default is "{0}", which means the parameter name is simply the field name.</returns>
	[DefaultValue("{0}")]
	public string OldValuesParameterFormatString
	{
		get
		{
			if (oldValuesParameterFormatString == null)
			{
				return "{0}";
			}
			return oldValuesParameterFormatString;
		}
		set
		{
			if (!(OldValuesParameterFormatString == value))
			{
				oldValuesParameterFormatString = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve a row count.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve a row count. The default is an empty string (""). </returns>
	public string SelectCountMethod
	{
		get
		{
			if (selectCountMethod == null)
			{
				return string.Empty;
			}
			return selectCountMethod;
		}
		set
		{
			if (!(SelectCountMethod == value))
			{
				selectCountMethod = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve data. The default is an empty string ("").</returns>
	public string SelectMethod
	{
		get
		{
			if (selectMethod == null)
			{
				return string.Empty;
			}
			return selectMethod;
		}
		set
		{
			if (!(SelectMethod == value))
			{
				selectMethod = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
	public ParameterCollection SelectParameters
	{
		get
		{
			if (selectParameters == null)
			{
				selectParameters = new ParameterCollection();
				selectParameters.ParametersChanged += OnParametersChanged;
				if (IsTrackingViewState)
				{
					((IStateManager)selectParameters).TrackViewState();
				}
			}
			return selectParameters;
		}
	}

	/// <summary>Gets or sets the name of the data retrieval method parameter that is used to specify a sort expression for data source sorting support.</summary>
	/// <returns>The name of the method parameter used to indicate the parameter that accepts this sort expression value. The default is an empty string ("").</returns>
	public string SortParameterName
	{
		get
		{
			if (sortParameterName == null)
			{
				return string.Empty;
			}
			return sortParameterName;
		}
		set
		{
			sortParameterName = value;
		}
	}

	/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the integer index of the first record to retrieve from the results set for data source paging support.</summary>
	/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The default is "startRowIndex".</returns>
	public string StartRowIndexParameterName
	{
		get
		{
			if (startRowIndexParameterName == null)
			{
				return "startRowIndex";
			}
			return startRowIndexParameterName;
		}
		set
		{
			if (!(StartRowIndexParameterName == value))
			{
				startRowIndexParameterName = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control represents.</summary>
	/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string.</returns>
	public string TypeName
	{
		get
		{
			if (typeName == null)
			{
				return string.Empty;
			}
			return typeName;
		}
		set
		{
			if (!(TypeName == value))
			{
				typeName = value;
				objectType = null;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to update data.</summary>
	/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to update data. The default is an empty string ("").</returns>
	public string UpdateMethod
	{
		get
		{
			if (updateMethod == null)
			{
				return string.Empty;
			}
			return updateMethod;
		}
		set
		{
			updateMethod = value;
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> method.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property.</returns>
	public ParameterCollection UpdateParameters
	{
		get
		{
			if (updateParameters == null)
			{
				updateParameters = new ParameterCollection();
			}
			return updateParameters;
		}
	}

	private Type ObjectType
	{
		get
		{
			if (objectType == null)
			{
				objectType = HttpApplication.LoadType(TypeName);
				if (objectType == null)
				{
					throw new InvalidOperationException("Type not found: " + TypeName);
				}
			}
			return objectType;
		}
	}

	private Type DataObjectType
	{
		get
		{
			if (dataObjectType == null)
			{
				dataObjectType = HttpApplication.LoadType(DataObjectTypeName);
				if (dataObjectType == null)
				{
					throw new InvalidOperationException("Type not found: " + DataObjectTypeName);
				}
			}
			return dataObjectType;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" />, if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => isTrackingViewState;

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>
	///     <see langword="true" />, if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Occurs when a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Deleted
	{
		add
		{
			base.Events.AddHandler(DeletedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DeletedEvent, value);
		}
	}

	/// <summary>Occurs before a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Deleting
	{
		add
		{
			base.Events.AddHandler(DeletingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(DeletingEvent, value);
		}
	}

	/// <summary>Occurs before a filter operation.</summary>
	public event ObjectDataSourceFilteringEventHandler Filtering
	{
		add
		{
			base.Events.AddHandler(FilteringEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(FilteringEvent, value);
		}
	}

	/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Inserted
	{
		add
		{
			base.Events.AddHandler(InsertedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(InsertedEvent, value);
		}
	}

	/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Inserting
	{
		add
		{
			base.Events.AddHandler(InsertingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(InsertingEvent, value);
		}
	}

	/// <summary>Occurs after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
	public event ObjectDataSourceObjectEventHandler ObjectCreated
	{
		add
		{
			base.Events.AddHandler(ObjectCreatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ObjectCreatedEvent, value);
		}
	}

	/// <summary>Occurs before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
	public event ObjectDataSourceObjectEventHandler ObjectCreating
	{
		add
		{
			base.Events.AddHandler(ObjectCreatingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ObjectCreatingEvent, value);
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instance of an object that it has created. </summary>
	public event ObjectDataSourceDisposingEventHandler ObjectDisposing
	{
		add
		{
			base.Events.AddHandler(ObjectDisposingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ObjectDisposingEvent, value);
		}
	}

	/// <summary>Occurs when a data retrieval operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Selected
	{
		add
		{
			base.Events.AddHandler(SelectedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectedEvent, value);
		}
	}

	/// <summary>Occurs before a data retrieval operation.</summary>
	public event ObjectDataSourceSelectingEventHandler Selecting
	{
		add
		{
			base.Events.AddHandler(SelectingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(SelectingEvent, value);
		}
	}

	/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation has completed.</summary>
	public event ObjectDataSourceStatusEventHandler Updated
	{
		add
		{
			base.Events.AddHandler(UpdatedEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UpdatedEvent, value);
		}
	}

	/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation.</summary>
	public event ObjectDataSourceMethodEventHandler Updating
	{
		add
		{
			base.Events.AddHandler(UpdatingEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(UpdatingEvent, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> class.</summary>
	/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is associated with. </param>
	/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it.</param>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" />.</param>
	public ObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context)
		: base(owner, name)
	{
		this.owner = owner;
		this.context = context;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a delete operation.</summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
	protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceStatusEventHandler)base.Events[Deleted])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a delete operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
	protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceMethodEventHandler)base.Events[Deleting])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Filtering" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a filtering operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs" /> that contains the event data.</param>
	protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceFilteringEventHandler)base.Events[Filtering])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an insert operation.</summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
	protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceStatusEventHandler)base.Events[Inserted])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an insert operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
	protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceMethodEventHandler)base.Events[Inserting])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> creates an instance of the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property. </summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
	protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceObjectEventHandler)base.Events[ObjectCreated])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of a business object to perform a data operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
	protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceObjectEventHandler)base.Events[ObjectCreating])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectDisposing" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instantiated type. </summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> that contains the event data.</param>
	protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceDisposingEventHandler)base.Events[ObjectDisposing])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selected" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a data retrieval operation.</summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data. </param>
	protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceStatusEventHandler)base.Events[Selected])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selecting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a data retrieval operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs" /> that contains the event data.</param>
	protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceSelectingEventHandler)base.Events[Selecting])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an update operation.</summary>
	/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
	protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceStatusEventHandler)base.Events[Updated])?.Invoke(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an update operation.</summary>
	/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
	protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
	{
		if (base.Events != null)
		{
			((ObjectDataSourceMethodEventHandler)base.Events[Updating])?.Invoke(this, e);
		}
	}

	/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows. For more information, see <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
	public IEnumerable Select(DataSourceSelectArguments arguments)
	{
		return ExecuteSelect(arguments);
	}

	/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to update. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update operation. If there are no parameters associated with the method, pass <see langword="null" />.</param>
	/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of new values to apply to the data source. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no parameters associated with the method, pass <see langword="null" />. </param>
	/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the delete method, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
	/// <returns>The number of rows updated; otherwise, -1, if the number is not known.</returns>
	public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
	{
		return ExecuteUpdate(keys, values, oldValues);
	}

	/// <summary>Performs a delete operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property using the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to delete. These parameters are used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass <see langword="null" />.</param>
	/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the method only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
	/// <returns>The number of rows deleted; otherwise, -1, if the number is not known.</returns>
	public int Delete(IDictionary keys, IDictionary oldValues)
	{
		return ExecuteDelete(keys, oldValues);
	}

	/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
	/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> collection of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass <see langword="null" />.</param>
	/// <returns>The number of rows inserted; otherwise, -1, if the number is not known.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns <see langword="false" />.</exception>
	public int Insert(IDictionary values)
	{
		return ExecuteInsert(values);
	}

	/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
	/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass <see langword="null" />.</param>
	/// <returns>The number of rows inserted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns <see langword="false" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="values" /> is <see langword="null" /> or empty.</exception>
	protected override int ExecuteInsert(IDictionary values)
	{
		if (!CanInsert)
		{
			throw new NotSupportedException("Insert operation not supported.");
		}
		IOrderedDictionary paramValues;
		MethodInfo method;
		if (DataObjectTypeName.Length == 0)
		{
			paramValues = MergeParameterValues(InsertParameters, values, null);
			method = GetObjectMethod(InsertMethod, paramValues, DataObjectMethodType.Insert);
		}
		else
		{
			method = ResolveDataObjectMethod(InsertMethod, values, null, out paramValues);
		}
		ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(paramValues);
		OnInserting(objectDataSourceMethodEventArgs);
		if (objectDataSourceMethodEventArgs.Cancel)
		{
			return -1;
		}
		ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = InvokeMethod(method, paramValues);
		OnInserted(objectDataSourceStatusEventArgs);
		if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw objectDataSourceStatusEventArgs.Exception;
		}
		if (owner.EnableCaching)
		{
			owner.Cache.Expire();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		return -1;
	}

	/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method and the specified <paramref name="keys" /> and <paramref name="oldValues" /> collection.</summary>
	/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass <see langword="null" />.</param>
	/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the  <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
	/// <returns>The number of rows deleted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanDelete" /> property returns <see langword="false" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value, and no values are passed in the <paramref name="oldValues" /> collection.</exception>
	protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
	{
		if (!CanDelete)
		{
			throw new NotSupportedException("Delete operation not supported.");
		}
		if (ConflictDetection == ConflictOptions.CompareAllValues && (oldValues == null || oldValues.Count == 0))
		{
			throw new InvalidOperationException("ConflictDetection is set to CompareAllValues and oldValues collection is null or empty.");
		}
		IDictionary dictionary = BuildOldValuesList(keys, oldValues, keysWin: false);
		IOrderedDictionary paramValues;
		MethodInfo method;
		if (DataObjectTypeName.Length == 0)
		{
			paramValues = MergeParameterValues(DeleteParameters, null, dictionary);
			method = GetObjectMethod(DeleteMethod, paramValues, DataObjectMethodType.Delete);
		}
		else
		{
			method = ResolveDataObjectMethod(DeleteMethod, dictionary, null, out paramValues);
		}
		ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(paramValues);
		OnDeleting(objectDataSourceMethodEventArgs);
		if (objectDataSourceMethodEventArgs.Cancel)
		{
			return -1;
		}
		ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = InvokeMethod(method, paramValues);
		OnDeleted(objectDataSourceStatusEventArgs);
		if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw objectDataSourceStatusEventArgs.Exception;
		}
		if (owner.EnableCaching)
		{
			owner.Cache.Expire();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		return -1;
	}

	/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no keys associated with the method, pass <see langword="null" />.</param>
	/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of values to be used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> to perform the update database operation. If there are no parameters associated with the method, pass <see langword="null" />. </param>
	/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that represents the original values in the underlying data store. If there are no parameters associated with the query, pass <see langword="null" />.</param>
	/// <returns>The number of rows updated; or -1, if the number is not known. For more information, see <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns <see langword="false" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///         <paramref name="oldValues" /> is <see langword="null" /> or empty and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> is set to <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" />.</exception>
	protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
	{
		IDictionary oldValues2 = BuildOldValuesList(keys, oldValues, keysWin: true);
		IOrderedDictionary paramValues;
		MethodInfo method;
		if (DataObjectTypeName.Length == 0)
		{
			paramValues = MergeParameterValues(UpdateParameters, values, oldValues2);
			method = GetObjectMethod(UpdateMethod, paramValues, DataObjectMethodType.Update);
		}
		else
		{
			if (ConflictDetection != ConflictOptions.CompareAllValues)
			{
				oldValues2 = null;
			}
			IDictionary dictionary = new Hashtable();
			if (keys != null)
			{
				foreach (DictionaryEntry key in keys)
				{
					dictionary[key.Key] = key.Value;
				}
			}
			if (values != null)
			{
				foreach (DictionaryEntry value in values)
				{
					dictionary[value.Key] = value.Value;
				}
			}
			method = ResolveDataObjectMethod(UpdateMethod, dictionary, oldValues2, out paramValues);
		}
		ObjectDataSourceMethodEventArgs objectDataSourceMethodEventArgs = new ObjectDataSourceMethodEventArgs(paramValues);
		OnUpdating(objectDataSourceMethodEventArgs);
		if (objectDataSourceMethodEventArgs.Cancel)
		{
			return -1;
		}
		ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = InvokeMethod(method, paramValues);
		OnUpdated(objectDataSourceStatusEventArgs);
		if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw objectDataSourceStatusEventArgs.Exception;
		}
		if (owner.EnableCaching)
		{
			owner.Cache.Expire();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		return -1;
	}

	private IDictionary BuildOldValuesList(IDictionary keys, IDictionary oldValues, bool keysWin)
	{
		IDictionary dictionary;
		if (ConflictDetection == ConflictOptions.CompareAllValues)
		{
			dictionary = new Hashtable();
			if (keys != null && !keysWin)
			{
				foreach (DictionaryEntry key in keys)
				{
					dictionary[key.Key] = key.Value;
				}
			}
			if (oldValues != null)
			{
				foreach (DictionaryEntry oldValue in oldValues)
				{
					dictionary[oldValue.Key] = oldValue.Value;
				}
			}
			if (keys != null && keysWin)
			{
				foreach (DictionaryEntry key2 in keys)
				{
					dictionary[key2.Key] = key2.Value;
				}
			}
		}
		else
		{
			dictionary = keys;
		}
		return dictionary;
	}

	/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>A <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.- or -The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is not a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />, and caching is enabled. Only <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> objects can be cached for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> does not support caching when client impersonation is enabled.</exception>
	/// <exception cref="T:System.InvalidOperationException">The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is a <see cref="T:System.Data.DataSet" />, but has no tables in its <see cref="P:System.Data.DataSet.Tables" /> collection.- or - The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.EnablePaging" /> property is set to <see langword="true" />, but the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.StartRowIndexParameterName" /> and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.MaximumRowsParameterName" /> properties are not set.</exception>
	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		arguments.RaiseUnsupportedCapabilitiesError(this);
		IOrderedDictionary orderedDictionary = MergeParameterValues(SelectParameters, null, null);
		ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(orderedDictionary, arguments, executingSelectCount: false);
		object obj = null;
		if (owner.EnableCaching)
		{
			obj = owner.Cache.GetCachedObject(SelectMethod, SelectParameters);
		}
		if (obj == null)
		{
			OnSelecting(objectDataSourceSelectingEventArgs);
			if (objectDataSourceSelectingEventArgs.Cancel)
			{
				return new ArrayList();
			}
			if (CanPage)
			{
				if (StartRowIndexParameterName.Length == 0)
				{
					throw new InvalidOperationException("Paging is enabled, but the StartRowIndexParameterName property is not set.");
				}
				if (MaximumRowsParameterName.Length == 0)
				{
					throw new InvalidOperationException("Paging is enabled, but the MaximumRowsParameterName property is not set.");
				}
				orderedDictionary[StartRowIndexParameterName] = arguments.StartRowIndex;
				orderedDictionary[MaximumRowsParameterName] = arguments.MaximumRows;
			}
			if (SortParameterName.Length > 0)
			{
				orderedDictionary[SortParameterName] = arguments.SortExpression;
			}
			obj = InvokeSelect(SelectMethod, orderedDictionary);
			if (CanRetrieveTotalRowCount && arguments.RetrieveTotalRowCount)
			{
				arguments.TotalRowCount = QueryTotalRowCount(MergeParameterValues(SelectParameters, null, null), arguments);
			}
			if (owner.EnableCaching)
			{
				owner.Cache.SetCachedObject(SelectMethod, SelectParameters, obj);
			}
		}
		if (FilterExpression.Length > 0 && !(obj is DataGrid) && !(obj is DataView) && !(obj is DataTable))
		{
			throw new NotSupportedException("The FilterExpression property was set and the Select method does not return a DataSet, DataTable, or DataView.");
		}
		if (owner.EnableCaching && obj is IDataReader)
		{
			throw new NotSupportedException("Data source does not support caching objects that implement IDataReader");
		}
		if (obj is DataSet)
		{
			DataSet obj2 = (DataSet)obj;
			if (obj2.Tables.Count == 0)
			{
				throw new InvalidOperationException("The select method returnet a DataSet which doesn't contain any table.");
			}
			obj = obj2.Tables[0];
		}
		if (obj is DataTable)
		{
			DataView dataView = new DataView((DataTable)obj);
			if (arguments.SortExpression != null && arguments.SortExpression.Length > 0)
			{
				dataView.Sort = arguments.SortExpression;
			}
			if (FilterExpression.Length > 0)
			{
				IOrderedDictionary values = FilterParameters.GetValues(context, owner);
				ObjectDataSourceFilteringEventArgs objectDataSourceFilteringEventArgs = new ObjectDataSourceFilteringEventArgs(values);
				OnFiltering(objectDataSourceFilteringEventArgs);
				if (!objectDataSourceFilteringEventArgs.Cancel)
				{
					object[] array = new object[values.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = values[i];
						if (array[i] == null)
						{
							return dataView;
						}
					}
					dataView.RowFilter = string.Format(FilterExpression, array);
				}
			}
			return dataView;
		}
		if (obj is IEnumerable)
		{
			return (IEnumerable)obj;
		}
		return new object[1] { obj };
	}

	private int QueryTotalRowCount(IOrderedDictionary mergedParameters, DataSourceSelectArguments arguments)
	{
		ObjectDataSourceSelectingEventArgs objectDataSourceSelectingEventArgs = new ObjectDataSourceSelectingEventArgs(mergedParameters, arguments, executingSelectCount: true);
		OnSelecting(objectDataSourceSelectingEventArgs);
		if (objectDataSourceSelectingEventArgs.Cancel)
		{
			return 0;
		}
		return (int)Convert.ChangeType(InvokeSelect(SelectCountMethod, mergedParameters), typeof(int));
	}

	private object InvokeSelect(string methodName, IOrderedDictionary paramValues)
	{
		MethodInfo objectMethod = GetObjectMethod(methodName, paramValues, DataObjectMethodType.Select);
		ObjectDataSourceStatusEventArgs objectDataSourceStatusEventArgs = InvokeMethod(objectMethod, paramValues);
		OnSelected(objectDataSourceStatusEventArgs);
		if (objectDataSourceStatusEventArgs.Exception != null && !objectDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw objectDataSourceStatusEventArgs.Exception;
		}
		return objectDataSourceStatusEventArgs.ReturnValue;
	}

	private ObjectDataSourceStatusEventArgs InvokeMethod(MethodInfo method, IOrderedDictionary paramValues)
	{
		object obj = null;
		if (!method.IsStatic)
		{
			obj = CreateObjectInstance();
		}
		ParameterInfo[] parameters = method.GetParameters();
		ArrayList outParamInfos;
		object[] parameterArray = GetParameterArray(parameters, paramValues, out outParamInfos);
		if (parameterArray == null)
		{
			throw CreateMethodException(method.Name, paramValues);
		}
		object returnValue = null;
		Hashtable hashtable = null;
		try
		{
			returnValue = method.Invoke(obj, parameterArray);
			if (outParamInfos != null)
			{
				hashtable = new Hashtable();
				foreach (ParameterInfo item in outParamInfos)
				{
					hashtable[item.Name] = parameterArray[item.Position];
				}
			}
			return new ObjectDataSourceStatusEventArgs(returnValue, hashtable, null);
		}
		catch (Exception exception)
		{
			return new ObjectDataSourceStatusEventArgs(returnValue, hashtable, exception);
		}
		finally
		{
			if (obj != null)
			{
				DisposeObjectInstance(obj);
			}
		}
	}

	private MethodInfo GetObjectMethod(string methodName, IOrderedDictionary parameters, DataObjectMethodType methodType)
	{
		MemberInfo[] member = ObjectType.GetMember(methodName, MemberTypes.Method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
		if (member.Length > 1)
		{
			DataObjectMethodAttribute dataObjectMethodAttribute = null;
			MethodInfo methodInfo = null;
			bool flag = false;
			MemberInfo[] array = member;
			for (int i = 0; i < array.Length; i++)
			{
				MethodInfo methodInfo2 = (MethodInfo)array[i];
				ParameterInfo[] parameters2 = methodInfo2.GetParameters();
				if (parameters2.Length != parameters.Count)
				{
					continue;
				}
				object[] customAttributes = methodInfo2.GetCustomAttributes(typeof(DataObjectMethodAttribute), inherit: true);
				DataObjectMethodAttribute dataObjectMethodAttribute2 = ((customAttributes != null && customAttributes.Length != 0) ? ((DataObjectMethodAttribute)customAttributes[0]) : null);
				if (dataObjectMethodAttribute2 != null && dataObjectMethodAttribute2.MethodType != methodType)
				{
					continue;
				}
				bool flag2 = true;
				ParameterInfo[] array2 = parameters2;
				foreach (ParameterInfo parameterInfo in array2)
				{
					if (!parameters.Contains(parameterInfo.Name))
					{
						flag2 = false;
						break;
					}
				}
				if (!flag2)
				{
					continue;
				}
				if (dataObjectMethodAttribute2 != null)
				{
					if (dataObjectMethodAttribute != null)
					{
						if (dataObjectMethodAttribute.IsDefault)
						{
							if (dataObjectMethodAttribute2.IsDefault)
							{
								methodInfo = null;
								break;
							}
							continue;
						}
						methodInfo = null;
						flag = !dataObjectMethodAttribute2.IsDefault;
					}
					else
					{
						methodInfo = null;
					}
				}
				if (methodInfo == null)
				{
					dataObjectMethodAttribute = dataObjectMethodAttribute2;
					methodInfo = methodInfo2;
				}
				else
				{
					flag = true;
				}
			}
			if (!flag && methodInfo != null)
			{
				return methodInfo;
			}
		}
		else if (member.Length == 1)
		{
			MethodInfo methodInfo3 = member[0] as MethodInfo;
			if (methodInfo3 != null && methodInfo3.GetParameters().Length == parameters.Count)
			{
				return methodInfo3;
			}
		}
		throw CreateMethodException(methodName, parameters);
	}

	private MethodInfo ResolveDataObjectMethod(string methodName, IDictionary values, IDictionary oldValues, out IOrderedDictionary paramValues)
	{
		MethodInfo methodInfo = ((oldValues == null) ? ObjectType.GetMethod(methodName, new Type[1] { DataObjectType }) : ObjectType.GetMethod(methodName, new Type[2] { DataObjectType, DataObjectType }));
		if (methodInfo == null)
		{
			throw new InvalidOperationException(string.Concat("ObjectDataSource ", owner.ID, " could not find a method named '", methodName, "' with parameters of type '", DataObjectType, "' in '", ObjectType, "'."));
		}
		paramValues = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
		ParameterInfo[] parameters = methodInfo.GetParameters();
		if (oldValues != null)
		{
			if (FormatOldParameter(parameters[0].Name) == parameters[1].Name)
			{
				paramValues[parameters[0].Name] = CreateDataObject(values);
				paramValues[parameters[1].Name] = CreateDataObject(oldValues);
			}
			else
			{
				if (!(FormatOldParameter(parameters[1].Name) == parameters[0].Name))
				{
					throw new InvalidOperationException("Method '" + methodName + "' does not have any parameter that fits the value of OldValuesParameterFormatString.");
				}
				paramValues[parameters[0].Name] = CreateDataObject(oldValues);
				paramValues[parameters[1].Name] = CreateDataObject(values);
			}
		}
		else
		{
			paramValues[parameters[0].Name] = CreateDataObject(values);
		}
		return methodInfo;
	}

	private Exception CreateMethodException(string methodName, IOrderedDictionary parameters)
	{
		string text = "";
		foreach (string key in parameters.Keys)
		{
			text = text + key + ", ";
		}
		return new InvalidOperationException(string.Concat("ObjectDataSource ", owner.ID, " could not find a method named '", methodName, "' with parameters ", text, "in type '", ObjectType, "'."));
	}

	private object CreateDataObject(IDictionary values)
	{
		object obj = Activator.CreateInstance(DataObjectType);
		foreach (DictionaryEntry value2 in values)
		{
			PropertyInfo property = DataObjectType.GetProperty((string)value2.Key);
			if (property == null)
			{
				throw new InvalidOperationException(string.Concat("Property ", value2.Key, " not found in type '", DataObjectType, "'."));
			}
			object[] customAttributes = property.GetCustomAttributes(typeof(TypeConverterAttribute), inherit: true);
			Type propertyType = property.PropertyType;
			object value = value2.Value;
			object obj2 = ConvertParameterWithTypeConverter(customAttributes, propertyType, value);
			if (obj2 == null)
			{
				obj2 = ConvertParameter(propertyType, value);
			}
			property.SetValue(obj, obj2, null);
		}
		return obj;
	}

	private object CreateObjectInstance()
	{
		ObjectDataSourceEventArgs objectDataSourceEventArgs = new ObjectDataSourceEventArgs(null);
		OnObjectCreating(objectDataSourceEventArgs);
		if (objectDataSourceEventArgs.ObjectInstance != null)
		{
			return objectDataSourceEventArgs.ObjectInstance;
		}
		object objectInstance = Activator.CreateInstance(ObjectType);
		objectDataSourceEventArgs.ObjectInstance = objectInstance;
		OnObjectCreated(objectDataSourceEventArgs);
		return objectDataSourceEventArgs.ObjectInstance;
	}

	private void DisposeObjectInstance(object obj)
	{
		ObjectDataSourceDisposingEventArgs objectDataSourceDisposingEventArgs = new ObjectDataSourceDisposingEventArgs(obj);
		OnObjectDisposing(objectDataSourceDisposingEventArgs);
		if (!objectDataSourceDisposingEventArgs.Cancel && obj is IDisposable disposable)
		{
			disposable.Dispose();
		}
	}

	private object FindValueByName(string name, IDictionary values, bool format)
	{
		if (values == null)
		{
			return null;
		}
		foreach (DictionaryEntry value in values)
		{
			string strB = (format ? FormatOldParameter(value.Key.ToString()) : value.Key.ToString());
			if (string.Compare(name, strB, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return values[value.Key];
			}
		}
		return null;
	}

	private IOrderedDictionary MergeParameterValues(ParameterCollection viewParams, IDictionary values, IDictionary oldValues)
	{
		IOrderedDictionary values2 = viewParams.GetValues(context, owner);
		OrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
		foreach (string key in values2.Keys)
		{
			orderedDictionary[key] = values2[key];
			if (oldValues != null)
			{
				object obj = FindValueByName(key, oldValues, format: true);
				if (obj != null)
				{
					object value = viewParams[key].ConvertValue(obj);
					orderedDictionary[key] = value;
				}
			}
			if (values != null)
			{
				object obj2 = FindValueByName(key, values, format: false);
				if (obj2 != null)
				{
					object value2 = viewParams[key].ConvertValue(obj2);
					orderedDictionary[key] = value2;
				}
			}
		}
		if (values != null)
		{
			foreach (DictionaryEntry value3 in values)
			{
				if (FindValueByName((string)value3.Key, orderedDictionary, format: false) == null)
				{
					orderedDictionary[value3.Key] = value3.Value;
				}
			}
		}
		if (oldValues != null)
		{
			foreach (DictionaryEntry oldValue in oldValues)
			{
				string text2 = FormatOldParameter((string)oldValue.Key);
				if (FindValueByName(text2, orderedDictionary, format: false) == null)
				{
					orderedDictionary[text2] = oldValue.Value;
				}
			}
		}
		return orderedDictionary;
	}

	private object[] GetParameterArray(ParameterInfo[] methodParams, IOrderedDictionary viewParams, out ArrayList outParamInfos)
	{
		outParamInfos = null;
		object[] array = new object[methodParams.Length];
		foreach (ParameterInfo parameterInfo in methodParams)
		{
			if (!viewParams.Contains(parameterInfo.Name))
			{
				return null;
			}
			array[parameterInfo.Position] = ConvertParameter(parameterInfo.ParameterType, viewParams[parameterInfo.Name]);
			if (parameterInfo.ParameterType.IsByRef)
			{
				if (outParamInfos == null)
				{
					outParamInfos = new ArrayList();
				}
				outParamInfos.Add(parameterInfo);
			}
		}
		return array;
	}

	private object ConvertParameterWithTypeConverter(object[] attributes, Type targetType, object value)
	{
		if (attributes == null || attributes.Length == 0 || value == null)
		{
			return null;
		}
		for (int i = 0; i < attributes.Length; i++)
		{
			if (!(attributes[i] is TypeConverterAttribute typeConverterAttribute))
			{
				continue;
			}
			Type type = HttpApplication.LoadType(typeConverterAttribute.ConverterTypeName, throwOnMissing: false);
			if (!(type == null) && Activator.CreateInstance(type, targetType) is TypeConverter typeConverter)
			{
				if (typeConverter.CanConvertFrom(value.GetType()))
				{
					return typeConverter.ConvertFrom(value);
				}
				if (typeConverter.CanConvertFrom(typeof(string)))
				{
					return typeConverter.ConvertFrom(value.ToString());
				}
			}
		}
		return null;
	}

	private object ConvertParameter(Type targetType, object value)
	{
		return ConvertParameter(Type.GetTypeCode(targetType), value);
	}

	private object ConvertParameter(TypeCode targetType, object value)
	{
		if (value == null)
		{
			if (targetType != TypeCode.Object && targetType != TypeCode.String)
			{
				value = 0;
			}
			else if (ConvertNullToDBNull)
			{
				return DBNull.Value;
			}
		}
		if (targetType == TypeCode.Object || targetType == TypeCode.Empty)
		{
			return value;
		}
		return Convert.ChangeType(value, targetType);
	}

	private string FormatOldParameter(string name)
	{
		string text = OldValuesParameterFormatString;
		if (text.Length > 0)
		{
			return string.Format(text, name);
		}
		return name;
	}

	private void OnParametersChanged(object sender, EventArgs args)
	{
		OnDataSourceViewChanged(EventArgs.Empty);
	}

	/// <summary>Restores previously saved view state for the data source view.</summary>
	/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
	protected virtual void LoadViewState(object savedState)
	{
		object[] array = ((savedState == null) ? new object[5] : ((object[])savedState));
		((IStateManager)SelectParameters).LoadViewState(array[0]);
		((IStateManager)UpdateParameters).LoadViewState(array[1]);
		((IStateManager)DeleteParameters).LoadViewState(array[2]);
		((IStateManager)InsertParameters).LoadViewState(array[3]);
		((IStateManager)FilterParameters).LoadViewState(array[4]);
	}

	/// <summary>Saves the changes to the view state for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object since the time when the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise <see langword="null" />, if there is no view state associated with the object.</returns>
	protected virtual object SaveViewState()
	{
		object[] array = new object[5];
		if (selectParameters != null)
		{
			array[0] = ((IStateManager)selectParameters).SaveViewState();
		}
		if (updateParameters != null)
		{
			array[1] = ((IStateManager)updateParameters).SaveViewState();
		}
		if (deleteParameters != null)
		{
			array[2] = ((IStateManager)deleteParameters).SaveViewState();
		}
		if (insertParameters != null)
		{
			array[3] = ((IStateManager)insertParameters).SaveViewState();
		}
		if (filterParameters != null)
		{
			array[4] = ((IStateManager)filterParameters).SaveViewState();
		}
		object[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			if (array2[i] != null)
			{
				return array;
			}
		}
		return null;
	}

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="P:System.Web.UI.Control.ViewState" /> object for the control and persisted across requests for the same page.</summary>
	protected virtual void TrackViewState()
	{
		isTrackingViewState = true;
		if (selectParameters != null)
		{
			((IStateManager)selectParameters).TrackViewState();
		}
		if (filterParameters != null)
		{
			((IStateManager)filterParameters).TrackViewState();
		}
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
	/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise, <see langword="null" />.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	static ObjectDataSourceView()
	{
		Deleted = new object();
		Deleting = new object();
		Filtering = new object();
		Inserted = new object();
		Inserting = new object();
		ObjectCreated = new object();
		ObjectCreating = new object();
		ObjectDisposing = new object();
		Selected = new object();
		Selecting = new object();
		Updated = new object();
		Updating = new object();
	}
}
