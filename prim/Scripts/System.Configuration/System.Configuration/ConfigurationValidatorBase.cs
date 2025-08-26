namespace System.Configuration;

/// <summary>Acts as a base class for deriving a validation class so that a value of an object can be verified.</summary>
public abstract class ConfigurationValidatorBase
{
	/// <summary>Initializes an instance of the <see cref="T:System.Configuration.ConfigurationValidatorBase" /> class.</summary>
	protected ConfigurationValidatorBase()
	{
	}

	/// <summary>Determines whether an object can be validated based on type.</summary>
	/// <param name="type">The object type.</param>
	/// <returns>
	///   <see langword="true" /> if the <paramref name="type" /> parameter value matches the expected <see langword="type" />; otherwise, <see langword="false" />.</returns>
	public virtual bool CanValidate(Type type)
	{
		return false;
	}

	/// <summary>Determines whether the value of an object is valid.</summary>
	/// <param name="value">The object value.</param>
	public abstract void Validate(object value);
}
