namespace System.Windows.Forms;

/// <summary>The site for a <see cref="T:System.Windows.Forms.Design.ComponentEditorPage" />.</summary>
/// <filterpriority>2</filterpriority>
public interface IComponentEditorPageSite
{
	/// <summary>Returns the parent control for the page window.</summary>
	/// <returns>The parent control for the page window.</returns>
	/// <filterpriority>1</filterpriority>
	Control GetControl();

	/// <summary>Notifies the site that the editor is in a modified state.</summary>
	/// <filterpriority>1</filterpriority>
	void SetDirty();
}
