using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace Mono.Web.Util;

internal class SettingsMapping
{
	private string _sectionTypeName;

	private Type _sectionType;

	private string _mapperTypeName;

	private Type _mapperType;

	private SettingsMappingPlatform _platform;

	private List<SettingsMappingWhat> _whats;

	public Type SectionType
	{
		get
		{
			if (_sectionType == null)
			{
				_sectionType = Type.GetType(_sectionTypeName, throwOnError: false);
			}
			return _sectionType;
		}
	}

	public Type MapperType
	{
		get
		{
			if (_mapperType == null)
			{
				_mapperType = Type.GetType(_mapperTypeName, throwOnError: true);
				if (!typeof(ISectionSettingsMapper).IsAssignableFrom(_mapperType))
				{
					_mapperType = null;
					throw new InvalidOperationException("Mapper type does not implement the ISectionSettingsMapper interface");
				}
			}
			return _mapperType;
		}
	}

	public SettingsMappingPlatform Platform => _platform;

	public SettingsMapping(XPathNavigator nav)
	{
		_sectionTypeName = nav.GetAttribute("sectionType", string.Empty);
		_mapperTypeName = nav.GetAttribute("mapperType", string.Empty);
		EnumConverter enumConverter = new EnumConverter(typeof(SettingsMappingPlatform));
		_platform = (SettingsMappingPlatform)enumConverter.ConvertFromInvariantString(nav.GetAttribute("platform", string.Empty));
		LoadContents(nav);
	}

	public object MapSection(object input, Type type)
	{
		if (type != SectionType)
		{
			throw new ArgumentException("type", "Invalid section type for this mapper");
		}
		if (!(Activator.CreateInstance(MapperType) is ISectionSettingsMapper sectionSettingsMapper))
		{
			return input;
		}
		return sectionSettingsMapper.MapSection(input, _whats);
	}

	private void LoadContents(XPathNavigator nav)
	{
		XPathNodeIterator xPathNodeIterator = nav.Select("./what[string-length (@value) > 0]");
		_whats = new List<SettingsMappingWhat>();
		while (xPathNodeIterator.MoveNext())
		{
			_whats.Add(new SettingsMappingWhat(xPathNodeIterator.Current));
		}
	}
}
