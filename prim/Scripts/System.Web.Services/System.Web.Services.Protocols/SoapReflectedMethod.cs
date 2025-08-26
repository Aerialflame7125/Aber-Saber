using System.Web.Services.Description;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class SoapReflectedMethod
{
	internal LogicalMethodInfo methodInfo;

	internal string action;

	internal string name;

	internal XmlMembersMapping requestMappings;

	internal XmlMembersMapping responseMappings;

	internal XmlMembersMapping inHeaderMappings;

	internal XmlMembersMapping outHeaderMappings;

	internal SoapReflectedHeader[] headers;

	internal SoapReflectedExtension[] extensions;

	internal bool oneWay;

	internal bool rpc;

	internal SoapBindingUse use;

	internal SoapParameterStyle paramStyle;

	internal WebServiceBindingAttribute binding;

	internal XmlQualifiedName requestElementName;

	internal XmlQualifiedName portType;

	internal bool IsClaimsConformance
	{
		get
		{
			if (binding != null)
			{
				return binding.ConformsTo == WsiProfiles.BasicProfile1_1;
			}
			return false;
		}
	}
}
