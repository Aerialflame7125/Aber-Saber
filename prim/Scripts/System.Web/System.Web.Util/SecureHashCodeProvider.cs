using System.Collections;
using System.Globalization;

namespace System.Web.Util;

internal class SecureHashCodeProvider : IHashCodeProvider
{
	private static readonly SecureHashCodeProvider singletonInvariant;

	private static SecureHashCodeProvider singleton;

	private static readonly object sync;

	private static readonly int seed;

	private TextInfo m_text;

	public static SecureHashCodeProvider Default
	{
		get
		{
			lock (sync)
			{
				if (singleton == null)
				{
					singleton = new SecureHashCodeProvider();
				}
				else if (singleton.m_text == null)
				{
					if (!AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
					{
						singleton = new SecureHashCodeProvider();
					}
				}
				else if (!AreEqual(singleton.m_text, CultureInfo.CurrentCulture))
				{
					singleton = new SecureHashCodeProvider();
				}
				return singleton;
			}
		}
	}

	public static SecureHashCodeProvider DefaultInvariant => singletonInvariant;

	static SecureHashCodeProvider()
	{
		singletonInvariant = new SecureHashCodeProvider(CultureInfo.InvariantCulture);
		sync = new object();
		seed = (int)DateTime.UtcNow.Ticks;
	}

	public SecureHashCodeProvider()
	{
		if (!AreEqual(CultureInfo.CurrentCulture, CultureInfo.InvariantCulture))
		{
			m_text = CultureInfo.CurrentCulture.TextInfo;
		}
	}

	public SecureHashCodeProvider(CultureInfo culture)
	{
		if (culture == null)
		{
			throw new ArgumentNullException("culture");
		}
		if (!AreEqual(culture, CultureInfo.InvariantCulture))
		{
			m_text = culture.TextInfo;
		}
	}

	private static bool AreEqual(CultureInfo a, CultureInfo b)
	{
		return a.LCID == b.LCID;
	}

	private static bool AreEqual(TextInfo info, CultureInfo culture)
	{
		return info.LCID == culture.LCID;
	}

	public int GetHashCode(object obj)
	{
		if (obj == null)
		{
			throw new ArgumentNullException("obj");
		}
		if (!(obj is string text))
		{
			return obj.GetHashCode();
		}
		int num = seed;
		if (m_text != null && !AreEqual(m_text, CultureInfo.InvariantCulture))
		{
			string text2 = m_text.ToLower(text);
			foreach (char c in text2)
			{
				num = num * 31 + c;
			}
		}
		else
		{
			for (int j = 0; j < text.Length; j++)
			{
				char c = char.ToLower(text[j], CultureInfo.InvariantCulture);
				num = num * 31 + c;
			}
		}
		return num;
	}
}
