namespace System.Web.Services.Description;

internal enum CodeFlags
{
	IsPublic = 1,
	IsAbstract = 2,
	IsStruct = 4,
	IsNew = 8,
	IsByRef = 0x10,
	IsOut = 0x20,
	IsInterface = 0x40
}
