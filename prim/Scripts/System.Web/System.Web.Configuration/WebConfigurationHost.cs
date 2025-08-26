using System.Configuration;
using System.Configuration.Internal;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Configuration;

internal class WebConfigurationHost : IInternalConfigHost
{
	private WebConfigurationFileMap map;

	private const string MachinePath = ":machine:";

	private const string MachineWebPath = ":web:";

	private string appVirtualPath;

	public virtual bool SupportsChangeNotifications => false;

	public virtual bool SupportsLocation => false;

	public virtual bool SupportsPath => false;

	public virtual bool SupportsRefresh => false;

	[MonoTODO("Always returns false")]
	public virtual bool IsRemote => false;

	public virtual object CreateConfigurationContext(string configPath, string locationSubPath)
	{
		return new WebContext(WebApplicationLevel.AtApplication, "", "", configPath, locationSubPath);
	}

	public virtual object CreateDeprecatedConfigContext(string configPath)
	{
		return new HttpConfigurationContext(configPath);
	}

	public virtual string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
	{
		if (protectedSection == null)
		{
			throw new ArgumentNullException("protectedSection");
		}
		return protectedSection.EncryptSection(encryptedXml, protectionProvider);
	}

	public virtual void DeleteStream(string streamName)
	{
		File.Delete(streamName);
	}

	public virtual string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider, ProtectedConfigurationSection protectedSection)
	{
		if (protectedSection == null)
		{
			throw new ArgumentNullException("protectedSection");
		}
		return protectedSection.EncryptSection(clearXml, protectionProvider);
	}

	public virtual string GetConfigPathFromLocationSubPath(string configPath, string locationSubPath)
	{
		if (!string.IsNullOrEmpty(locationSubPath) && !string.IsNullOrEmpty(configPath))
		{
			string text = ((configPath.Length == 1) ? null : (configPath.Substring(1) + "/"));
			if (text != null && locationSubPath.StartsWith(text, StringComparison.Ordinal))
			{
				locationSubPath = locationSubPath.Substring(text.Length);
			}
		}
		string text2 = configPath + "/" + locationSubPath;
		if (!string.IsNullOrEmpty(text2) && text2[0] == '/')
		{
			return text2.Substring(1);
		}
		return text2;
	}

	public virtual Type GetConfigType(string typeName, bool throwOnError)
	{
		Type type = HttpApplication.LoadType(typeName);
		if (type == null && throwOnError)
		{
			throw new ConfigurationErrorsException("Type not found: '" + typeName + "'");
		}
		return type;
	}

	public virtual string GetConfigTypeName(Type t)
	{
		return t.AssemblyQualifiedName;
	}

	public virtual void GetRestrictedPermissions(IInternalConfigRecord configRecord, out PermissionSet permissionSet, out bool isHostReady)
	{
		throw new NotImplementedException();
	}

	public virtual string GetStreamName(string configPath)
	{
		if (configPath == ":machine:")
		{
			if (map == null)
			{
				return RuntimeEnvironment.SystemConfigurationFile;
			}
			return map.MachineConfigFilename;
		}
		if (configPath == ":web:")
		{
			string dir = ((map != null) ? Path.GetDirectoryName(map.MachineConfigFilename) : Path.GetDirectoryName(RuntimeEnvironment.SystemConfigurationFile));
			return GetWebConfigFileName(dir);
		}
		return GetWebConfigFileName(MapPath(configPath));
	}

	public virtual string GetStreamNameForConfigSource(string streamName, string configSource)
	{
		throw new NotImplementedException();
	}

	public virtual object GetStreamVersion(string streamName)
	{
		throw new NotImplementedException();
	}

	public virtual IDisposable Impersonate()
	{
		throw new NotImplementedException();
	}

	public virtual void Init(IInternalConfigRoot root, params object[] hostInitParams)
	{
	}

	public virtual void InitForConfiguration(ref string locationSubPath, out string configPath, out string locationConfigPath, IInternalConfigRoot root, params object[] hostInitConfigurationParams)
	{
		string text = (string)hostInitConfigurationParams[1];
		map = (WebConfigurationFileMap)hostInitConfigurationParams[0];
		bool flag = false;
		if (hostInitConfigurationParams.Length > 7 && hostInitConfigurationParams[7] is bool)
		{
			flag = (bool)hostInitConfigurationParams[7];
		}
		if (flag)
		{
			appVirtualPath = text;
		}
		else
		{
			appVirtualPath = HttpRuntime.AppDomainAppVirtualPath;
		}
		if (locationSubPath == ":web:")
		{
			locationSubPath = ":machine:";
			configPath = ":web:";
			locationConfigPath = null;
			return;
		}
		if (locationSubPath == ":machine:")
		{
			locationSubPath = null;
			configPath = ":machine:";
			locationConfigPath = null;
			return;
		}
		if (locationSubPath == null)
		{
			configPath = text;
			if (configPath.Length > 1)
			{
				configPath = VirtualPathUtility.RemoveTrailingSlash(configPath);
			}
		}
		else
		{
			configPath = locationSubPath;
		}
		int num = ((!(configPath == HttpRuntime.AppDomainAppVirtualPath) && !(configPath == "/")) ? configPath.LastIndexOf("/") : (-1));
		if (num != -1)
		{
			locationConfigPath = configPath.Substring(num + 1);
			if (num == 0)
			{
				locationSubPath = "/";
			}
			else
			{
				locationSubPath = text.Substring(0, num);
			}
		}
		else
		{
			locationSubPath = ":web:";
			locationConfigPath = null;
		}
	}

	public string MapPath(string virtualPath)
	{
		if (!string.IsNullOrEmpty(virtualPath) && virtualPath.StartsWith("/@@MonoFakeVirtualPath@@", StringComparison.Ordinal))
		{
			return HttpRuntime.AppDomainAppPath;
		}
		if (map != null)
		{
			return MapPathFromMapper(virtualPath);
		}
		if (HttpContext.Current != null && HttpContext.Current.Request != null)
		{
			return HttpContext.Current.Request.MapPath(virtualPath);
		}
		if (HttpRuntime.AppDomainAppVirtualPath != null && virtualPath.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
		{
			if (virtualPath == HttpRuntime.AppDomainAppVirtualPath)
			{
				return HttpRuntime.AppDomainAppPath;
			}
			return UrlUtils.Combine(HttpRuntime.AppDomainAppPath, virtualPath.Substring(HttpRuntime.AppDomainAppVirtualPath.Length));
		}
		return virtualPath;
	}

	public string NormalizeVirtualPath(string virtualPath)
	{
		virtualPath = ((virtualPath != null && virtualPath.Length != 0) ? virtualPath.Trim() : ".");
		if (virtualPath[0] == '~' && virtualPath.Length > 2 && virtualPath[1] == '/')
		{
			virtualPath = virtualPath.Substring(1);
		}
		if (Path.DirectorySeparatorChar != '/')
		{
			virtualPath = virtualPath.Replace(Path.DirectorySeparatorChar, '/');
		}
		if (UrlUtils.IsRooted(virtualPath))
		{
			virtualPath = UrlUtils.Canonic(virtualPath);
		}
		else if (map.VirtualDirectories.Count > 0)
		{
			virtualPath = UrlUtils.Combine(map.VirtualDirectories[0].VirtualDirectory, virtualPath);
			virtualPath = UrlUtils.Canonic(virtualPath);
		}
		return virtualPath;
	}

	public string MapPathFromMapper(string virtualPath)
	{
		string text = NormalizeVirtualPath(virtualPath);
		foreach (VirtualDirectoryMapping virtualDirectory in map.VirtualDirectories)
		{
			if (text.StartsWith(virtualDirectory.VirtualDirectory))
			{
				int length = virtualDirectory.VirtualDirectory.Length;
				if (text.Length == length)
				{
					return virtualDirectory.PhysicalDirectory;
				}
				if (text[length] == '/')
				{
					string path = text.Substring(length + 1).Replace('/', Path.DirectorySeparatorChar);
					return Path.Combine(virtualDirectory.PhysicalDirectory, path);
				}
			}
		}
		throw new HttpException("Invalid virtual directory: " + virtualPath);
	}

	internal static string GetWebConfigFileName(string dir)
	{
		AppDomain currentDomain = AppDomain.CurrentDomain;
		if (currentDomain.GetData(".:!MonoAspNetHostedApp!:.") as string == "yes")
		{
			return ApplicationHost.FindWebConfig(dir);
		}
		string fileName = Path.GetFileName((Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly()).Location);
		string[] obj = new string[2]
		{
			fileName + ".config",
			fileName + ".Config"
		};
		string baseDirectory = currentDomain.BaseDirectory;
		string[] array = obj;
		foreach (string path in array)
		{
			string text = Path.Combine(baseDirectory, path);
			if (File.Exists(text))
			{
				return text;
			}
		}
		return null;
	}

	public virtual bool IsAboveApplication(string configPath)
	{
		return !configPath.Contains(HttpRuntime.AppDomainAppPath);
	}

	public virtual bool IsConfigRecordRequired(string configPath)
	{
		throw new NotImplementedException();
	}

	public virtual bool IsDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition)
	{
		switch (allowDefinition)
		{
		case ConfigurationAllowDefinition.MachineOnly:
			if (!(configPath == ":machine:"))
			{
				return configPath == ":web:";
			}
			return true;
		case ConfigurationAllowDefinition.MachineToWebRoot:
		case ConfigurationAllowDefinition.MachineToApplication:
		{
			if (string.IsNullOrEmpty(configPath))
			{
				return true;
			}
			string text = ((!VirtualPathUtility.IsRooted(configPath)) ? configPath : VirtualPathUtility.Normalize(configPath));
			if (string.Compare(text, ":machine:", StringComparison.Ordinal) == 0 || string.Compare(text, ":web:", StringComparison.Ordinal) == 0)
			{
				return true;
			}
			if (string.Compare(text, appVirtualPath) != 0)
			{
				return IsApplication(text);
			}
			return true;
		}
		default:
			return true;
		}
	}

	[MonoTODO("Should return false in case strPath points to the root of an application.")]
	internal bool IsApplication(string strPath)
	{
		return true;
	}

	public virtual bool IsFile(string streamName)
	{
		throw new NotImplementedException();
	}

	public virtual bool IsLocationApplicable(string configPath)
	{
		throw new NotImplementedException();
	}

	public virtual Stream OpenStreamForRead(string streamName)
	{
		if (!File.Exists(streamName))
		{
			return null;
		}
		return new FileStream(streamName, FileMode.Open, FileAccess.Read);
	}

	[MonoTODO("Not implemented")]
	public virtual Stream OpenStreamForRead(string streamName, bool assertPermissions)
	{
		throw new NotImplementedException();
	}

	public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext)
	{
		if (!IsAboveApplication(streamName))
		{
			WebConfigurationManager.SuppressAppReload(newValue: true);
		}
		return new FileStream(streamName, FileMode.Create, FileAccess.Write);
	}

	[MonoTODO("Not implemented")]
	public virtual Stream OpenStreamForWrite(string streamName, string templateStreamName, ref object writeContext, bool assertPermissions)
	{
		throw new NotImplementedException();
	}

	public virtual bool PrefetchAll(string configPath, string streamName)
	{
		throw new NotImplementedException();
	}

	public virtual bool PrefetchSection(string sectionGroupName, string sectionName)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	public virtual void RequireCompleteInit(IInternalConfigRecord configRecord)
	{
		throw new NotImplementedException();
	}

	public virtual object StartMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
	{
		throw new NotImplementedException();
	}

	public virtual void StopMonitoringStreamForChanges(string streamName, StreamChangeCallback callback)
	{
		throw new NotImplementedException();
	}

	public virtual void VerifyDefinitionAllowed(string configPath, ConfigurationAllowDefinition allowDefinition, ConfigurationAllowExeDefinition allowExeDefinition, IConfigErrorInfo errorInfo)
	{
		if (!IsDefinitionAllowed(configPath, allowDefinition, allowExeDefinition))
		{
			throw new ConfigurationErrorsException(string.Concat("The section can't be defined in this file (the allowed definition context is '", allowDefinition, "')."), errorInfo.Filename, errorInfo.LineNumber);
		}
	}

	public virtual void WriteCompleted(string streamName, bool success, object writeContext)
	{
		WriteCompleted(streamName, success, writeContext, assertPermissions: false);
	}

	public virtual void WriteCompleted(string streamName, bool success, object writeContext, bool assertPermissions)
	{
		if (!IsAboveApplication(streamName))
		{
			WebConfigurationManager.SuppressAppReload(newValue: true);
		}
	}

	[MonoTODO("Not implemented")]
	public virtual bool IsFullTrustSectionWithoutAptcaAllowed(IInternalConfigRecord configRecord)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	public virtual bool IsInitDelayed(IInternalConfigRecord configRecord)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	public virtual bool IsSecondaryRoot(string configPath)
	{
		throw new NotImplementedException();
	}

	[MonoTODO("Not implemented")]
	public virtual bool IsTrustedConfigPath(string configPath)
	{
		throw new NotImplementedException();
	}
}
