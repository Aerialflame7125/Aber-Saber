using System.IO;

namespace System.Web.Services.Protocols;

internal class UnsupportedRequestProtocol : ServerProtocol
{
	private int httpCode;

	internal int HttpCode => httpCode;

	internal override bool IsOneWay => false;

	internal override LogicalMethodInfo MethodInfo => null;

	internal override ServerType ServerType => null;

	internal UnsupportedRequestProtocol(int httpCode)
	{
		this.httpCode = httpCode;
	}

	internal override bool Initialize()
	{
		return true;
	}

	internal override object[] ReadParameters()
	{
		return new object[0];
	}

	internal override void WriteReturns(object[] returnValues, Stream outputStream)
	{
	}

	internal override bool WriteException(Exception e, Stream outputStream)
	{
		return false;
	}
}
