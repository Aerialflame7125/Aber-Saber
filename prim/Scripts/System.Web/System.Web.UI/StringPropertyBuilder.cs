namespace System.Web.UI;

internal class StringPropertyBuilder : ControlBuilder
{
	private string prop_name;

	public string PropertyName => prop_name;

	public StringPropertyBuilder(string prop_name)
	{
		this.prop_name = prop_name;
	}

	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	public override void AppendSubBuilder(ControlBuilder subBuilder)
	{
		throw new HttpException("StringPropertyBuilder should never be called");
	}
}
