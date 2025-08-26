namespace System.Web.Security;

internal interface IMembershipHelper
{
	int UserIsOnlineTimeWindow { get; }

	MembershipProviderCollection Providers { get; }

	byte[] DecryptPassword(byte[] encodedPassword);

	byte[] EncryptPassword(byte[] password);
}
