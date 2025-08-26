using System.Collections.Generic;
using System.Xml.XPath;

namespace Mono.Web.Util;

public class SettingsMappingWhat
{
	private string _value;

	private List<SettingsMappingWhatContents> _contents;

	public string Value => _value;

	public List<SettingsMappingWhatContents> Contents => _contents;

	public SettingsMappingWhat(XPathNavigator nav)
	{
		_value = nav.GetAttribute("value", string.Empty);
		XPathNodeIterator xPathNodeIterator = nav.Select("./*");
		_contents = new List<SettingsMappingWhatContents>();
		while (xPathNodeIterator.MoveNext())
		{
			XPathNavigator current = xPathNodeIterator.Current;
			switch (current.LocalName)
			{
			case "replace":
				_contents.Add(new SettingsMappingWhatContents(current, SettingsMappingWhatOperation.Replace));
				break;
			case "add":
				_contents.Add(new SettingsMappingWhatContents(current, SettingsMappingWhatOperation.Add));
				break;
			case "clear":
				_contents.Add(new SettingsMappingWhatContents(current, SettingsMappingWhatOperation.Clear));
				break;
			case "remove":
				_contents.Add(new SettingsMappingWhatContents(current, SettingsMappingWhatOperation.Remove));
				break;
			}
		}
	}
}
