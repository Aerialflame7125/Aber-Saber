using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms.Design;

/// <summary>Provides a user interface for choosing a folder from the file system.</summary>
[System.MonoTODO]
public class FolderNameEditor : UITypeEditor
{
	/// <summary>Defines identifiers used to indicate the root folder for a folder browser to initially browse to.</summary>
	protected enum FolderBrowserFolder
	{
		/// <summary>The user's desktop.</summary>
		Desktop = 0,
		/// <summary>The user's favorites list.</summary>
		Favorites = 6,
		/// <summary>The contents of the My Computer icon.</summary>
		MyComputer = 17,
		/// <summary>The user's My Documents folder.</summary>
		MyDocuments = 5,
		/// <summary>User's location to store pictures.</summary>
		MyPictures = 39,
		/// <summary>Network and dial-up connections.</summary>
		NetAndDialUpConnections = 49,
		/// <summary>The network neighborhood.</summary>
		NetworkNeighborhood = 18,
		/// <summary>A folder containing installed printers.</summary>
		Printers = 4,
		/// <summary>A folder containing shortcuts to recently opened files.</summary>
		Recent = 8,
		/// <summary>A folder containing shortcuts to applications to send documents to.</summary>
		SendTo = 9,
		/// <summary>The user's start menu.</summary>
		StartMenu = 11,
		/// <summary>The user's file templates.</summary>
		Templates = 21
	}

	/// <summary>Defines identifiers used to specify behavior of a <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" />.</summary>
	[Flags]
	protected enum FolderBrowserStyles
	{
		/// <summary>The folder browser can only return computers. If the user selects anything other than a computer, the OK button is grayed.</summary>
		BrowseForComputer = 0x1000,
		/// <summary>The folder browser can return any object that it can return.</summary>
		BrowseForEverything = 0x4000,
		/// <summary>The folder browser can only return printers. If the user selects anything other than a printer, the OK button is grayed.</summary>
		BrowseForPrinter = 0x2000,
		/// <summary>The folder browser will not include network folders below the domain level in the dialog box's tree view control, or allow navigation to network locations outside of the domain.</summary>
		RestrictToDomain = 2,
		/// <summary>The folder browser will only return local file system directories. If the user selects folders that are not part of the local file system, the OK button is grayed.</summary>
		RestrictToFilesystem = 1,
		/// <summary>The folder browser will only return obejcts of the local file system that are within the root folder or a subfolder of the root folder. If the user selects a subfolder of the root folder that is not part of the local file system, the OK button is grayed.</summary>
		RestrictToSubfolders = 8,
		/// <summary>The folder browser includes a <see cref="T:System.Windows.Forms.TextBox" /> control in the browse dialog box that allows the user to type the name of an item.</summary>
		ShowTextBox = 0x10
	}

	/// <summary>Represents a dialog box that allows the user to choose a folder. This class cannot be inherited.</summary>
	protected sealed class FolderBrowser : Component
	{
		private string descriptionText;

		private string directoryPath;

		private FolderBrowserStyles publicOptions;

		private FolderBrowserFolder startLocation;

		/// <summary>Gets or sets a description to show above the folders.</summary>
		/// <returns>The description to show above the folders.</returns>
		public string Description
		{
			get
			{
				return descriptionText;
			}
			set
			{
				descriptionText = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets the directory path to the object the user picked.</summary>
		/// <returns>The directory path to the object the user picked.</returns>
		public string DirectoryPath => directoryPath;

		/// <summary>Gets or sets the start location of the root node.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserFolder" /> that indicates the location for the folder browser to initially browse to.</returns>
		public FolderBrowserFolder StartLocation
		{
			get
			{
				return startLocation;
			}
			set
			{
				startLocation = value;
			}
		}

		/// <summary>The styles the folder browser will use when browsing folders. This should be a combination of flags from the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserStyles" /> enumeration.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowserStyles" /> enumeration member that indicates behavior for the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> to use.</returns>
		public FolderBrowserStyles Style
		{
			get
			{
				return publicOptions;
			}
			set
			{
				publicOptions = value;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> class.</summary>
		[System.MonoTODO]
		public FolderBrowser()
		{
			startLocation = FolderBrowserFolder.Desktop;
			publicOptions = FolderBrowserStyles.RestrictToFilesystem;
			descriptionText = string.Empty;
			directoryPath = string.Empty;
		}

		/// <summary>Shows the folder browser dialog.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.DialogResult" /> from the form.</returns>
		[System.MonoTODO]
		public DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		/// <summary>Shows the folder browser dialog with the specified owner.</summary>
		/// <param name="owner">Top-level window that will own the modal dialog (e.g.: System.Windows.Forms.Form).</param>
		/// <returns>The <see cref="T:System.Windows.Forms.DialogResult" /> from the form.</returns>
		[System.MonoTODO]
		public DialogResult ShowDialog(IWin32Window owner)
		{
			throw new NotImplementedException();
		}
	}

	private FolderBrowser folderBrowser;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.FolderNameEditor" /> class.</summary>
	public FolderNameEditor()
	{
	}

	/// <summary>Edits the specified object using the editor style provided by <see cref="M:System.Windows.Forms.Design.FolderNameEditor.GetEditStyle(System.ComponentModel.ITypeDescriptorContext)" />.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <param name="provider">A service object provider.</param>
	/// <param name="value">The value to set.</param>
	/// <returns>The new value of the object, or the old value if the object couldn't be updated.</returns>
	public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
	{
		if (folderBrowser == null)
		{
			folderBrowser = new FolderBrowser();
			InitializeDialog(folderBrowser);
		}
		if (folderBrowser.ShowDialog() != DialogResult.OK)
		{
			return value;
		}
		return folderBrowser.DirectoryPath;
	}

	/// <summary>Gets the editing style used by the <see cref="M:System.Windows.Forms.Design.FolderNameEditor.EditValue(System.ComponentModel.ITypeDescriptorContext,System.IServiceProvider,System.Object)" /> method.</summary>
	/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information.</param>
	/// <returns>A <see cref="T:System.Drawing.Design.UITypeEditorEditStyle" /> enumeration value indicating the provided editing style.</returns>
	public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
	{
		return UITypeEditorEditStyle.Modal;
	}

	/// <summary>Initializes the folder browser dialog.</summary>
	/// <param name="folderBrowser">A <see cref="T:System.Windows.Forms.Design.FolderNameEditor.FolderBrowser" /> to choose a folder.</param>
	protected virtual void InitializeDialog(FolderBrowser folderBrowser)
	{
	}
}
