using System.Runtime.Serialization;

namespace System.Web;

[Serializable]
internal abstract class HtmlizedException : HttpException
{
	public abstract string Title { get; }

	public new abstract string Description { get; }

	public abstract string ErrorMessage { get; }

	public abstract string FileName { get; }

	public abstract string SourceFile { get; }

	public abstract string FileText { get; }

	public abstract int[] ErrorLines { get; }

	public abstract bool ErrorLinesPaired { get; }

	protected HtmlizedException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	protected HtmlizedException()
	{
	}

	protected HtmlizedException(string message)
		: base(message)
	{
	}

	protected HtmlizedException(string message, Exception inner)
		: base(message, inner)
	{
	}
}
