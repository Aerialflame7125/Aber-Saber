using System.IO;
using System.Security.Permissions;
using System.Security.Policy;
using System.Web.Configuration;

namespace System.Web.Hosting;

/// <summary>Enables hosting of ASP.NET pages outside the Internet Information Services (IIS) application. This class enables the host to create application domains for processing ASP.NET requests.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ApplicationHost
{
	private const string DEFAULT_WEB_CONFIG_NAME = "web.config";

	internal const string MonoHostedDataKey = ".:!MonoAspNetHostedApp!:.";

	private static object create_dir = new object();

	private ApplicationHost()
	{
	}

	internal static string FindWebConfig(string basedir)
	{
		if (string.IsNullOrEmpty(basedir) || !Directory.Exists(basedir))
		{
			return null;
		}
		string[] fileSystemEntries = Directory.GetFileSystemEntries(basedir, "?eb.?onfig");
		if (fileSystemEntries == null || fileSystemEntries.Length == 0)
		{
			return null;
		}
		return fileSystemEntries[0];
	}

	internal static bool ClearDynamicBaseDirectory(string directory)
	{
		string[] array = null;
		try
		{
			array = Directory.GetDirectories(directory);
		}
		catch
		{
		}
		bool result = true;
		if (array != null && array.Length != 0)
		{
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (ClearDynamicBaseDirectory(text))
				{
					try
					{
						Directory.Delete(text);
					}
					catch
					{
						result = false;
					}
				}
			}
		}
		try
		{
			array = Directory.GetFiles(directory);
		}
		catch
		{
			array = null;
		}
		if (array != null && array.Length != 0)
		{
			string[] array2 = array;
			foreach (string path in array2)
			{
				try
				{
					File.Delete(path);
				}
				catch
				{
					result = false;
				}
			}
		}
		return result;
	}

	private static bool CreateDirectory(string directory)
	{
		lock (create_dir)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
				return false;
			}
			return true;
		}
	}

	private static string BuildPrivateBinPath(string physicalPath, string[] dirs)
	{
		int num = dirs.Length;
		string[] array = new string[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = Path.Combine(physicalPath, dirs[i]);
		}
		return string.Join(";", array);
	}

	/// <summary>Creates and configures an application domain for hosting ASP.NET.</summary>
	/// <param name="hostType">The name of a user-supplied class to be created in the new application domain.</param>
	/// <param name="virtualDir">The virtual directory for the application domain; for example, /myapp.</param>
	/// <param name="physicalDir">The physical directory for the application domain where ASP.NET pages are located; for example, c:\mypages.</param>
	/// <returns>An instance of a user-supplied class used to marshal calls into the newly created application domain.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">The Web host computer is not running the Windows NT platform or a Coriolis environment.</exception>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public static object CreateApplicationHost(Type hostType, string virtualDir, string physicalDir)
	{
		if (physicalDir == null)
		{
			throw new NullReferenceException();
		}
		physicalDir = Path.GetFullPath(physicalDir);
		if (hostType == null)
		{
			throw new ArgumentException("hostType can't be null");
		}
		if (virtualDir == null)
		{
			throw new ArgumentNullException("virtualDir");
		}
		Evidence securityInfo = new Evidence(AppDomain.CurrentDomain.Evidence);
		AppDomainSetup appDomainSetup = new AppDomainSetup();
		appDomainSetup.ApplicationBase = physicalDir;
		string text = FindWebConfig(physicalDir);
		if (text == null)
		{
			text = Path.Combine(physicalDir, "web.config");
		}
		appDomainSetup.ConfigurationFile = text;
		appDomainSetup.DisallowCodeDownload = true;
		string[] array = new string[1] { Path.Combine(physicalDir, "bin") };
		string[] binDirs = HttpApplication.BinDirs;
		foreach (string path in binDirs)
		{
			string text2 = Path.Combine(physicalDir, path);
			if (Directory.Exists(text2))
			{
				array[0] = text2;
				break;
			}
		}
		appDomainSetup.PrivateBinPath = BuildPrivateBinPath(physicalDir, array);
		appDomainSetup.PrivateBinPathProbe = "*";
		string text3 = null;
		string userName = Environment.UserName;
		int num = 0;
		string text4 = userName + "-temp-aspnet-";
		int num2 = 0;
		while (true)
		{
			string text5 = Path.Combine(Path.GetTempPath(), text4 + num2.ToString("x"));
			try
			{
				CreateDirectory(text5);
				string text6 = Path.Combine(text5, "stamp");
				CreateDirectory(text6);
				text3 = text5;
				try
				{
					Directory.Delete(text6);
				}
				catch (Exception)
				{
				}
				num = num2.GetHashCode();
			}
			catch (UnauthorizedAccessException)
			{
				goto IL_0151;
			}
			break;
			IL_0151:
			num2++;
		}
		string text7 = ((virtualDir.GetHashCode() + 1) ^ (physicalDir.GetHashCode() + 2) ^ num).ToString("x");
		string environmentVariable = Environment.GetEnvironmentVariable("__MONO_DOMAIN_ID_SUFFIX");
		if (environmentVariable != null && environmentVariable.Length > 0)
		{
			text7 += environmentVariable;
		}
		appDomainSetup.ApplicationName = text7;
		appDomainSetup.DynamicBase = text3;
		appDomainSetup.CachePath = text3;
		string dynamicBase = appDomainSetup.DynamicBase;
		if (CreateDirectory(dynamicBase) && Environment.GetEnvironmentVariable("MONO_ASPNET_NODELETE") == null)
		{
			ClearDynamicBaseDirectory(dynamicBase);
		}
		AppDomain appDomain = AppDomain.CreateDomain(text7, securityInfo, appDomainSetup);
		appDomain.SetData(".appDomain", "*");
		int length = physicalDir.Length;
		if (physicalDir[length - 1] != Path.DirectorySeparatorChar)
		{
			physicalDir += Path.DirectorySeparatorChar;
		}
		appDomain.SetData(".appPath", physicalDir);
		appDomain.SetData(".appVPath", virtualDir);
		appDomain.SetData(".appId", text7);
		appDomain.SetData(".domainId", text7);
		appDomain.SetData(".hostingVirtualPath", virtualDir);
		appDomain.SetData(".hostingInstallDir", Path.GetDirectoryName(typeof(object).Assembly.CodeBase));
		appDomain.SetData("DataDirectory", Path.Combine(physicalDir, "App_Data"));
		appDomain.SetData(".:!MonoAspNetHostedApp!:.", "yes");
		appDomain.DoCallBack(SetHostingEnvironment);
		return appDomain.CreateInstanceAndUnwrap(hostType.Module.Assembly.FullName, hostType.FullName);
	}

	private static void SetHostingEnvironment()
	{
		bool flag = true;
		if (WebConfigurationManager.GetWebApplicationSection("system.web/hostingEnvironment") is HostingEnvironmentSection hostingEnvironmentSection)
		{
			flag = hostingEnvironmentSection.ShadowCopyBinAssemblies;
		}
		if (flag)
		{
			AppDomain currentDomain = AppDomain.CurrentDomain;
			currentDomain.SetShadowCopyFiles();
			currentDomain.SetShadowCopyPath(currentDomain.SetupInformation.PrivateBinPath);
		}
		HostingEnvironment.IsHosted = true;
		HostingEnvironment.SiteName = HostingEnvironment.ApplicationID;
	}
}
