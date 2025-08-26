namespace System.Web;

/// <summary>Represents a method that is called to validate a cached item before the item is served from the cache. </summary>
/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object containing information about the current request. </param>
/// <param name="data">User-supplied data used to validate the cached item. </param>
/// <param name="validationStatus">An <see cref="T:System.Web.HttpValidationStatus" /> enumeration value. Your delegate should set this value to indicate the result of the validation. </param>
public delegate void HttpCacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus);
