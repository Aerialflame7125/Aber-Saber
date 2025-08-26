using System.IO;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class XmlReturnWriter : MimeReturnWriter
{
	private XmlSerializer xmlSerializer;

	public override void Initialize(object o)
	{
		xmlSerializer = (XmlSerializer)o;
	}

	public override object[] GetInitializers(LogicalMethodInfo[] methodInfos)
	{
		return XmlReturn.GetInitializers(methodInfos);
	}

	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return XmlReturn.GetInitializer(methodInfo);
	}

	internal override void Write(HttpResponse response, Stream outputStream, object returnValue)
	{
		Encoding encoding = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
		response.ContentType = ContentType.Compose("text/xml", encoding);
		StreamWriter streamWriter = new StreamWriter(outputStream, encoding);
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "Write") : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceWriteResponse"), caller, new TraceMethod(xmlSerializer, "Serialize", streamWriter, returnValue));
		}
		xmlSerializer.Serialize(streamWriter, returnValue);
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceWriteResponse"), caller);
		}
	}
}
