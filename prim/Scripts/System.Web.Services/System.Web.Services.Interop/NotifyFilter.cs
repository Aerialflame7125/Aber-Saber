namespace System.Web.Services.Interop;

internal enum NotifyFilter
{
	OnSyncCallOut = 1,
	OnSyncCallEnter = 2,
	OnSyncCallExit = 4,
	OnSyncCallReturn = 8,
	AllSync = 15,
	All = -1,
	None = 0
}
