namespace System.Web.Compilation;

internal sealed class AspComponent
{
	public readonly Type Type;

	public readonly string Prefix;

	public readonly string Source;

	public readonly bool FromConfig;

	public readonly string Namespace;

	public AspComponent(Type type, string ns, string prefix, string source, bool fromConfig)
	{
		Type = type;
		Namespace = ns;
		Prefix = prefix;
		Source = source;
		FromConfig = fromConfig;
	}
}
