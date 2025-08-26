using System.Web.Compilation;

namespace System.Web.UI;

internal abstract class CodeBuilder : ControlBuilder
{
	private string code;

	private bool isAssign;

	internal string Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	internal bool IsAssign => isAssign;

	public CodeBuilder(string code, bool isAssign, ILocation location)
	{
		this.code = code;
		this.isAssign = isAssign;
		base.Line = location.BeginLine;
		base.FileName = location.Filename;
		base.Location = location;
	}

	internal override object CreateInstance()
	{
		return null;
	}
}
