namespace System.Web.Configuration;

/// <summary>Specifies the mode for asynchronous requests.</summary>
[Flags]
public enum AsyncPreloadModeFlags
{
	/// <summary>No asynchronous upload of the request entity body will occur.</summary>
	None = 0,
	/// <summary>Asynchronous uploads of the request entity body will occur only for form posts. This flag enables asynchronous preloading for MIME types that are specifically set to application/x-www-form-urlencoded.</summary>
	Form = 1,
	/// <summary>Asynchronous uploads of the request entity body will occur only for multi-part form data. This flag enables asynchronous preloading for MIME types that are specifically set to multipart/form-data.</summary>
	FormMultiPart = 2,
	/// <summary>Asynchronous uploads of the request entity body will occur only for non-form posts.</summary>
	NonForm = 4,
	/// <summary>Asynchronous uploads of the request entity body will occur only for form posts.</summary>
	AllFormTypes = 3,
	/// <summary>Asynchronous uploads of the request entity body will occur for all posts.</summary>
	All = 7
}
