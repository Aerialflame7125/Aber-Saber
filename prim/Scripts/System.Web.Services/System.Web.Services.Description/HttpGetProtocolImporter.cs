using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class HttpGetProtocolImporter : HttpProtocolImporter
{
	public override string ProtocolName => "HttpGet";

	internal override Type BaseClass
	{
		get
		{
			if (base.Style == ServiceDescriptionImportStyle.Client)
			{
				return typeof(HttpGetClientProtocol);
			}
			return typeof(WebService);
		}
	}

	public HttpGetProtocolImporter()
		: base(hasInputPayload: false)
	{
	}

	protected override bool IsBindingSupported()
	{
		HttpBinding httpBinding = (HttpBinding)base.Binding.Extensions.Find(typeof(HttpBinding));
		if (httpBinding == null)
		{
			return false;
		}
		if (httpBinding.Verb != "GET")
		{
			return false;
		}
		return true;
	}
}
