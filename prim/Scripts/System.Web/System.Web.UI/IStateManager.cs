namespace System.Web.UI;

/// <summary>Defines the properties and methods any class must implement to support view state management for a server control.</summary>
public interface IStateManager
{
	/// <summary>When implemented by a class, gets a value indicating whether a server control is tracking its view state changes.</summary>
	/// <returns>
	///     <see langword="true" /> if a server control is tracking its view state changes; otherwise, <see langword="false" />.</returns>
	bool IsTrackingViewState { get; }

	/// <summary>When implemented by a class, loads the server control's previously saved view state to the control.</summary>
	/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
	void LoadViewState(object state);

	/// <summary>When implemented by a class, saves the changes to a server control's view state to an <see cref="T:System.Object" />.</summary>
	/// <returns>The <see cref="T:System.Object" /> that contains the view state changes.</returns>
	object SaveViewState();

	/// <summary>When implemented by a class, instructs the server control to track changes to its view state.</summary>
	void TrackViewState();
}
