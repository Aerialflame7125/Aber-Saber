namespace System.Web.UI.WebControls;

/// <summary>Specifies whether the <see cref="T:System.Web.UI.WebControls.Menu" /> control renders HTML <see langword="table" /> elements and inline styles, or <see langword="listitem" /> elements and cascading style sheet (CSS) styles.</summary>
public enum MenuRenderingMode
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup in the way it does by default for the version of ASP.NET indicated by the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property of the control. If the value of the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property is 3.5, this setting is equivalent to <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.Table" />. If the value of the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property is 4.0 or greater, this setting is equivalent to <see cref="F:System.Web.UI.WebControls.MenuRenderingMode.List" />.</summary>
	Default,
	/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup by using <see langword="table" /> elements and inline styles. </summary>
	Table,
	/// <summary>The <see cref="T:System.Web.UI.WebControls.Menu" /> control renders markup by using list item (<see langword="li" />) elements and CSS styles. </summary>
	List
}
