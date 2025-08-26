namespace System.Web.Security;

/// <summary>Defines the delegate for the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event of the <see cref="T:System.Web.Security.RoleManagerModule" /> class.</summary>
/// <param name="sender">The <see cref="T:System.Web.Security.RoleManagerModule" /> that raised the <see cref="E:System.Web.Security.RoleManagerModule.GetRoles" /> event.</param>
/// <param name="e">A <see cref="T:System.Web.Security.RoleManagerEventArgs" /> object that contains the event data.</param>
public delegate void RoleManagerEventHandler(object sender, RoleManagerEventArgs e);
