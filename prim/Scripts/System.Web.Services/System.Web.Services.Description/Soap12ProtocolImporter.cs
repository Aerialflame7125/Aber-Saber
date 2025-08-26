using System.Security.Permissions;

namespace System.Web.Services.Description;

[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
internal class Soap12ProtocolImporter : SoapProtocolImporter
{
	public override string ProtocolName => "Soap12";

	protected override bool IsBindingSupported()
	{
		Soap12Binding soap12Binding = (Soap12Binding)base.Binding.Extensions.Find(typeof(Soap12Binding));
		if (soap12Binding == null)
		{
			return false;
		}
		if (GetTransport(soap12Binding.Transport) == null)
		{
			UnsupportedBindingWarning(Res.GetString("ThereIsNoSoapTransportImporterThatUnderstands1", soap12Binding.Transport));
			return false;
		}
		return true;
	}

	protected override bool IsSoapEncodingPresent(string uriList)
	{
		int num = 0;
		do
		{
			num = uriList.IndexOf("http://www.w3.org/2003/05/soap-encoding", num, StringComparison.Ordinal);
			if (num < 0)
			{
				break;
			}
			int num2 = num + "http://www.w3.org/2003/05/soap-encoding".Length;
			if ((num == 0 || uriList[num - 1] == ' ') && (num2 == uriList.Length || uriList[num2] == ' '))
			{
				return true;
			}
			num = num2;
		}
		while (num < uriList.Length);
		if (base.IsSoapEncodingPresent(uriList))
		{
			UnsupportedOperationBindingWarning(Res.GetString("WebSoap11EncodingStyleNotSupported1", "http://www.w3.org/2003/05/soap-encoding"));
		}
		return false;
	}
}
