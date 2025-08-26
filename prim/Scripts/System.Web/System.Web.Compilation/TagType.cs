namespace System.Web.Compilation;

internal enum TagType
{
	Text,
	Tag,
	Close,
	SelfClosing,
	Directive,
	ServerComment,
	DataBinding,
	CodeRender,
	CodeRenderExpression,
	Include,
	CodeRenderEncode
}
