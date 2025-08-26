namespace System.Web.UI;

/// <summary>Serves as the base class for all property entries that require a control builder.</summary>
public abstract class BuilderPropertyEntry : PropertyEntry
{
	/// <summary>Gets or sets the control builder for the property entry.</summary>
	/// <returns>A <see cref="T:System.Web.UI.ControlBuilder" /> for this property entry.</returns>
	public ControlBuilder Builder { get; set; }
}
