using System.Collections;
using System.Reflection;
using System.Text;

namespace System.Web.UI;

internal sealed class CollectionBuilder : ControlBuilder
{
	private Type[] possibleElementTypes;

	internal CollectionBuilder()
	{
	}

	public override void AppendLiteralString(string s)
	{
		if (s != null && s.Trim().Length > 0)
		{
			throw new HttpException("Literal content not allowed for " + base.ControlType);
		}
	}

	public override Type GetChildControlType(string tagName, IDictionary attribs)
	{
		Type childControlType = base.Root.GetChildControlType(tagName, attribs);
		if (possibleElementTypes != null)
		{
			bool flag = false;
			for (int i = 0; i < possibleElementTypes.Length; i++)
			{
				if (flag)
				{
					break;
				}
				flag = possibleElementTypes[i].IsAssignableFrom(childControlType);
			}
			if (!flag)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int j = 0; j < possibleElementTypes.Length; j++)
				{
					if (j != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(possibleElementTypes[j]);
				}
				throw new HttpException(string.Concat("Cannot add a ", childControlType, " to ", stringBuilder));
			}
		}
		return childControlType;
	}

	public override void Init(TemplateParser parser, ControlBuilder parentBuilder, Type type, string tagName, string id, IDictionary attribs)
	{
		base.Init(parser, parentBuilder, type, tagName, id, attribs);
		PropertyInfo property = parentBuilder.ControlType.GetProperty(tagName, ControlBuilder.FlagsNoCase);
		SetControlType(property.PropertyType);
		MemberInfo[] member = base.ControlType.GetMember("Item", MemberTypes.Property, ControlBuilder.FlagsNoCase & ~BindingFlags.IgnoreCase);
		if (member.Length != 0)
		{
			possibleElementTypes = new Type[member.Length];
			for (int i = 0; i < member.Length; i++)
			{
				possibleElementTypes[i] = ((PropertyInfo)member[i]).PropertyType;
			}
			return;
		}
		throw new HttpException(string.Concat("Collection of type '", base.ControlType, "' does not have an indexer."));
	}
}
