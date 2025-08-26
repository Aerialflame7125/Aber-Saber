using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Serves as the abstract base class for button fields, such as the <see cref="T:System.Web.UI.WebControls.ButtonField" /> or <see cref="T:System.Web.UI.WebControls.CommandField" /> class. The <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class provides the methods and properties that are common to all button fields.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class ButtonFieldBase : DataControlField
{
	/// <summary>Gets or sets the button type to display in the button field.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. The default is <see langword="ButtonType.Link" />.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value for the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ButtonType" /> property is not one of the <see cref="T:System.Web.UI.WebControls.ButtonType" /> values. </exception>
	[DefaultValue(ButtonType.Link)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual ButtonType ButtonType
	{
		get
		{
			return (ButtonType)base.ViewState.GetInt("ButtonType", 2);
		}
		set
		{
			base.ViewState["ButtonType"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
	/// <returns>
	///     <see langword="true" /> to perform validation when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool CausesValidation
	{
		get
		{
			return base.ViewState.GetBool("CausesValidation", def: false);
		}
		set
		{
			base.ViewState["CausesValidation"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether the header section is displayed in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object.</summary>
	/// <returns>
	///     <see langword="true" /> to show the header section; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public override bool ShowHeader
	{
		get
		{
			return base.ViewState.GetBool("showHeader", def: false);
		}
		set
		{
			base.ViewState["showHeader"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the group of validation controls to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> object is clicked.</summary>
	/// <returns>The name of the validation group to validate when a button in a <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> is clicked. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonFieldBase.ValidationGroup" /> property is not set.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual string ValidationGroup
	{
		get
		{
			return base.ViewState.GetString("ValidationGroup", string.Empty);
		}
		set
		{
			base.ViewState["ValidationGroup"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Copies the properties of the current object that is derived from the <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" />.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		ButtonFieldBase obj = (ButtonFieldBase)newField;
		obj.ButtonType = ButtonType;
		obj.CausesValidation = CausesValidation;
		obj.ShowHeader = ShowHeader;
		obj.ValidationGroup = ValidationGroup;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonFieldBase" /> class.</summary>
	protected ButtonFieldBase()
	{
	}
}
