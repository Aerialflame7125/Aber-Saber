using System.IO;
using System.Reflection;
using System.Text;
using System.Web.Services.Configuration;

namespace System.Web.Services.Protocols;

internal abstract class HttpServerProtocol : ServerProtocol
{
	private HttpServerMethod serverMethod;

	private HttpServerType serverType;

	private bool hasInputPayload;

	internal override bool IsOneWay => false;

	internal override LogicalMethodInfo MethodInfo => serverMethod.methodInfo;

	internal override ServerType ServerType => serverType;

	protected HttpServerProtocol(bool hasInputPayload)
	{
		this.hasInputPayload = hasInputPayload;
	}

	internal override bool Initialize()
	{
		string text = base.Request.PathInfo.Substring(1);
		if ((serverType = (HttpServerType)GetFromCache(typeof(HttpServerProtocol), base.Type)) == null && (serverType = (HttpServerType)GetFromCache(typeof(HttpServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
		{
			lock (ServerProtocol.InternalSyncObject)
			{
				if ((serverType = (HttpServerType)GetFromCache(typeof(HttpServerProtocol), base.Type)) == null && (serverType = (HttpServerType)GetFromCache(typeof(HttpServerProtocol), base.Type, excludeSchemeHostPort: true)) == null)
				{
					bool excludeSchemeHostPort = IsCacheUnderPressure(typeof(HttpServerProtocol), base.Type);
					serverType = new HttpServerType(base.Type);
					AddToCache(typeof(HttpServerProtocol), base.Type, serverType, excludeSchemeHostPort);
				}
			}
		}
		serverMethod = serverType.GetMethod(text);
		if (serverMethod == null)
		{
			serverMethod = serverType.GetMethodIgnoreCase(text);
			if (serverMethod != null)
			{
				throw new ArgumentException(Res.GetString("WebInvalidMethodNameCase", text, serverMethod.name), "methodName");
			}
			string @string = Encoding.UTF8.GetString(Encoding.Default.GetBytes(text));
			serverMethod = serverType.GetMethod(@string);
			if (serverMethod == null)
			{
				throw new InvalidOperationException(Res.GetString("WebInvalidMethodName", text));
			}
		}
		return true;
	}

	internal override object[] ReadParameters()
	{
		if (serverMethod.readerTypes == null)
		{
			return new object[0];
		}
		for (int i = 0; i < serverMethod.readerTypes.Length; i++)
		{
			if (!hasInputPayload)
			{
				if (serverMethod.readerTypes[i] != typeof(UrlParameterReader))
				{
					continue;
				}
			}
			else if (serverMethod.readerTypes[i] == typeof(UrlParameterReader))
			{
				continue;
			}
			object[] array = ((MimeParameterReader)MimeFormatter.CreateInstance(serverMethod.readerTypes[i], serverMethod.readerInitializers[i])).Read(base.Request);
			if (array != null)
			{
				return array;
			}
		}
		if (!hasInputPayload)
		{
			throw new InvalidOperationException(Res.GetString("WebInvalidRequestFormat"));
		}
		throw new InvalidOperationException(Res.GetString("WebInvalidRequestFormatDetails", base.Request.ContentType));
	}

	internal override void WriteReturns(object[] returnValues, Stream outputStream)
	{
		if (!(serverMethod.writerType == null))
		{
			((MimeReturnWriter)MimeFormatter.CreateInstance(serverMethod.writerType, serverMethod.writerInitializer)).Write(base.Response, outputStream, returnValues[0]);
		}
	}

	internal override bool WriteException(Exception e, Stream outputStream)
	{
		base.Response.Clear();
		base.Response.ClearHeaders();
		base.Response.ContentType = ContentType.Compose("text/plain", Encoding.UTF8);
		ServerProtocol.SetHttpResponseStatusCode(base.Response, 500);
		base.Response.StatusDescription = HttpWorkerRequest.GetStatusDescription(base.Response.StatusCode);
		StreamWriter streamWriter = new StreamWriter(outputStream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		if (WebServicesSection.Current.Diagnostics.SuppressReturningExceptions)
		{
			streamWriter.WriteLine(Res.GetString("WebSuppressedExceptionMessage"));
		}
		else
		{
			streamWriter.WriteLine(GenerateFaultString(e, htmlEscapeMessage: true));
		}
		streamWriter.Flush();
		return true;
	}

	internal static bool AreUrlParametersSupported(LogicalMethodInfo methodInfo)
	{
		if (methodInfo.OutParameters.Length != 0)
		{
			return false;
		}
		ParameterInfo[] inParameters = methodInfo.InParameters;
		for (int i = 0; i < inParameters.Length; i++)
		{
			Type parameterType = inParameters[i].ParameterType;
			if (parameterType.IsArray)
			{
				if (!ScalarFormatter.IsTypeSupported(parameterType.GetElementType()))
				{
					return false;
				}
			}
			else if (!ScalarFormatter.IsTypeSupported(parameterType))
			{
				return false;
			}
		}
		return true;
	}
}
