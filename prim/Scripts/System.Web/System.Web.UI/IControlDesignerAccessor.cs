using System.Collections;

namespace System.Web.UI;

/// <summary>Provides access to a control designer to store temporary design-time data associated with a control.</summary>
public interface IControlDesignerAccessor
{
	/// <summary>When implemented, gets a collection of information that can be accessed by a control designer.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing information about the control.</returns>
	IDictionary UserData { get; }

	/// <summary>When implemented, gets the state from the control during use on the design surface.</summary>
	/// <returns>An <see cref="T:System.Collections.IDictionary" /> of the control state.</returns>
	IDictionary GetDesignModeState();

	/// <summary>When implemented, sets control state before rendering it on the design surface.</summary>
	/// <param name="data">The <see cref="T:System.Collections.IDictionary" /> containing the control state.</param>
	void SetDesignModeState(IDictionary data);

	/// <summary>When implemented, specifies the control that acts as the owner to the control implementing this method.</summary>
	/// <param name="owner">The control to act as owner.</param>
	void SetOwnerControl(Control owner);
}
