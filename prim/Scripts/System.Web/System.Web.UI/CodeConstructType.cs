namespace System.Web.UI;

/// <summary>Specifies the code constructs that can be parsed in the <see cref="M:System.Web.UI.PageParserFilter.ProcessCodeConstruct(System.Web.UI.CodeConstructType,System.String)" /> method of the <see cref="T:System.Web.UI.PageParserFilter" /> class.</summary>
public enum CodeConstructType
{
	/// <summary>An expression in <see langword="&lt;% ..." /> <see langword="%&gt;" /> tags.</summary>
	CodeSnippet,
	/// <summary>An expression in <see langword="&lt;%# ... %&gt;" /> tags.</summary>
	ExpressionSnippet,
	/// <summary>An expression in <see langword="&lt;%= ... %&gt;" /> tags.</summary>
	DataBindingSnippet,
	/// <summary>An expression in a <see langword="script" /> element that contains the <see langword="runat=&quot;server&quot;" /> attribute.</summary>
	ScriptTag
}
