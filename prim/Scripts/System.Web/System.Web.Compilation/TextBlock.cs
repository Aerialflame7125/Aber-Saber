namespace System.Web.Compilation;

internal sealed class TextBlock
{
	public string Content;

	public readonly TextBlockType Type;

	public readonly int Length;

	public TextBlock(TextBlockType type, string content)
	{
		Content = content;
		Type = type;
		Length = content.Length;
	}

	public override string ToString()
	{
		return string.Concat(GetType().FullName, " [", Type, "]");
	}
}
