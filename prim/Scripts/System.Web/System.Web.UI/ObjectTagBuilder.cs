using System.Collections;
using System.Security.Permissions;
using System.Web.Compilation;

namespace System.Web.UI;

/// <summary>Used by the ASP.NET <see cref="T:System.Web.UI.TemplateParser" /> class to parse server-side <see langword="&lt;object&gt;" /> tags. This class can not be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class ObjectTagBuilder : ControlBuilder
{
	private string id;

	private string scope;

	private Type type;

	internal Type Type => type;

	internal string ObjectID => id;

	internal string Scope => scope;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectTagBuilder" /> class. </summary>
	public ObjectTagBuilder()
	{
		SetTagName("object");
	}

	/// <summary>Adds content, such as text or HTML, to a control.</summary>
	/// <param name="s">The content to add to the control.</param>
	public override void AppendLiteralString(string s)
	{
	}

	/// <summary>Adds builders to the <see cref="T:System.Web.UI.ObjectTagBuilder" /> object for any child controls that belong to the container control.</summary>
	/// <param name="subBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> assigned to the child control.</param>
	public override void AppendSubBuilder(ControlBuilder subBuilder)
	{
	}

	/// <summary>Initializes the object tag builder when the page is parsed.</summary>
	/// <param name="parser">The <see cref="T:System.Web.UI.TemplateParser" /> responsible for parsing the control.</param>
	/// <param name="parentBuilder">The <see cref="T:System.Web.UI.ControlBuilder" /> responsible for building the control.</param>
	/// <param name="type">The <see cref="T:System.Type" /> assigned to the control that the builder will create.</param>
	/// <param name="tagName">The name of the tag to be built. This allows the builder to support multiple tag types.</param>
	/// <param name="id">The <see cref="P:System.Web.UI.Control.ID" /> assigned to the control.</param>
	/// <param name="attribs">The <see cref="T:System.Collections.IDictionary" /> that holds all the specified tag attributes.</param>
	/// <exception cref="T:System.Web.HttpException">The <paramref name="id" /> parameter is <see langword="null" />.- or -The object tag scope is invalid.- or -The <see langword="classid" /> or <see langword="progid" /> attributes are not included or are invalid.</exception>
	public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
	{
		if (id == null && attribs == null)
		{
			throw new HttpException("Missing 'id'.");
		}
		if (attribs == null)
		{
			throw new ParseException(parser.Location, "Error in ObjectTag.");
		}
		attribs.Remove("runat");
		this.id = attribs["id"] as string;
		attribs.Remove("id");
		if (this.id == null || this.id.Trim() == "")
		{
			throw new ParseException(parser.Location, "Object tag must have a valid ID.");
		}
		scope = attribs["scope"] as string;
		string text = attribs["class"] as string;
		attribs.Remove("scope");
		attribs.Remove("class");
		if (text == null || text.Trim() == "")
		{
			throw new ParseException(parser.Location, "Object tag must have 'class' attribute.");
		}
		this.type = parser.LoadType(text);
		if (this.type == null)
		{
			throw new ParseException(parser.Location, "Type " + text + " not found.");
		}
		if (attribs["progid"] != null || attribs["classid"] != null)
		{
			throw new ParseException(parser.Location, "ClassID and ProgID are not supported.");
		}
		if (attribs.Count > 0)
		{
			throw new ParseException(parser.Location, "Unknown attribute");
		}
	}
}
