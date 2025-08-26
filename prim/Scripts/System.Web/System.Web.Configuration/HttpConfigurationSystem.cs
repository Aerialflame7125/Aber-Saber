using System.Configuration.Internal;

namespace System.Web.Configuration;

internal class HttpConfigurationSystem : IInternalConfigSystem
{
	bool IInternalConfigSystem.SupportsUserConfig => true;

	object IInternalConfigSystem.GetSection(string configKey)
	{
		return WebConfigurationManager.GetSection(configKey);
	}

	void IInternalConfigSystem.RefreshConfig(string sectionName)
	{
	}
}
