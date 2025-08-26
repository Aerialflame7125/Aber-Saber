namespace System.Windows.Forms;

/// <summary>Specifies the source for <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> automatic completion functionality.</summary>
/// <filterpriority>2</filterpriority>
public enum AutoCompleteSource
{
	/// <summary>Specifies the file system as the source.</summary>
	FileSystem = 1,
	/// <summary>Includes the Uniform Resource Locators (URLs) in the history list.</summary>
	HistoryList = 2,
	/// <summary>Includes the Uniform Resource Locators (URLs) in the list of those URLs most recently used.</summary>
	RecentlyUsedList = 4,
	/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.HistoryList" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.RecentlyUsedList" /> as the source.</summary>
	AllUrl = 6,
	/// <summary>Specifies the equivalent of <see cref="F:System.Windows.Forms.AutoCompleteSource.FileSystem" /> and <see cref="F:System.Windows.Forms.AutoCompleteSource.AllUrl" /> as the source. This is the default value when <see cref="T:System.Windows.Forms.AutoCompleteMode" /> has been set to a value other than the default.</summary>
	AllSystemSources = 7,
	/// <summary>Specifies that only directory names and not file names will be automatically completed.</summary>
	FileSystemDirectories = 32,
	/// <summary>Specifies strings from a built-in <see cref="T:System.Windows.Forms.AutoCompleteStringCollection" /> as the source.</summary>
	CustomSource = 64,
	/// <summary>Specifies that no <see cref="T:System.Windows.Forms.AutoCompleteSource" /> is currently in use. This is the default value of <see cref="T:System.Windows.Forms.AutoCompleteSource" />.</summary>
	None = 128,
	/// <summary>Specifies that the items of the <see cref="T:System.Windows.Forms.ComboBox" /> represent the source.</summary>
	ListItems = 256
}
