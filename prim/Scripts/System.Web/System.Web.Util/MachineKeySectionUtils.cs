using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Util;

internal static class MachineKeySectionUtils
{
	private static byte ToHexValue(char c, bool high)
	{
		byte b;
		if (c >= '0' && c <= '9')
		{
			b = (byte)(c - 48);
		}
		else if (c >= 'a' && c <= 'f')
		{
			b = (byte)(c - 97 + 10);
		}
		else
		{
			if (c < 'A' || c > 'F')
			{
				throw new ArgumentException("Invalid hex character");
			}
			b = (byte)(c - 65 + 10);
		}
		if (high)
		{
			b <<= 4;
		}
		return b;
	}

	internal static byte[] GetBytes(string key, int len)
	{
		byte[] array = new byte[len / 2];
		for (int i = 0; i < len; i += 2)
		{
			array[i / 2] = (byte)(ToHexValue(key[i], high: true) + ToHexValue(key[i + 1], high: false));
		}
		return array;
	}

	public static string GetHexString(byte[] bytes)
	{
		StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
		int num = 55;
		foreach (byte num2 in bytes)
		{
			int num3 = num2 & 0xF;
			int num4 = (num2 >> 4) & 0xF;
			stringBuilder.Append((char)((num4 > 9) ? (num + num4) : (48 + num4)));
			stringBuilder.Append((char)((num3 > 9) ? (num + num3) : (48 + num3)));
		}
		return stringBuilder.ToString();
	}

	public static SymmetricAlgorithm GetDecryptionAlgorithm(string name)
	{
		SymmetricAlgorithm symmetricAlgorithm = null;
		switch (name)
		{
		case "AES":
		case "Auto":
			return Rijndael.Create();
		case "DES":
			return DES.Create();
		case "3DES":
			return TripleDES.Create();
		default:
			if (name.StartsWith("alg:"))
			{
				return SymmetricAlgorithm.Create(name.Substring(4));
			}
			throw new ConfigurationErrorsException();
		}
	}

	public static KeyedHashAlgorithm GetValidationAlgorithm(MachineKeySection section)
	{
		KeyedHashAlgorithm result = null;
		switch (section.Validation)
		{
		case MachineKeyValidation.MD5:
			result = new HMACMD5();
			break;
		case MachineKeyValidation.SHA1:
		case MachineKeyValidation.TripleDES:
		case MachineKeyValidation.AES:
			result = new HMACSHA1();
			break;
		case MachineKeyValidation.HMACSHA256:
			result = new HMACSHA256();
			break;
		case MachineKeyValidation.HMACSHA384:
			result = new HMACSHA384();
			break;
		case MachineKeyValidation.HMACSHA512:
			result = new HMACSHA512();
			break;
		case MachineKeyValidation.Custom:
		{
			string validationAlgorithm = section.ValidationAlgorithm;
			if (validationAlgorithm.StartsWith("alg:"))
			{
				result = KeyedHashAlgorithm.Create(validationAlgorithm.Substring(4));
			}
			break;
		}
		}
		return result;
	}

	private static SymmetricAlgorithm GetDecryptionAlgorithm(MachineKeySection section)
	{
		return section.GetDecryptionAlgorithm();
	}

	private static byte[] GetDecryptionKey(MachineKeySection section)
	{
		return section.GetDecryptionKey();
	}

	public static byte[] GetValidationKey(MachineKeySection section)
	{
		return section.GetValidationKey();
	}

	public static byte[] Decrypt(MachineKeySection section, byte[] encodedData)
	{
		return Decrypt(section, encodedData, 0, encodedData.Length);
	}

	private static byte[] Decrypt(MachineKeySection section, byte[] encodedData, int offset, int length)
	{
		using SymmetricAlgorithm symmetricAlgorithm = GetDecryptionAlgorithm(section);
		symmetricAlgorithm.Key = GetDecryptionKey(section);
		return Decrypt(symmetricAlgorithm, encodedData, offset, length);
	}

	public static byte[] Decrypt(SymmetricAlgorithm alg, byte[] encodedData, int offset, int length)
	{
		byte[] array = new byte[alg.IV.Length];
		Array.Copy(encodedData, 0, array, 0, array.Length);
		using ICryptoTransform cryptoTransform = alg.CreateDecryptor(alg.Key, array);
		try
		{
			return cryptoTransform.TransformFinalBlock(encodedData, array.Length + offset, length - array.Length);
		}
		catch (CryptographicException)
		{
			return null;
		}
	}

	public static byte[] Encrypt(MachineKeySection section, byte[] data)
	{
		using SymmetricAlgorithm symmetricAlgorithm = GetDecryptionAlgorithm(section);
		symmetricAlgorithm.Key = GetDecryptionKey(section);
		return Encrypt(symmetricAlgorithm, data);
	}

	public static byte[] Encrypt(SymmetricAlgorithm alg, byte[] data)
	{
		byte[] iV = alg.IV;
		using ICryptoTransform cryptoTransform = alg.CreateEncryptor(alg.Key, iV);
		byte[] array = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
		byte[] array2 = new byte[iV.Length + array.Length];
		Array.Copy(iV, 0, array2, 0, iV.Length);
		Array.Copy(array, 0, array2, iV.Length, array.Length);
		return array2;
	}

	public static byte[] Sign(MachineKeySection section, byte[] data)
	{
		return Sign(section, data, 0, data.Length);
	}

	private static byte[] Sign(MachineKeySection section, byte[] data, int offset, int length)
	{
		using KeyedHashAlgorithm keyedHashAlgorithm = GetValidationAlgorithm(section);
		keyedHashAlgorithm.Key = GetValidationKey(section);
		byte[] array = keyedHashAlgorithm.ComputeHash(data, offset, length);
		byte[] array2 = new byte[length + array.Length];
		Array.Copy(data, array2, length);
		Array.Copy(array, 0, array2, length, array.Length);
		return array2;
	}

	public static byte[] Verify(MachineKeySection section, byte[] data)
	{
		byte[] array = null;
		bool flag = true;
		using (KeyedHashAlgorithm keyedHashAlgorithm = GetValidationAlgorithm(section))
		{
			keyedHashAlgorithm.Key = GetValidationKey(section);
			int num = keyedHashAlgorithm.HashSize >> 3;
			byte[] array2 = Sign(section, data, 0, data.Length - num);
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != data[data.Length - array2.Length + i])
				{
					flag = false;
				}
			}
			array = new byte[data.Length - num];
			Array.Copy(data, 0, array, 0, array.Length);
		}
		if (!flag)
		{
			return null;
		}
		return array;
	}

	public static byte[] EncryptSign(MachineKeySection section, byte[] data)
	{
		byte[] data2 = Encrypt(section, data);
		return Sign(section, data2);
	}

	public static byte[] VerifyDecrypt(MachineKeySection section, byte[] block)
	{
		bool flag = true;
		int num;
		using (KeyedHashAlgorithm keyedHashAlgorithm = GetValidationAlgorithm(section))
		{
			keyedHashAlgorithm.Key = GetValidationKey(section);
			num = keyedHashAlgorithm.HashSize >> 3;
			byte[] array = Sign(section, block, 0, block.Length - num);
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != block[block.Length - array.Length + i])
				{
					flag = false;
				}
			}
		}
		try
		{
			byte[] array2 = Decrypt(section, block, 0, block.Length - num);
			return flag ? array2 : null;
		}
		catch
		{
			return null;
		}
	}
}
