using System.Configuration;

namespace System.Web.Configuration;

internal class Web20DefaultConfig : IConfigurationSystem
{
	private static Web20DefaultConfig instance;

	static Web20DefaultConfig()
	{
		instance = new Web20DefaultConfig();
	}

	public static Web20DefaultConfig GetInstance()
	{
		return instance;
	}

	public object GetConfig(string sectionName)
	{
		object webApplicationSection = WebConfigurationManager.GetWebApplicationSection(sectionName);
		if (webApplicationSection == null || webApplicationSection is IgnoreSection)
		{
			object config = WebConfigurationManager.oldConfig.GetConfig(sectionName);
			if (config != null)
			{
				return config;
			}
		}
		return webApplicationSection;
	}

	public void Init()
	{
	}
}
