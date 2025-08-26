using System.Collections.ObjectModel;

namespace System.Windows.Forms;

/// <summary>Represents a collection of Windows Vista custom places for the <see cref="T:System.Windows.Forms.FileDialog" /> class.</summary>
public class FileDialogCustomPlacesCollection : Collection<FileDialogCustomPlace>
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> class. </summary>
	public FileDialogCustomPlacesCollection()
	{
	}

	/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
	/// <param name="knownFolderGuid">A <see cref="T:System.Guid" /> that represents a Windows Vista Known Folder. </param>
	public void Add(Guid knownFolderGuid)
	{
		Add(new FileDialogCustomPlace(knownFolderGuid));
	}

	/// <summary>Adds a custom place to the <see cref="T:System.Windows.Forms.FileDialogCustomPlacesCollection" /> collection.</summary>
	/// <param name="path">A folder path to the custom place.</param>
	public void Add(string path)
	{
		Add(new FileDialogCustomPlace(path));
	}
}
