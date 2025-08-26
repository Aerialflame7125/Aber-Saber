namespace System.Web.UI;

/// <summary>Specifies whether view-state information is encrypted.</summary>
public enum ViewStateEncryptionMode
{
	/// <summary>The view-state information is encrypted if a control requests encryption by calling the <see cref="M:System.Web.UI.Page.RegisterRequiresViewStateEncryption" /> method. This is the default.</summary>
	Auto,
	/// <summary>The view-state information is always encrypted.</summary>
	Always,
	/// <summary>The view-state information is never encrypted, even if a control requests it.</summary>
	Never
}
