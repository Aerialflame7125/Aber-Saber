namespace System.Web.UI;

/// <summary>Defines the properties and methods that objects that participate in Web Forms validation must implement.</summary>
public interface IValidator
{
	/// <summary>When implemented by a class, gets or sets a value indicating whether the user-entered content in the specified control passes validation.</summary>
	/// <returns>
	///     <see langword="true" /> if the content is valid; otherwise, <see langword="false" />.</returns>
	bool IsValid { get; set; }

	/// <summary>When implemented by a class, gets or sets the error message text generated when the condition being validated fails.</summary>
	/// <returns>The error message to generate.</returns>
	string ErrorMessage { get; set; }

	/// <summary>When implemented by a class, evaluates the condition it checks and updates the <see cref="P:System.Web.UI.IValidator.IsValid" /> property.</summary>
	void Validate();
}
