namespace System.Web.Compilation;

internal enum BuildResultTypeCode
{
	Unknown = 0,
	AppCodeSubFolder = 1,
	Handler = 2,
	PageOrControl = 3,
	AppCode = 6,
	Global = 8,
	TopLevelAssembly = 9
}
