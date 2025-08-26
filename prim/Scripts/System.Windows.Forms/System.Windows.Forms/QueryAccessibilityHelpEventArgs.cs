using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Control.QueryAccessibilityHelp" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class QueryAccessibilityHelpEventArgs : EventArgs
{
	private string help_namespace;

	private string help_string;

	private string help_keyword;

	/// <summary>Gets or sets the Help keyword for the specified control.</summary>
	/// <returns>The Help topic associated with the <see cref="T:System.Windows.Forms.AccessibleObject" /> that was queried.</returns>
	/// <filterpriority>1</filterpriority>
	public string HelpKeyword
	{
		get
		{
			return help_keyword;
		}
		set
		{
			help_keyword = value;
		}
	}

	/// <summary>Gets or sets a value specifying the name of the Help file.</summary>
	/// <returns>The name of the Help file. This name can be in the form C:\path\sample.chm or /folder/file.htm.</returns>
	/// <filterpriority>1</filterpriority>
	public string HelpNamespace
	{
		get
		{
			return help_namespace;
		}
		set
		{
			help_namespace = value;
		}
	}

	/// <summary>Gets or sets the string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />.</summary>
	/// <returns>The Help to retrieve for the accessible object.</returns>
	/// <filterpriority>1</filterpriority>
	public string HelpString
	{
		get
		{
			return help_string;
		}
		set
		{
			help_string = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
	public QueryAccessibilityHelpEventArgs()
	{
		help_namespace = null;
		help_string = null;
		help_keyword = null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QueryAccessibilityHelpEventArgs" /> class.</summary>
	/// <param name="helpNamespace">The file containing Help for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
	/// <param name="helpString">The string defining what Help to get for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
	/// <param name="helpKeyword">The keyword to associate with the Help request for the <see cref="T:System.Windows.Forms.AccessibleObject" />. </param>
	public QueryAccessibilityHelpEventArgs(string helpNamespace, string helpString, string helpKeyword)
	{
		help_namespace = helpNamespace;
		help_string = helpString;
		help_keyword = helpKeyword;
	}
}
