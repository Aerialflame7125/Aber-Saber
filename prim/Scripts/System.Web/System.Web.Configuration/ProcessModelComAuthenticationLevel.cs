namespace System.Web.Configuration;

/// <summary>Specifies the level of authentication for DCOM security.</summary>
public enum ProcessModelComAuthenticationLevel
{
	/// <summary>Specifies no authentication. This field is constant. </summary>
	None,
	/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
	Call,
	/// <summary>Specifies that DCOM authenticates the credentials of the client. This field is constant.</summary>
	Connect,
	/// <summary>Specifies that DCOM determines the authentication level. This field is constant. </summary>
	Default,
	/// <summary>Specifies that DCOM verifies that all data received is from the expected client. This field is constant. </summary>
	Pkt,
	/// <summary>Specifies that DCOM authenticates and verifies the data transferred. This field is constant. </summary>
	PktIntegrity,
	/// <summary>Specifies that DCOM authenticates all previous levels and does encryption. This field is constant. </summary>
	PktPrivacy
}
