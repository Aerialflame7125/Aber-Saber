using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop;

[ComImport]
[CoClass(typeof(FileSaveDialogRCW))]
[Guid("84bccd23-5fde-4cdb-aea4-af64b83d78ab")]
internal interface NativeFileSaveDialog : IFileSaveDialog, IFileDialog, IModalWindow
{
}
