using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace System.Web.Security;

/// <summary>Validates whether a password field meets the current password requirements for the membership provider.</summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MembershipPasswordAttribute : ValidationAttribute
{
	/// <summary>Gets or sets the <see langword="MinNonAlphanumericCharactersError" /> attribute.</summary>
	/// <returns>The <see langword="MinNonAlphanumericCharactersError" /> attribute.</returns>
	public string MinNonAlphanumericCharactersError { get; set; }

	/// <summary>Gets or sets the <see langword="MinPasswordLengthError" /> attribute.</summary>
	/// <returns>The <see langword="MinPasswordLengthError" /> attribute.</returns>
	public string MinPasswordLengthError { get; set; }

	/// <summary>Gets or sets the minimum required non-alpha numeric characters the attribute uses for validation.</summary>
	/// <returns>The minimum required non-alpha numeric characters the attribute uses for validation.</returns>
	public int MinRequiredNonAlphanumericCharacters { get; set; }

	/// <summary>Gets or sets the minimum required password length this attribute uses for validation.</summary>
	/// <returns>The minimum required password length this attribute uses for validation.</returns>
	public int MinRequiredPasswordLength { get; set; }

	/// <summary>Gets or sets the <see langword="PasswordStrengthError" /> attribute.</summary>
	/// <returns>The <see langword="PasswordStrengthError" /> attribute.</returns>
	public string PasswordStrengthError { get; set; }

	/// <summary>Gets or sets the regular expression string representing the password strength the attribute uses for validation.</summary>
	/// <returns>The regular expression string representing the password strength the attribute uses for validation.</returns>
	public string PasswordStrengthRegularExpression { get; set; }

	/// <summary>Gets or sets the <see cref="T:System.Type" /> that contains the resources for the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinPasswordLengthError" /> property, the <see cref="P:System.Web.Security.MembershipPasswordAttribute.MinNonAlphanumericCharactersError" /> property, and the <see cref="P:System.Web.Security.MembershipPasswordAttribute.PasswordStrengthError" /> property.</summary>
	public Type ResourceType { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.MembershipPasswordAttribute" /> class.</summary>
	public MembershipPasswordAttribute()
	{
		if (Membership.Provider != null)
		{
			MinRequiredNonAlphanumericCharacters = Membership.Provider.MinRequiredNonAlphanumericCharacters;
			MinRequiredPasswordLength = Membership.Provider.MinRequiredPasswordLength;
			PasswordStrengthRegularExpression = Membership.Provider.PasswordStrengthRegularExpression;
		}
		else
		{
			MinRequiredPasswordLength = 7;
			MinRequiredNonAlphanumericCharacters = 1;
			PasswordStrengthRegularExpression = "(?=.{6,})(?=(.*\\d){1,})(?=(.*\\W){1,})";
		}
		MinNonAlphanumericCharactersError = "The '{0}' field is an invalid password. Password must have {1} or more non-alphanumeric characters.";
		MinPasswordLengthError = "The '{0}' field is an invalid password. Password must have {1} or more characters.";
		PasswordStrengthError = "The '{0}' field is an invalid password. It does not meet the password strength requirements";
		base.ErrorMessage = "The field {0} is invalid.";
	}

	/// <summary>Validates the specified value with respect to the current validation attribute.</summary>
	/// <param name="value">The value to validate.</param>
	/// <param name="validationContext">The context information about the validation operation.</param>
	/// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult" /> class.</returns>
	protected override ValidationResult IsValid(object value, ValidationContext validationContext)
	{
		string text = value as string;
		bool flag = false;
		if (string.IsNullOrEmpty(text))
		{
			return null;
		}
		string format = string.Empty;
		int num = 0;
		Regex regex = new Regex("\\W|_");
		if (MinRequiredPasswordLength > 0 && text.Length < MinRequiredPasswordLength)
		{
			format = MinPasswordLengthError;
			num = MinRequiredPasswordLength;
			flag = true;
		}
		if (!flag && MinRequiredNonAlphanumericCharacters > 0 && regex.Matches(text).Count < MinRequiredNonAlphanumericCharacters)
		{
			format = MinNonAlphanumericCharactersError;
			num = MinRequiredNonAlphanumericCharacters;
			flag = true;
		}
		if (!flag && !string.IsNullOrEmpty(PasswordStrengthRegularExpression) && new Regex(PasswordStrengthRegularExpression).IsMatch(text))
		{
			format = PasswordStrengthError;
			flag = true;
		}
		if (flag)
		{
			if (validationContext == null)
			{
				return new ValidationResult("error");
			}
			return new ValidationResult(string.Format(format, validationContext.DisplayName, num), new string[1] { validationContext.MemberName });
		}
		return ValidationResult.Success;
	}
}
