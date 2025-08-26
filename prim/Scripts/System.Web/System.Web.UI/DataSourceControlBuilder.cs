namespace System.Web.UI;

/// <summary>Supports the page parser in building controls that are connected to a data provider. This class cannot be inherited.</summary>
public sealed class DataSourceControlBuilder : ControlBuilder
{
	/// <summary>Determines whether white-space literals are permitted in the content between a control's opening and closing tags.</summary>
	/// <returns>Always <see langword="false" />.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceControlBuilder" /> class. </summary>
	public DataSourceControlBuilder()
	{
	}
}
