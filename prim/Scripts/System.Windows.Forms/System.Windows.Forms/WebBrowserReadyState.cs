namespace System.Windows.Forms;

/// <summary>Specifies constants that define the state of the <see cref="T:System.Windows.Forms.WebBrowser" /> control.</summary>
/// <filterpriority>2</filterpriority>
public enum WebBrowserReadyState
{
	/// <summary>No document is currently loaded.</summary>
	Uninitialized,
	/// <summary>The control is loading a new document.</summary>
	Loading,
	/// <summary>The control has loaded and initialized the new document, but has not yet received all the document data.</summary>
	Loaded,
	/// <summary>The control has loaded enough of the document to allow limited user interaction, such as clicking hyperlinks that have been displayed.</summary>
	Interactive,
	/// <summary>The control has finished loading the new document and all its contents.</summary>
	Complete
}
