using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Represents HTML elements, text, and any other strings in an ASP.NET page that do not require processing on the server.</summary>
[ToolboxItem(false)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class LiteralControl : Control, ITextControl
{
	private string _text;

	/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.LiteralControl" /> object.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the text content of the literal control. The default is <see cref="F:System.String.Empty" />.</returns>
	public virtual string Text
	{
		get
		{
			return _text;
		}
		set
		{
			_text = ((value == null) ? string.Empty : value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class that contains a literal string to be rendered on the requested ASP.NET page.</summary>
	public LiteralControl()
	{
		EnableViewState = false;
		base.AutoID = false;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.LiteralControl" /> class with the specified text.</summary>
	/// <param name="text">The text to be rendered on the requested Web page. </param>
	public LiteralControl(string text)
		: this()
	{
		Text = text;
	}

	/// <summary>Writes the content of the <see cref="T:System.Web.UI.LiteralControl" /> object to the ASP.NET page.</summary>
	/// <param name="output">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the content of the <see cref="T:System.Web.UI.LiteralControl" /> to the requesting client. </param>
	protected internal override void Render(HtmlTextWriter output)
	{
		output.Write(_text);
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.LiteralControl" /> class.</summary>
	/// <returns>The <see cref="T:System.Web.UI.EmptyControlCollection" /> for the current control.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}
}
