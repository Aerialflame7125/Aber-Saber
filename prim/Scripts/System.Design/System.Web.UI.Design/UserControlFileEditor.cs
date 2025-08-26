using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a dialog box for selecting files to edit at design time.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class UserControlFileEditor : UrlEditor
{
	/// <summary>Gets the caption for the dialog box.</summary>
	/// <returns>The caption for the editor window.</returns>
	[System.MonoTODO]
	protected override string Caption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the file name filter string used to determine the items that appear in the file list of the dialog box.</summary>
	/// <returns>A string that filters the list of files available in the dialog box, such as "*.txt".</returns>
	[System.MonoTODO]
	protected override string Filter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.UserControlFileEditor" /> class.</summary>
	public UserControlFileEditor()
	{
	}
}
