namespace System.Web;

[Flags]
internal enum CookieFlags : byte
{
	Secure = 1,
	HttpOnly = 2
}
