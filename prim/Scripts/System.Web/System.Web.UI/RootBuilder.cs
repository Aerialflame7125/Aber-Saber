using System.Collections;
using System.Security.Permissions;
using System.Web.Compilation;
using System.Web.UI.HtmlControls;

namespace System.Web.UI;

/// <summary>Supports the page parser in defining the behavior for how content is parsed.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RootBuilder : TemplateBuilder
{
	private Hashtable built_objects;

	private static Hashtable htmlControls;

	private static Hashtable htmlInputControls;

	private AspComponentFoundry foundry;

	internal AspComponentFoundry Foundry
	{
		get
		{
			return foundry;
		}
		set
		{
			if (value != null)
			{
				foundry = value;
			}
		}
	}

	/// <summary>Gets a collection of the objects to persist that were built by the root builder.</summary>
	/// <returns>A <see cref="T:System.Collections.Hashtable" /> object that contains the objects that were built by the root builder.</returns>
	public IDictionary BuiltObjects
	{
		get
		{
			if (built_objects == null)
			{
				built_objects = new Hashtable();
			}
			return built_objects;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RootBuilder" /> class. </summary>
	public RootBuilder()
	{
		foundry = new AspComponentFoundry();
		base.Line = 1;
	}

	static RootBuilder()
	{
		htmlControls = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		htmlControls.Add("A", typeof(HtmlAnchor));
		htmlControls.Add("BUTTON", typeof(HtmlButton));
		htmlControls.Add("FORM", typeof(HtmlForm));
		htmlControls.Add("HEAD", typeof(HtmlHead));
		htmlControls.Add("IMG", typeof(HtmlImage));
		htmlControls.Add("INPUT", "INPUT");
		htmlControls.Add("SELECT", typeof(HtmlSelect));
		htmlControls.Add("TABLE", typeof(HtmlTable));
		htmlControls.Add("TD", typeof(HtmlTableCell));
		htmlControls.Add("TH", typeof(HtmlTableCell));
		htmlControls.Add("TR", typeof(HtmlTableRow));
		htmlControls.Add("TEXTAREA", typeof(HtmlTextArea));
		htmlInputControls = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
		htmlInputControls.Add("BUTTON", typeof(HtmlInputButton));
		htmlInputControls.Add("SUBMIT", typeof(HtmlInputSubmit));
		htmlInputControls.Add("RESET", typeof(HtmlInputReset));
		htmlInputControls.Add("CHECKBOX", typeof(HtmlInputCheckBox));
		htmlInputControls.Add("FILE", typeof(HtmlInputFile));
		htmlInputControls.Add("HIDDEN", typeof(HtmlInputHidden));
		htmlInputControls.Add("IMAGE", typeof(HtmlInputImage));
		htmlInputControls.Add("RADIO", typeof(HtmlInputRadioButton));
		htmlInputControls.Add("TEXT", typeof(HtmlInputText));
		htmlInputControls.Add("PASSWORD", typeof(HtmlInputPassword));
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RootBuilder" /> class with the specified template parser.</summary>
	/// <param name="parser">The object to use to parse files.</param>
	public RootBuilder(TemplateParser parser)
	{
		foundry = new AspComponentFoundry();
		base.Line = 1;
		if (parser != null)
		{
			base.FileName = parser.InputFile;
		}
		Init(parser, null, null, null, null, null);
	}

	/// <summary>Returns the control type of any parsed child controls.</summary>
	/// <param name="tagName">The tag name of the child control.</param>
	/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> object that holds all the specified tag attributes.</param>
	/// <returns>The type of the child control.</returns>
	public override Type GetChildControlType(string tagName, IDictionary attribs)
	{
		if (tagName == null)
		{
			throw new ArgumentNullException("tagName");
		}
		AspComponent component = foundry.GetComponent(tagName);
		if (component != null)
		{
			if (!string.IsNullOrEmpty(component.Source))
			{
				TemplateParser templateParser = base.Parser;
				if (component.FromConfig)
				{
					string baseVirtualDir = templateParser.BaseVirtualDir;
					VirtualPath virtualPath = new VirtualPath(component.Source);
					if (baseVirtualDir == virtualPath.Directory)
					{
						throw new ParseException(templateParser.Location, $"The page '{templateParser.VirtualPath}' cannot use the user control '{virtualPath.Absolute}', because it is registered in web.config and lives in the same directory as the page.");
					}
					base.Parser.AddDependency(component.Source);
				}
			}
			return component.Type;
		}
		if (component != null && component.Prefix != string.Empty)
		{
			throw new Exception("Unknown server tag '" + tagName + "'");
		}
		return LookupHtmlControls(tagName, attribs);
	}

	private static Type LookupHtmlControls(string tagName, IDictionary attribs)
	{
		object obj = htmlControls[tagName];
		if (obj is string)
		{
			if (attribs == null)
			{
				throw new HttpException("Unable to map input type control to a Type.");
			}
			string text = attribs["TYPE"] as string;
			if (text == null)
			{
				text = "TEXT";
			}
			Type obj2 = htmlInputControls[text] as Type;
			if (obj2 == null)
			{
				throw new HttpException("Unable to map input type control to a Type.");
			}
			return obj2;
		}
		if (obj == null)
		{
			obj = typeof(HtmlGenericControl);
		}
		return (Type)obj;
	}
}
