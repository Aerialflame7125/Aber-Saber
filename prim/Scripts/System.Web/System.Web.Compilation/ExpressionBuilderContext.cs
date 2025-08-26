using System.Web.UI;

namespace System.Web.Compilation;

/// <summary>Provides the context for an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object.</summary>
public sealed class ExpressionBuilderContext
{
	private TemplateControl tcontrol;

	private string vpath;

	/// <summary>Provides an <see cref="T:System.Web.Compilation.ExpressionBuilder" /> object with a reference to a <see cref="T:System.Web.UI.TemplateControl" /> object.</summary>
	/// <returns>The <see cref="T:System.Web.UI.TemplateControl" /> that contains this expression.</returns>
	public TemplateControl TemplateControl => tcontrol;

	/// <summary>Returns a virtual path to the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> object.</summary>
	/// <returns>The virtual path of the file associated with the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" />.</returns>
	public string VirtualPath => vpath;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified virtual path.</summary>
	/// <param name="virtualPath">The virtual path of the file associated with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
	public ExpressionBuilderContext(string virtualPath)
	{
		vpath = virtualPath;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ExpressionBuilderContext" /> class using the specified template control.</summary>
	/// <param name="templateControl">The <see cref="T:System.Web.UI.TemplateControl" /> to use with the specified <see cref="T:System.Web.Compilation.ExpressionBuilder" />.</param>
	public ExpressionBuilderContext(TemplateControl templateControl)
	{
		tcontrol = templateControl;
	}
}
