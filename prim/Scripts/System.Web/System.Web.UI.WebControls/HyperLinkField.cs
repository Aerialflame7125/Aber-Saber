using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls;

/// <summary>Represents a field that is displayed as a hyperlink in a data-bound control.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class HyperLinkField : DataControlField
{
	private PropertyDescriptor textProperty;

	private PropertyDescriptor[] urlProperties;

	private static string[] emptyFields;

	/// <summary>Gets or sets the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
	/// <returns>An array containing the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty array, indicating that <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFields" /> is not set.</returns>
	[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[TypeConverter(typeof(StringArrayConverter))]
	[WebCategory("Data")]
	[DefaultValue(null)]
	public virtual string[] DataNavigateUrlFields
	{
		get
		{
			object obj = base.ViewState["DataNavigateUrlFields"];
			if (obj != null)
			{
				return (string[])obj;
			}
			if (emptyFields == null)
			{
				emptyFields = new string[0];
			}
			return emptyFields;
		}
		set
		{
			base.ViewState["DataNavigateUrlFields"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are rendered.</summary>
	/// <returns>A string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are rendered. The default is an empty string (""), which indicates that no special formatting is applied to the URL values.</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public virtual string DataNavigateUrlFormatString
	{
		get
		{
			object obj = base.ViewState["DataNavigateUrlFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataNavigateUrlFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the name of the field from the data source containing the text to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
	/// <returns>The name of the field from the data source containing the values to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[WebCategory("Data")]
	[DefaultValue("")]
	[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public virtual string DataTextField
	{
		get
		{
			object obj = base.ViewState["DataTextField"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataTextField"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Get or sets the string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are displayed.</summary>
	/// <returns>A string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are displayed. The default is an empty string (""), which indicates that no special formatting is applied to the hyperlink captions.</returns>
	[DefaultValue("")]
	[WebCategory("Data")]
	public virtual string DataTextFormatString
	{
		get
		{
			object obj = base.ViewState["DataTextFormatString"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["DataTextFormatString"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
	/// <returns>The URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[UrlProperty]
	[WebCategory("Behavior")]
	public virtual string NavigateUrl
	{
		get
		{
			object obj = base.ViewState["NavigateUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["NavigateUrl"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the target window or frame in which to display the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
	/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
	[DefaultValue("")]
	[WebCategory("Behavior")]
	[TypeConverter(typeof(TargetConverter))]
	public virtual string Target
	{
		get
		{
			object obj = base.ViewState["Target"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["Target"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Gets or sets the text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
	/// <returns>The text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	[Localizable(true)]
	[DefaultValue("")]
	[WebCategory("Appearance")]
	public virtual string Text
	{
		get
		{
			object obj = base.ViewState["Text"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			base.ViewState["Text"] = value;
			OnFieldChanged();
		}
	}

	/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
	/// <param name="enableSorting">
	///       <see langword="true" /> if sorting is supported; otherwise, <see langword="false" />.</param>
	/// <param name="control">The data control that acts as the parent for the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
	/// <returns>Always returns <see langword="false" />.</returns>
	public override bool Initialize(bool enableSorting, Control control)
	{
		return base.Initialize(enableSorting, control);
	}

	/// <summary>Initializes a cell in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
	/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
	/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
	/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values that specifies the state of the row containing the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
	/// <param name="rowIndex">The index of the row in the table.</param>
	public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
	{
		base.InitializeCell(cell, cellType, rowState, rowIndex);
		if (cellType == DataControlCellType.DataCell)
		{
			HyperLink hyperLink = new HyperLink();
			bool flag = false;
			if (Target.Length > 0)
			{
				hyperLink.Target = Target;
			}
			if (DataTextField.Length > 0)
			{
				flag = true;
			}
			else
			{
				hyperLink.Text = Text;
			}
			if (DataNavigateUrlFields.Length != 0)
			{
				flag = true;
			}
			else
			{
				hyperLink.NavigateUrl = NavigateUrl;
			}
			if (flag && cellType == DataControlCellType.DataCell && (rowState & DataControlRowState.Insert) == 0)
			{
				cell.DataBinding += OnDataBindField;
			}
			hyperLink.ControlStyle.CopyFrom(base.ControlStyle);
			cell.Controls.Add(hyperLink);
		}
	}

	/// <summary>Formats the navigation URL using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFormatString" /> property.</summary>
	/// <param name="dataUrlValues">An array of values to combine with the format string.</param>
	/// <returns>The formatted URL value.</returns>
	protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
	{
		if (dataUrlValues == null || dataUrlValues.Length == 0)
		{
			return string.Empty;
		}
		if (DataNavigateUrlFormatString.Length > 0)
		{
			return string.Format(DataNavigateUrlFormatString, dataUrlValues);
		}
		return dataUrlValues[0].ToString();
	}

	/// <summary>Formats the caption text using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataTextFormatString" /> property.</summary>
	/// <param name="dataTextValue">The text value to format. </param>
	/// <returns>The formatted text value.</returns>
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

	private void OnDataBindField(object sender, EventArgs e)
	{
		DataControlFieldCell obj = (DataControlFieldCell)sender;
		HyperLink hyperLink = (HyperLink)obj.Controls[0];
		object bindingContainer = obj.BindingContainer;
		object dataItem = DataBinder.GetDataItem(bindingContainer);
		if (DataTextField.Length > 0)
		{
			if (textProperty == null)
			{
				SetupProperties(bindingContainer);
			}
			hyperLink.Text = FormatDataTextValue(textProperty.GetValue(dataItem));
		}
		string[] dataNavigateUrlFields = DataNavigateUrlFields;
		if (dataNavigateUrlFields.Length != 0)
		{
			if (urlProperties == null)
			{
				SetupProperties(bindingContainer);
			}
			object[] array = new object[dataNavigateUrlFields.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = urlProperties[i].GetValue(dataItem);
			}
			hyperLink.NavigateUrl = FormatDataNavigateUrlValue(array);
		}
	}

	private void SetupProperties(object controlContainer)
	{
		object dataItem = DataBinder.GetDataItem(controlContainer);
		PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
		if (DataTextField.Length > 0)
		{
			textProperty = properties.Find(DataTextField, ignoreCase: true);
			if (textProperty == null)
			{
				throw new InvalidOperationException("Property '" + DataTextField + "' not found in object of type " + dataItem.GetType());
			}
		}
		string[] dataNavigateUrlFields = DataNavigateUrlFields;
		if (dataNavigateUrlFields.Length == 0)
		{
			return;
		}
		urlProperties = new PropertyDescriptor[dataNavigateUrlFields.Length];
		for (int i = 0; i < dataNavigateUrlFields.Length; i++)
		{
			PropertyDescriptor propertyDescriptor = properties.Find(dataNavigateUrlFields[i], ignoreCase: true);
			if (propertyDescriptor == null)
			{
				throw new InvalidOperationException("Property '" + dataNavigateUrlFields[i] + "' not found in object of type " + dataItem.GetType());
			}
			urlProperties[i] = propertyDescriptor;
		}
	}

	/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> class.</summary>
	/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</returns>
	protected override DataControlField CreateField()
	{
		return new HyperLinkField();
	}

	/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object to the specified object.</summary>
	/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object that receives the copy.</param>
	protected override void CopyProperties(DataControlField newField)
	{
		base.CopyProperties(newField);
		HyperLinkField obj = (HyperLinkField)newField;
		obj.DataNavigateUrlFields = DataNavigateUrlFields;
		obj.DataNavigateUrlFormatString = DataNavigateUrlFormatString;
		obj.DataTextField = DataTextField;
		obj.DataTextFormatString = DataTextFormatString;
		obj.NavigateUrl = NavigateUrl;
		obj.Target = Target;
		obj.Text = Text;
	}

	/// <summary>Indicates that the controls contained by the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object support callbacks.</summary>
	public override void ValidateSupportsCallback()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> class.</summary>
	public HyperLinkField()
	{
	}
}
