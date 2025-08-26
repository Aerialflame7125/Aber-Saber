using System.Configuration;

namespace System.Web.Services.Configuration;

/// <summary>Represents the &lt;diagnostics&gt; element in the Web.config configuration file.</summary>
public sealed class DiagnosticsElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty suppressReturningExceptions = new ConfigurationProperty("suppressReturningExceptions", typeof(bool), false);

	/// <summary>Gets or sets a value that indicates whether the service returns exceptions.</summary>
	/// <returns>
	///     <see langword="true" /> if the service returns exceptions; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("suppressReturningExceptions", DefaultValue = false)]
	public bool SuppressReturningExceptions
	{
		get
		{
			return (bool)base[suppressReturningExceptions];
		}
		set
		{
			base[suppressReturningExceptions] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.DiagnosticsElement" /> class. </summary>
	public DiagnosticsElement()
	{
		properties.Add(suppressReturningExceptions);
	}
}
