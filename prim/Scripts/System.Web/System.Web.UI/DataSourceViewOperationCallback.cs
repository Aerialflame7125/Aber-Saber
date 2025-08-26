namespace System.Web.UI;

/// <summary>Represents the asynchronous callback method that a data-bound control supplies to a data source view for asynchronous insert, update, or delete data operations.</summary>
/// <param name="affectedRecords">The number of records that the data operation affected.</param>
/// <param name="ex">An <see cref="T:System.Exception" />, if one is thrown by the data operation during processing.</param>
/// <returns>A value indicating whether any exceptions thrown during the data operation were handled.</returns>
public delegate bool DataSourceViewOperationCallback(int affectedRecords, Exception ex);
