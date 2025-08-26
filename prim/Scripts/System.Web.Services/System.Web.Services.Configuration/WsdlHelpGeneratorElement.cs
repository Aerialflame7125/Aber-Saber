using System.Configuration;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Xml;

namespace System.Web.Services.Configuration;

/// <summary>Represents <see langword="WsdlHelpGenerator" /> element in the configuration file that specifies the XML Web service Help page (an .aspx file) that is displayed to a browser when the browser navigates directly to an ASMX XML Web services page.</summary>
public sealed class WsdlHelpGeneratorElement : ConfigurationElement
{
	private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

	private readonly ConfigurationProperty href = new ConfigurationProperty("href", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

	private string virtualPath;

	private string actualPath;

	private bool needToValidateHref;

	internal string HelpGeneratorVirtualPath => virtualPath + Href;

	internal string HelpGeneratorPath => Path.Combine(actualPath, Href);

	/// <summary>Gets or sets the file path to the Help page.</summary>
	/// <returns>A <see cref="T:System.String" /> that contains the file path to the Help page.</returns>
	[ConfigurationProperty("href", IsRequired = true)]
	public string Href
	{
		get
		{
			return (string)base[href];
		}
		set
		{
			if (value == null)
			{
				value = string.Empty;
			}
			if (needToValidateHref && value.Length > 0)
			{
				CheckIOReadPermission(actualPath, value);
			}
			base[href] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties => properties;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.WsdlHelpGeneratorElement" /> class.</summary>
	public WsdlHelpGeneratorElement()
	{
		properties.Add(href);
	}

	[FileIOPermission(SecurityAction.Assert, Unrestricted = true)]
	private string GetConfigurationDirectory()
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		return HttpRuntime.MachineConfigurationDirectory;
	}

	protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		base.DeserializeElement(reader, serializeCollectionKey);
		try
		{
			_ = base.EvaluationContext;
		}
		catch (ConfigurationErrorsException)
		{
			actualPath = GetConfigurationDirectory();
			return;
		}
		if (!(base.EvaluationContext.HostingContext is WebContext webContext) || Href.Length == 0)
		{
			return;
		}
		string text = webContext.Path;
		string text2 = null;
		if (text == null)
		{
			text = HostingEnvironment.ApplicationVirtualPath;
			if (text == null)
			{
				text = "";
			}
			text2 = GetConfigurationDirectory();
		}
		else
		{
			text2 = HostingEnvironment.MapPath(text);
		}
		if (!text.EndsWith("/", StringComparison.Ordinal))
		{
			text += "/";
		}
		CheckIOReadPermission(text2, Href);
		actualPath = text2;
		virtualPath = text;
		needToValidateHref = true;
	}

	protected override void Reset(ConfigurationElement parentElement)
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		WsdlHelpGeneratorElement wsdlHelpGeneratorElement = (WsdlHelpGeneratorElement)parentElement;
		try
		{
			_ = base.EvaluationContext;
		}
		catch (ConfigurationErrorsException)
		{
			base.Reset(parentElement);
			actualPath = GetConfigurationDirectory();
			return;
		}
		if (base.EvaluationContext.HostingContext is WebContext { Path: var text })
		{
			bool num = text == null;
			actualPath = wsdlHelpGeneratorElement.actualPath;
			if (num)
			{
				text = HostingEnvironment.ApplicationVirtualPath;
			}
			if (text != null && !text.EndsWith("/", StringComparison.Ordinal))
			{
				text += "/";
			}
			if (text == null && parentElement != null)
			{
				virtualPath = wsdlHelpGeneratorElement.virtualPath;
			}
			else if (text != null)
			{
				virtualPath = text;
			}
		}
		base.Reset(parentElement);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal void SetDefaults()
	{
		PartialTrustHelpers.FailIfInPartialTrustOutsideAspNet();
		if (HttpContext.Current != null)
		{
			virtualPath = HostingEnvironment.ApplicationVirtualPath;
		}
		actualPath = GetConfigurationDirectory();
		if (virtualPath != null && !virtualPath.EndsWith("/", StringComparison.Ordinal))
		{
			virtualPath += "/";
		}
		if (actualPath != null && !actualPath.EndsWith("\\", StringComparison.Ordinal))
		{
			actualPath += "\\";
		}
		Href = "DefaultWsdlHelpGenerator.aspx";
		CheckIOReadPermission(actualPath, Href);
		needToValidateHref = true;
	}

	private static void CheckIOReadPermission(string path, string file)
	{
		if (path != null)
		{
			string fullPath = Path.GetFullPath(Path.Combine(path, file));
			new FileIOPermission(FileIOPermissionAccess.Read, fullPath).Demand();
		}
	}
}
