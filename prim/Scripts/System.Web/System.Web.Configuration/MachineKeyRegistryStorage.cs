using Microsoft.Win32;

namespace System.Web.Configuration;

internal class MachineKeyRegistryStorage
{
	public enum KeyType
	{
		Validation,
		Encryption
	}

	private static string keyEncryption;

	private static string keyValidation;

	static MachineKeyRegistryStorage()
	{
		string applicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName;
		if (applicationName != null)
		{
			string text = applicationName.GetHashCode().ToString("x");
			keyEncryption = "software\\mono\\asp.net\\" + Environment.Version.ToString() + "\\autogenkeys\\" + text + "-" + 1;
			keyValidation = "software\\mono\\asp.net\\" + Environment.Version.ToString() + "\\autogenkeys\\" + text + "-" + 0;
		}
	}

	public static byte[] Retrieve(KeyType kt)
	{
		string text = null;
		text = kt switch
		{
			KeyType.Validation => keyValidation, 
			KeyType.Encryption => keyEncryption, 
			_ => throw new ArgumentException("Unknown key type."), 
		};
		if (text == null)
		{
			return null;
		}
		object obj = null;
		try
		{
			obj = OpenRegistryKey(text, write: false).GetValue("AutoGenKey", null);
		}
		catch (Exception)
		{
			return null;
		}
		if (obj == null || obj.GetType() != typeof(byte[]))
		{
			return null;
		}
		return (byte[])obj;
	}

	private static RegistryKey OpenRegistryKey(string path, bool write)
	{
		string[] array = path.Split('\\');
		int num = array.Length;
		RegistryKey registryKey = Registry.CurrentUser;
		for (int i = 0; i < num; i++)
		{
			RegistryKey registryKey2 = registryKey.OpenSubKey(array[i], writable: true);
			if (registryKey2 == null)
			{
				if (!write)
				{
					return null;
				}
				registryKey2 = registryKey.CreateSubKey(array[i]);
			}
			registryKey = registryKey2;
		}
		return registryKey;
	}

	public static void Store(byte[] buf, KeyType kt)
	{
		if (buf == null)
		{
			return;
		}
		string text = null;
		text = kt switch
		{
			KeyType.Validation => keyValidation, 
			KeyType.Encryption => keyEncryption, 
			_ => throw new ArgumentException("Unknown key type."), 
		};
		if (text == null)
		{
			return;
		}
		try
		{
			using RegistryKey registryKey = OpenRegistryKey(text, write: true);
			registryKey.SetValue("AutoGenKey", buf, RegistryValueKind.Binary);
			registryKey.SetValue("AutoGenKeyCreationTime", DateTime.Now.Ticks, RegistryValueKind.QWord);
			registryKey.SetValue("AutoGenKeyFormat", 2, RegistryValueKind.DWord);
			registryKey.Flush();
		}
		catch (Exception arg)
		{
			Console.Error.WriteLine("(info) Auto generated encryption keys not saved: {0}", arg);
		}
	}
}
