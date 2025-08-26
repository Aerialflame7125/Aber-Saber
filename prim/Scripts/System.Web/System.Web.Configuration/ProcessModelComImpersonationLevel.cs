namespace System.Web.Configuration;

/// <summary>Specifies the authentication level for COM security.</summary>
public enum ProcessModelComImpersonationLevel
{
	/// <summary>Specifies that DCOM determines the impersonation level. This field is constant. </summary>
	Default,
	/// <summary>Specifies that the client is anonymous to the server. This field is constant. </summary>
	Anonymous,
	/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
	Delegate,
	/// <summary>Specifies that the server can obtain the client's identity. This field is constant. </summary>
	Identify,
	/// <summary>Specifies that the server process can impersonate the client's security context while acting on behalf of the client. This field is constant. </summary>
	Impersonate
}
