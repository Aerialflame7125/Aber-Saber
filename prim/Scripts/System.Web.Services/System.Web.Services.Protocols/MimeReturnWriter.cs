using System.IO;

namespace System.Web.Services.Protocols;

internal abstract class MimeReturnWriter : MimeFormatter
{
	internal abstract void Write(HttpResponse response, Stream outputStream, object returnValue);
}
