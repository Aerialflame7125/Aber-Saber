using System.Collections;
using System.Web.Services.Protocols;
using System.Xml;

namespace System.Web.Services.Description;

internal class Soap12ProtocolReflector : SoapProtocolReflector
{
	private Hashtable requestElements;

	private Hashtable actions;

	private XmlQualifiedName soap11PortType;

	internal override WsiProfiles ConformsTo => WsiProfiles.None;

	public override string ProtocolName => "Soap12";

	protected override void BeginClass()
	{
		requestElements = new Hashtable();
		actions = new Hashtable();
		soap11PortType = null;
		base.BeginClass();
	}

	protected override bool ReflectMethod()
	{
		if (base.ReflectMethod())
		{
			if (base.Binding != null)
			{
				soap11PortType = base.SoapMethod.portType;
				if (soap11PortType != base.Binding.Type)
				{
					base.HeaderMessages.Clear();
				}
			}
			return true;
		}
		return false;
	}

	protected override void EndClass()
	{
		if (base.PortType == null || base.Binding == null || !(soap11PortType != null) || !(soap11PortType != base.Binding.Type))
		{
			return;
		}
		foreach (Operation operation in base.PortType.Operations)
		{
			foreach (OperationMessage message2 in operation.Messages)
			{
				ServiceDescription serviceDescription = GetServiceDescription(message2.Message.Namespace);
				if (serviceDescription != null)
				{
					Message message = serviceDescription.Messages[message2.Message.Name];
					if (message != null)
					{
						serviceDescription.Messages.Remove(message);
					}
				}
			}
		}
		base.Binding.Type = soap11PortType;
		base.PortType.ServiceDescription.PortTypes.Remove(base.PortType);
	}

	protected override SoapBinding CreateSoapBinding(SoapBindingStyle style)
	{
		return new Soap12Binding
		{
			Transport = "http://schemas.xmlsoap.org/soap/http",
			Style = style
		};
	}

	protected override SoapAddressBinding CreateSoapAddressBinding(string serviceUrl)
	{
		Soap12AddressBinding soapAddress = new Soap12AddressBinding();
		soapAddress.Location = serviceUrl;
		if (base.UriFixups != null)
		{
			base.UriFixups.Add(delegate(Uri current)
			{
				soapAddress.Location = DiscoveryServerType.CombineUris(current, soapAddress.Location);
			});
		}
		return soapAddress;
	}

	protected override SoapOperationBinding CreateSoapOperationBinding(SoapBindingStyle style, string action)
	{
		Soap12OperationBinding soap12OperationBinding = new Soap12OperationBinding();
		soap12OperationBinding.SoapAction = action;
		soap12OperationBinding.Style = style;
		soap12OperationBinding.Method = base.SoapMethod;
		DealWithAmbiguity(action, base.SoapMethod.requestElementName.ToString(), soap12OperationBinding);
		return soap12OperationBinding;
	}

	protected override SoapBodyBinding CreateSoapBodyBinding(SoapBindingUse use, string ns)
	{
		Soap12BodyBinding soap12BodyBinding = new Soap12BodyBinding();
		soap12BodyBinding.Use = use;
		if (use == SoapBindingUse.Encoded)
		{
			soap12BodyBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
		}
		soap12BodyBinding.Namespace = ns;
		return soap12BodyBinding;
	}

	protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, SoapBindingUse use)
	{
		return CreateSoapHeaderBinding(message, partName, null, use);
	}

	protected override SoapHeaderBinding CreateSoapHeaderBinding(XmlQualifiedName message, string partName, string ns, SoapBindingUse use)
	{
		Soap12HeaderBinding soap12HeaderBinding = new Soap12HeaderBinding();
		soap12HeaderBinding.Message = message;
		soap12HeaderBinding.Part = partName;
		soap12HeaderBinding.Namespace = ns;
		soap12HeaderBinding.Use = use;
		if (use == SoapBindingUse.Encoded)
		{
			soap12HeaderBinding.Encoding = "http://www.w3.org/2003/05/soap-encoding";
		}
		return soap12HeaderBinding;
	}

	private void DealWithAmbiguity(string action, string requestElement, Soap12OperationBinding operation)
	{
		Soap12OperationBinding soap12OperationBinding = (Soap12OperationBinding)actions[action];
		if (soap12OperationBinding != null)
		{
			operation.DuplicateBySoapAction = soap12OperationBinding;
			soap12OperationBinding.DuplicateBySoapAction = operation;
			CheckOperationDuplicates(soap12OperationBinding);
		}
		else
		{
			actions[action] = operation;
		}
		Soap12OperationBinding soap12OperationBinding2 = (Soap12OperationBinding)requestElements[requestElement];
		if (soap12OperationBinding2 != null)
		{
			operation.DuplicateByRequestElement = soap12OperationBinding2;
			soap12OperationBinding2.DuplicateByRequestElement = operation;
			CheckOperationDuplicates(soap12OperationBinding2);
		}
		else
		{
			requestElements[requestElement] = operation;
		}
		CheckOperationDuplicates(operation);
	}

	private void CheckOperationDuplicates(Soap12OperationBinding operation)
	{
		if (operation.DuplicateByRequestElement != null)
		{
			if (operation.DuplicateBySoapAction != null)
			{
				throw new InvalidOperationException(Res.GetString("TheMethodsAndUseTheSameRequestElementAndSoapActionXmlns6", operation.Method.name, operation.DuplicateByRequestElement.Method.name, operation.Method.requestElementName.Name, operation.Method.requestElementName.Namespace, operation.DuplicateBySoapAction.Method.name, operation.Method.action));
			}
			operation.SoapActionRequired = true;
		}
		else
		{
			operation.SoapActionRequired = false;
		}
	}
}
