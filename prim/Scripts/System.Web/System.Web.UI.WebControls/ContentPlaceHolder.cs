using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Defines a region for content in an ASP.NET master page.</summary>
[ToolboxItemFilter("Microsoft.VisualStudio.Web.WebForms.MasterPageWebFormDesigner", ToolboxItemFilterType.Require)]
[ToolboxItemFilter("System.Web.UI", ToolboxItemFilterType.Allow)]
[Designer("System.Web.UI.Design.WebControls.ContentPlaceHolderDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxData("<;{0}:ContentPlaceHolder runat=&quot;server&quot;></{0}:ContentPlaceHolder>")]
[ControlBuilder(typeof(ContentPlaceHolderBuilder))]
public class ContentPlaceHolder : Control, INamingContainer, INonBindingContainer
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ContentPlaceHolder" /> class. </summary>
	public ContentPlaceHolder()
	{
	}
}
