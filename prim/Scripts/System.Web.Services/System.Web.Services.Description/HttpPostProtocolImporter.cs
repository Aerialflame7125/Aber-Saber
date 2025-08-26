using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class HttpPostProtocolImporter : HttpProtocolImporter
{
	public override string ProtocolName => "HttpPost";

	internal override Type BaseClass
	{
		get
		{
			if (base.Style == ServiceDescriptionImportStyle.Client)
			{
				return typeof(HttpPostClientProtocol);
			}
			return typeof(WebService);
		}
	}

	public HttpPostProtocolImporter()
		: base(hasInputPayload: true)
	{
	}

	protected override bool IsBindingSupported()
	{
		HttpBinding httpBinding = (HttpBinding)base.Binding.Extensions.Find(typeof(HttpBinding));
		if (httpBinding == null)
		{
			return false;
		}
		if (httpBinding.Verb != "POST")
		{
			return false;
		}
		return true;
	}
}
