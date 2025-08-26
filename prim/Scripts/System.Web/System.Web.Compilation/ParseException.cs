using System.IO;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Compilation;

[Serializable]
internal class ParseException : HtmlizedException
{
	private ILocation location;

	private string fileText;

	public override string Title => "Parser Error";

	public override string Description => "Error parsing a resource required to service this request. Review your source file and modify it to fix this error.";

	public override string ErrorMessage => Message;

	public override string SourceFile => FileName;

	public override string FileName
	{
		get
		{
			if (location == null)
			{
				return null;
			}
			return location.Filename;
		}
	}

	public override string FileText
	{
		get
		{
			if (fileText != null)
			{
				return fileText;
			}
			string text = ((location != null) ? location.FileText : null);
			if (text != null && text.Length > 0)
			{
				return text;
			}
			if (FileName == null)
			{
				return null;
			}
			using (TextReader textReader = new StreamReader(FileName))
			{
				fileText = textReader.ReadToEnd();
			}
			return fileText;
		}
	}

	public override int[] ErrorLines
	{
		get
		{
			if (location == null)
			{
				return null;
			}
			return new int[2] { location.BeginLine, location.EndLine };
		}
	}

	public override bool ErrorLinesPaired => true;

	private ParseException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	public ParseException(ILocation location, string message)
		: this(location, message, null)
	{
		location = new Location(location);
	}

	public ParseException(ILocation location, string message, Exception inner)
		: base(message, inner)
	{
		this.location = location;
	}

	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
	{
		base.GetObjectData(info, ctx);
	}
}
