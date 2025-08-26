using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Creates a control that generates a list of items in a bulleted format.</summary>
[Designer("System.Web.UI.Design.WebControls.BulletedListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[DefaultEvent("Click")]
[DefaultProperty("BulletStyle")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class BulletedList : ListControl, IPostBackEventHandler
{
	private static readonly object ClickEvent;

	private PostBackOptions postBackOptions;

	/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.ListControl.AutoPostBack" /> property for the base class.</summary>
	/// <returns>
	///     <see langword="false" />.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.AutoPostBack" />. </exception>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override bool AutoPostBack
	{
		get
		{
			return base.AutoPostBack;
		}
		set
		{
			throw new NotSupportedException($"This property is not supported in {GetType()}");
		}
	}

	/// <summary>Gets or sets the zero-based index of the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>Always returns -1.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedIndex" />. </exception>
	[Bindable(false)]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override int SelectedIndex
	{
		get
		{
			return -1;
		}
		set
		{
			throw new NotSupportedException($"This property is not supported in {GetType()}");
		}
	}

	/// <summary>Gets the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>
	///     <see langword="null" />.</returns>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override ListItem SelectedItem => null;

	/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> object in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> in the <see cref="T:System.Web.UI.WebControls.BulletedList" />; otherwise, an empty string (""), if no item is selected.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedValue" />. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Bindable(false)]
	public override string SelectedValue
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Gets or sets the path to an image to display for each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>The path to an image to display as each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" />.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	public virtual string BulletImageUrl
	{
		get
		{
			return ViewState.GetString("BulletImageUrl", string.Empty);
		}
		set
		{
			ViewState["BulletImageUrl"] = value;
		}
	}

	/// <summary>Gets or sets the bullet style for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletStyle.NotSet" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. </exception>
	[DefaultValue(BulletStyle.NotSet)]
	public virtual BulletStyle BulletStyle
	{
		get
		{
			return (BulletStyle)ViewState.GetInt("BulletStyle", 0);
		}
		set
		{
			if (value < BulletStyle.NotSet || value > BulletStyle.CustomImage)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["BulletStyle"] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> collection for the control.</summary>
	/// <returns>A control collection for the control.</returns>
	public override ControlCollection Controls => new EmptyControlCollection(this);

	/// <summary>Gets or sets the display mode of the list content in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletedListDisplayMode.Text" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. </exception>
	[DefaultValue(BulletedListDisplayMode.Text)]
	public virtual BulletedListDisplayMode DisplayMode
	{
		get
		{
			return (BulletedListDisplayMode)ViewState.GetInt("DisplayMode", 0);
		}
		set
		{
			if (value < BulletedListDisplayMode.Text || value > BulletedListDisplayMode.LinkButton)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["DisplayMode"] = value;
		}
	}

	/// <summary>Gets or sets the value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>The value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. The default is 1.</returns>
	[DefaultValue(1)]
	public virtual int FirstBulletNumber
	{
		get
		{
			return ViewState.GetInt("FirstBulletNumber", 1);
		}
		set
		{
			ViewState["FirstBulletNumber"] = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>The HTML text writer tag value.</returns>
	protected override HtmlTextWriterTag TagKey
	{
		get
		{
			switch (BulletStyle)
			{
			case BulletStyle.Numbered:
			case BulletStyle.LowerAlpha:
			case BulletStyle.UpperAlpha:
			case BulletStyle.LowerRoman:
			case BulletStyle.UpperRoman:
				return HtmlTextWriterTag.Ol;
			default:
				return HtmlTextWriterTag.Ul;
			}
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page content that is linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
	/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> is clicked. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[TypeConverter(typeof(TargetConverter))]
	public virtual string Target
	{
		get
		{
			return ViewState.GetString("Target", string.Empty);
		}
		set
		{
			ViewState["Target"] = value;
		}
	}

	/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.BulletedList" />, if one of the items in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> is selected; otherwise, an empty string ("").</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.Text" />. </exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public override string Text
	{
		get
		{
			return string.Empty;
		}
		set
		{
			throw new NotSupportedException();
		}
	}

	/// <summary>Occurs when a link button in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
	public event BulletedListEventHandler Click
	{
		add
		{
			base.Events.AddHandler(ClickEvent, value);
		}
		remove
		{
			base.Events.RemoveHandler(ClickEvent, value);
		}
	}

	/// <summary>Adds the HTML attributes and styles for a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.  </param>
	[MonoTODO("we are missing a new style enum, we should be using it")]
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		bool flag = false;
		switch (BulletStyle)
		{
		case BulletStyle.Numbered:
			writer.AddStyleAttribute("list-style-type", "decimal");
			flag = true;
			break;
		case BulletStyle.LowerAlpha:
			writer.AddStyleAttribute("list-style-type", "lower-alpha");
			flag = true;
			break;
		case BulletStyle.UpperAlpha:
			writer.AddStyleAttribute("list-style-type", "upper-alpha");
			flag = true;
			break;
		case BulletStyle.LowerRoman:
			writer.AddStyleAttribute("list-style-type", "lower-roman");
			flag = true;
			break;
		case BulletStyle.UpperRoman:
			writer.AddStyleAttribute("list-style-type", "upper-roman");
			flag = true;
			break;
		case BulletStyle.Disc:
			writer.AddStyleAttribute("list-style-type", "disc");
			break;
		case BulletStyle.Circle:
			writer.AddStyleAttribute("list-style-type", "circle");
			break;
		case BulletStyle.Square:
			writer.AddStyleAttribute("list-style-type", "square");
			break;
		case BulletStyle.CustomImage:
			writer.AddStyleAttribute("list-style-image", "url(" + ResolveClientUrl(BulletImageUrl) + ")");
			break;
		}
		if (flag && FirstBulletNumber != 1)
		{
			writer.AddAttribute("start", FirstBulletNumber.ToString());
		}
		base.AddAttributesToRender(writer);
	}

	/// <summary>Renders the bulleted text for each list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <param name="item">A collection of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in a <see cref="T:System.Web.UI.WebControls.BulletedList" />. </param>
	/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to retrieve from the collection. </param>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	protected virtual void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
	{
		string value = HttpUtility.HtmlEncode(item.Text);
		switch (DisplayMode)
		{
		case BulletedListDisplayMode.Text:
			if (!item.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
			}
			writer.Write(value);
			if (!item.Enabled)
			{
				writer.RenderEndTag();
			}
			break;
		case BulletedListDisplayMode.HyperLink:
			if (base.IsEnabled && item.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, item.Value);
				if (Target.Length > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Target, Target);
				}
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write(value);
			writer.RenderEndTag();
			break;
		case BulletedListDisplayMode.LinkButton:
			if (base.IsEnabled && item.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Href, Page.ClientScript.GetPostBackEventReference(GetPostBackOptions(index.ToString(Helpers.InvariantCulture)), registerForEventValidation: true));
			}
			else
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", fEncode: false);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.A);
			writer.Write(value);
			writer.RenderEndTag();
			break;
		}
	}

	private PostBackOptions GetPostBackOptions(string argument)
	{
		if (postBackOptions == null)
		{
			postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.RequiresJavaScriptProtocol = true;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = CausesValidation && Page != null && Page.AreValidatorsUplevel(ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = ValidationGroup;
			}
		}
		postBackOptions.Argument = argument;
		return postBackOptions;
	}

	/// <summary>Renders the list items of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control as bullets into the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
	protected internal override void RenderContents(HtmlTextWriter writer)
	{
		int num = 0;
		Page page = Page;
		ClientScriptManager clientScriptManager = page?.ClientScript;
		foreach (ListItem item in Items)
		{
			if (page != null)
			{
				clientScriptManager.RegisterForEventValidation(UniqueID, item.Value);
			}
			if (item.HasAttributes)
			{
				item.Attributes.AddAttributes(writer);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			RenderBulletText(item, num++, writer);
			writer.RenderEndTag();
		}
	}

	/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object for display on the client.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
	protected internal override void Render(HtmlTextWriter writer)
	{
		base.Render(writer);
	}

	/// <summary>For a description of this method, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
	/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
	void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
	{
		RaisePostBackEvent(eventArgument);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control when a form is posted back to the server.</summary>
	/// <param name="eventArgument">The string representation for the index of the list item that raised the event.</param>
	protected virtual void RaisePostBackEvent(string eventArgument)
	{
		ValidateEvent(UniqueID, eventArgument);
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnClick(new BulletedListEventArgs(int.Parse(eventArgument, Helpers.InvariantCulture)));
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> that contains the event data. </param>
	protected virtual void OnClick(BulletedListEventArgs e)
	{
		if (base.Events != null)
		{
			((BulletedListEventHandler)base.Events[Click])?.Invoke(this, e);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.BulletedList" /> class.</summary>
	public BulletedList()
	{
	}

	static BulletedList()
	{
		Click = new object();
	}
}
