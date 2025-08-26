namespace System.Web.UI.WebControls.WebParts;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.AuthorizeWebPart" /> event. </summary>
public class WebPartAuthorizationEventArgs : EventArgs
{
	private bool authorized;

	private Type type;

	private string path;

	private string authorizationFilter;

	private bool isShared;

	/// <summary>Gets the <see cref="T:System.Type" /> of the Web Parts control being checked for authorization.</summary>
	/// <returns>The <see cref="T:System.Type" /> of the control being checked for authorization.</returns>
	public Type Type => type;

	/// <summary>Gets the relative application path to the source file for the control being authorized, if the control is a user control.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the relative application path.</returns>
	public string Path => path;

	/// <summary>Gets the string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page.</summary>
	/// <returns>A <see cref="T:System.String" /> used in determining whether a control is authorized to be added to a page.</returns>
	public string AuthorizationFilter => authorizationFilter;

	/// <summary>Gets a value that indicates whether a Web Parts control is visible to all users of a Web Parts page.</summary>
	/// <returns>
	///     <see langword="true" /> if the Web Parts control is visible to all users of the page; otherwise, <see langword="false" />.</returns>
	public bool IsShared => isShared;

	/// <summary>Gets or sets the value indicating whether a Web Parts control can be added to a page.</summary>
	/// <returns>
	///     <see langword="true" /> if the Web Parts control can be added to the page; otherwise, <see langword="false" />.</returns>
	public bool IsAuthorized
	{
		get
		{
			return authorized;
		}
		set
		{
			authorized = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartAuthorizationEventArgs" /> class. </summary>
	/// <param name="type">The <see cref="T:System.Type" /> of the control being checked for authorization. </param>
	/// <param name="path">The relative application path to the source file for the control being authorized, if the control is a user control. </param>
	/// <param name="authorizationFilter">An arbitrary string value assigned to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.AuthorizationFilter" /> property of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control, used for authorizing whether a control can be added to a page. </param>
	/// <param name="isShared">Indicates whether the control being checked for authorization is a shared control, meaning that it is visible to many or all users of the application, and its <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.IsShared" /> property value is set to <see langword="true" />. </param>
	public WebPartAuthorizationEventArgs(Type type, string path, string authorizationFilter, bool isShared)
	{
		this.type = type;
		this.path = path;
		this.authorizationFilter = authorizationFilter;
		this.isShared = isShared;
	}
}
