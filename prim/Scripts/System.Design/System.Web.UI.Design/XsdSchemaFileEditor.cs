using System.Security.Permissions;

namespace System.Web.UI.Design;

/// <summary>Provides a design-time user interface for selecting an XML schema definition file.</summary>
[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
public class XsdSchemaFileEditor : UrlEditor
{
	/// <summary>Gets the caption to display on the selection dialog box.</summary>
	/// <returns>The caption text to display on the selection dialog box.</returns>
	[System.MonoTODO]
	protected override string Caption
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the URL filter options for the editor, which are used to filter the items that appear in the URL selection dialog box.</summary>
	/// <returns>A string that represents one or more URL filter options for the dialog box.</returns>
	[System.MonoTODO]
	protected override string Filter
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.XsdSchemaFileEditor" /> class.</summary>
	public XsdSchemaFileEditor()
	{
	}
}
