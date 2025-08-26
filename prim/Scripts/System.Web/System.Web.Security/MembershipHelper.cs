using System.Configuration.Provider;
using System.Security.Cryptography;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security;

internal sealed class MembershipHelper : IMembershipHelper
{
	internal const int SALT_BYTES = 16;

	public int UserIsOnlineTimeWindow => Membership.UserIsOnlineTimeWindow;

	public MembershipProviderCollection Providers => Membership.Providers;

	private static SymmetricAlgorithm GetAlgorithm()
	{
		MachineKeySection config = MachineKeySection.Config;
		if (config.DecryptionKey.StartsWith("AutoGenerate"))
		{
			throw new ProviderException("You must explicitly specify a decryption key in the <machineKey> section when using encrypted passwords.");
		}
		SymmetricAlgorithm obj = config.GetDecryptionAlgorithm() ?? throw new ProviderException($"Unsupported decryption attribute '{config.Decryption}' in <machineKey> configuration section");
		obj.Key = config.GetDecryptionKey();
		return obj;
	}

	public byte[] DecryptPassword(byte[] encodedPassword)
	{
		using SymmetricAlgorithm alg = GetAlgorithm();
		return MachineKeySectionUtils.Decrypt(alg, encodedPassword, 0, encodedPassword.Length);
	}

	public byte[] EncryptPassword(byte[] password)
	{
		using SymmetricAlgorithm alg = GetAlgorithm();
		return MachineKeySectionUtils.Encrypt(alg, password);
	}
}
