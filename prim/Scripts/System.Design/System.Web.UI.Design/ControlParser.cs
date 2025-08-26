using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Provides methods for creating a Web server <see cref="T:System.Web.UI.Control" /> control or <see cref="T:System.Web.UI.ITemplate" /> interface from a string of markup that represents a persisted control or template.</summary>
public sealed class ControlParser
{
	private ControlParser()
	{
	}

	/// <summary>Creates a control from the specified markup using the specified designer host.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
	/// <param name="controlText">The HTML markup for the control.</param>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> that controlText represents; otherwise, <see langword="null" />, if the parser cannot build the control.</returns>
	/// <exception cref="T:System.ArgumentNullException">A parameter is not valid.</exception>
	[System.MonoTODO]
	public static Control ParseControl(IDesignerHost designerHost, string controlText)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a control from the specified markup using the specified designer host and directives.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
	/// <param name="controlText">The text of the HTML markup for the control.</param>
	/// <param name="directives">The page directives to include in the code for the control.</param>
	/// <returns>The <see cref="T:System.Web.UI.Control" /> that <paramref name="controlText" /> represents.</returns>
	/// <exception cref="T:System.ArgumentNullException">A parameter is not valid.</exception>
	[System.MonoTODO]
	public static Control ParseControl(IDesignerHost designerHost, string controlText, string directives)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an array of controls from the specified markup using the specified designer host.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
	/// <param name="controlText">A string that represents a collection of markup for controls.</param>
	/// <returns>An array of <see cref="T:System.Web.UI.Control" /> elements, parsed from <paramref name="controlText" />; otherwise, <see langword="null" />, if the parser cannot build the controls.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designerHost" /> is <see langword="null" />.  
	/// -or-
	///  <paramref name="controlText" /> is <see langword="null" /> or an empty string ("").</exception>
	[System.MonoTODO]
	public static Control[] ParseControls(IDesignerHost designerHost, string controlText)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates an <see cref="T:System.Web.UI.ITemplate" /> interface from the specified template markup.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
	/// <param name="templateText">A string containing the template markup.</param>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> instance created by parsing <paramref name="templateText" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designerHost" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText)
	{
		throw new NotImplementedException();
	}

	/// <summary>Parses the specified template markup and creates an <see cref="T:System.Web.UI.ITemplate" /> interface.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> instance that is the designer host for the page.</param>
	/// <param name="templateText">A string containing the template markup.</param>
	/// <param name="directives">Any directives to add to the beginning of the code for the template.</param>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> instance created by parsing <paramref name="templateText" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="designerHost" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public static ITemplate ParseTemplate(IDesignerHost designerHost, string templateText, string directives)
	{
		throw new NotImplementedException();
	}
}
