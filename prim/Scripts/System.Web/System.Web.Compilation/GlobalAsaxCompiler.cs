using System.Collections;
using System.Web.UI;
using System.Web.Util;

namespace System.Web.Compilation;

internal class GlobalAsaxCompiler : BaseCompiler
{
	private ApplicationFileParser parser;

	private static ArrayList applicationObjectTags = new ArrayList(1);

	private static ArrayList sessionObjectTags = new ArrayList(1);

	internal static ArrayList ApplicationObjects => applicationObjectTags;

	internal static ArrayList SessionObjects => sessionObjectTags;

	public GlobalAsaxCompiler(ApplicationFileParser parser)
		: base(parser)
	{
		applicationObjectTags.Clear();
		sessionObjectTags.Clear();
		this.parser = parser;
	}

	public static Type CompileApplicationType(ApplicationFileParser parser)
	{
		return new AspGenerator(parser).GetCompiledType();
	}

	protected internal override void CreateMethods()
	{
		base.CreateMethods();
		CreateProfileProperty();
		ProcessObjects(parser.RootBuilder);
	}

	private void ProcessObjects(ControlBuilder builder)
	{
		if (builder.Children == null)
		{
			return;
		}
		foreach (object child in builder.Children)
		{
			if (!(child is ObjectTagBuilder))
			{
				continue;
			}
			ObjectTagBuilder objectTagBuilder = (ObjectTagBuilder)child;
			if (objectTagBuilder.Scope == null)
			{
				string fieldName = CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
				CreatePropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, fieldName, isPublic: true);
				continue;
			}
			if (string.Compare(objectTagBuilder.Scope, "session", ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				sessionObjectTags.Add(objectTagBuilder);
				CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, isApplication: false, isPublic: false);
				continue;
			}
			if (string.Compare(objectTagBuilder.Scope, "application", ignoreCase: true, Helpers.InvariantCulture) == 0)
			{
				applicationObjectTags.Add(objectTagBuilder);
				CreateFieldForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID);
				CreateApplicationOrSessionPropertyForObject(objectTagBuilder.Type, objectTagBuilder.ObjectID, isApplication: true, isPublic: false);
				continue;
			}
			throw new ParseException(objectTagBuilder.Location, "Invalid scope: " + objectTagBuilder.Scope);
		}
	}
}
