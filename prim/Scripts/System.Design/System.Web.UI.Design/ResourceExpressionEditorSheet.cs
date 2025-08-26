using System.ComponentModel;

namespace System.Web.UI.Design;

/// <summary>Represents a design-time editor sheet for the properties of a resource expression in the UI of a designer host at design time.</summary>
public class ResourceExpressionEditorSheet : ExpressionEditorSheet
{
	/// <summary>Gets or sets the key that matches the filename for the resource in the project's global resource folder.</summary>
	/// <returns>The key for a resource file in the global resource folder.</returns>
	[System.MonoTODO]
	[DefaultValue("")]
	public string ClassKey
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value that indicates whether the resource expression string is valid.</summary>
	/// <returns>
	///   <see langword="true" /> if the resource expression string is valid; otherwise <see langword="false" />.</returns>
	[System.MonoTODO]
	public override bool IsValid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the name of the resource, which is used as a key to find the resource value.</summary>
	/// <returns>The name of the resource.</returns>
	[System.MonoTODO]
	[DefaultValue("")]
	public string ResourceKey
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.ResourceExpressionEditorSheet" /> class.</summary>
	/// <param name="expression">A resource expression, used to initialize the expression editor sheet.</param>
	/// <param name="serviceProvider">A service provider implementation supplied by the designer host, used to obtain additional design-time services.</param>
	[System.MonoTODO]
	public ResourceExpressionEditorSheet(string expression, IServiceProvider serviceProvider)
		: base(serviceProvider)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a resource expression that is formed by the expression editor sheet property values.</summary>
	/// <returns>The resource expression string for the current settings in the sheet.</returns>
	[System.MonoTODO]
	public override string GetExpression()
	{
		throw new NotImplementedException();
	}
}
