namespace System.Web.Routing;

internal sealed class ParameterSubsegment : PathSubsegment
{
	public bool IsCatchAll { get; private set; }

	public string ParameterName { get; private set; }

	public ParameterSubsegment(string parameterName)
	{
		if (parameterName.StartsWith("*", StringComparison.Ordinal))
		{
			ParameterName = parameterName.Substring(1);
			IsCatchAll = true;
		}
		else
		{
			ParameterName = parameterName;
		}
	}
}
