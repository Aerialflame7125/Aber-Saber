using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms;

/// <summary>Prompts the user to open a file. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
public sealed class OpenFileDialog : FileDialog
{
	/// <summary>Gets or sets a value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist. </summary>
	/// <returns>true if the dialog box displays a warning when the user specifies a file name that does not exist; otherwise, false. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public override bool CheckFileExists
	{
		get
		{
			return base.CheckFileExists;
		}
		set
		{
			base.CheckFileExists = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the dialog box allows multiple files to be selected. </summary>
	/// <returns>true if the dialog box allows multiple files to be selected together or concurrently; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool Multiselect
	{
		get
		{
			return base.BMultiSelect;
		}
		set
		{
			base.BMultiSelect = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the read-only check box is selected. </summary>
	/// <returns>true if the read-only check box is selected; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public new bool ReadOnlyChecked
	{
		get
		{
			return base.ReadOnlyChecked;
		}
		set
		{
			base.ReadOnlyChecked = value;
		}
	}

	/// <summary>Gets the file name and extension for the file selected in the dialog box. The file name does not include the path.</summary>
	/// <returns>The file name and extension for the file selected in the dialog box. The file name does not include the path. The default value is an empty string.</returns>
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public string SafeFileName => Path.GetFileName(base.FileName);

	/// <summary>Gets an array of file names and extensions for all the selected files in the dialog box. The file names do not include the path.</summary>
	/// <returns>An array of file names and extensions for all the selected files in the dialog box. The file names do not include the path. If no files are selected, an empty array is returned.</returns>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string[] SafeFileNames
	{
		get
		{
			string[] array = base.FileNames;
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Path.GetFileName(array[i]);
			}
			return array;
		}
	}

	/// <summary>Gets or sets a value indicating whether the dialog box contains a read-only check box. </summary>
	/// <returns>true if the dialog box contains a read-only check box; otherwise, false. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public new bool ShowReadOnly
	{
		get
		{
			return base.ShowReadOnly;
		}
		set
		{
			base.ShowReadOnly = value;
		}
	}

	internal override string DialogTitle
	{
		get
		{
			string text = base.DialogTitle;
			if (text.Length == 0)
			{
				text = "Open";
			}
			return text;
		}
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.OpenFileDialog" /> class.</summary>
	public OpenFileDialog()
	{
		form.SuspendLayout();
		form.Text = "Open";
		CheckFileExists = true;
		base.OpenSaveButtonText = "Open";
		base.SearchSaveLabel = "Look in:";
		fileDialogType = FileDialogType.OpenFileDialog;
		form.ResumeLayout(performLayout: false);
	}

	/// <summary>Opens the file selected by the user, with read-only permission. The file is specified by the <see cref="P:System.Windows.Forms.FileDialog.FileName" /> property. </summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> that specifies the read-only file selected by the user.</returns>
	/// <exception cref="T:System.ArgumentNullException">The file name is null. </exception>
	/// <filterpriority>1</filterpriority>
	public Stream OpenFile()
	{
		if (base.FileName.Length == 0)
		{
			throw new ArgumentNullException("OpenFile", "FileName is null");
		}
		return new FileStream(base.FileName, FileMode.Open, FileAccess.Read);
	}

	/// <summary>Resets all properties to their default values. </summary>
	/// <filterpriority>1</filterpriority>
	public override void Reset()
	{
		base.Reset();
		base.BMultiSelect = false;
		base.CheckFileExists = true;
		base.ReadOnlyChecked = false;
		base.ShowReadOnly = false;
	}
}
