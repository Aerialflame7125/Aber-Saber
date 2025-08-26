namespace System.Web.UI;

/// <summary>Allows the control serializer to get to the builder for a control.</summary>
public interface IControlBuilderAccessor
{
	/// <summary>Gets the control builder for this control.</summary>
	/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, <see langword="null" /> if no builder was used.</returns>
	ControlBuilder ControlBuilder { get; }
}
