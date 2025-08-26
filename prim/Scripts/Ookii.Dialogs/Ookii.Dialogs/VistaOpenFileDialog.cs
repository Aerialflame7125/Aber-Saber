using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Ookii.Dialogs.Interop;

namespace Ookii.Dialogs;

[Description("Prompts the user to open a file.")]
[ToolboxBitmap(typeof(OpenFileDialog), "OpenFileDialog.bmp")]
public class VistaOpenFileDialog : VistaFileDialog
{
	private const int _openDropDownId = 16386;

	private const int _openItemId = 16387;

	private const int _readOnlyItemId = 16388;

	private bool _showReadOnly;

	private bool _readOnlyChecked;

	[DefaultValue(true)]
	[Description("A value indicating whether the dialog box displays a warning if the user specifies a file name that does not exist.")]
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

	[Description("A value indicating whether the dialog box allows multiple files to be selected.")]
	[DefaultValue(false)]
	[Category("Behavior")]
	public bool Multiselect
	{
		get
		{
			if (base.DownlevelDialog != null)
			{
				return ((OpenFileDialog)base.DownlevelDialog).Multiselect;
			}
			return GetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT);
		}
		set
		{
			if (base.DownlevelDialog != null)
			{
				((OpenFileDialog)base.DownlevelDialog).Multiselect = value;
			}
			SetOption(NativeMethods.FOS.FOS_ALLOWMULTISELECT, value);
		}
	}

	[Description("A value indicating whether the dialog box contains a read-only check box.")]
	[Category("Behavior")]
	[DefaultValue(false)]
	public bool ShowReadOnly
	{
		get
		{
			if (base.DownlevelDialog != null)
			{
				return ((OpenFileDialog)base.DownlevelDialog).ShowReadOnly;
			}
			return _showReadOnly;
		}
		set
		{
			if (base.DownlevelDialog != null)
			{
				((OpenFileDialog)base.DownlevelDialog).ShowReadOnly = value;
			}
			else
			{
				_showReadOnly = value;
			}
		}
	}

	[DefaultValue(false)]
	[Category("Behavior")]
	[Description("A value indicating whether the read-only check box is selected.")]
	public bool ReadOnlyChecked
	{
		get
		{
			if (base.DownlevelDialog != null)
			{
				return ((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked;
			}
			return _readOnlyChecked;
		}
		set
		{
			if (base.DownlevelDialog != null)
			{
				((OpenFileDialog)base.DownlevelDialog).ReadOnlyChecked = value;
			}
			else
			{
				_readOnlyChecked = value;
			}
		}
	}

	public VistaOpenFileDialog()
		: this(forceDownlevel: false)
	{
	}

	public VistaOpenFileDialog(bool forceDownlevel)
	{
		if (forceDownlevel || !VistaFileDialog.IsVistaFileDialogSupported)
		{
			base.DownlevelDialog = new OpenFileDialog();
		}
	}

	public override void Reset()
	{
		base.Reset();
		if (base.DownlevelDialog == null)
		{
			CheckFileExists = true;
			_showReadOnly = false;
			_readOnlyChecked = false;
		}
	}

	public Stream OpenFile()
	{
		if (base.DownlevelDialog != null)
		{
			return ((OpenFileDialog)base.DownlevelDialog).OpenFile();
		}
		string fileName = base.FileName;
		if (string.IsNullOrEmpty(fileName))
		{
			throw new ArgumentNullException("FileName");
		}
		return new FileStream(fileName, FileMode.Open, FileAccess.Read);
	}

	internal override IFileDialog CreateFileDialog()
	{
		return (NativeFileOpenDialog)new FileOpenDialogRCW();
	}

	internal override void SetDialogProperties(IFileDialog dialog)
	{
		base.SetDialogProperties(dialog);
		if (_showReadOnly)
		{
			IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
			fileDialogCustomize.EnableOpenDropDown(16386);
			fileDialogCustomize.AddControlItem(16386, 16387, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.OpenButton));
			fileDialogCustomize.AddControlItem(16386, 16388, ComDlgResources.LoadString(ComDlgResources.ComDlgResourceId.ReadOnly));
		}
	}

	internal override void GetResult(IFileDialog dialog)
	{
		if (Multiselect)
		{
			((IFileOpenDialog)dialog).GetResults(out var ppenum);
			ppenum.GetCount(out var pdwNumItems);
			string[] array = new string[pdwNumItems];
			for (uint num = 0u; num < pdwNumItems; num++)
			{
				ppenum.GetItemAt(num, out var ppsi);
				ppsi.GetDisplayName(NativeMethods.SIGDN.SIGDN_FILESYSPATH, out var ppszName);
				array[num] = ppszName;
			}
			base.FileNamesInternal = array;
		}
		else
		{
			base.FileNamesInternal = null;
		}
		if (ShowReadOnly)
		{
			IFileDialogCustomize fileDialogCustomize = (IFileDialogCustomize)dialog;
			fileDialogCustomize.GetSelectedControlItem(16386, out var pdwIDItem);
			_readOnlyChecked = pdwIDItem == 16388;
		}
		base.GetResult(dialog);
	}
}
