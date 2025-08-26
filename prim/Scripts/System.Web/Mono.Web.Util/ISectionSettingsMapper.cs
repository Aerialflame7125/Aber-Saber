using System.Collections.Generic;

namespace Mono.Web.Util;

public interface ISectionSettingsMapper
{
	object MapSection(object section, List<SettingsMappingWhat> whats);
}
