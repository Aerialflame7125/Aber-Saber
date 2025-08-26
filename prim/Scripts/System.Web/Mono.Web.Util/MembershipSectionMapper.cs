using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;

namespace Mono.Web.Util;

internal class MembershipSectionMapper : ISectionSettingsMapper
{
	public object MapSection(object _section, List<SettingsMappingWhat> whats)
	{
		if (!(_section is MembershipSection membershipSection))
		{
			return _section;
		}
		foreach (SettingsMappingWhat what in whats)
		{
			List<SettingsMappingWhatContents> contents = what.Contents;
			if (contents == null || contents.Count == 0)
			{
				continue;
			}
			foreach (SettingsMappingWhatContents item in contents)
			{
				switch (item.Operation)
				{
				case SettingsMappingWhatOperation.Add:
					ProcessAdd(membershipSection, item);
					break;
				case SettingsMappingWhatOperation.Clear:
					ProcessClear(membershipSection, item);
					break;
				case SettingsMappingWhatOperation.Replace:
					ProcessReplace(membershipSection, item);
					break;
				case SettingsMappingWhatOperation.Remove:
					ProcessRemove(membershipSection, item);
					break;
				}
			}
		}
		return membershipSection;
	}

	private bool GetCommonAttributes(SettingsMappingWhatContents how, out string name, out string type)
	{
		name = (type = null);
		Dictionary<string, string> attributes = how.Attributes;
		if (attributes == null || attributes.Count == 0)
		{
			return false;
		}
		if (!attributes.TryGetValue("name", out name))
		{
			return false;
		}
		if (string.IsNullOrEmpty(name))
		{
			return false;
		}
		attributes.TryGetValue("type", out type);
		return true;
	}

	private void SetProviderProperties(SettingsMappingWhatContents how, ProviderSettings prov)
	{
		Dictionary<string, string> attributes = how.Attributes;
		if (attributes == null || attributes.Count == 0)
		{
			return;
		}
		foreach (KeyValuePair<string, string> item in attributes)
		{
			string key = item.Key;
			if (!(key == "name"))
			{
				if (key == "type")
				{
					prov.Type = item.Value;
				}
				else
				{
					prov.Parameters[key] = item.Value;
				}
			}
		}
	}

	private void ProcessAdd(MembershipSection section, SettingsMappingWhatContents how)
	{
		if (GetCommonAttributes(how, out var name, out var type))
		{
			ProviderSettingsCollection providers = section.Providers;
			if (providers[name] == null)
			{
				ProviderSettings providerSettings = new ProviderSettings(name, type);
				SetProviderProperties(how, providerSettings);
				providers.Add(providerSettings);
			}
		}
	}

	private void ProcessRemove(MembershipSection section, SettingsMappingWhatContents how)
	{
		if (GetCommonAttributes(how, out var name, out var type))
		{
			ProviderSettingsCollection providers = section.Providers;
			ProviderSettings providerSettings = providers[name];
			if (providerSettings != null && !(providerSettings.Type != type))
			{
				providers.Remove(name);
			}
		}
	}

	private void ProcessClear(MembershipSection section, SettingsMappingWhatContents how)
	{
		section.Providers.Clear();
	}

	private void ProcessReplace(MembershipSection section, SettingsMappingWhatContents how)
	{
		if (GetCommonAttributes(how, out var name, out var _))
		{
			ProviderSettings providerSettings = section.Providers[name];
			if (providerSettings != null)
			{
				SetProviderProperties(how, providerSettings);
			}
		}
	}
}
