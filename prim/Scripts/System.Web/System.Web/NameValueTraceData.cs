namespace System.Web;

internal sealed class NameValueTraceData
{
	public string Name;

	public string Value;

	public NameValueTraceData(string name, string value)
	{
		Name = name;
		Value = value;
	}
}
