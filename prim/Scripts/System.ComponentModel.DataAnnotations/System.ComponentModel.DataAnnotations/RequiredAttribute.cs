namespace System.ComponentModel.DataAnnotations;

/// <summary>Specifies that a data field value is required.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class RequiredAttribute : ValidationAttribute
{
	/// <summary>Gets or sets a value that indicates whether an empty string is allowed.</summary>
	/// <returns>
	///   <see langword="true" /> if an empty string is allowed; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	public bool AllowEmptyStrings { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.RequiredAttribute" /> class.</summary>
	public RequiredAttribute()
		: base(() => "The {0} field is required.")
	{
	}

	/// <summary>Checks that the value of the required data field is not empty.</summary>
	/// <param name="value">The data field value to validate.</param>
	/// <returns>
	///   <see langword="true" /> if validation is successful; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ComponentModel.DataAnnotations.ValidationException">The data field value was <see langword="null" />.</exception>
	public override bool IsValid(object value)
	{
		if (value == null)
		{
			return false;
		}
		if (value is string text && !AllowEmptyStrings)
		{
			return text.Trim().Length != 0;
		}
		return true;
	}
}
