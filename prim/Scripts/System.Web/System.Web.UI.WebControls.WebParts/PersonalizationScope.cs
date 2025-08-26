namespace System.Web.UI.WebControls.WebParts;

/// <summary>Indicates the personalization scope for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> object or the personalization scope that applies to a property on a Web Parts control.</summary>
public enum PersonalizationScope
{
	/// <summary>When referring to the scope on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />control, <see langword="User" /> scope means that personalization data that is user-specific, as well as personalization data that applies to all users, is loaded for all personalizable controls on a page. Only personalization data that is user-specific can be saved on the page. </summary>
	User,
	/// <summary>When referring to the scope on the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control, <see langword="Shared" /> scope means that personalization data applies to all users for all personalizable controls on a page and is also available to be saved on the page. </summary>
	Shared
}
