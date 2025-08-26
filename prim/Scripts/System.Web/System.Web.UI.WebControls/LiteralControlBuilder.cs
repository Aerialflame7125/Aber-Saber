namespace System.Web.UI.WebControls;

/// <summary>Interacts with the parser to build a <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
public class LiteralControlBuilder : ControlBuilder
{
	/// <summary>Determines whether the control builder should process the white space literals that are represented by the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	/// <returns>
	///     <see langword="false" />.</returns>
	public override bool AllowWhitespaceLiterals()
	{
		return false;
	}

	/// <summary>Throws <see cref="T:System.Web.HttpException" />, because adding child control builders does not apply to the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> object to add the child control builders to. </param>
	/// <exception cref="T:System.Web.HttpException">An attempt is made to use this method. </exception>
	public override void AppendSubBuilder(ControlBuilder subBuilder)
	{
		throw new HttpException("LiteralControlBuilder should never be called");
	}

	/// <summary>Adds the specified literal content to a control. The <see cref="M:System.Web.UI.WebControls.LiteralControlBuilder.AppendLiteralString(System.String)" /> method is called by the ASP.NET page framework.</summary>
	/// <param name="s">The content to add to the control.</param>
	/// <exception cref="T:System.Web.HttpException">The string literal is not well formed. </exception>
	public override void AppendLiteralString(string s)
	{
		base.AppendLiteralString(s);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LiteralControlBuilder" /> class. </summary>
	public LiteralControlBuilder()
	{
	}
}
