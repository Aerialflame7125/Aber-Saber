using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class MimeFormReflector : MimeReflector
{
	internal override bool ReflectParameters()
	{
		if (!ValueCollectionParameterReader.IsSupported(base.ReflectionContext.Method))
		{
			return false;
		}
		base.ReflectionContext.ReflectStringParametersMessage();
		MimeContentBinding mimeContentBinding = new MimeContentBinding();
		mimeContentBinding.Type = "application/x-www-form-urlencoded";
		base.ReflectionContext.OperationBinding.Input.Extensions.Add(mimeContentBinding);
		return true;
	}

	internal override bool ReflectReturn()
	{
		return false;
	}
}
