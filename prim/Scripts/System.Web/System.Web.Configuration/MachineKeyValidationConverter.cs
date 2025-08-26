using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration;

/// <summary>Provides methods for converting <see cref="T:System.Web.Configuration.MachineKeyValidation" /> objects to and from strings.</summary>
public sealed class MachineKeyValidationConverter : ConfigurationConverterBase
{
	private const string InvalidValue = "The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.";

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.MachineKeyValidationConverter" /> class. </summary>
	public MachineKeyValidationConverter()
	{
	}

	/// <summary>Converts a string to the equivalent <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</summary>
	/// <param name="ctx">This parameter is not used.</param>
	/// <param name="ci">This parameter is not used.</param>
	/// <param name="data">The string to convert.</param>
	/// <returns>The equivalent <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</returns>
	/// <exception cref="T:System.ArgumentException">The data is not one of the expected strings.</exception>
	public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
	{
		return (string)data switch
		{
			"MD5" => MachineKeyValidation.MD5, 
			"SHA1" => MachineKeyValidation.SHA1, 
			"3DES" => MachineKeyValidation.TripleDES, 
			"AES" => MachineKeyValidation.AES, 
			"HMACSHA256" => MachineKeyValidation.HMACSHA256, 
			"HMACSHA384" => MachineKeyValidation.HMACSHA384, 
			"HMACSHA512" => MachineKeyValidation.HMACSHA512, 
			_ => throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512."), 
		};
	}

	/// <summary>Converts a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value to the string representation of that value.</summary>
	/// <param name="ctx">This parameter is not used.</param>
	/// <param name="ci">This parameter is not used.</param>
	/// <param name="value">The <see cref="T:System.Web.Configuration.MachineKeyValidation" /> to be converted.</param>
	/// <param name="type">This parameter is not used.</param>
	/// <returns>A string representing a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> value.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="value" /> parameter is not one of the expected enumerated values.</exception>
	/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter is not a <see cref="T:System.Web.Configuration.MachineKeyValidation" /> object.</exception>
	public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
	{
		if (value == null || value.GetType() != typeof(MachineKeyValidation))
		{
			throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512.");
		}
		return (MachineKeyValidation)value switch
		{
			MachineKeyValidation.MD5 => "MD5", 
			MachineKeyValidation.SHA1 => "SHA1", 
			MachineKeyValidation.TripleDES => "3DES", 
			MachineKeyValidation.AES => "AES", 
			MachineKeyValidation.HMACSHA256 => "HMACSHA256", 
			MachineKeyValidation.HMACSHA384 => "HMACSHA384", 
			MachineKeyValidation.HMACSHA512 => "HMACSHA512", 
			_ => throw new ArgumentException("The enumeration value must be one of the following: SHA1, MD5, 3DES, AES, HMACSHA256, HMACSHA384, HMACSHA512."), 
		};
	}
}
