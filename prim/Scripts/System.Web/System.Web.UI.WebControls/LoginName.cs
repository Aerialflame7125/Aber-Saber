using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Displays the value of the <see langword="System.Web.UI.Page.User.Identity.Name" /> property. </summary>
[Bindable(false)]
[DefaultProperty("FormatString")]
[Designer("System.Web.UI.Design.WebControls.LoginNameDesigner,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class LoginName : WebControl
{
	/// <summary>Provides a format item string to display.</summary>
	/// <returns>A string containing format items for displaying the user's name. The default value is "{0}".</returns>
	/// <exception cref="T:System.FormatException">The format string is not valid. </exception>
	[DefaultValue("{0}")]
	[Localizable(true)]
	public virtual string FormatString
	{
		get
		{
			object obj = ViewState["FormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "{0}";
		}
		set
		{
			if (value == null)
			{
				ViewState.Remove("FormatString");
			}
			else
			{
				ViewState["FormatString"] = value;
			}
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	private bool Anonymous => User.Length == 0;

	private string User
	{
		get
		{
			if (Page == null || Page.User == null)
			{
				return string.Empty;
			}
			return Page.User.Identity.Name;
		}
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to its default values.</summary>
	public LoginName()
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.WebControls.LoginName" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> control. </summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		if (!Anonymous)
		{
			RenderBeginTag(writer);
			RenderContents(writer);
			RenderEndTag(writer);
		}
	}

	/// <summary>Renders the HTML opening tag of the control to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
	public override void RenderBeginTag(HtmlTextWriter writer)
	{
		if (!Anonymous)
		{
			base.RenderBeginTag(writer);
		}
	}

	/// <summary>Renders the contents of the control to the specified writer. This method is used primarily by control developers.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
	/// <exception cref="T:System.FormatException">The <see cref="P:System.Web.UI.WebControls.LoginName.FormatString" /> property is not set to a valid format string.</exception>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		if (!Anonymous)
		{
			string text = (string)ViewState["FormatString"];
			if (text == null || text.Length == 0)
			{
				writer.Write(User);
			}
			else
			{
				writer.Write(text, User);
			}
		}
	}

	/// <summary>Renders the HTML closing tag of the control to the specified writer.</summary>
	/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream that renders HTML content to the client.</param>
	public override void RenderEndTag(HtmlTextWriter writer)
	{
		if (!Anonymous)
		{
			base.RenderEndTag(writer);
		}
	}
}
