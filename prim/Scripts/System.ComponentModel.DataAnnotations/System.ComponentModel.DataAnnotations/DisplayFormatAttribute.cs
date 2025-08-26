namespace System.ComponentModel.DataAnnotations;

/// <summary>Specifies how data fields are displayed and formatted by ASP.NET Dynamic Data.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class DisplayFormatAttribute : Attribute
{
	/// <summary>Gets or sets the display format for the field value.</summary>
	/// <returns>A formatting string that specifies the display format for the value of the data field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
	public string DataFormatString { get; set; }

	/// <summary>Gets or sets the text that is displayed for a field when the field's value is <see langword="null" />.</summary>
	/// <returns>The text that is displayed for a field when the field's value is <see langword="null" />. The default is an empty string (""), which indicates that this property is not set.</returns>
	public string NullDisplayText { get; set; }

	/// <summary>Gets or sets a value that indicates whether empty string values ("") are automatically converted to <see langword="null" /> when the data field is updated in the data source.</summary>
	/// <returns>
	///   <see langword="true" /> if empty string values are automatically converted to <see langword="null" />; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool ConvertEmptyStringToNull { get; set; }

	/// <summary>Gets or sets a value that indicates whether the formatting string that is specified by the <see cref="P:System.ComponentModel.DataAnnotations.DisplayFormatAttribute.DataFormatString" /> property is applied to the field value when the data field is in edit mode.</summary>
	/// <returns>
	///   <see langword="true" /> if the formatting string applies to the field value in edit mode; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool ApplyFormatInEditMode { get; set; }

	/// <summary>Gets or sets a value that indicates whether the field should be HTML-encoded.</summary>
	/// <returns>
	///   <see langword="true" /> if the field should be HTML-encoded; otherwise, <see langword="false" />.</returns>
	public bool HtmlEncode { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.DisplayFormatAttribute" /> class.</summary>
	public DisplayFormatAttribute()
	{
		ConvertEmptyStringToNull = true;
		HtmlEncode = true;
	}
}
