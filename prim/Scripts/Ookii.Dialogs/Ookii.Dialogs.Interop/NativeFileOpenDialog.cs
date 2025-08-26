using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop;

[ComImport]
[CoClass(typeof(FileOpenDialogRCW))]
[Guid("d57c7288-d4ad-4768-be02-9d969532d960")]
internal interface NativeFileOpenDialog : IFileOpenDialog, IFileDialog, IModalWindow
{
}
