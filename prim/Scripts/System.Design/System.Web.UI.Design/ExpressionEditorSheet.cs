using System.ComponentModel;

namespace System.Web.UI.Design;

/// <summary>Represents a design-time editor sheet for a custom expression. This class must be inherited.</summary>
public abstract class ExpressionEditorSheet
{
	private IServiceProvider serviceProvider;

	/// <summary>Gets a value that indicates whether the expression string is valid.</summary>
	/// <returns>
	///   <see langword="true" />, if the expression string is valid; otherwise <see langword="false" />.</returns>
	[Browsable(false)]
	public virtual bool IsValid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the service provider implementation that is used by the expression editor sheet.</summary>
	/// <returns>An <see cref="T:System.IServiceProvider" />, typically provided by the design host, that can be used to obtain additional design-time services.</returns>
	[Browsable(false)]
	public IServiceProvider ServiceProvider => serviceProvider;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ExpressionEditorSheet" /> class.</summary>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	protected ExpressionEditorSheet(IServiceProvider serviceProvider)
	{
		this.serviceProvider = serviceProvider;
	}

	/// <summary>When overridden in a derived class, returns the expression string that is formed by the expression editor sheet property values.</summary>
	/// <returns>The custom expression string for the current property values.</returns>
	public abstract string GetExpression();
}
