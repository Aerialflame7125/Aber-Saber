using System.Collections;

namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines additional management capabilities for the application and extraction of personalization state. </summary>
public interface IPersonalizable
{
	/// <summary>Gets a value that indicates whether the custom data that a control manages has changed. </summary>
	/// <returns>
	///     <see langword="true" /> if the custom data managed with the <see cref="T:System.Web.UI.WebControls.WebParts.IPersonalizable" /> interface has changed; otherwise, <see langword="false" />.</returns>
	bool IsDirty { get; }

	void Load(IDictionary sharedState, IDictionary userState);

	void Save(IDictionary state);
}
