using System.Windows.Forms.Layout;

namespace System.Windows.Forms;

/// <summary>Provides a base class for collecting layout scheme characteristics.</summary>
/// <filterpriority>2</filterpriority>
public abstract class LayoutSettings
{
	/// <summary>Gets the current table layout engine.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Layout.LayoutEngine" /> currently being used.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual LayoutEngine LayoutEngine => null;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LayoutSettings" /> class. </summary>
	protected LayoutSettings()
	{
	}
}
