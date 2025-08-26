using System.Configuration;

namespace System.Web.Configuration;

[AttributeUsage(AttributeTargets.Property)]
internal sealed class RegexMatchTimeoutValidatorAttribute : ConfigurationValidatorAttribute
{
	public override ConfigurationValidatorBase ValidatorInstance => new RegexMatchTimeoutValidator();
}
