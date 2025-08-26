using System.IO;
using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class MimeAnyImporter : MimeImporter
{
	internal override MimeParameterCollection ImportParameters()
	{
		return null;
	}

	internal override MimeReturn ImportReturn()
	{
		if (base.ImportContext.OperationBinding.Output.Extensions.Count == 0)
		{
			return null;
		}
		return new MimeReturn
		{
			TypeName = typeof(Stream).FullName,
			ReaderType = typeof(AnyReturnReader)
		};
	}
}
