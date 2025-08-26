using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>A collection of <see cref="T:System.Web.UI.Design.TemplateDefinition" /> objects representing the template elements in a Web server control at design time.</summary>
public class TemplateGroup
{
	/// <summary>Gets the name of the group.</summary>
	/// <returns>The name of the group.</returns>
	[System.MonoNotSupported("")]
	public string GroupName
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the current style for the group.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.Style" /> for the group if set; otherwise, <see langword="null" />.</returns>
	[System.MonoNotSupported("")]
	public Style GroupStyle
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether there are any templates in the group.</summary>
	/// <returns>
	///   <see langword="true" /> if there are no templates in the group; otherwise <see langword="false" />.</returns>
	[System.MonoNotSupported("")]
	public bool IsEmpty
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets an array of all template definitions in the group.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.TemplateDefinition" /> objects.</returns>
	[System.MonoNotSupported("")]
	public TemplateDefinition[] Templates
	{
		[System.MonoNotSupported("")]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> class, using the provided name.</summary>
	/// <param name="groupName">The name of the group.</param>
	[System.MonoNotSupported("")]
	public TemplateGroup(string groupName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateGroup" /> class, using the provided name and style.</summary>
	/// <param name="groupName">The name of the group</param>
	/// <param name="groupStyle">A <see cref="T:System.Web.UI.WebControls.Style" /> object to be applied to templates in the group.</param>
	[System.MonoNotSupported("")]
	public TemplateGroup(string groupName, Style groupStyle)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds the provided <see cref="T:System.Web.UI.Design.TemplateDefinition" /> to the group.</summary>
	/// <param name="templateDefinition">A <see cref="T:System.Web.UI.Design.TemplateDefinition" />.</param>
	[System.MonoNotSupported("")]
	public void AddTemplateDefinition(TemplateDefinition templateDefinition)
	{
		throw new NotImplementedException();
	}
}
