namespace System.Web;

[Flags]
internal enum ExceptionPageTemplateType
{
	Standard = 1,
	CustomErrorDefault = 2,
	Htmlized = 4,
	SourceError = 8,
	CompilerOutput = 0x10,
	Any = 0xFFFF
}
