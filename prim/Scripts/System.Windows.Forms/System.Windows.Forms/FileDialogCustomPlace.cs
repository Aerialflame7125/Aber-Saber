namespace System.Windows.Forms;

/// <summary>Represents an entry in a <see cref="T:System.Windows.Forms.FileDialog" /> custom place collection for Windows Vista.</summary>
public class FileDialogCustomPlace
{
	private string path;

	private Guid guid;

	/// <summary>Gets or sets the folder path to the custom place.</summary>
	/// <returns>A folder path to the custom place. If the custom place was specified with a Windows Vista Known Folder GUID, then an empty string is returned.</returns>
	public string Path
	{
		get
		{
			return path;
		}
		set
		{
			path = value;
			guid = Guid.Empty;
		}
	}

	/// <summary>Gets or sets the Windows Vista Known Folder GUID for the custom place.</summary>
	/// <returns>A <see cref="T:System.Guid" /> that indicates the Windows Vista Known Folder for the custom place. If the custom place was specified with a folder path, then an empty GUID is returned. For a list of the available Windows Vista Known Folders, see Known Folder GUIDs for File Dialog Custom Places or the KnownFolders.h file in the Windows SDK.</returns>
	public Guid KnownFolderGuid
	{
		get
		{
			return guid;
		}
		set
		{
			guid = value;
			path = string.Empty;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class. with a specified folder path to a custom place.</summary>
	/// <param name="path">A folder path to the custom place.</param>
	public FileDialogCustomPlace(string path)
	{
		this.path = path;
		guid = Guid.Empty;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> class with a custom place identified by a Windows Vista Known Folder GUID.</summary>
	/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder. </param>
	public FileDialogCustomPlace(Guid knownFolderGuid)
	{
		path = string.Empty;
		guid = knownFolderGuid;
	}

	/// <summary>Returns a string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</summary>
	/// <returns>A string that represents this <see cref="T:System.Windows.Forms.FileDialogCustomPlace" /> instance.</returns>
	public override string ToString()
	{
		return $"{GetType().ToString()} Path: {path} KnownFolderGuid: {guid}";
	}
}
