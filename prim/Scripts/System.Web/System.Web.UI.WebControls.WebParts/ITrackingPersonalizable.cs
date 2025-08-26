namespace System.Web.UI.WebControls.WebParts;

/// <summary>Allows Web Parts controls to track the specific phases of the personalization load and save process.</summary>
public interface ITrackingPersonalizable
{
	/// <summary>Indicates whether the control tracks the status of its changes.</summary>
	/// <returns>
	///     <see langword="true" /> if the Web Parts control is responsible for determining when the control is considered changed ("dirty"); otherwise, <see langword="false" />.</returns>
	bool TracksChanges { get; }

	/// <summary>Represents the beginning of the load phase for personalization information. </summary>
	void BeginLoad();

	/// <summary>Represents the phase prior to extracting personalization data from a control. </summary>
	void BeginSave();

	/// <summary>Represents the phase after personalization data has been applied to a control. </summary>
	void EndLoad();

	/// <summary>Represents the phase after personalization data has been extracted from a control. </summary>
	void EndSave();
}
