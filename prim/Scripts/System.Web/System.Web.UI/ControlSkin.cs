using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Represents a control skin, which is a means to define stylistic properties that are applied to an ASP.NET Web server control. </summary>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public class ControlSkin
{
	private Type controlType;

	private ControlSkinDelegate themeDelegate;

	/// <summary>Gets the <see cref="T:System.Type" /> of the control that the <see cref="T:System.Web.UI.ControlSkin" /> object is associated with.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the <see cref="T:System.Web.UI.Control" /> used in this instance.</returns>
	public Type ControlType => controlType;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.UI.ControlSkin" /> class using the passed <see cref="T:System.Web.UI.Control" /> type and delegate.</summary>
	/// <param name="controlType">The <see cref="T:System.Type" /> of <see cref="T:System.Web.UI.Control" /> to which the skin is applied, used to enforce type consistency among named skins. </param>
	/// <param name="themeDelegate">The <see cref="T:System.Web.UI.ControlSkinDelegate" /> that applies the style elements defined in a control skin file to the type identified by the <paramref name="controlType" /> parameter. </param>
	public ControlSkin(Type controlType, ControlSkinDelegate themeDelegate)
	{
		this.controlType = controlType;
		this.themeDelegate = themeDelegate;
	}

	/// <summary>Applies the skin to the <see cref="T:System.Web.UI.Control" /> control contained by the <see cref="T:System.Web.UI.ControlSkin" /> object.</summary>
	/// <param name="control">The control to which to apply the skin. </param>
	public void ApplySkin(Control control)
	{
		themeDelegate(control);
	}
}
