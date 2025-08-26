namespace System.Web.UI;

/// <summary>Defines the metadata attribute that enables an embedded resource in an assembly. This class cannot be inherited. </summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class WebResourceAttribute : Attribute
{
	private bool performSubstitution;

	private string webResource;

	private string contentType;

	/// <summary>Gets a string containing the MIME type of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
	/// <returns>The content type of the resource.</returns>
	public string ContentType => contentType;

	/// <summary>Gets or sets a Boolean value that determines whether, during processing of the embedded resource referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class, other Web resource URLs are parsed and replaced with the full path to the resource.</summary>
	/// <returns>
	///     <see langword="true" /> if embedded resources are resolved during processing of the resource; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool PerformSubstitution
	{
		get
		{
			return performSubstitution;
		}
		set
		{
			performSubstitution = value;
		}
	}

	/// <summary>Gets a string containing the name of the resource that is referenced by the <see cref="T:System.Web.UI.WebResourceAttribute" /> class.</summary>
	/// <returns>The name of the resource.</returns>
	public string WebResource => webResource;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebResourceAttribute" /> class with the specified Web resource and resource content type.</summary>
	/// <param name="webResource">The name of the of Web resource.</param>
	/// <param name="contentType">The type of resource, such as "image/gif" or "text/javascript".</param>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="webResource" /> is <see langword="null" /> or an empty string ("").- or -
	///         <paramref name="contentType" /> is <see langword="null" /> or an empty string ("").</exception>
	public WebResourceAttribute(string webResource, string contentType)
	{
		this.webResource = webResource;
		this.contentType = contentType;
	}
}
