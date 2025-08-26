using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Stores dynamically added server controls on the Web page.</summary>
[ControlBuilder(typeof(PlaceHolderControlBuilder))]
public class PlaceHolder : Control
{
	/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
	/// <returns>
	///     <see langword="true" /> to use themes; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[Browsable(true)]
	public override bool EnableTheming { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PlaceHolder" /> class. </summary>
	public PlaceHolder()
	{
	}
}
