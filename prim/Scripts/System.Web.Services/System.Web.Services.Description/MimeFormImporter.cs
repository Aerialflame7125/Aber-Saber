using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class MimeFormImporter : MimeImporter
{
	internal override MimeParameterCollection ImportParameters()
	{
		MimeContentBinding mimeContentBinding = (MimeContentBinding)base.ImportContext.OperationBinding.Input.Extensions.Find(typeof(MimeContentBinding));
		if (mimeContentBinding == null)
		{
			return null;
		}
		if (string.Compare(mimeContentBinding.Type, "application/x-www-form-urlencoded", StringComparison.OrdinalIgnoreCase) != 0)
		{
			return null;
		}
		MimeParameterCollection mimeParameterCollection = base.ImportContext.ImportStringParametersMessage();
		if (mimeParameterCollection == null)
		{
			return null;
		}
		mimeParameterCollection.WriterType = typeof(HtmlFormParameterWriter);
		return mimeParameterCollection;
	}

	internal override MimeReturn ImportReturn()
	{
		return null;
	}
}
