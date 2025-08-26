namespace System.Web;

internal sealed class InfoTraceData
{
	public string Category;

	public string Message;

	public string Exception;

	public double TimeSinceFirst;

	public double TimeSinceLast;

	public bool IsWarning;

	public InfoTraceData(string category, string message, string exception, double timeSinceFirst, double timeSinceLast, bool isWarning)
	{
		Category = category;
		Message = message;
		Exception = exception;
		TimeSinceFirst = timeSinceFirst;
		TimeSinceLast = timeSinceLast;
		IsWarning = isWarning;
	}
}
