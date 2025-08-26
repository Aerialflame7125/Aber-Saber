namespace System.Web.Util;

internal interface ISyncContext
{
	HttpContext HttpContext { get; }

	ISyncContextLock Enter();
}
