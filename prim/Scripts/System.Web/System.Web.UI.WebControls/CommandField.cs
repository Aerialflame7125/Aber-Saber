using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a special field that displays command buttons to perform selecting, editing, inserting, or deleting operations in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class CommandField : ButtonFieldBase
{
	/// <summary>Gets or sets the URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string CancelImageUrl
	{
		get
		{
			return base.ViewState.GetString("CancelImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["CancelImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for the Cancel button displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for the Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Cancel".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string CancelText
	{
		get
		{
			return base.ViewState.GetString("CancelText", "Cancel");
		}
		set
		{
			base.ViewState["CancelText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether validation is performed when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to perform validation when the user clicks a button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	public override bool CausesValidation
	{
		get
		{
			return base.ViewState.GetBool("CausesValidation", def: true);
		}
		set
		{
			base.ViewState["CausesValidation"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string DeleteImageUrl
	{
		get
		{
			return base.ViewState.GetString("DeleteImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["DeleteImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Delete".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string DeleteText
	{
		get
		{
			return base.ViewState.GetString("DeleteText", "Delete");
		}
		set
		{
			base.ViewState["DeleteText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string EditImageUrl
	{
		get
		{
			return base.ViewState.GetString("EditImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["EditImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Edit".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string EditText
	{
		get
		{
			return base.ViewState.GetString("EditText", "Edit");
		}
		set
		{
			base.ViewState["EditText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string InsertImageUrl
	{
		get
		{
			return base.ViewState.GetString("InsertImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["InsertImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for the Insert button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Insert".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string InsertText
	{
		get
		{
			return base.ViewState.GetString("InsertText", "Insert");
		}
		set
		{
			base.ViewState["InsertText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string NewImageUrl
	{
		get
		{
			return base.ViewState.GetString("NewImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["NewImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for the New button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field. The default is "New".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string NewText
	{
		get
		{
			return base.ViewState.GetString("NewText", "New");
		}
		set
		{
			base.ViewState["NewText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL to an image to display for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string SelectImageUrl
	{
		get
		{
			return base.ViewState.GetString("SelectImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["SelectImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Select".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string SelectText
	{
		get
		{
			return base.ViewState.GetString("SelectText", "Select");
		}
		set
		{
			base.ViewState["SelectText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether a Cancel button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to display a Cancel button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DefaultValue(true)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ShowCancelButton
	{
		get
		{
			return base.ViewState.GetBool("ShowCancelButton", def: true);
		}
		set
		{
			base.ViewState["ShowCancelButton"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether a Delete button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to display a Delete button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ShowDeleteButton
	{
		get
		{
			return base.ViewState.GetBool("ShowDeleteButton", def: false);
		}
		set
		{
			base.ViewState["ShowDeleteButton"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether an Edit button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to display an Edit button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ShowEditButton
	{
		get
		{
			return base.ViewState.GetBool("ShowEditButton", def: false);
		}
		set
		{
			base.ViewState["ShowEditButton"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether a Select button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to display a Select button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ShowSelectButton
	{
		get
		{
			return base.ViewState.GetBool("ShowSelectButton", def: false);
		}
		set
		{
			base.ViewState["ShowSelectButton"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets a value indicating whether a New button is displayed in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>
	///     <see langword="true" /> to display a New button in a <see cref="T:System.Web.UI.WebControls.CommandField" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[DefaultValue(false)]
	[WebSysDescription("")]
	[WebCategory("Behavior")]
	public virtual bool ShowInsertButton
	{
		get
		{
			return base.ViewState.GetBool("ShowInsertButton", def: false);
		}
		set
		{
			base.ViewState["ShowInsertButton"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The URL for an image to display for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string UpdateImageUrl
	{
		get
		{
			return base.ViewState.GetString("UpdateImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["UpdateImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" /> field.</summary>
	/// <returns>The caption for an Update button in a <see cref="T:System.Web.UI.WebControls.CommandField" />. The default is "Update".</returns>
	[Localizable(true)]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string UpdateText
	{
		get
		{
			return base.ViewState.GetString("UpdateText", "Update");
		}
		set
		{
			base.ViewState["UpdateText"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="rowIndex">The zero-based index of the row that contains the cell.</param>
	public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		string arg = rowIndex.ToString();
		if (cellType == DataControlCellType.DataCell)
		{
			if ((rowState & DataControlRowState.Edit) != 0)
			{
				if (ShowEditButton)
				{
					cell.Controls.Add(CreateButton(UpdateText, UpdateImageUrl, "Update", arg));
					if (ShowCancelButton)
					{
						AddSeparator(cell);
						cell.Controls.Add(CreateButton(CancelText, CancelImageUrl, "Cancel", arg));
					}
				}
				return;
			}
			if ((rowState & DataControlRowState.Insert) != 0)
			{
				if (ShowInsertButton)
				{
					cell.Controls.Add(CreateButton(InsertText, InsertImageUrl, "Insert", arg));
					if (ShowCancelButton)
					{
						AddSeparator(cell);
						cell.Controls.Add(CreateButton(CancelText, CancelImageUrl, "Cancel", arg));
					}
				}
				return;
			}
			if (ShowEditButton)
			{
				AddSeparator(cell);
				cell.Controls.Add(CreateButton(EditText, EditImageUrl, "Edit", arg));
			}
			if (ShowDeleteButton)
			{
				AddSeparator(cell);
				cell.Controls.Add(CreateButton(DeleteText, DeleteImageUrl, "Delete", arg));
			}
			if (ShowInsertButton)
			{
				AddSeparator(cell);
				cell.Controls.Add(CreateButton(NewText, NewImageUrl, "New", arg));
			}
			if (ShowSelectButton)
			{
				AddSeparator(cell);
				cell.Controls.Add(CreateButton(SelectText, SelectImageUrl, "Select", arg));
			}
		}
		else
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
		}
	}

	private Control CreateButton(string text, string image, string command, string arg)
	{
		IDataControlButton dataControlButton = DataControlButton.CreateButton(ButtonType, base.Control, text, image, command, arg, allowCallback: false);
		if (CausesValidation && (command == "Update" || command == "Insert"))
		{
			dataControlButton.Container = null;
			dataControlButton.CausesValidation = true;
			dataControlButton.ValidationGroup = ValidationGroup;
		}
		return (Control)dataControlButton;
	}

	private void AddSeparator(DataControlFieldCell cell)
	{
		if (cell.Controls.Count > 0)
		{
			Literal literal = new Literal();
			literal.Text = "&nbsp;";
			cell.Controls.Add(literal);
		}
	}

	/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CommandField" /> object.</summary>
	/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CommandField" />.</returns>
	protected override DataControlField CreateField()
	{
		return new CommandField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CommandField" /> to.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		CommandField obj = (CommandField)newField;
		obj.CancelImageUrl = CancelImageUrl;
		obj.CancelText = CancelText;
		obj.DeleteImageUrl = DeleteImageUrl;
		obj.DeleteText = DeleteText;
		obj.EditImageUrl = EditImageUrl;
		obj.EditText = EditText;
		obj.InsertImageUrl = InsertImageUrl;
		obj.InsertText = InsertText;
		obj.NewImageUrl = NewImageUrl;
		obj.NewText = NewText;
		obj.SelectImageUrl = SelectImageUrl;
		obj.SelectText = SelectText;
		obj.ShowCancelButton = ShowCancelButton;
		obj.ShowDeleteButton = ShowDeleteButton;
		obj.ShowEditButton = ShowEditButton;
		obj.ShowSelectButton = ShowSelectButton;
		obj.ShowInsertButton = ShowInsertButton;
		obj.UpdateImageUrl = UpdateImageUrl;
		obj.UpdateText = UpdateText;
	}

	/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CommandField" /> object support callbacks.</summary>
	/// <exception cref="T:System.NotSupportedException">The Select button is displayed in the <see cref="T:System.Web.UI.WebControls.CommandField" /> object. The <see cref="T:System.Web.UI.WebControls.CommandField" />  class does support callbacks when the Select button is displayed.</exception>
	public override void ValidateSupportsCallback()
	{
		if (ShowSelectButton)
		{
			throw new NotSupportedException("Callbacks are not supported on CommandField when the select button is enabled because other controls on your page that are dependent on the selected value of '" + base.Control.ID + "' for their rendering will not update in a callback.  Turn callbacks off on '" + base.Control.ID + "'.");
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandField" /> class.</summary>
	public CommandField()
	{
	}
}
