using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Reserves a location on the Web page to display static text.</summary>
[ControlBuilder(typeof(LiteralControlBuilder))]
[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[DefaultProperty("Text")]
[Designer("System.Web.UI.Design.WebControls.LiteralDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class Literal : Control, ITextControl
{
	/// <summary>Gets or sets an enumeration value that specifies how the content in the <see cref="T:System.Web.UI.WebControls.Literal" /> control is rendered.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. The default is <see langword="Transform" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.LiteralMode" /> enumeration values. </exception>
	[DefaultValue(LiteralMode.Transform)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public LiteralMode Mode
	{
		get
		{
			if (ViewState["Mode"] != null)
			{
				return (LiteralMode)ViewState["Mode"];
			}
			return LiteralMode.Transform;
		}
		set
		{
			if (value < LiteralMode.Transform || value > LiteralMode.Encode)
			{
				throw new ArgumentOutOfRangeException();
			}
			ViewState["Mode"] = value;
		}
	}

	/// <summary>Gets or sets the caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	/// <returns>The caption displayed in the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</returns>
	[Bindable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[Localizable(true)]
	public string Text
	{
		get
		{
			return ViewState.GetString("Text", string.Empty);
		}
		set
		{
			ViewState["Text"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Literal" /> class. </summary>
	public Literal()
	{
	}

	/// <summary>Set input focus to a control; the <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> base control method is not supported on a <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.WebControls.Literal.Focus" /> was called on a <see cref="T:System.Web.UI.WebControls.Literal" />.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override void Focus()
	{
		throw new NotSupportedException();
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object for the current instance of the <see cref="T:System.Web.UI.WebControls.Literal" /> control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> object to contain the current server control's child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new EmptyControlCollection(this);
	}

	/// <summary>Notifies the <see cref="T:System.Web.UI.WebControls.Literal" /> control that an XML or HTML element was parsed and adds that element to the <see cref="T:System.Web.UI.ControlCollection" /> of the control.</summary>
	/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <paramref name="obj" /> is not of type <see cref="T:System.Web.UI.LiteralControl" />.</exception>
	protected override void AddParsedSubObject(object obj)
	{
		if (obj is LiteralControl literalControl)
		{
			Text = literalControl.Text;
			return;
		}
		throw new HttpException(Locale.GetText("'Literal' cannot have children of type '{0}'", obj.GetType()));
	}

	/// <summary>Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to be rendered on the client.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the server control content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (Mode == LiteralMode.Encode)
		{
			writer.Write(HttpUtility.HtmlEncode(Text));
		}
		else
		{
			writer.Write(Text);
		}
	}
}
