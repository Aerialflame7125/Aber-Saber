namespace System.Web.Security.Cryptography;

internal interface ICryptoService
{
	byte[] Protect(byte[] clearData);

	byte[] Unprotect(byte[] protectedData);
}
