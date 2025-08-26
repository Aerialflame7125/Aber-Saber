using System.Configuration;

namespace System.Web.Configuration;

public class PositiveTimeSpanValidator : ConfigurationValidatorBase
{
	public override bool CanValidate(Type t)
	{
		return t == typeof(TimeSpan);
	}

	public override void Validate(object value)
	{
		if (((TimeSpan)value).Ticks <= 0)
		{
			throw new ConfigurationErrorsException("TimeSpan value must be positive.");
		}
	}
}
