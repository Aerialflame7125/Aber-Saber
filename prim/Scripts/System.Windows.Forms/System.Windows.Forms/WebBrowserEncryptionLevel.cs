namespace System.Windows.Forms;

/// <summary>Specifies constants that define the encryption methods used by documents displayed in the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
/// <filterpriority>2</filterpriority>
public enum WebBrowserEncryptionLevel
{
	/// <summary>No security encryption.</summary>
	Insecure,
	/// <summary>Multiple security encryption methods in different Web page frames.</summary>
	Mixed,
	/// <summary>Unknown security encryption.</summary>
	Unknown,
	/// <summary>40-bit security encryption.</summary>
	Bit40,
	/// <summary>56-bit security encryption.</summary>
	Bit56,
	/// <summary>Fortezza security encryption.</summary>
	Fortezza,
	/// <summary>128-bit security encryption.</summary>
	Bit128
}
