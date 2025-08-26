namespace System.Web.UI.WebControls.WebParts;

/// <summary>Specifies whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported.</summary>
public enum WebPartExportMode
{
	/// <summary>None of a Web Parts control's properties can be exported. </summary>
	None,
	/// <summary>All of a Web Parts control's properties can be exported.</summary>
	All,
	/// <summary>Only properties of a Web Parts control that have been defined as non-sensitive can be exported.  </summary>
	NonSensitiveData
}
