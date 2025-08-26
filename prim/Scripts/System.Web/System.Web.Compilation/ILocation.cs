namespace System.Web.Compilation;

internal interface ILocation
{
	string Filename { get; }

	int BeginLine { get; }

	int EndLine { get; }

	int BeginColumn { get; }

	int EndColumn { get; }

	string PlainText { get; }

	string FileText { get; }
}
