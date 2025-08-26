using System.ComponentModel;
using System.Drawing;

namespace System.Web.UI.WebControls;

/// <summary>Reserves a location on a Web page in which to display localized static text. </summary>
[Designer("System.Web.UI.Design.WebControls.LocalizeDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
[ToolboxBitmap("")]
public class Localize : Literal, ITextControl
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Localize" /> class.</summary>
	public Localize()
	{
	}
}
