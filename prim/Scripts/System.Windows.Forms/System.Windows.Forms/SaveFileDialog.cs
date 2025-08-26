using System.ComponentModel;
using System.IO;

namespace System.Windows.Forms;

/// <summary>Prompts the user to select a location for saving a file. This class cannot be inherited.</summary>
/// <filterpriority>2</filterpriority>
[Designer("System.Windows.Forms.Design.SaveFileDialogDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
public sealed class SaveFileDialog : FileDialog
{
	/// <summary>Gets or sets a value indicating whether the dialog box prompts the user for permission to create a file if the user specifies a file that does not exist.</summary>
	/// <returns>true if the dialog box prompts the user before creating a file if the user specifies a file name that does not exist; false if the dialog box automatically creates the new file without prompting the user for permission. The default value is false.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(false)]
	public bool CreatePrompt
	{
		get
		{
			return createPrompt;
		}
		set
		{
			createPrompt = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the Save As dialog box displays a warning if the user specifies a file name that already exists.</summary>
	/// <returns>true if the dialog box prompts the user before overwriting an existing file if the user specifies a file name that already exists; false if the dialog box automatically overwrites the existing file without prompting the user for permission. The default value is true.</returns>
	/// <filterpriority>1</filterpriority>
	[DefaultValue(true)]
	public bool OverwritePrompt
	{
		get
		{
			return overwritePrompt;
		}
		set
		{
			overwritePrompt = value;
		}
	}

	internal override string DialogTitle
	{
		get
		{
			string text = base.DialogTitle;
			if (text.Length == 0)
			{
				text = "Save As";
			}
			return text;
		}
	}

	/// <summary>Initializes a new instance of this class.</summary>
	public SaveFileDialog()
	{
		form.SuspendLayout();
		form.Text = "Save As";
		base.FileTypeLabel = "Save as type:";
		base.OpenSaveButtonText = "Save";
		base.SearchSaveLabel = "Save in:";
		fileDialogType = FileDialogType.SaveFileDialog;
		form.ResumeLayout(performLayout: false);
	}

	/// <summary>Opens the file with read/write permission selected by the user.</summary>
	/// <returns>The read/write file selected by the user.</returns>
	/// <filterpriority>1</filterpriority>
	public Stream OpenFile()
	{
		if (base.FileName == null)
		{
			throw new ArgumentNullException("OpenFile", "FileName is null");
		}
		try
		{
			return new FileStream(base.FileName, FileMode.Create, FileAccess.ReadWrite);
		}
		catch (Exception)
		{
			return null;
		}
	}

	/// <summary>Resets all dialog box options to their default values.</summary>
	/// <filterpriority>1</filterpriority>
	public override void Reset()
	{
		base.Reset();
		overwritePrompt = true;
		createPrompt = false;
	}
}
