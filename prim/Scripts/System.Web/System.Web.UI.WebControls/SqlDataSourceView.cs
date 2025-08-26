using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace System.Web.UI.WebControls;

/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control and provides an interface for data-bound controls to perform SQL data operations against relational databases.</summary>
public class SqlDataSourceView : DataSourceView, IStateManager
{
	private HttpContext context;

	private DbProviderFactory factory;

	private DbConnection connection;

	private bool cancelSelectOnNullParameter = true;

	private ConflictOptions conflictDetection;

	private string deleteCommand = string.Empty;

	private SqlDataSourceCommandType deleteCommandType;

	private string filterExpression;

	private string insertCommand = string.Empty;

	private SqlDataSourceCommandType insertCommandType;

	private string oldValuesParameterFormatString = "{0}";

	private string selectCommand;

	private SqlDataSourceCommandType selectCommandType;

	private string sortParameterName = string.Empty;

	private string updateCommand = string.Empty;

	private SqlDataSourceCommandType updateCommandType;

	private ParameterCollection deleteParameters;

	private ParameterCollection filterParameters;

	private ParameterCollection insertParameters;

	private ParameterCollection selectParameters;

	private ParameterCollection updateParameters;

	private bool tracking;

	private string name;

	private SqlDataSource owner;

	private static readonly object EventDeleted = new object();

	private static readonly object EventDeleting = new object();

	private static readonly object EventFiltering = new object();

	private static readonly object EventInserted = new object();

	private static readonly object EventInserting = new object();

	private static readonly object EventSelected = new object();

	private static readonly object EventSelecting = new object();

	private static readonly object EventUpdated = new object();

	private static readonly object EventUpdating = new object();

	/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
	/// <returns>
	///     <see langword="true" />, if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	bool IStateManager.IsTrackingViewState => IsTrackingViewState;

	/// <summary>Gets or sets a value indicating whether a data retrieval operation is canceled when any parameter that is contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection evaluates to <see langword="null" />.</summary>
	/// <returns>
	///     <see langword="true" />, if a data retrieval operation is canceled when a parameter contained in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection evaluated to <see langword="null" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool CancelSelectOnNullParameter
	{
		get
		{
			return cancelSelectOnNullParameter;
		}
		set
		{
			if (CancelSelectOnNullParameter != value)
			{
				cancelSelectOnNullParameter = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the delete operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />.</returns>
	public override bool CanDelete
	{
		get
		{
			if (DeleteCommand != null)
			{
				return DeleteCommand != "";
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the insert operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />.</returns>
	public override bool CanInsert
	{
		get
		{
			if (InsertCommand != null)
			{
				return InsertCommand != "";
			}
			return false;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the paging of retrieved data.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool CanPage => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports retrieving the total number of data rows, in addition to the set of data.</summary>
	/// <returns>
	///     <see langword="false" /> in all cases.</returns>
	public override bool CanRetrieveTotalRowCount => false;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports a sorted view on the retrieved data.</summary>
	/// <returns>
	///     <see langword="true" />, if sorting is supported; otherwise, <see langword="false" />.</returns>
	public override bool CanSort
	{
		get
		{
			if (owner.DataSourceMode != SqlDataSourceMode.DataSet)
			{
				if (SortParameterName != null)
				{
					return SortParameterName != "";
				}
				return false;
			}
			return true;
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control supports the update operation.</summary>
	/// <returns>
	///     <see langword="true" />, if the operation is supported; otherwise, <see langword="false" />.</returns>
	public override bool CanUpdate
	{
		get
		{
			if (UpdateCommand != null)
			{
				return UpdateCommand != "";
			}
			return false;
		}
	}

	/// <summary>Gets or sets the value indicating how the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control performs updates and deletes when data in a row in the underlying database changes during the time of the operation.</summary>
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

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to delete data from the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to delete data.</returns>
	public string DeleteCommand
	{
		get
		{
			return deleteCommand;
		}
		set
		{
			deleteCommand = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
	public SqlDataSourceCommandType DeleteCommandType
	{
		get
		{
			return deleteCommandType;
		}
		set
		{
			deleteCommandType = value;
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> property.</returns>
	[DefaultValue(null)]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public ParameterCollection DeleteParameters => GetParameterCollection(ref deleteParameters, propagateTrackViewState: false, subscribeChanged: false);

	/// <summary>Gets or sets a filtering expression that is applied when the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Select" /> method is called.</summary>
	/// <returns>A string that represents a filtering expression applied when data is retrieved using the <see cref="M:System.Web.UI.WebControls.SqlDataSource.Select(System.Web.UI.DataSourceSelectArguments)" /> method.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> property was set when the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> is in the <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataReader" /> mode. </exception>
	public string FilterExpression
	{
		get
		{
			return filterExpression ?? string.Empty;
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

	/// <summary>Gets a collection of parameters that are associated with any parameter placeholders in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> string.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.FilterExpression" /> property.</returns>
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	public ParameterCollection FilterParameters => GetParameterCollection(ref filterParameters, propagateTrackViewState: true, subscribeChanged: true);

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to insert data into the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to insert data.</returns>
	public string InsertCommand
	{
		get
		{
			return insertCommand;
		}
		set
		{
			insertCommand = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The value is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
	public SqlDataSourceCommandType InsertCommandType
	{
		get
		{
			return insertCommandType;
		}
		set
		{
			insertCommandType = value;
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property.</returns>
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	public ParameterCollection InsertParameters => GetParameterCollection(ref insertParameters, propagateTrackViewState: false, subscribeChanged: false);

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object is saving changes to its view state.</summary>
	/// <returns>
	///     <see langword="true" />, if the data source view is marked to save its state; otherwise, <see langword="false" />.</returns>
	protected bool IsTrackingViewState => tracking;

	/// <summary>Gets or sets a format string to apply to the names of any parameters that are passed to the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Update" /> method. </summary>
	/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> parameters passed to the <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.SqlDataSourceView.Update" /> methods. The default is "{0}".</returns>
	[DefaultValue("{0}")]
	public string OldValuesParameterFormatString
	{
		get
		{
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

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to retrieve data from the underlying database.</summary>
	/// <returns>An SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to retrieve data.</returns>
	public string SelectCommand
	{
		get
		{
			if (selectCommand == null)
			{
				return string.Empty;
			}
			return selectCommand;
		}
		set
		{
			if (!(SelectCommand == value))
			{
				selectCommand = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property is a SQL query or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
	public SqlDataSourceCommandType SelectCommandType
	{
		get
		{
			return selectCommandType;
		}
		set
		{
			selectCommandType = value;
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> property.</returns>
	public ParameterCollection SelectParameters => GetParameterCollection(ref selectParameters, propagateTrackViewState: true, subscribeChanged: true);

	/// <summary>Gets or sets the name of a stored procedure parameter that is used to sort retrieved data when data retrieval is performed using a stored procedure.</summary>
	/// <returns>The name of a stored procedure parameter used to sort retrieved data when data retrieval is performed using a stored procedure.</returns>
	public string SortParameterName
	{
		get
		{
			return sortParameterName;
		}
		set
		{
			if (!(SortParameterName == value))
			{
				sortParameterName = value;
				OnDataSourceViewChanged(EventArgs.Empty);
			}
		}
	}

	/// <summary>Gets or sets the SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object uses to update data in the underlying database.</summary>
	/// <returns>A SQL string that the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> uses to update data.</returns>
	public string UpdateCommand
	{
		get
		{
			return updateCommand;
		}
		set
		{
			updateCommand = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the text in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is a SQL statement or the name of a stored procedure.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values. The default is the <see cref="F:System.Web.UI.WebControls.SqlDataSourceCommandType.Text" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandType" /> values.</exception>
	public SqlDataSourceCommandType UpdateCommandType
	{
		get
		{
			return updateCommandType;
		}
		set
		{
			updateCommandType = value;
		}
	}

	/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property.</returns>
	[Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PersistenceMode(PersistenceMode.InnerProperty)]
	[DefaultValue(null)]
	public ParameterCollection UpdateParameters => GetParameterCollection(ref updateParameters, propagateTrackViewState: false, subscribeChanged: false);

	/// <summary>Gets the string that is used to prefix a parameter placeholder in a parameterized SQL query.</summary>
	/// <returns>The "@" string.</returns>
	protected virtual string ParameterPrefix
	{
		get
		{
			string providerName = owner.ProviderName;
			if ((providerName == null || providerName.Length != 0) && !(providerName == "System.Data.SqlClient"))
			{
				if (providerName == "System.Data.OracleClient")
				{
					return ":";
				}
				return "";
			}
			return "@";
		}
	}

	/// <summary>Occurs when a delete operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Deleted
	{
		add
		{
			base.Events.AddHandler(EventDeleted, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventDeleted, value);
		}
	}

	/// <summary>Occurs before a delete operation.</summary>
	public event SqlDataSourceCommandEventHandler Deleting
	{
		add
		{
			base.Events.AddHandler(EventDeleting, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventDeleting, value);
		}
	}

	/// <summary>Occurs before a filter operation.</summary>
	public event SqlDataSourceFilteringEventHandler Filtering
	{
		add
		{
			base.Events.AddHandler(EventFiltering, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventFiltering, value);
		}
	}

	/// <summary>Occurs when an insert operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Inserted
	{
		add
		{
			base.Events.AddHandler(EventInserted, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventInserted, value);
		}
	}

	/// <summary>Occurs before an insert operation.</summary>
	public event SqlDataSourceCommandEventHandler Inserting
	{
		add
		{
			base.Events.AddHandler(EventInserting, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventInserting, value);
		}
	}

	/// <summary>Occurs when a data retrieval operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Selected
	{
		add
		{
			base.Events.AddHandler(EventSelected, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventSelected, value);
		}
	}

	/// <summary>Occurs before a data retrieval operation.</summary>
	public event SqlDataSourceSelectingEventHandler Selecting
	{
		add
		{
			base.Events.AddHandler(EventSelecting, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventSelecting, value);
		}
	}

	/// <summary>Occurs when an update operation has completed.</summary>
	public event SqlDataSourceStatusEventHandler Updated
	{
		add
		{
			base.Events.AddHandler(EventUpdated, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventUpdated, value);
		}
	}

	/// <summary>Occurs before an update operation.</summary>
	public event SqlDataSourceCommandEventHandler Updating
	{
		add
		{
			base.Events.AddHandler(EventUpdating, value);
		}
		remove
		{
			base.Events.RemoveHandler(EventUpdating, value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> class setting the specified <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control as the owner of the current view.</summary>
	/// <param name="owner">The data source control with which the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> is associated. </param>
	/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it. </param>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" />.</param>
	public SqlDataSourceView(SqlDataSource owner, string name, HttpContext context)
		: base(owner, name)
	{
		this.owner = owner;
		this.name = name;
		this.context = context;
	}

	private void InitConnection()
	{
		if (factory == null)
		{
			factory = owner.GetDbProviderFactoryInternal();
		}
		if (connection == null)
		{
			connection = factory.CreateConnection();
			connection.ConnectionString = owner.ConnectionString;
		}
	}

	/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteParameters" /> collection, and the values that are in the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row key values for the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation to delete.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated only if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value.</param>
	/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanDelete" /> property is <see langword="false" />. </exception>
	public int Delete(IDictionary keys, IDictionary oldValues)
	{
		return ExecuteDelete(keys, oldValues);
	}

	/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.DeleteParameters" /> collection, and the values that are in the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row key values for the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation to delete.</param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated only if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value.</param>
	/// <returns>A value that represents the number of rows deleted from the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed. </exception>
	/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to access to the database.- or -The instance of the control is an <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control and access is denied to the path specified for the <see cref="P:System.Web.UI.WebControls.AccessDataSource.DataFile" /> property.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanDelete" /> property is <see langword="false" />. - or -A design-time relative path was not mapped correctly by the designer before using an instance of the <see cref="T:System.Web.UI.WebControls.AccessDataSource" /> control.</exception>
	protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
	{
		if (!CanDelete)
		{
			throw new NotSupportedException("Delete operation is not supported");
		}
		if (oldValues == null && ConflictDetection == ConflictOptions.CompareAllValues)
		{
			throw new InvalidOperationException("oldValues parameters should be specified when ConflictOptions is set to CompareAllValues");
		}
		InitConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = DeleteCommand;
		dbCommand.Connection = connection;
		if (DeleteCommandType == SqlDataSourceCommandType.Text)
		{
			dbCommand.CommandType = CommandType.Text;
		}
		else
		{
			dbCommand.CommandType = CommandType.StoredProcedure;
		}
		IDictionary dictionary;
		if (ConflictDetection == ConflictOptions.CompareAllValues)
		{
			dictionary = new Hashtable();
			if (keys != null)
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
		}
		else
		{
			dictionary = keys;
		}
		InitializeParameters(dbCommand, DeleteParameters, null, dictionary, parametersMayMatchOldValues: true);
		SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
		OnDeleting(sqlDataSourceCommandEventArgs);
		if (sqlDataSourceCommandEventArgs.Cancel)
		{
			return -1;
		}
		bool flag = connection.State == ConnectionState.Closed;
		if (flag)
		{
			connection.Open();
		}
		Exception ex = null;
		int num = -1;
		try
		{
			num = dbCommand.ExecuteNonQuery();
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		if (flag)
		{
			connection.Close();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, num, ex);
		OnDeleted(sqlDataSourceStatusEventArgs);
		if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw ex;
		}
		return num;
	}

	/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertParameters" /> collection, and the values that are in the specified <paramref name="values" /> collection.</summary>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of parameters for the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property to use to perform the insert database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> is not a parameterized SQL query, pass <see langword="null" />. </param>
	/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanInsert" /> property is <see langword="false" />. </exception>
	public int Insert(IDictionary values)
	{
		return ExecuteInsert(values);
	}

	/// <summary>Performs an insert operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> SQL string, any parameters that are specified in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertParameters" /> collection, and the values that are in the specified <paramref name="values" /> collection.</summary>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values used with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property to perform the insert database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.InsertCommand" /> property is not a parameterized SQL query, pass <see langword="null" />.</param>
	/// <returns>A value that represents the number of rows inserted into the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to gain access to the database.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanInsert" /> property is <see langword="false" />. </exception>
	protected override int ExecuteInsert(IDictionary values)
	{
		if (!CanInsert)
		{
			throw new NotSupportedException("Insert operation is not supported");
		}
		InitConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = InsertCommand;
		dbCommand.Connection = connection;
		if (InsertCommandType == SqlDataSourceCommandType.Text)
		{
			dbCommand.CommandType = CommandType.Text;
		}
		else
		{
			dbCommand.CommandType = CommandType.StoredProcedure;
		}
		InitializeParameters(dbCommand, InsertParameters, values, null, parametersMayMatchOldValues: false);
		SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
		OnInserting(sqlDataSourceCommandEventArgs);
		if (sqlDataSourceCommandEventArgs.Cancel)
		{
			return -1;
		}
		bool flag = connection.State == ConnectionState.Closed;
		if (flag)
		{
			connection.Open();
		}
		Exception ex = null;
		int num = -1;
		try
		{
			num = dbCommand.ExecuteNonQuery();
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		if (flag)
		{
			connection.Close();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		OnInserted(new SqlDataSourceStatusEventArgs(dbCommand, num, ex));
		if (ex != null)
		{
			throw ex;
		}
		return num;
	}

	/// <summary>Retrieves data from the underlying database using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="selectArgs" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	public IEnumerable Select(DataSourceSelectArguments arguments)
	{
		return ExecuteSelect(arguments);
	}

	/// <summary>Retrieves data from the underlying database using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectCommand" /> SQL string and any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SelectParameters" /> collection.</summary>
	/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object used to request operations on the data beyond basic data retrieval.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
	/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.- or -Caching is enabled but the <see cref="P:System.Web.UI.WebControls.SqlDataSource.DataSourceMode" /> property of the data source is not set to <see cref="F:System.Web.UI.WebControls.SqlDataSourceMode.DataSet" />.- or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.SortParameterName" /> property is set but <see cref="P:System.Data.SqlClient.SqlCommand.CommandType" /> is not set to <see cref="F:System.Data.CommandType.StoredProcedure" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The data source cannot create a database connection.- or -Caching is enabled but the internal cache and command types do not match.</exception>
	protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
	{
		if (SortParameterName.Length > 0 && SelectCommandType == SqlDataSourceCommandType.Text)
		{
			throw new NotSupportedException("The SortParameterName property is only supported with stored procedure commands in SqlDataSource");
		}
		if (arguments.SortExpression.Length > 0 && owner.DataSourceMode == SqlDataSourceMode.DataReader)
		{
			throw new NotSupportedException("SqlDataSource cannot sort. Set DataSourceMode to DataSet to enable sorting.");
		}
		if (arguments.StartRowIndex > 0 || arguments.MaximumRows > 0)
		{
			throw new NotSupportedException("SqlDataSource does not have paging enabled. Set the DataSourceMode to DataSet to enable paging.");
		}
		if (FilterExpression.Length > 0 && owner.DataSourceMode == SqlDataSourceMode.DataReader)
		{
			throw new NotSupportedException("SqlDataSource only supports filtering when the data source's DataSourceMode is set to DataSet.");
		}
		InitConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = SelectCommand;
		dbCommand.Connection = connection;
		if (SelectCommandType == SqlDataSourceCommandType.Text)
		{
			dbCommand.CommandType = CommandType.Text;
		}
		else
		{
			dbCommand.CommandType = CommandType.StoredProcedure;
			if (SortParameterName.Length > 0 && arguments.SortExpression.Length > 0)
			{
				dbCommand.Parameters.Add(CreateDbParameter(SortParameterName, arguments.SortExpression));
			}
		}
		if (SelectParameters.Count > 0)
		{
			InitializeParameters(dbCommand, SelectParameters, null, null, parametersMayMatchOldValues: false);
		}
		Exception ex = null;
		if (owner.DataSourceMode == SqlDataSourceMode.DataSet)
		{
			DataView dataView = null;
			if (owner.EnableCaching)
			{
				dataView = (DataView)owner.Cache.GetCachedObject(SelectCommand, SelectParameters);
			}
			if (dataView == null)
			{
				SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs = new SqlDataSourceSelectingEventArgs(dbCommand, arguments);
				OnSelecting(sqlDataSourceSelectingEventArgs);
				if (sqlDataSourceSelectingEventArgs.Cancel || !PrepareNullParameters(dbCommand, CancelSelectOnNullParameter))
				{
					return null;
				}
				try
				{
					DbDataAdapter dbDataAdapter = factory.CreateDataAdapter();
					DataSet dataSet = new DataSet();
					dbDataAdapter.SelectCommand = dbCommand;
					dbDataAdapter.Fill(dataSet, name);
					dataView = dataSet.Tables[0].DefaultView;
					if (dataView == null)
					{
						throw new InvalidOperationException();
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
				}
				int affectedRows = dataView?.Count ?? 0;
				SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows, ex);
				OnSelected(sqlDataSourceStatusEventArgs);
				if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
				{
					throw ex;
				}
				if (owner.EnableCaching)
				{
					owner.Cache.SetCachedObject(SelectCommand, selectParameters, dataView);
				}
			}
			if (SortParameterName.Length == 0 || SelectCommandType == SqlDataSourceCommandType.Text)
			{
				dataView.Sort = arguments.SortExpression;
			}
			if (FilterExpression.Length > 0)
			{
				IOrderedDictionary values = FilterParameters.GetValues(context, owner);
				SqlDataSourceFilteringEventArgs sqlDataSourceFilteringEventArgs = new SqlDataSourceFilteringEventArgs(values);
				OnFiltering(sqlDataSourceFilteringEventArgs);
				if (!sqlDataSourceFilteringEventArgs.Cancel)
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
		SqlDataSourceSelectingEventArgs sqlDataSourceSelectingEventArgs2 = new SqlDataSourceSelectingEventArgs(dbCommand, arguments);
		OnSelecting(sqlDataSourceSelectingEventArgs2);
		if (sqlDataSourceSelectingEventArgs2.Cancel || !PrepareNullParameters(dbCommand, CancelSelectOnNullParameter))
		{
			return null;
		}
		DbDataReader dbDataReader = null;
		bool flag = connection.State == ConnectionState.Closed;
		if (flag)
		{
			connection.Open();
		}
		try
		{
			dbDataReader = dbCommand.ExecuteReader(flag ? CommandBehavior.CloseConnection : CommandBehavior.Default);
		}
		catch (Exception ex3)
		{
			ex = ex3;
		}
		int affectedRows2 = dbDataReader?.RecordsAffected ?? 0;
		SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs2 = new SqlDataSourceStatusEventArgs(dbCommand, affectedRows2, ex);
		OnSelected(sqlDataSourceStatusEventArgs2);
		if (ex != null && !sqlDataSourceStatusEventArgs2.ExceptionHandled)
		{
			throw ex;
		}
		return dbDataReader;
	}

	private static bool PrepareNullParameters(DbCommand command, bool cancelIfHas)
	{
		for (int i = 0; i < command.Parameters.Count; i++)
		{
			DbParameter dbParameter = command.Parameters[i];
			if (dbParameter.Value == null && (dbParameter.Direction & ParameterDirection.Input) != 0)
			{
				if (cancelIfHas)
				{
					return false;
				}
				dbParameter.Value = DBNull.Value;
			}
		}
		return true;
	}

	/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> SQL string, any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateParameters" /> collection, and the values that are in the specified <paramref name="keys" />, <paramref name="values" />, and <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no keys associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass <see langword="null" />.</param>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass <see langword="null" />. </param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that represents the original values in the database. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass <see langword="null" />.</param>
	/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. </exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanUpdate" /> property is <see langword="false" />. </exception>
	public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
	{
		return ExecuteUpdate(keys, values, oldValues);
	}

	/// <summary>Performs an update operation using the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> SQL string, any parameters that are in the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateParameters" /> collection, and the values that are in the specified <paramref name="keys" />, <paramref name="values" />, and <paramref name="oldValues" /> collections.</summary>
	/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no keys associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is not a parameterized SQL query, pass <see langword="null" />.</param>
	/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of values to use with the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property to perform the update database operation. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> is not a parameterized SQL query, pass <see langword="null" />. </param>
	/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> that represents the original values in the database. If there are no parameters associated with the query or if the <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.UpdateCommand" /> property is not a parameterized SQL query, pass <see langword="null" />.</param>
	/// <returns>A value that represents the number of rows updated in the underlying database.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> cannot establish a connection with the underlying data source. - or -The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value and no <paramref name="oldValues" /> parameters are passed.</exception>
	/// <exception cref="T:System.Web.HttpException">The current user does not have the correct permissions to gain access to the database.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.SqlDataSourceView.CanUpdate" /> property is <see langword="false" />. </exception>
	protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
	{
		if (!CanUpdate)
		{
			throw new NotSupportedException("Update operation is not supported");
		}
		if (oldValues == null && ConflictDetection == ConflictOptions.CompareAllValues)
		{
			throw new InvalidOperationException("oldValues parameters should be specified when ConflictOptions is set to CompareAllValues");
		}
		InitConnection();
		DbCommand dbCommand = factory.CreateCommand();
		dbCommand.CommandText = UpdateCommand;
		dbCommand.Connection = connection;
		if (UpdateCommandType == SqlDataSourceCommandType.Text)
		{
			dbCommand.CommandType = CommandType.Text;
		}
		else
		{
			dbCommand.CommandType = CommandType.StoredProcedure;
		}
		IDictionary dictionary;
		if (ConflictDetection == ConflictOptions.CompareAllValues)
		{
			dictionary = new OrderedDictionary();
			if (keys != null)
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
		}
		else
		{
			dictionary = keys;
		}
		InitializeParameters(dbCommand, UpdateParameters, values, dictionary, ConflictDetection == ConflictOptions.OverwriteChanges);
		SqlDataSourceCommandEventArgs sqlDataSourceCommandEventArgs = new SqlDataSourceCommandEventArgs(dbCommand);
		OnUpdating(sqlDataSourceCommandEventArgs);
		if (sqlDataSourceCommandEventArgs.Cancel)
		{
			return -1;
		}
		bool flag = connection.State == ConnectionState.Closed;
		if (flag)
		{
			connection.Open();
		}
		Exception ex = null;
		int num = -1;
		try
		{
			num = dbCommand.ExecuteNonQuery();
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		if (flag)
		{
			connection.Close();
		}
		OnDataSourceViewChanged(EventArgs.Empty);
		SqlDataSourceStatusEventArgs sqlDataSourceStatusEventArgs = new SqlDataSourceStatusEventArgs(dbCommand, num, ex);
		OnUpdated(sqlDataSourceStatusEventArgs);
		if (ex != null && !sqlDataSourceStatusEventArgs.ExceptionHandled)
		{
			throw ex;
		}
		return num;
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

	private object FindValueByName(string parameterName, IDictionary values, bool format)
	{
		if (values == null)
		{
			return null;
		}
		foreach (DictionaryEntry value in values)
		{
			string strB = (format ? FormatOldParameter(value.Key.ToString()) : value.Key.ToString());
			if (string.Compare(parameterName, strB, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return values[value.Key];
			}
		}
		return null;
	}

	private void InitializeParameters(DbCommand command, ParameterCollection parameters, IDictionary values, IDictionary oldValues, bool parametersMayMatchOldValues)
	{
		IOrderedDictionary values2 = parameters.GetValues(context, owner);
		foreach (string key in values2.Keys)
		{
			Parameter parameter = parameters[key];
			object obj = FindValueByName(key, values, format: false);
			string text2 = key;
			if (obj == null)
			{
				obj = FindValueByName(key, oldValues, format: true);
			}
			if (obj == null && parametersMayMatchOldValues)
			{
				obj = FindValueByName(key, oldValues, format: false);
				text2 = FormatOldParameter(key);
			}
			if (obj != null)
			{
				object value = parameter.ConvertValue(obj);
				DbParameter dbParameter = CreateDbParameter(text2, value, parameter.Direction, parameter.Size);
				if (!command.Parameters.Contains(dbParameter.ParameterName))
				{
					command.Parameters.Add(dbParameter);
				}
			}
			else
			{
				command.Parameters.Add(CreateDbParameter(parameter.Name, values2[key], parameter.Direction, parameter.Size));
			}
		}
		if (values != null)
		{
			foreach (DictionaryEntry value2 in values)
			{
				if (!command.Parameters.Contains(ParameterPrefix + (string)value2.Key))
				{
					command.Parameters.Add(CreateDbParameter((string)value2.Key, value2.Value));
				}
			}
		}
		if (oldValues == null)
		{
			return;
		}
		foreach (DictionaryEntry oldValue in oldValues)
		{
			if (!command.Parameters.Contains(ParameterPrefix + FormatOldParameter((string)oldValue.Key)))
			{
				command.Parameters.Add(CreateDbParameter(FormatOldParameter((string)oldValue.Key), oldValue.Value));
			}
		}
	}

	private DbParameter CreateDbParameter(string name, object value)
	{
		return CreateDbParameter(name, value, ParameterDirection.Input, -1);
	}

	private DbParameter CreateDbParameter(string name, object value, ParameterDirection dir, int size)
	{
		DbParameter dbParameter = factory.CreateParameter();
		dbParameter.ParameterName = ParameterPrefix + name;
		dbParameter.Value = value;
		dbParameter.Direction = dir;
		if (size != -1)
		{
			dbParameter.Size = size;
		}
		return dbParameter;
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
	/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> state to restore.</param>
	void IStateManager.LoadViewState(object savedState)
	{
		LoadViewState(savedState);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> view state; otherwise, <see langword="null" />, if there is no view state associated with the object.</returns>
	object IStateManager.SaveViewState()
	{
		return SaveViewState();
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
	void IStateManager.TrackViewState()
	{
		TrackViewState();
	}

	private NotSupportedException CreateNotSupportedException(string capabilityName)
	{
		return new NotSupportedException("Data source does not have the '" + capabilityName + "' capability enabled.");
	}

	/// <summary>Compares the capabilities that are requested for an <see cref="M:System.Web.UI.WebControls.SqlDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the view supports and is called by the <see cref="M:System.Web.UI.DataSourceSelectArguments.RaiseUnsupportedCapabilitiesError(System.Web.UI.DataSourceView)" /> method.</summary>
	/// <param name="capability">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values that is compared against the capabilities that the view supports.</param>
	/// <exception cref="T:System.NotSupportedException">The data source does not have the selected <paramref name="capability" /> enabled.</exception>
	protected internal override void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
	{
		if ((capability & DataSourceCapabilities.Sort) != 0 && !CanSort)
		{
			throw CreateNotSupportedException("Sort");
		}
		if ((capability & DataSourceCapabilities.Page) != 0 && !CanPage)
		{
			throw CreateNotSupportedException("Page");
		}
		if ((capability & DataSourceCapabilities.RetrieveTotalRowCount) != 0 && !CanRetrieveTotalRowCount)
		{
			throw CreateNotSupportedException("RetrieveTotalRowCount");
		}
	}

	/// <summary>Restores the previously saved view state for the data source view.</summary>
	/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> state to restore. </param>
	protected virtual void LoadViewState(object savedState)
	{
		if (savedState is object[] array)
		{
			if (array[0] != null)
			{
				((IStateManager)deleteParameters).LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				((IStateManager)filterParameters).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)insertParameters).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)selectParameters).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)updateParameters).LoadViewState(array[4]);
			}
		}
	}

	/// <summary>Saves the changes to the view state for the  <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> control since the time that the page was posted back to the server.</summary>
	/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> view state; otherwise, <see langword="null" />, if there is no view state associated with the object.</returns>
	protected virtual object SaveViewState()
	{
		object[] array = new object[5];
		if (deleteParameters != null)
		{
			array[0] = ((IStateManager)deleteParameters).SaveViewState();
		}
		if (filterParameters != null)
		{
			array[1] = ((IStateManager)filterParameters).SaveViewState();
		}
		if (insertParameters != null)
		{
			array[2] = ((IStateManager)insertParameters).SaveViewState();
		}
		if (selectParameters != null)
		{
			array[3] = ((IStateManager)selectParameters).SaveViewState();
		}
		if (updateParameters != null)
		{
			array[4] = ((IStateManager)updateParameters).SaveViewState();
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

	/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.SqlDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="T:System.Web.UI.StateBag" /> object for the control and persisted across requests for the same page.</summary>
	protected virtual void TrackViewState()
	{
		tracking = true;
		if (filterParameters != null)
		{
			((IStateManager)filterParameters).TrackViewState();
		}
		if (selectParameters != null)
		{
			((IStateManager)selectParameters).TrackViewState();
		}
	}

	private void ParametersChanged(object source, EventArgs args)
	{
		OnDataSourceViewChanged(EventArgs.Empty);
	}

	private ParameterCollection GetParameterCollection(ref ParameterCollection output, bool propagateTrackViewState, bool subscribeChanged)
	{
		if (output != null)
		{
			return output;
		}
		output = new ParameterCollection();
		if (subscribeChanged)
		{
			output.ParametersChanged += ParametersChanged;
		}
		if (IsTrackingViewState && propagateTrackViewState)
		{
			((IStateManager)output).TrackViewState();
		}
		return output;
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Deleted" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed a delete operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
	protected virtual void OnDeleted(SqlDataSourceStatusEventArgs e)
	{
		if (HasEvents() && base.Events[EventDeleted] is SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler)
		{
			sqlDataSourceStatusEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Deleting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts a delete operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
	protected virtual void OnDeleting(SqlDataSourceCommandEventArgs e)
	{
		if (HasEvents() && base.Events[EventDeleting] is SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler)
		{
			sqlDataSourceCommandEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Filtering" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control filters the results of a select operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceFilteringEventArgs" /> that contains the event data. </param>
	protected virtual void OnFiltering(SqlDataSourceFilteringEventArgs e)
	{
		if (HasEvents() && base.Events[EventFiltering] is SqlDataSourceFilteringEventHandler sqlDataSourceFilteringEventHandler)
		{
			sqlDataSourceFilteringEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Inserted" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed an insert operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
	protected virtual void OnInserted(SqlDataSourceStatusEventArgs e)
	{
		if (HasEvents() && base.Events[EventInserted] is SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler)
		{
			sqlDataSourceStatusEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Inserting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts an insert operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
	protected virtual void OnInserting(SqlDataSourceCommandEventArgs e)
	{
		if (HasEvents() && base.Events[EventInserting] is SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler)
		{
			sqlDataSourceCommandEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Selected" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed a data retrieval operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
	protected virtual void OnSelected(SqlDataSourceStatusEventArgs e)
	{
		if (HasEvents() && base.Events[EventSelected] is SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler)
		{
			sqlDataSourceStatusEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Selecting" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts a data retrieval operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs" /> that contains the event data. </param>
	protected virtual void OnSelecting(SqlDataSourceSelectingEventArgs e)
	{
		if (HasEvents() && base.Events[EventSelecting] is SqlDataSourceSelectingEventHandler sqlDataSourceSelectingEventHandler)
		{
			sqlDataSourceSelectingEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Updated" /> event after the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control has completed an update operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceStatusEventArgs" /> that contains the event data. </param>
	protected virtual void OnUpdated(SqlDataSourceStatusEventArgs e)
	{
		if (owner.EnableCaching)
		{
			owner.Cache.Expire();
		}
		if (HasEvents() && base.Events[EventUpdated] is SqlDataSourceStatusEventHandler sqlDataSourceStatusEventHandler)
		{
			sqlDataSourceStatusEventHandler(this, e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.SqlDataSourceView.Updating" /> event before the <see cref="T:System.Web.UI.WebControls.SqlDataSource" /> control attempts an update operation.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.SqlDataSourceCommandEventArgs" /> that contains the event data. </param>
	protected virtual void OnUpdating(SqlDataSourceCommandEventArgs e)
	{
		if (HasEvents() && base.Events[EventUpdating] is SqlDataSourceCommandEventHandler sqlDataSourceCommandEventHandler)
		{
			sqlDataSourceCommandEventHandler(this, e);
		}
	}
}
