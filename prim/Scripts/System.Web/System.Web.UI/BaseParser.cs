using System.Collections;
using System.Configuration;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Provides a base set of functionality for classes involved in parsing ASP.NET page requests and server controls.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class BaseParser
{
	private HttpContext context;

	private string baseDir;

	private string baseVDir;

	private ILocation location;

	internal ILocation Location
	{
		get
		{
			return location;
		}
		set
		{
			location = value;
		}
	}

	internal HttpContext Context
	{
		get
		{
			return context;
		}
		set
		{
			context = value;
		}
	}

	internal string BaseDir
	{
		get
		{
			if (baseDir == null)
			{
				baseDir = MapPath(BaseVirtualDir, allowCrossAppMapping: false);
			}
			return baseDir;
		}
	}

	internal virtual string BaseVirtualDir
	{
		get
		{
			if (baseVDir == null)
			{
				baseVDir = VirtualPathUtility.GetDirectory(context.Request.FilePath);
			}
			return baseVDir;
		}
		set
		{
			if (VirtualPathUtility.IsRooted(value))
			{
				baseVDir = VirtualPathUtility.ToAbsolute(value);
			}
			else
			{
				baseVDir = value;
			}
		}
	}

	internal VirtualPath VirtualPath { get; set; }

	internal CompilationSection CompilationConfig => GetConfigSection<CompilationSection>("system.web/compilation");

	internal string MapPath(string path)
	{
		return MapPath(path, allowCrossAppMapping: true);
	}

	internal string MapPath(string path, bool allowCrossAppMapping)
	{
		if (context == null)
		{
			throw new HttpException("context is null!!");
		}
		return context.Request.MapPath(path, BaseVirtualDir, allowCrossAppMapping);
	}

	internal string PhysicalPath(string path)
	{
		if (Path.DirectorySeparatorChar != '/')
		{
			path = path.Replace('/', '\\');
		}
		return Path.Combine(BaseDir, path);
	}

	internal bool GetBool(IDictionary hash, string key, bool deflt)
	{
		if (!(hash[key] is string strA))
		{
			return deflt;
		}
		hash.Remove(key);
		bool result = false;
		if (string.Compare(strA, "true", ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			result = true;
		}
		else if (string.Compare(strA, "false", ignoreCase: true, Helpers.InvariantCulture) != 0)
		{
			ThrowParseException("Invalid value for " + key);
		}
		return result;
	}

	internal static string GetString(IDictionary hash, string key, string deflt)
	{
		if (!(hash[key] is string result))
		{
			return deflt;
		}
		hash.Remove(key);
		return result;
	}

	internal static bool IsDirective(string value, char directiveChar)
	{
		if (value == null || value == string.Empty)
		{
			return false;
		}
		value = value.Trim();
		if (!StrUtils.StartsWith(value, "<%") || !StrUtils.EndsWith(value, "%>"))
		{
			return false;
		}
		int num = value.IndexOf(directiveChar, 2);
		switch (num)
		{
		case -1:
			return false;
		case 2:
			return true;
		default:
			for (num--; num >= 2; num--)
			{
				if (!char.IsWhiteSpace(value[num]))
				{
					return false;
				}
			}
			return true;
		}
	}

	internal static bool IsDataBound(string value)
	{
		return IsDirective(value, '#');
	}

	internal static bool IsExpression(string value)
	{
		return IsDirective(value, '$');
	}

	internal void ThrowParseException(string message, params object[] parms)
	{
		if (parms == null)
		{
			throw new ParseException(location, message);
		}
		throw new ParseException(location, string.Format(message, parms));
	}

	internal void ThrowParseException(string message, Exception inner, params object[] parms)
	{
		if (parms == null || parms.Length == 0)
		{
			throw new ParseException(location, message, inner);
		}
		throw new ParseException(location, string.Format(message, parms), inner);
	}

	internal void ThrowParseFileNotFound(string path, params object[] parms)
	{
		ThrowParseException("The file '" + path + "' does not exist", parms);
	}

	internal TSection GetConfigSection<TSection>(string section) where TSection : ConfigurationSection
	{
		string text = VirtualPath?.Absolute;
		if (text == null)
		{
			return WebConfigurationManager.GetSection(section) as TSection;
		}
		return WebConfigurationManager.GetSection(section, text) as TSection;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.BaseParser" /> class.</summary>
	public BaseParser()
	{
	}
}
