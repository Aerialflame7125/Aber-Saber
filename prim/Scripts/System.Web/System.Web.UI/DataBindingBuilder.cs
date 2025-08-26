using System.Web.Compilation;

namespace System.Web.UI;

internal sealed class DataBindingBuilder : CodeBuilder
{
	public DataBindingBuilder(string code, ILocation location)
		: base(code, isAssign: false, location)
	{
		SetControlType(typeof(DataBoundLiteralControl));
	}
}
