namespace System.Web.UI;

/// <summary>Represents the method that renders the specified <see cref="T:System.Web.UI.Control" /> container to the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
/// <param name="output">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to render content to.</param>
/// <param name="container">The <see cref="T:System.Web.UI.Control" /> to render.</param>
public delegate void RenderMethod(HtmlTextWriter output, Control container);
