using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.Configuration;
using System.Xml.XPath;

namespace Mono.Web.Util;

public class SettingsMappingManager
{
	private const string settingsMapFileName = "settings.map";

	private const string localSettingsMapFileName = "settings.map.config";

	private static object mapperLock;

	private static SettingsMappingManager _instance;

	private static string _mappingFile;

	private Dictionary<Type, SettingsMapping> _mappers;

	private static Dictionary<object, object> _mappedSections;

	private static SettingsMappingPlatform _myPlatform;

	private static bool _runningOnWindows;

	internal static bool IsRunningOnWindows => _runningOnWindows;

	public static SettingsMappingPlatform Platform => _myPlatform;

	public bool HasMappings
	{
		get
		{
			if (_mappers != null)
			{
				return _mappers.Count > 0;
			}
			return false;
		}
	}

	static SettingsMappingManager()
	{
		mapperLock = new object();
		_mappingFile = Path.Combine(Path.GetDirectoryName(RuntimeEnvironment.SystemConfigurationFile), "settings.map");
		PlatformID platform = Environment.OSVersion.Platform;
		_runningOnWindows = platform != (PlatformID)128 && platform != PlatformID.Unix && platform != PlatformID.MacOSX;
	}

	public static void Init()
	{
		if (_instance != null || Environment.GetEnvironmentVariable("MONO_ASPNET_INHIBIT_SETTINGSMAP") != null)
		{
			return;
		}
		NameValueCollection appSettings = WebConfigurationManager.AppSettings;
		if (appSettings == null || string.Compare(appSettings["MonoAspnetInhibitSettingsMap"], "true", StringComparison.OrdinalIgnoreCase) != 0)
		{
			if (IsRunningOnWindows)
			{
				_myPlatform = SettingsMappingPlatform.Windows;
			}
			else
			{
				_myPlatform = SettingsMappingPlatform.Unix;
			}
			SettingsMappingManager settingsMappingManager = new SettingsMappingManager();
			settingsMappingManager.LoadMappings();
			if (settingsMappingManager.HasMappings)
			{
				_instance = settingsMappingManager;
				_mappedSections = new Dictionary<object, object>();
			}
		}
	}

	private void LoadMappings()
	{
		if (File.Exists(_mappingFile))
		{
			LoadMappings(_mappingFile);
		}
		string text = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "settings.map.config");
		if (File.Exists(text))
		{
			LoadMappings(text);
		}
	}

	private void LoadMappings(string mappingFilePath)
	{
		XPathNavigator xPathNavigator;
		try
		{
			xPathNavigator = new XPathDocument(mappingFilePath).CreateNavigator();
		}
		catch (Exception innerException)
		{
			throw new ApplicationException("Error loading mapping settings", innerException);
		}
		XPathNodeIterator xPathNodeIterator;
		if (_mappers == null)
		{
			_mappers = new Dictionary<Type, SettingsMapping>();
		}
		else
		{
			xPathNodeIterator = xPathNavigator.Select("//settingsMap/clear");
			if (xPathNodeIterator.MoveNext())
			{
				_mappers.Clear();
			}
		}
		xPathNodeIterator = xPathNavigator.Select("//settingsMap/map[string-length (@sectionType) > 0 and string-length (@mapperType) > 0 and string-length (@platform) > 0]");
		while (xPathNodeIterator.MoveNext())
		{
			SettingsMapping settingsMapping = new SettingsMapping(xPathNodeIterator.Current);
			if (_myPlatform == settingsMapping.Platform)
			{
				if (!_mappers.ContainsKey(settingsMapping.SectionType))
				{
					_mappers.Add(settingsMapping.SectionType, settingsMapping);
				}
				else
				{
					_mappers[settingsMapping.SectionType] = settingsMapping;
				}
			}
		}
	}

	public static object MapSection(object input)
	{
		if (_instance == null || input == null)
		{
			return input;
		}
		if (_mappedSections.TryGetValue(input, out var value))
		{
			return value;
		}
		object obj = _instance.MapSection(input, input.GetType());
		lock (mapperLock)
		{
			if (obj != null && !_mappedSections.ContainsKey(obj))
			{
				_mappedSections.Add(obj, obj);
			}
		}
		return obj;
	}

	private object MapSection(object input, Type type)
	{
		if (_mappers == null || _mappers.Count == 0 || !_mappers.ContainsKey(type))
		{
			return input;
		}
		if (!_mappers.TryGetValue(type, out var value))
		{
			return input;
		}
		if (value == null)
		{
			return input;
		}
		return value.MapSection(input, type);
	}
}
