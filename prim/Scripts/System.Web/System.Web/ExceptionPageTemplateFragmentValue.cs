namespace System.Web;

internal sealed class ExceptionPageTemplateFragmentValue
{
	private Func<string, string> valueProvider;

	private string value;

	private string name;

	public string Value
	{
		get
		{
			if (valueProvider != null)
			{
				return valueProvider(name);
			}
			return value;
		}
	}

	public ExceptionPageTemplateFragmentValue(string name, Func<string, string> valueProvider)
	{
		this.valueProvider = valueProvider;
	}

	public ExceptionPageTemplateFragmentValue(string name, string value)
	{
		this.name = name;
		this.value = value;
	}
}
