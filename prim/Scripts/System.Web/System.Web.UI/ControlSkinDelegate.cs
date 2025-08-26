using System.ComponentModel;

namespace System.Web.UI;

/// <summary>Represents the method that applies the correct control skin to the specified control.</summary>
/// <param name="control">The <see cref="T:System.Web.UI.Control" /> to which to apply the theme skin.</param>
/// <returns>The <see cref="T:System.Web.UI.Control" /> that was passed to the method, with a control skin applied.</returns>
[EditorBrowsable(EditorBrowsableState.Advanced)]
public delegate Control ControlSkinDelegate(Control control);
