using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls;

/// <summary>Serves as the abstract base class for HTML server controls that map to HTML elements that are required to have an opening and a closing tag.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class HtmlContainerControl : HtmlControl
{
	/// <summary>Gets or sets the content found between the opening and closing tags of the specified HTML server control.</summary>
	/// <returns>The HTML content between opening and closing tags of an HTML server control.</returns>
	/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or -The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
	[HtmlControlPersistable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string InnerHtml
	{
		get
		{
			if (Controls.Count == 0)
			{
				return string.Empty;
			}
			if (Controls.Count == 1)
			{
				Control control = Controls[0];
				if (control is LiteralControl literalControl)
				{
					return literalControl.Text;
				}
				if (control is DataBoundLiteralControl dataBoundLiteralControl)
				{
					return dataBoundLiteralControl.Text;
				}
			}
			throw new HttpException("There is no literal content!");
		}
		set
		{
			Controls.Clear();
			Controls.Add(new LiteralControl(value));
			if (value == null)
			{
				ViewState.Remove("innerhtml");
			}
			else
			{
				ViewState["innerhtml"] = value;
			}
		}
	}

	/// <summary>Gets or sets the text between the opening and closing tags of the specified HTML server control.</summary>
	/// <returns>The text between the opening and closing tags of an HTML server control.</returns>
	/// <exception cref="T:System.Web.HttpException">There is more than one HTML server control.- or - The HTML server control is not a <see cref="T:System.Web.UI.LiteralControl" /> or a <see cref="T:System.Web.UI.DataBoundLiteralControl" />. </exception>
	[HtmlControlPersistable(false)]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public virtual string InnerText
	{
		get
		{
			return HttpUtility.HtmlDecode(InnerHtml);
		}
		set
		{
			InnerHtml = HttpUtility.HtmlEncode(value);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using default values.</summary>
	protected HtmlContainerControl()
		: this("span")
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> class using the specified tag name.</summary>
	/// <param name="tag">A string that specifies the tag name of the control. </param>
	public HtmlContainerControl(string tag)
		: base(tag)
	{
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> content.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		RenderBeginTag(writer);
		RenderChildren(writer);
		RenderEndTag(writer);
	}

	/// <summary>Renders the closing tag for the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
	protected virtual void RenderEndTag(HtmlTextWriter writer)
	{
		writer.WriteEndTag(TagName);
	}

	/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that receives the rendered content.</param>
	protected override void RenderAttributes(HtmlTextWriter writer)
	{
		ViewState.Remove("innerhtml");
		base.RenderAttributes(writer);
	}

	/// <summary>Creates a new <see cref="T:System.Web.UI.ControlCollection" /> object to hold the child controls (both literal and server) of the server control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlCollection" /> that contains the <see cref="T:System.Web.UI.HtmlControls.HtmlControl" /> child server controls.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return new ControlCollection(this);
	}

	/// <summary>Restores the <see cref="T:System.Web.UI.HtmlControls.HtmlContainerControl" /> control's view state from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.</summary>
	/// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
	protected override void LoadViewState(object savedState)
	{
		if (savedState != null)
		{
			base.LoadViewState(savedState);
			if (ViewState["innerhtml"] is string innerHtml)
			{
				InnerHtml = innerHtml;
			}
		}
	}
}
