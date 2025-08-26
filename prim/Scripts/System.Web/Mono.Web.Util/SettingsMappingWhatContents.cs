using System.Collections.Generic;
using System.Xml.XPath;

namespace Mono.Web.Util;

public class SettingsMappingWhatContents
{
	private SettingsMappingWhatOperation _operation;

	private Dictionary<string, string> _attributes = new Dictionary<string, string>();

	public SettingsMappingWhatOperation Operation => _operation;

	public Dictionary<string, string> Attributes => _attributes;

	public SettingsMappingWhatContents(XPathNavigator nav, SettingsMappingWhatOperation operation)
	{
		_operation = operation;
		if (nav.HasAttributes)
		{
			nav.MoveToFirstAttribute();
			_attributes.Add(nav.Name, nav.Value);
			while (nav.MoveToNextAttribute())
			{
				_attributes.Add(nav.Name, nav.Value);
			}
		}
	}
}
