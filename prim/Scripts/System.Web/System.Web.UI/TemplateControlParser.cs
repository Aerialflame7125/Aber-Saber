using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.UI;

/// <summary>Implements ASP.NET template parsing for template controls.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public abstract class TemplateControlParser : BaseTemplateParser
{
	private bool autoEventWireup = true;

	private bool enableViewState = true;

	private CompilationMode compilationMode = CompilationMode.Always;

	private ClientIDMode? clientIDMode;

	private TextReader reader;

	internal bool AutoEventWireup => autoEventWireup;

	internal bool EnableViewState => enableViewState;

	internal CompilationMode CompilationMode => compilationMode;

	internal ClientIDMode? ClientIDMode => clientIDMode;

	internal override TextReader Reader
	{
		get
		{
			return reader;
		}
		set
		{
			reader = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.TemplateControlParser" /> class.</summary>
	protected TemplateControlParser()
	{
		LoadConfigDefaults();
	}

	internal override void LoadConfigDefaults()
	{
		base.LoadConfigDefaults();
		PagesSection pagesConfig = base.PagesConfig;
		autoEventWireup = pagesConfig.AutoEventWireup;
		enableViewState = pagesConfig.EnableViewState;
		compilationMode = pagesConfig.CompilationMode;
	}

	internal override void ProcessMainAttributes(IDictionary atts)
	{
		autoEventWireup = GetBool(atts, "AutoEventWireup", autoEventWireup);
		enableViewState = GetBool(atts, "EnableViewState", enableViewState);
		string @string = BaseParser.GetString(atts, "CompilationMode", compilationMode.ToString());
		if (!string.IsNullOrEmpty(@string))
		{
			try
			{
				compilationMode = (CompilationMode)Enum.Parse(typeof(CompilationMode), @string, ignoreCase: true);
			}
			catch (Exception inner)
			{
				ThrowParseException("Invalid value of the CompilationMode attribute.", inner);
			}
		}
		atts.Remove("TargetSchema");
		@string = BaseParser.GetString(atts, "ClientIDMode", null);
		if (!string.IsNullOrEmpty(@string))
		{
			try
			{
				clientIDMode = (ClientIDMode)Enum.Parse(typeof(ClientIDMode), @string, ignoreCase: true);
			}
			catch (Exception inner2)
			{
				ThrowParseException("Invalid value of the ClientIDMode attribute.", inner2);
			}
		}
		base.ProcessMainAttributes(atts);
	}

	internal object GetCompiledInstance()
	{
		Type type = CompileIntoType();
		if (type == null)
		{
			return null;
		}
		object obj = Activator.CreateInstance(type);
		if (obj == null)
		{
			return null;
		}
		HandleOptions(obj);
		return obj;
	}

	internal override void AddDirective(string directive, IDictionary atts)
	{
		if (string.Compare("Register", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			string @string = BaseParser.GetString(atts, "TagPrefix", null);
			if (@string == null || @string.Trim() == "")
			{
				ThrowParseException("No TagPrefix attribute found.");
			}
			string string2 = BaseParser.GetString(atts, "Namespace", null);
			string string3 = BaseParser.GetString(atts, "Assembly", null);
			if (string2 == null && string3 != null)
			{
				ThrowParseException("Need a Namespace attribute with Assembly.");
			}
			if (string2 != null)
			{
				if (atts.Count != 0)
				{
					ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts));
				}
				RegisterNamespace(@string, string2, string3);
				return;
			}
			string string4 = BaseParser.GetString(atts, "TagName", null);
			string string5 = BaseParser.GetString(atts, "Src", null);
			if (string4 == null && string5 != null)
			{
				ThrowParseException("Need a TagName attribute with Src.");
			}
			if (string4 != null && string5 == null)
			{
				ThrowParseException("Need a Src attribute with TagName.");
			}
			RegisterCustomControl(@string, string4, string5);
		}
		else if (string.Compare("Reference", directive, ignoreCase: true, Helpers.InvariantCulture) == 0)
		{
			string text = null;
			string string6 = BaseParser.GetString(atts, "Page", null);
			bool flag = string6 != null;
			if (flag)
			{
				text = string6;
			}
			bool flag2 = false;
			string string7 = BaseParser.GetString(atts, "Control", null);
			if (string7 != null)
			{
				if (flag)
				{
					flag2 = true;
				}
				else
				{
					text = string7;
				}
			}
			string string8 = BaseParser.GetString(atts, "VirtualPath", null);
			if (string8 != null)
			{
				if (text != null)
				{
					flag2 = true;
				}
				else
				{
					text = string8;
				}
			}
			if (text == null)
			{
				ThrowParseException("Must provide one of the 'page', 'control' or 'virtualPath' attributes");
			}
			if (flag2)
			{
				ThrowParseException("Only one attribute can be specified.");
			}
			text = HostingEnvironment.VirtualPathProvider.CombineVirtualPaths(base.VirtualPath.Absolute, text);
			AddDependency(text, combinePaths: false);
			Type compiledType = BuildManager.GetCompiledType(text);
			AddAssembly(compiledType.Assembly, fullPath: true);
			if (atts.Count != 0)
			{
				ThrowParseException("Unknown attribute: " + TemplateParser.GetOneKey(atts));
			}
		}
		else
		{
			base.AddDirective(directive, atts);
		}
	}

	internal override void HandleOptions(object obj)
	{
		base.HandleOptions(obj);
		Control obj2 = obj as Control;
		obj2.AutoEventWireup = autoEventWireup;
		obj2.EnableViewState = enableViewState;
	}
}
