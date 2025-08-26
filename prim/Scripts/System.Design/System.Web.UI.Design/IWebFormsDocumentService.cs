namespace System.Web.UI.Design;

/// <summary>Provides methods to access services for tracking the loading state of a Web Forms document, handling events at load time, accessing a document's location, managing a document's undo service, and setting a new selection within the document.</summary>
[Obsolete("Use new WebFormsReferenceManager feature")]
public interface IWebFormsDocumentService
{
	/// <summary>Gets the URL at which the document is located.</summary>
	/// <returns>The URL at which the document is located, or <see langword="null" /> if the document has no associated URL.</returns>
	string DocumentUrl { get; }

	/// <summary>Gets a value indicating whether the document service is currently loading.</summary>
	/// <returns>
	///   <see langword="true" /> if the document service is loading; otherwise, <see langword="false" />.</returns>
	bool IsLoading { get; }

	/// <summary>Occurs when the service has finished loading.</summary>
	event EventHandler LoadComplete;

	/// <summary>Creates a discardable undo unit.</summary>
	/// <returns>The new discardable undo unit.</returns>
	object CreateDiscardableUndoUnit();

	/// <summary>Discards the specified undo unit.</summary>
	/// <param name="discardableUndoUnit">The undo unit to discard.</param>
	void DiscardUndoUnit(object discardableUndoUnit);

	/// <summary>Enables the ability to undo actions that occur within undoable action units or transactions.</summary>
	/// <param name="enable">
	///   <see langword="true" /> if actions should be undoable; otherwise, <see langword="false" />.</param>
	void EnableUndo(bool enable);

	/// <summary>When implemented in a derived class, updates the current selection.</summary>
	void UpdateSelection();
}
