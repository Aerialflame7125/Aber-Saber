namespace System.Web.UI.WebControls;

/// <summary>Defines a method that enables controls to obtain client-side script options.</summary>
public interface IPostBackContainer
{
	/// <summary>Returns the options required for a postback script for a specified button control.</summary>
	/// <param name="buttonControl">The control generating the postback event.</param>
	/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> object containing the options required to generate a postback script for <paramref name="buttonControl" />.</returns>
	PostBackOptions GetPostBackOptions(IButtonControl buttonControl);
}
