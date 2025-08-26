using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a control that allows the user to select a single item from a drop-down list. </summary>
[ValidationProperty("SelectedItem")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class DropDownList : ListControl, IPostBackDataHandler
{
	/// <summary>Gets or sets the border color of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control.</returns>
	[Browsable(false)]
	public override Color BorderColor
	{
		get
		{
			return base.BorderColor;
		}
		set
		{
			base.BorderColor = value;
		}
	}

	/// <summary>Gets or sets the border style of the control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> values.</returns>
	[Browsable(false)]
	public override BorderStyle BorderStyle
	{
		get
		{
			return base.BorderStyle;
		}
		set
		{
			base.BorderStyle = value;
		}
	}

	/// <summary>Gets or sets the border width for the control.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width for the control.</returns>
	[Browsable(false)]
	public override Unit BorderWidth
	{
		get
		{
			return base.BorderWidth;
		}
		set
		{
			base.BorderWidth = value;
		}
	}

	/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
	/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control. The default value is 0, which selects the first item in the list.</returns>
	[DefaultValue(0)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[WebSysDescription("")]
	[WebCategory("Misc")]
	public override int SelectedIndex
	{
		get
		{
			int selectedIndex = base.SelectedIndex;
			if (selectedIndex != -1 || Items.Count == 0)
			{
				return selectedIndex;
			}
			Items[0].Selected = true;
			return 0;
		}
		set
		{
			base.SelectedIndex = value;
		}
	}

	/// <summary>Gets a value that indicates whether the control should set the <see langword="disabled" /> attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, <see langword="false" />.</returns>
	public override bool SupportsDisabledAttribute => base.RenderingCompatibilityLessThan40;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DropDownList" /> class.</summary>
	public DropDownList()
	{
	}

	/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
	/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML contents to the client.</param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		Page page = Page;
		page?.VerifyRenderingInServerForm(this);
		if (writer != null)
		{
			if (!string.IsNullOrEmpty(UniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID, fEncode: true);
			}
			if (!base.IsEnabled && SelectedIndex == -1)
			{
				SelectedIndex = 1;
			}
			if (AutoPostBack)
			{
				string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(GetPostBackOptions(), registerForEventValidation: true) : string.Empty);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, BuildScriptAttribute("onchange", text));
			}
			base.AddAttributesToRender(writer);
		}
	}

	private PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ActionUrl = null;
		postBackOptions.ValidationGroup = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = true;
		Page page = Page;
		postBackOptions.PerformValidation = CausesValidation && page != null && page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	/// <summary>Creates a collection to store child controls.</summary>
	/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
	protected override ControlCollection CreateControlCollection()
	{
		return base.CreateControlCollection();
	}

	/// <summary>Always throws an <see cref="T:System.Web.HttpException" /> exception because multiple selection is not supported for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
	/// <exception cref="T:System.Web.HttpException">In all cases.</exception>
	protected internal override void VerifyMultiSelect()
	{
		throw new HttpException("DropDownList only may have a single selected item");
	}

	/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
	/// <param name="postDataKey">The index within the posted collection that references the content to load.</param>
	/// <param name="postCollection">The collection of all incoming name values posted to the server.</param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control's state changes as a result of the postback event; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		EnsureDataBound();
		int num = Items.IndexOf(postCollection[postDataKey]);
		ValidateEvent(postDataKey, postCollection[postDataKey]);
		if (num != SelectedIndex)
		{
			SelectedIndex = num;
			return true;
		}
		return false;
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control when postback occurs.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		if (CausesValidation)
		{
			Page?.Validate(ValidationGroup);
		}
		OnSelectedIndexChanged(EventArgs.Empty);
	}

	/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
	/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.  </param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control on postback.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}
}
