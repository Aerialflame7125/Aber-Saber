using System.Configuration;

namespace System.Web.Configuration;

internal class ProfilePropertyNameValidator : StringValidator
{
	public ProfilePropertyNameValidator()
		: base(1)
	{
	}

	public override void Validate(object value)
	{
		base.Validate(value);
		string obj = value as string;
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		string text = obj.Trim();
		if (string.IsNullOrEmpty(text))
		{
			throw new ArgumentException("name cannot be empty.");
		}
		if (text.Contains("."))
		{
			throw new ArgumentException("name cannot contain period");
		}
	}
}
