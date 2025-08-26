using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace System.Web.Configuration;

internal sealed class VersionConverter : ConfigurationConverterBase
{
	private Version minVersion;

	private string exceptionText;

	public VersionConverter()
	{
	}

	public VersionConverter(int minMajor, int minMinor, string exceptionText = null)
	{
		minVersion = new Version(minMajor, minMinor);
		this.exceptionText = exceptionText;
	}

	public override object ConvertFrom(ITypeDescriptorContext ctx, CultureInfo ci, object data)
	{
		string obj = data as string;
		if (string.IsNullOrEmpty(obj))
		{
			throw new ConfigurationErrorsException("The input string is too short or null.");
		}
		if (!Version.TryParse(obj, out var result))
		{
			throw new ConfigurationErrorsException("The input string wasn't in correct format.");
		}
		if (minVersion != null && result < minVersion)
		{
			throw new ConfigurationErrorsException(string.Format(exceptionText, result, minVersion));
		}
		return result;
	}

	public override object ConvertTo(ITypeDescriptorContext ctx, CultureInfo ci, object value, Type type)
	{
		Version version = value as Version;
		if (version == null)
		{
			throw new ArgumentException("Is not an instance of the Version type", "value");
		}
		if (type == typeof(string))
		{
			return version.ToString();
		}
		if (type == typeof(Version))
		{
			return version.Clone();
		}
		throw new ConfigurationErrorsException(string.Concat("Conversion to type '", type, "' is not supported."));
	}
}
