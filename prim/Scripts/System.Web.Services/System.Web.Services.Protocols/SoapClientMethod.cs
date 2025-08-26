using System.Web.Services.Description;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class SoapClientMethod
{
	internal XmlSerializer returnSerializer;

	internal XmlSerializer parameterSerializer;

	internal XmlSerializer inHeaderSerializer;

	internal XmlSerializer outHeaderSerializer;

	internal string action;

	internal LogicalMethodInfo methodInfo;

	internal SoapHeaderMapping[] inHeaderMappings;

	internal SoapHeaderMapping[] outHeaderMappings;

	internal SoapReflectedExtension[] extensions;

	internal object[] extensionInitializers;

	internal bool oneWay;

	internal bool rpc;

	internal SoapBindingUse use;

	internal SoapParameterStyle paramStyle;
}
