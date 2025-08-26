using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a radio button control.</summary>
[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[SupportsEventValidation]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RadioButton : CheckBox, IPostBackDataHandler
{
	/// <summary>Gets or sets the name of the group that the radio button belongs to.</summary>
	/// <returns>The name of the group that the radio button belongs to. The default is an empty string ("").</returns>
	[DefaultValue("")]
	[Themeable(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string GroupName
	{
		get
		{
			return ViewState.GetString("GroupName", string.Empty);
		}
		set
		{
			ViewState["GroupName"] = value;
		}
	}

	internal override string NameAttribute
	{
		get
		{
			string text = UniqueID;
			string groupName = GroupName;
			if (groupName.Length == 0)
			{
				return text;
			}
			int num = -1;
			if (text != null)
			{
				num = text.LastIndexOf(base.IdSeparator);
			}
			if (num == -1)
			{
				return groupName;
			}
			return text.Substring(0, num + 1) + groupName;
		}
	}

	internal string ValueAttribute
	{
		get
		{
			string text = (string)ViewState["Value"];
			if (text != null)
			{
				return text;
			}
			string iD = ID;
			if (!string.IsNullOrEmpty(iD))
			{
				return iD;
			}
			return UniqueID;
		}
		set
		{
			ViewState["Value"] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RadioButton" /> class.</summary>
	public RadioButton()
		: base("radio")
	{
	}

	internal override void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
	{
		Page?.ClientScript.RegisterForEventValidation(NameAttribute, ValueAttribute);
		base.InternalAddAttributesToRender(w, enabled);
		w.AddAttribute(HtmlTextWriterAttribute.Value, ValueAttribute);
	}

	/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected internal override void OnPreRender(EventArgs e)
	{
		base.OnPreRender(e);
	}

	/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> control.</summary>
	/// <param name="postDataKey">The key identifier for the control.</param>
	/// <param name="postCollection">The collection of all incoming name values.</param>
	/// <returns>
	///     <see langword="true" /> if the data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> has changed; otherwise, <see langword="false" />.</returns>
	protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		string text = postCollection[NameAttribute];
		bool flag = text == ValueAttribute;
		ValidateEvent(NameAttribute, text);
		if (Checked == flag)
		{
			return false;
		}
		Checked = flag;
		return flag;
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.RadioButton.CheckedChanged" /> event, if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property has changed on postback.</summary>
	protected override void RaisePostDataChangedEvent()
	{
		if (CausesValidation)
		{
			Page.Validate(ValidationGroup);
		}
		OnCheckedChanged(EventArgs.Empty);
	}

	/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
	/// <param name="postDataKey">A string.</param>
	/// <param name="postCollection">A name value collection that represents the posted collection of data. </param>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Web.UI.WebControls.RadioButton" /> is checked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
	{
		return LoadPostData(postDataKey, postCollection);
	}
}
