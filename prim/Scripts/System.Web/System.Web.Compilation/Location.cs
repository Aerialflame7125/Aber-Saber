namespace System.Web.Compilation;

internal class Location : ILocation
{
	private int beginLine;

	private int endLine;

	private int beginColumn;

	private int endColumn;

	private string fileName;

	private string plainText;

	private ILocation location;

	public string Filename
	{
		get
		{
			return fileName;
		}
		set
		{
			fileName = value;
		}
	}

	public int BeginLine
	{
		get
		{
			return beginLine;
		}
		set
		{
			beginLine = value;
		}
	}

	public int EndLine
	{
		get
		{
			return endLine;
		}
		set
		{
			endLine = value;
		}
	}

	public int BeginColumn
	{
		get
		{
			return beginColumn;
		}
		set
		{
			beginColumn = value;
		}
	}

	public int EndColumn
	{
		get
		{
			return endColumn;
		}
		set
		{
			endColumn = value;
		}
	}

	public string PlainText
	{
		get
		{
			return plainText;
		}
		set
		{
			plainText = value;
		}
	}

	public string FileText
	{
		get
		{
			if (location != null)
			{
				return location.FileText;
			}
			return null;
		}
	}

	public Location(ILocation location)
	{
		Init(location);
	}

	public void Init(ILocation location)
	{
		if (location == null)
		{
			beginLine = 0;
			endLine = 0;
			beginColumn = 0;
			endColumn = 0;
			fileName = null;
			plainText = null;
		}
		else
		{
			beginLine = location.BeginLine;
			endLine = location.EndLine;
			beginColumn = location.BeginColumn;
			endColumn = location.EndColumn;
			fileName = location.Filename;
			plainText = location.PlainText;
		}
		this.location = location;
	}
}
