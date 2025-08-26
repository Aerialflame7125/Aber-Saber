using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a field that is displayed as a button in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class ButtonField : ButtonFieldBase
{
	private PropertyDescriptor boundProperty;

	/// <summary>Gets or sets a string that represents the action to perform when a button in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object is clicked.</summary>
	/// <returns>The name of the action to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> is clicked.</returns>
	[DefaultValue("")]
	[WebSysDescription("Raised when a Button Command is executed.")]
	[WebCategory("Behavior")]
	public virtual string CommandName
	{
		get
		{
			return base.ViewState.GetString("CommandName", string.Empty);
		}
		set
		{
			base.ViewState["CommandName"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the data field for which the value is bound to the <see cref="P:System.Web.UI.WebControls.Button.Text" /> property of the <see cref="T:System.Web.UI.WebControls.Button" /> control that is rendered by the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
	/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextField" /> property is not set.</returns>
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataTextField
	{
		get
		{
			return base.ViewState.GetString("DataTextField", string.Empty);
		}
		set
		{
			base.ViewState["DataTextField"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
	/// <returns>A format string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Data")]
	public virtual string DataTextFormatString
	{
		get
		{
			return base.ViewState.GetString("DataTextFormatString", string.Empty);
		}
		set
		{
			base.ViewState["DataTextFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
	/// <returns>The image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.ImageUrl" /> property is not set.</returns>
	[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	[DefaultValue("")]
	[UrlProperty]
	public virtual string ImageUrl
	{
		get
		{
			return base.ViewState.GetString("ImageUrl", string.Empty);
		}
		set
		{
			base.ViewState["ImageUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the static caption that is displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
	/// <returns>The caption displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string ("").</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebSysDescription("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			return base.ViewState.GetString("Text", string.Empty);
		}
		set
		{
			base.ViewState["Text"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Initializes the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
	/// <param name="sortingEnabled">
	///       <see langword="true" /> to enable sorting; otherwise, <see langword="false" />. </param>
	/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.ButtonField" />. </param>
	/// <returns>
	///     <see langword="false" />, which indicates the control does not need to rebind to the data.</returns>
	public override bool Initialize(bool sortingEnabled, Control control)
	{
		return base.Initialize(sortingEnabled, control);
	}

	/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
	/// <param name="dataTextValue">The field value to format. </param>
	/// <returns>The field value converted to the format specified by the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextFormatString" /> property.</returns>
	protected virtual string FormatDataTextValue(object dataTextValue)
	{
		if (DataTextFormatString.Length > 0)
		{
			return string.Format(DataTextFormatString, dataTextValue);
		}
		if (dataTextValue == null)
		{
			return string.Empty;
		}
		return dataTextValue.ToString();
	}

	/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
	/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
	/// <param name="cellType">A <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> value that indicates the type of row (header, footer, or data).</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
	/// <param name="rowIndex">The zero-based index of the row.</param>
	public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		string commandArg = rowIndex.ToString();
		if (cellType == DataControlCellType.DataCell)
		{
			IDataControlButton dataControlButton = DataControlButton.CreateButton(ButtonType, base.Control, Text, ImageUrl, CommandName, commandArg, allowCallback: false);
			if (CausesValidation)
			{
				dataControlButton.Container = null;
				dataControlButton.CausesValidation = true;
				dataControlButton.ValidationGroup = ValidationGroup;
			}
			if (!string.IsNullOrEmpty(DataTextField) && (rowState & DataControlRowState.Insert) == 0)
			{
				cell.DataBinding += OnDataBindField;
			}
			cell.Controls.Add((Control)dataControlButton);
		}
		else
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
		}
	}

	private void OnDataBindField(object sender, EventArgs e)
	{
		DataControlFieldCell dataControlFieldCell = (DataControlFieldCell)sender;
		((IDataControlButton)dataControlFieldCell.Controls[0]).Text = FormatDataTextValue(GetBoundValue(dataControlFieldCell.BindingContainer));
	}

	private object GetBoundValue(Control controlContainer)
	{
		IDataItemContainer dataItemContainer = controlContainer as IDataItemContainer;
		if (boundProperty == null)
		{
			boundProperty = TypeDescriptor.GetProperties(dataItemContainer.DataItem)[DataTextField];
			if (boundProperty == null)
			{
				throw new InvalidOperationException("Property '" + DataTextField + "' not found in object of type " + dataItemContainer.DataItem.GetType());
			}
		}
		return boundProperty.GetValue(dataItemContainer.DataItem);
	}

	/// <summary>Creates and returns a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</summary>
	/// <returns>A new instance of the  <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</returns>
	protected override DataControlField CreateField()
	{
		return new ButtonField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" />.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		ButtonField obj = (ButtonField)newField;
		obj.CommandName = CommandName;
		obj.DataTextField = DataTextField;
		obj.DataTextFormatString = DataTextFormatString;
		obj.ImageUrl = ImageUrl;
		obj.Text = Text;
	}

	/// <summary>Determines whether the controls that are contained in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object support callbacks.</summary>
	public override void ValidateSupportsCallback()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</summary>
	public ButtonField()
	{
	}
}
