namespace System.Web.Routing;

internal sealed class LiteralSubsegment : PathSubsegment
{
	public string Literal { get; private set; }

	public LiteralSubsegment(string literal)
	{
		Literal = literal;
	}
}
