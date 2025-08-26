using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides parsing at design time.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public static class DesignTimeTemplateParser
{
	/// <summary>Parses and builds one control at design time.</summary>
	/// <param name="data">Information used in creating the parser.</param>
	/// <returns>The built <see cref="T:System.Web.UI.Control" /> object.</returns>
	[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
	public static Control ParseControl(DesignTimeParseData data)
	{
		TemplateParser templateParser = InitParser(data);
		templateParser.RootBuilder.Text = data.ParseText;
		if (templateParser.RootBuilder.Children == null)
		{
			return null;
		}
		IEnumerator enumerator = templateParser.RootBuilder.Children.GetEnumerator();
		try
		{
			if (enumerator.MoveNext())
			{
				return (Control)((ControlBuilder)enumerator.Current).CreateInstance();
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		return null;
	}

	/// <summary>Parses a template at design time.</summary>
	/// <param name="data">Information used in creating the parser.</param>
	/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the template.</returns>
	[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
	public static ITemplate ParseTemplate(DesignTimeParseData data)
	{
		TemplateParser templateParser = InitParser(data);
		templateParser.RootBuilder.Text = data.ParseText;
		return templateParser.RootBuilder;
	}

	[MonoTODO]
	private static TemplateParser InitParser(DesignTimeParseData data)
	{
		return new PageParser();
	}

	/// <summary>Parses and builds one or more controls at design time.</summary>
	/// <param name="data">Information used in creating the parser.</param>
	/// <returns>An array of built <see cref="T:System.Web.UI.Control" /> objects.</returns>
	[MonoTODO("Not implemented")]
	[SecurityPermission(SecurityAction.Demand, ControlThread = true, UnmanagedCode = true)]
	public static Control[] ParseControls(DesignTimeParseData data)
	{
		throw new NotImplementedException();
	}

	/// <summary>Parses a theme at design time.</summary>
	/// <param name="host">Manages designer components.</param>
	/// <param name="theme">The content to parse.</param>
	/// <param name="themePath">The path to the theme, which is used in creating the parser.</param>
	/// <returns>The <see cref="T:System.Web.UI.RootBuilder" /> from the parser that parsed the theme.</returns>
	/// <exception cref="T:System.Exception">An error occurred while parsing the theme.</exception>
	[MonoTODO("Not implemented")]
	[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
	public static ControlBuilder ParseTheme(IDesignerHost host, string theme, string themePath)
	{
		throw new NotImplementedException();
	}
}
