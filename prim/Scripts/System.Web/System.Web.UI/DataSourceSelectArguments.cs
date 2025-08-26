namespace System.Web.UI;

/// <summary>Provides a mechanism that data-bound controls use to request data-related operations from data source controls when data is retrieved. This class cannot be inherited.</summary>
public sealed class DataSourceSelectArguments
{
	private string sortExpression;

	private int startingRowIndex;

	private int maxRows;

	private bool getTotalRowCount;

	private int totalRowCount = -1;

	private DataSourceCapabilities dsc;

	/// <summary>Gets a <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object with the sort expression set to <see cref="F:System.String.Empty" />. </summary>
	/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object.</returns>
	public static DataSourceSelectArguments Empty => new DataSourceSelectArguments();

	private DataSourceCapabilities RequestedCapabilities
	{
		get
		{
			DataSourceCapabilities dataSourceCapabilities = DataSourceCapabilities.None;
			if (!string.IsNullOrEmpty(SortExpression))
			{
				dataSourceCapabilities |= DataSourceCapabilities.Sort;
			}
			if (RetrieveTotalRowCount)
			{
				dataSourceCapabilities |= DataSourceCapabilities.RetrieveTotalRowCount;
			}
			if (StartRowIndex > 0 || MaximumRows > 0)
			{
				dataSourceCapabilities |= DataSourceCapabilities.Page;
			}
			return dataSourceCapabilities;
		}
	}

	/// <summary>Gets or sets a value that represents the maximum number of data rows that a data source control returns for a data retrieval operation.</summary>
	/// <returns>The maximum number of data rows that a data source returns for a data retrieval operation. The default value is 0, which indicates that all possible data rows are returned.</returns>
	public int MaximumRows
	{
		get
		{
			return maxRows;
		}
		set
		{
			maxRows = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether a data source control should retrieve a count of all the data rows during a data retrieval operation.</summary>
	/// <returns>
	///     <see langword="true" /> if the data source control should retrieve a total data row count; otherwise, <see langword="false" />.</returns>
	public bool RetrieveTotalRowCount
	{
		get
		{
			return getTotalRowCount;
		}
		set
		{
			getTotalRowCount = value;
		}
	}

	/// <summary>Gets or sets an expression that the data source view uses to sort the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.Select(System.Web.UI.DataSourceSelectArguments,System.Web.UI.DataSourceViewSelectCallback)" /> method.</summary>
	/// <returns>A string that the data source view uses to sort data retrieved by the <see cref="M:System.Web.UI.DataSourceView.Select(System.Web.UI.DataSourceSelectArguments,System.Web.UI.DataSourceViewSelectCallback)" /> method. <see cref="F:System.String.Empty" /> is returned if sort expression has not been set.</returns>
	public string SortExpression
	{
		get
		{
			if (sortExpression == null)
			{
				return string.Empty;
			}
			return sortExpression;
		}
		set
		{
			sortExpression = value;
		}
	}

	/// <summary>Gets or sets a value that represents the starting position the data source control should use when retrieving data rows during a data retrieval operation.</summary>
	/// <returns>The starting row position from which a data source control retrieves data. The default value is 0, which indicates that the starting position is the beginning of the result set.</returns>
	public int StartRowIndex
	{
		get
		{
			return startingRowIndex;
		}
		set
		{
			startingRowIndex = value;
		}
	}

	/// <summary>Gets or sets the number of rows retrieved during a data retrieval operation.</summary>
	/// <returns>The total number of data rows retrieved by the data retrieval operation. </returns>
	public int TotalRowCount
	{
		get
		{
			return totalRowCount;
		}
		set
		{
			totalRowCount = value;
		}
	}

	/// <summary>Initializes a new default instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class. </summary>
	public DataSourceSelectArguments()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified sort expression.</summary>
	/// <param name="sortExpression">A sort expression that data source controls use to sort the result of a data retrieval operation before the result is returned to a caller.</param>
	public DataSourceSelectArguments(string sortExpression)
	{
		this.sortExpression = sortExpression;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified starting position and number of rows to return for paging scenarios.</summary>
	/// <param name="startRowIndex">The index of the data row that marks the beginning of data returned by a data retrieval operation.</param>
	/// <param name="maximumRows">The maximum number of rows that a data retrieval operation returns.</param>
	public DataSourceSelectArguments(int startRowIndex, int maximumRows)
	{
		startingRowIndex = startRowIndex;
		maxRows = maximumRows;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> class with the specified sort expression, starting position, and number of rows to return for paging scenarios.</summary>
	/// <param name="sortExpression">A sort expression that data source controls use to sort the result of a data retrieval operation before the result is returned to a caller.</param>
	/// <param name="startRowIndex">The index of the data row that marks the beginning of data returned by a data retrieval operation.</param>
	/// <param name="maximumRows">The maximum number of rows that a data retrieval operation returns.</param>
	public DataSourceSelectArguments(string sortExpression, int startRowIndex, int maximumRows)
	{
		this.sortExpression = sortExpression;
		startingRowIndex = startRowIndex;
		maxRows = maximumRows;
	}

	/// <summary>Adds one capability to the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> instance, which is used to compare supported capabilities and requested capabilities. </summary>
	/// <param name="capabilities">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values. </param>
	public void AddSupportedCapabilities(DataSourceCapabilities capabilities)
	{
		dsc |= capabilities;
	}

	/// <summary>Determines whether the specified <see cref="T:System.Web.UI.DataSourceSelectArguments" /> instance is equal to the current instance.</summary>
	/// <param name="obj">The <see cref="T:System.Web.UI.DataSourceSelectArguments" /> to compare with the current one.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <see cref="T:System.Web.UI.DataSourceSelectArguments" /> is equal to the current instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		if (!(obj is DataSourceSelectArguments dataSourceSelectArguments))
		{
			return false;
		}
		if (SortExpression == dataSourceSelectArguments.SortExpression && StartRowIndex == dataSourceSelectArguments.StartRowIndex && MaximumRows == dataSourceSelectArguments.MaximumRows && RetrieveTotalRowCount == dataSourceSelectArguments.RetrieveTotalRowCount)
		{
			return TotalRowCount == dataSourceSelectArguments.TotalRowCount;
		}
		return false;
	}

	/// <summary>Returns the hash code for the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> type.</summary>
	/// <returns>The hash code for the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> type.</returns>
	public override int GetHashCode()
	{
		return ((SortExpression != null) ? SortExpression.GetHashCode() : 0) ^ StartRowIndex ^ MaximumRows ^ RetrieveTotalRowCount.GetHashCode() ^ TotalRowCount;
	}

	/// <summary>Compares the capabilities requested for an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the specified data source view supports.</summary>
	/// <param name="view">The data source view that performs the data retrieval operation.</param>
	/// <exception cref="T:System.NotSupportedException">The data source view does not support the data source capability specified.</exception>
	public void RaiseUnsupportedCapabilitiesError(DataSourceView view)
	{
		DataSourceCapabilities requestedCapabilities = RequestedCapabilities;
		DataSourceCapabilities dataSourceCapabilities = (requestedCapabilities ^ dsc) & requestedCapabilities;
		if (dataSourceCapabilities != 0)
		{
			if ((dataSourceCapabilities & DataSourceCapabilities.RetrieveTotalRowCount) > DataSourceCapabilities.None)
			{
				dataSourceCapabilities = DataSourceCapabilities.RetrieveTotalRowCount;
			}
			else if ((dataSourceCapabilities & DataSourceCapabilities.Page) > DataSourceCapabilities.None)
			{
				dataSourceCapabilities = DataSourceCapabilities.Page;
			}
			view.RaiseUnsupportedCapabilityError(dataSourceCapabilities);
		}
	}
}
