namespace System.Web.Services.Protocols;

internal class HttpClientMethod
{
	internal Type readerType;

	internal object readerInitializer;

	internal Type writerType;

	internal object writerInitializer;

	internal LogicalMethodInfo methodInfo;
}
