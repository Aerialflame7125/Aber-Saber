namespace System.Web.Services.Description;

internal abstract class MimeReflector
{
	private HttpProtocolReflector protocol;

	internal HttpProtocolReflector ReflectionContext
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

	internal abstract bool ReflectParameters();

	internal abstract bool ReflectReturn();
}
