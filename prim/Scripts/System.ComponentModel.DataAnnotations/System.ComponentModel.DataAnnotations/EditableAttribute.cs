namespace System.ComponentModel.DataAnnotations;

/// <summary>Indicates whether a data field is editable.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
public sealed class EditableAttribute : Attribute
{
	/// <summary>Gets a value that indicates whether a field is editable.</summary>
	/// <returns>
	///   <see langword="true" /> if the field is editable; otherwise, <see langword="false" />.</returns>
	public bool AllowEdit { get; private set; }

	/// <summary>Gets or sets a value that indicates whether an initial value is enabled.</summary>
	/// <returns>
	///   <see langword="true" /> if an initial value is enabled; otherwise, <see langword="false" />.</returns>
	public bool AllowInitialValue { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.EditableAttribute" /> class.</summary>
	/// <param name="allowEdit">
	///   <see langword="true" /> to specify that field is editable; otherwise, <see langword="false" />.</param>
	public EditableAttribute(bool allowEdit)
	{
		AllowEdit = allowEdit;
		AllowInitialValue = allowEdit;
	}
}
