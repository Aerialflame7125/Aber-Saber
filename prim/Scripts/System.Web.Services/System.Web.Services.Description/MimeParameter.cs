using System.CodeDom;

namespace System.Web.Services.Description;

internal class MimeParameter
{
	private string name;

	private string typeName;

	private CodeAttributeDeclarationCollection attrs;

	internal string Name
	{
		get
		{
			if (name != null)
			{
				return name;
			}
			return string.Empty;
		}
		set
		{
			name = value;
		}
	}

	internal string TypeName
	{
		get
		{
			if (typeName != null)
			{
				return typeName;
			}
			return string.Empty;
		}
		set
		{
			typeName = value;
		}
	}

	internal CodeAttributeDeclarationCollection Attributes
	{
		get
		{
			if (attrs == null)
			{
				attrs = new CodeAttributeDeclarationCollection();
			}
			return attrs;
		}
	}
}
