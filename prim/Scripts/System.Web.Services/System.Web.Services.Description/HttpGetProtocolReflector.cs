using System.Web.Services.Protocols;

namespace System.Web.Services.Description;

internal class HttpGetProtocolReflector : HttpProtocolReflector
{
	public override string ProtocolName => "HttpGet";

	protected override void BeginClass()
	{
		if (base.IsEmptyBinding)
		{
			return;
		}
		HttpBinding httpBinding = new HttpBinding();
		httpBinding.Verb = "GET";
		base.Binding.Extensions.Add(httpBinding);
		HttpAddressBinding httpAddressBinding = new HttpAddressBinding();
		httpAddressBinding.Location = base.ServiceUrl;
		if (base.UriFixups != null)
		{
			base.UriFixups.Add(delegate(Uri current)
			{
				httpAddressBinding.Location = DiscoveryServerType.CombineUris(current, httpAddressBinding.Location);
			});
		}
		base.Port.Extensions.Add(httpAddressBinding);
	}

	protected override bool ReflectMethod()
	{
		if (!ReflectUrlParameters())
		{
			return false;
		}
		if (!ReflectMimeReturn())
		{
			return false;
		}
		HttpOperationBinding httpOperationBinding = new HttpOperationBinding();
		httpOperationBinding.Location = base.MethodUrl;
		base.OperationBinding.Extensions.Add(httpOperationBinding);
		return true;
	}
}
