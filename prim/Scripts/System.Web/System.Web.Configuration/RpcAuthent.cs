namespace System.Web.Configuration;

internal enum RpcAuthent
{
	None = 0,
	DcePrivate = 1,
	DcePublic = 2,
	DecPublic = 4,
	GssNegotiate = 9,
	WinNT = 10,
	GssSchannel = 14,
	GssKerberos = 16,
	DPA = 17,
	MSN = 18,
	Digest = 21,
	MQ = 100,
	Default = -1
}
