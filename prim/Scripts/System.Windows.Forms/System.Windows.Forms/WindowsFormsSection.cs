using System.Configuration;

namespace System.Windows.Forms;

/// <summary>Defines a new <see cref="T:System.Configuration.ConfigurationSection" /> for parsing application settings. This class cannot be inherited. </summary>
public sealed class WindowsFormsSection : ConfigurationSection
{
	private ConfigurationPropertyCollection properties;

	private ConfigurationProperty jit_debugging;

	/// <summary>Gets or sets a value indicating whether just-in-time (JIT) debugging is used.</summary>
	/// <returns>true if JIT debugging is used; otherwise, false.</returns>
	[ConfigurationProperty("jitDebugging", DefaultValue = "False")]
	public bool JitDebugging
	{
		get
		{
			return (bool)base[jit_debugging];
		}
		set
		{
			base[jit_debugging] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.WindowsFormsSection" /> class. </summary>
	public WindowsFormsSection()
	{
		properties = new ConfigurationPropertyCollection();
		jit_debugging = new ConfigurationProperty("jitDebugging", typeof(bool), false);
		properties.Add(jit_debugging);
	}
}
