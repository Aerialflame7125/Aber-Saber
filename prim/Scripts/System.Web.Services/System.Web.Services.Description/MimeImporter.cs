using System.CodeDom;

namespace System.Web.Services.Description;

internal abstract class MimeImporter
{
	private HttpProtocolImporter protocol;

	internal HttpProtocolImporter ImportContext
	{
		get
		{
			return protocol;
		}
		set
		{
			protocol = value;
		}
	}

	internal abstract MimeParameterCollection ImportParameters();

	internal abstract MimeReturn ImportReturn();

	internal virtual void GenerateCode(MimeReturn[] importedReturns, MimeParameterCollection[] importedParameters)
	{
	}

	internal virtual void AddClassMetadata(CodeTypeDeclaration codeClass)
	{
	}
}
