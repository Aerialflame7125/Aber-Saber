namespace System.Web.Services.Protocols;

internal class HttpServerMethod
{
	internal string name;

	internal LogicalMethodInfo methodInfo;

	internal Type[] readerTypes;

	internal object[] readerInitializers;

	internal Type writerType;

	internal object writerInitializer;
}
