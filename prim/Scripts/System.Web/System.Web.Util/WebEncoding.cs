using System.Text;
using System.Web.Configuration;

namespace System.Web.Util;

internal class WebEncoding
{
	private static bool cached;

	private static GlobalizationSection sect;

	private static GlobalizationSection GlobalizationConfig
	{
		get
		{
			if (!cached)
			{
				try
				{
					sect = (GlobalizationSection)WebConfigurationManager.GetWebApplicationSection("system.web/globalization");
				}
				catch
				{
				}
				cached = true;
			}
			return sect;
		}
	}

	public static Encoding FileEncoding
	{
		get
		{
			if (GlobalizationConfig == null)
			{
				return Encoding.Default;
			}
			return GlobalizationConfig.FileEncoding;
		}
	}

	public static Encoding ResponseEncoding
	{
		get
		{
			if (GlobalizationConfig == null)
			{
				return Encoding.Default;
			}
			return GlobalizationConfig.ResponseEncoding;
		}
	}

	public static Encoding RequestEncoding
	{
		get
		{
			if (GlobalizationConfig == null)
			{
				return Encoding.Default;
			}
			return GlobalizationConfig.RequestEncoding;
		}
	}
}
