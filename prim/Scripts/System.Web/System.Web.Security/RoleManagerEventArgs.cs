namespace System.Web.Security;

/// <summary>Provides event data for the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
public sealed class RoleManagerEventArgs : EventArgs
{
	private HttpContext _Context;

	private bool _RolesPopulated;

	/// <summary>Gets or sets a value indicating whether role information has been applied to the current user.</summary>
	/// <returns>
	///     <see langword="true" /> if role information has been applied to the current user; otherwise, <see langword="false" />.</returns>
	public bool RolesPopulated
	{
		get
		{
			return _RolesPopulated;
		}
		set
		{
			_RolesPopulated = value;
		}
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
	public HttpContext Context => _Context;

	/// <summary>Creates an instance of the <see cref="T:System.Web.Security.RoleManagerEventArgs" /> class and sets the <see cref="P:System.Web.Security.RoleManagerEventArgs.Context" /> property to the specified <see cref="T:System.Web.HttpContext" />.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
	public RoleManagerEventArgs(HttpContext context)
	{
		_Context = context;
	}
}
