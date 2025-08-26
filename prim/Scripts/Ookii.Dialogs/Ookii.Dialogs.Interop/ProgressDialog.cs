using System.Runtime.InteropServices;

namespace Ookii.Dialogs.Interop;

[ComImport]
[CoClass(typeof(ProgressDialogRCW))]
[Guid("EBBC7C04-315E-11d2-B62F-006097DF5BD4")]
internal interface ProgressDialog : IProgressDialog
{
}
