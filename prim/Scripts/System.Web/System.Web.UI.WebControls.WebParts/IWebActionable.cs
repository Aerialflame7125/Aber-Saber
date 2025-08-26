namespace System.Web.UI.WebControls.WebParts;

/// <summary>Enables <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls or other server controls to contain collections of verbs.</summary>
public interface IWebActionable
{
	/// <summary>Gets a reference to a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects.</returns>
	WebPartVerbCollection Verbs { get; }
}
