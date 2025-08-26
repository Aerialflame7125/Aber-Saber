namespace System.Web.Handlers;

[Serializable]
internal class TraceNotAvailableException : HttpException
{
	private bool notLocal;

	internal override string Description
	{
		get
		{
			if (notLocal)
			{
				return "Trace is not enabled for remote clients.";
			}
			return "Trace.axd is not enabled in the configuration file for this application.";
		}
	}

	public TraceNotAvailableException(bool notLocal)
		: base(notLocal ? 403 : 500, "Trace Error")
	{
		this.notLocal = notLocal;
	}
}
