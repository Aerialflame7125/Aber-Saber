using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a user interface for selecting and editing a mail file name for a property at design time.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class MailFileEditor : UrlEditor
{
	/// <summary>Gets the caption for the editor dialog.</summary>
	/// <returns>The caption for the design-time dialog box.</returns>
	[System.MonoTODO]
	protected override string Caption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the file filter string for the dialog (such as "*.txt").</summary>
	/// <returns>The filter for selecting files in the design-time dialog box.</returns>
	[System.MonoTODO]
	protected override string Filter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.MailFileEditor" /> class.</summary>
	public MailFileEditor()
	{
	}
}
