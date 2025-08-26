namespace System.Web.UI;

internal sealed class MainDirectiveAttribute<T>
{
	private string unparsedValue;

	private T value;

	private bool isExpression;

	public string UnparsedValue => unparsedValue;

	public bool IsExpression => isExpression;

	public T Value => value;

	public MainDirectiveAttribute(string value)
	{
		unparsedValue = value;
		if (value != null)
		{
			isExpression = BaseParser.IsExpression(value);
		}
	}

	public MainDirectiveAttribute(T value, bool unused)
	{
		this.value = value;
	}
}
