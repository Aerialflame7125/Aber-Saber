using System.CodeDom;

namespace System.Web.Services.Description;

internal class MimeReturn
{
	private string typeName;

	private Type readerType;

	private CodeAttributeDeclarationCollection attrs;

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

	internal Type ReaderType
	{
		get
		{
			return readerType;
		}
		set
		{
			readerType = value;
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
