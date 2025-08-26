using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls;

/// <summary>Represents a list box control that allows single or multiple item selection.</summary>
[ValidationProperty("SelectedItem")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ListBox : ListControl, IPostBackDataHandler
{
	/// <summary>Gets or sets the border color of the control.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> object that represents the border color of the control.</returns>
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
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that represents the border width of the control.</returns>
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

	/// <summary>Gets or sets the number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
	/// <returns>The number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control. The default value is <see langword="4" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of rows is less than one or greater than 2000. </exception>
	[DefaultValue(4)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual int Rows
	{
		get
		{
			return ViewState.GetInt("Rows", 4);
		}
		set
		{
			if (value < 1 || value > 2000)
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["Rows"] = value;
		}
	}

	/// <summary>Gets or sets the selection mode of the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. The default value is <see langword="Single" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. </exception>
	[DefaultValue(ListSelectionMode.Single)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual ListSelectionMode SelectionMode
	{
		get
		{
			return (ListSelectionMode)ViewState.GetInt("SelectionMode", 0);
		}
		set
		{
			if (!Enum.IsDefined(typeof(ListSelectionMode), value))
			{
				throw new ArgumentOutOfRangeException("value");
			}
			ViewState["SelectionMode"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ListBox" /> class.</summary>
	public ListBox()
	{
	}

	/// <summary>Gets the array of index values for currently selected items in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
	/// <returns>An array of integers, each representing the index of a selected item in the list box.</returns>
	public virtual int[] GetSelectedIndices()
	{
		return (int[])GetSelectedIndicesInternal().ToArray(typeof(int));
	}

	/// <summary>Adds <see langword="name" />, <see langword="size" />, <see langword="multiple" />, and <see langword="onchange" /> to the list of attributes to render.</summary>
	/// <param name="writer">The output stream that renders HTML content to the client. </param>
	protected override void AddAttributesToRender(HtmlTextWriter writer)
	{
		if (Page != null)
		{
			Page.VerifyRenderingInServerForm(this);
		}
		if (ID != null)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Name, UniqueID);
		}
		if (AutoPostBack)
		{
			string postBackEventReference = Page.ClientScript.GetPostBackEventReference(GetPostBackOptions(), registerForEventValidation: true);
			postBackEventReference = "setTimeout('" + postBackEventReference.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
			writer.AddAttribute(HtmlTextWriterAttribute.Onchange, BuildScriptAttribute("onchange", postBackEventReference));
		}
		if (SelectionMode == ListSelectionMode.Multiple)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple", fEncode: false);
		}
		writer.AddAttribute(HtmlTextWriterAttribute.Size, Rows.ToString(Helpers.InvariantCulture));
		base.AddAttributesToRender(writer);
	}

	private PostBackOptions GetPostBackOptions()
	{
		PostBackOptions postBackOptions = new PostBackOptions(this);
		postBackOptions.ActionUrl = null;
		postBackOptions.ValidationGroup = null;
		postBackOptions.Argument = string.Empty;
		postBackOptions.RequiresJavaScriptProtocol = false;
		postBackOptions.ClientSubmit = true;
		postBackOptions.PerformValidation = CausesValidation && Page != null && Page.AreValidatorsUplevel(ValidationGroup);
		if (postBackOptions.PerformValidation)
		{
			postBackOptions.ValidationGroup = ValidationGroup;
		}
		return postBackOptions;
	}

	/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.ListBox" /> control prior to rendering on the client.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
		Page page = Page;
		if (page != null && base.IsEnabled)
		{
			page.RegisterRequiresPostBack(this);
		}
	}

	/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
	/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
	/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />.</returns>
	protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		EnsureDataBound();
		string[] values = postCollection.GetValues(postDataKey);
		if (values == null || values.Length == 0)
		{
			int selectedIndex = SelectedIndex;
			SelectedIndex = -1;
			return selectedIndex != -1;
		}
		ValidateEvent(UniqueID, values[0]);
		if (SelectionMode == ListSelectionMode.Single)
		{
			return SelectSingle(values);
		}
		return SelectMultiple(values);
	}

	private bool SelectSingle(string[] values)
	{
		string value = values[0];
		int num = Items.IndexOf(value);
		int selectedIndex = SelectedIndex;
		if (num != selectedIndex)
		{
			SelectedIndex = num;
			return true;
		}
		return false;
	}

	private bool SelectMultiple(string[] values)
	{
		ArrayList selectedIndicesInternal = GetSelectedIndicesInternal();
		ClearSelection();
		foreach (string value in values)
		{
			ListItem listItem = Items.FindByValue(value);
			if (listItem != null)
			{
				listItem.Selected = true;
			}
		}
		ArrayList selectedIndicesInternal2 = GetSelectedIndicesInternal();
		int num = selectedIndicesInternal.Count;
		if (selectedIndicesInternal2.Count != num)
		{
			return true;
		}
		while (--num >= 0)
		{
			if ((int)selectedIndicesInternal[num] != (int)selectedIndicesInternal2[num])
			{
				return true;
			}
		}
		return false;
	}

	/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
	protected virtual void RaisePostDataChangedEvent()
	{
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnSelectedIndexChanged(EventArgs.Empty);
	}

	/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
	/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
	/// <param name="postCollection">The collection posted to the server. </param>
	/// <returns>
	///     <see langword="true" /> if the posted content is different from the last posting; otherwise, <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}

	/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
	void IPostBackDataHandler.RaisePostDataChangedEvent()
	{
		RaisePostDataChangedEvent();
	}

	internal override bool MultiSelectOk()
	{
		return SelectionMode == ListSelectionMode.Multiple;
	}
}
