using System.Collections;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

/// <summary>The <see cref="T:System.Web.Services.Description.WebServicesInteroperability" /> class provides methods to verify whether a given Web service or services conforms to a given Web Services Interoperability (WS-I) Organization specification.</summary>
public sealed class WebServicesInteroperability
{
	private WebServicesInteroperability()
	{
	}

	/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
	/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
	/// <param name="description">A <see cref="T:System.Web.Services.Description.ServiceDescription" /> that describes a Web service.</param>
	/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
	/// <returns>
	///     <see langword="true" /> if the Web service described by <paramref name="description" /> conforms to the Web services interoperability specification indicated by <paramref name="claims" />; otherwise<see langword=" false" />.</returns>
	public static bool CheckConformance(WsiProfiles claims, ServiceDescription description, BasicProfileViolationCollection violations)
	{
		if (description == null)
		{
			throw new ArgumentNullException("description");
		}
		ServiceDescriptionCollection serviceDescriptionCollection = new ServiceDescriptionCollection();
		serviceDescriptionCollection.Add(description);
		return CheckConformance(claims, serviceDescriptionCollection, violations);
	}

	/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
	/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
	/// <param name="descriptions">A <see cref="T:System.Web.Services.Description.ServiceDescriptionCollection" /> that contains Web service descriptions.</param>
	/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
	/// <returns>
	///     <see langword="true" /> if the Web service descriptions contained in <paramref name="descriptions" /> conform to the Web services interoperability specification indicated by <paramref name="claims" />; <see langword="false" /> otherwise.</returns>
	public static bool CheckConformance(WsiProfiles claims, ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
	{
		if ((claims & WsiProfiles.BasicProfile1_1) == 0)
		{
			return true;
		}
		if (descriptions == null)
		{
			throw new ArgumentNullException("descriptions");
		}
		if (violations == null)
		{
			throw new ArgumentNullException("violations");
		}
		int count = violations.Count;
		AnalyzeDescription(descriptions, violations);
		return count == violations.Count;
	}

	/// <summary>Verifies whether a given Web service or services conforms to a given WS-I specification, and provides a list of any violations that it finds.</summary>
	/// <param name="claims">A member of <see cref="T:System.Web.Services.WsiProfiles" /> that indicates a Web services interoperability specification.</param>
	/// <param name="webReference">A <see cref="T:System.Web.Services.Description.WebReference" /> that describes a Web service.</param>
	/// <param name="violations">A <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> that contains any violations that were found.</param>
	/// <returns>
	///     <see langword="true" /> if the Web service described by <paramref name="webReference" /> conforms to the Web services interoperability specification indicated by <paramref name="claims" />; <see langword="false" /> otherwise.</returns>
	public static bool CheckConformance(WsiProfiles claims, WebReference webReference, BasicProfileViolationCollection violations)
	{
		if ((claims & WsiProfiles.BasicProfile1_1) == 0)
		{
			return true;
		}
		if (webReference == null)
		{
			return true;
		}
		if (violations == null)
		{
			throw new ArgumentNullException("violations");
		}
		XmlSchemas schemas = new XmlSchemas();
		ServiceDescriptionCollection descriptions = new ServiceDescriptionCollection();
		StringCollection warnings = new StringCollection();
		foreach (DictionaryEntry document in webReference.Documents)
		{
			ServiceDescriptionImporter.AddDocument((string)document.Key, document.Value, schemas, descriptions, warnings);
		}
		int count = violations.Count;
		AnalyzeDescription(descriptions, violations);
		return count == violations.Count;
	}

	internal static bool AnalyzeBinding(Binding binding, ServiceDescription description, ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
	{
		bool flag = false;
		bool flag2 = false;
		SoapBinding soapBinding = (SoapBinding)binding.Extensions.Find(typeof(SoapBinding));
		if (soapBinding == null || soapBinding.GetType() != typeof(SoapBinding))
		{
			return false;
		}
		SoapBindingStyle soapBindingStyle = ((soapBinding.Style == SoapBindingStyle.Default) ? SoapBindingStyle.Document : soapBinding.Style);
		if (soapBinding.Transport.Length == 0)
		{
			violations.Add("R2701", Res.GetString("BindingMissingAttribute", binding.Name, description.TargetNamespace, "transport"));
		}
		else if (soapBinding.Transport != "http://schemas.xmlsoap.org/soap/http")
		{
			violations.Add("R2702", Res.GetString("BindingInvalidAttribute", binding.Name, description.TargetNamespace, "transport", soapBinding.Transport));
		}
		PortType portType = descriptions.GetPortType(binding.Type);
		Hashtable hashtable = new Hashtable();
		if (portType != null)
		{
			foreach (Operation operation3 in portType.Operations)
			{
				if (operation3.Messages.Flow == OperationFlow.Notification)
				{
					violations.Add("R2303", Res.GetString("OperationFlowNotification", operation3.Name, binding.Type.Namespace, binding.Type.Namespace));
				}
				if (operation3.Messages.Flow == OperationFlow.SolicitResponse)
				{
					violations.Add("R2303", Res.GetString("OperationFlowSolicitResponse", operation3.Name, binding.Type.Namespace, binding.Type.Namespace));
				}
				if (hashtable[operation3.Name] != null)
				{
					violations.Add("R2304", Res.GetString("Operation", operation3.Name, binding.Type.Name, binding.Type.Namespace));
					continue;
				}
				OperationBinding operationBinding = null;
				foreach (OperationBinding operation4 in binding.Operations)
				{
					if (operation3.IsBoundBy(operation4))
					{
						if (operationBinding != null)
						{
							violations.Add("R2304", Res.GetString("OperationBinding", operationBinding.Name, operationBinding.Parent.Name, description.TargetNamespace));
						}
						operationBinding = operation4;
					}
				}
				if (operationBinding == null)
				{
					violations.Add("R2718", Res.GetString("OperationMissingBinding", operation3.Name, binding.Type.Name, binding.Type.Namespace));
				}
				else
				{
					hashtable.Add(operation3.Name, operation3);
				}
			}
		}
		Hashtable wireSignatures = new Hashtable();
		SoapBindingStyle soapBindingStyle2 = SoapBindingStyle.Default;
		foreach (OperationBinding operation5 in binding.Operations)
		{
			SoapBindingStyle soapBindingStyle3 = soapBindingStyle;
			string name = operation5.Name;
			if (name == null)
			{
				continue;
			}
			if (hashtable[name] == null)
			{
				violations.Add("R2718", Res.GetString("PortTypeOperationMissing", operation5.Name, binding.Name, description.TargetNamespace, binding.Type.Name, binding.Type.Namespace));
			}
			Operation operation2 = FindOperation(portType.Operations, operation5);
			SoapOperationBinding soapOperationBinding = (SoapOperationBinding)operation5.Extensions.Find(typeof(SoapOperationBinding));
			if (soapOperationBinding != null)
			{
				if (soapBindingStyle2 == SoapBindingStyle.Default)
				{
					soapBindingStyle2 = soapOperationBinding.Style;
				}
				flag |= soapBindingStyle2 != soapOperationBinding.Style;
				soapBindingStyle3 = ((soapOperationBinding.Style != 0) ? soapOperationBinding.Style : soapBindingStyle);
			}
			if (operation5.Input != null)
			{
				SoapBodyBinding soapBodyBinding = FindSoapBodyBinding(input: true, operation5.Input.Extensions, violations, soapBindingStyle3 == SoapBindingStyle.Document, operation5.Name, binding.Name, description.TargetNamespace);
				if (soapBodyBinding != null && soapBodyBinding.Use != SoapBindingUse.Encoded)
				{
					Message message = ((operation2 == null) ? null : ((operation2.Messages.Input == null) ? null : descriptions.GetMessage(operation2.Messages.Input.Message)));
					if (soapBindingStyle3 == SoapBindingStyle.Rpc)
					{
						CheckMessageParts(message, soapBodyBinding.Parts, element: false, operation5.Name, binding.Name, description.TargetNamespace, wireSignatures, violations);
					}
					else
					{
						flag2 = flag2 || (soapBodyBinding.Parts != null && soapBodyBinding.Parts.Length > 1);
						int num = ((soapBodyBinding.Parts != null) ? soapBodyBinding.Parts.Length : 0);
						CheckMessageParts(message, soapBodyBinding.Parts, element: true, operation5.Name, binding.Name, description.TargetNamespace, wireSignatures, violations);
						if (num == 0 && message != null && message.Parts.Count > 1)
						{
							violations.Add("R2210", Res.GetString("OperationBinding", operation5.Name, binding.Name, description.TargetNamespace));
						}
					}
				}
			}
			if (operation5.Output != null)
			{
				SoapBodyBinding soapBodyBinding2 = FindSoapBodyBinding(input: false, operation5.Output.Extensions, violations, soapBindingStyle3 == SoapBindingStyle.Document, operation5.Name, binding.Name, description.TargetNamespace);
				if (soapBodyBinding2 != null && soapBodyBinding2.Use != SoapBindingUse.Encoded)
				{
					Message message2 = ((operation2 == null) ? null : ((operation2.Messages.Output == null) ? null : descriptions.GetMessage(operation2.Messages.Output.Message)));
					if (soapBindingStyle3 == SoapBindingStyle.Rpc)
					{
						CheckMessageParts(message2, soapBodyBinding2.Parts, element: false, operation5.Name, binding.Name, description.TargetNamespace, null, violations);
					}
					else
					{
						flag2 = flag2 || (soapBodyBinding2.Parts != null && soapBodyBinding2.Parts.Length > 1);
						int num2 = ((soapBodyBinding2.Parts != null) ? soapBodyBinding2.Parts.Length : 0);
						CheckMessageParts(message2, soapBodyBinding2.Parts, element: true, operation5.Name, binding.Name, description.TargetNamespace, null, violations);
						if (num2 == 0 && message2 != null && message2.Parts.Count > 1)
						{
							violations.Add("R2210", Res.GetString("OperationBinding", operation5.Name, binding.Name, description.TargetNamespace));
						}
					}
				}
			}
			foreach (FaultBinding fault in operation5.Faults)
			{
				foreach (ServiceDescriptionFormatExtension extension in fault.Extensions)
				{
					if (!(extension is SoapFaultBinding))
					{
						continue;
					}
					SoapFaultBinding soapFaultBinding = (SoapFaultBinding)extension;
					if (soapFaultBinding.Use == SoapBindingUse.Encoded)
					{
						violations.Add("R2706", MessageString(soapFaultBinding, operation5.Name, binding.Name, description.TargetNamespace, input: false, null));
						continue;
					}
					if (soapFaultBinding.Name == null || soapFaultBinding.Name.Length == 0)
					{
						violations.Add("R2721", Res.GetString("FaultBinding", fault.Name, operation5.Name, binding.Name, description.TargetNamespace));
					}
					else if (soapFaultBinding.Name != fault.Name)
					{
						violations.Add("R2754", Res.GetString("FaultBinding", fault.Name, operation5.Name, binding.Name, description.TargetNamespace));
					}
					if (soapFaultBinding.Namespace != null && soapFaultBinding.Namespace.Length > 0)
					{
						violations.Add((soapBindingStyle3 == SoapBindingStyle.Document) ? "R2716" : "R2726", MessageString(soapFaultBinding, operation5.Name, binding.Name, description.TargetNamespace, input: false, null));
					}
				}
			}
			if (hashtable[operation5.Name] == null)
			{
				violations.Add("R2718", Res.GetString("PortTypeOperationMissing", operation5.Name, binding.Name, description.TargetNamespace, binding.Type.Name, binding.Type.Namespace));
			}
		}
		if (flag2)
		{
			violations.Add("R2201", Res.GetString("BindingMultipleParts", binding.Name, description.TargetNamespace, "parts"));
		}
		if (flag)
		{
			violations.Add("R2705", Res.GetString("Binding", binding.Name, description.TargetNamespace));
		}
		return true;
	}

	internal static void AnalyzeDescription(ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
	{
		bool flag = false;
		foreach (ServiceDescription description in descriptions)
		{
			SchemaCompiler.Compile(description.Types.Schemas);
			CheckWsdlImports(description, violations);
			CheckTypes(description, violations);
			StringEnumerator enumerator2 = description.ValidationWarnings.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					string current = enumerator2.Current;
					violations.Add("R2028, R2029", current);
				}
			}
			finally
			{
				if (enumerator2 is IDisposable disposable)
				{
					disposable.Dispose();
				}
			}
			foreach (Binding binding in description.Bindings)
			{
				flag |= AnalyzeBinding(binding, description, descriptions, violations);
			}
		}
		if (flag)
		{
			CheckExtensions(descriptions, violations);
		}
		else
		{
			violations.Add("Rxxxx");
		}
	}

	private static void CheckWsdlImports(ServiceDescription description, BasicProfileViolationCollection violations)
	{
		foreach (Import import in description.Imports)
		{
			if (import.Location == null || import.Location.Length == 0)
			{
				violations.Add("R2007", Res.GetString("Description", description.TargetNamespace));
			}
			string @namespace = import.Namespace;
			if (@namespace.Length != 0 && !Uri.TryCreate(@namespace, UriKind.Absolute, out var _))
			{
				violations.Add("R2803", Res.GetString("Description", description.TargetNamespace));
			}
		}
	}

	private static void CheckTypes(ServiceDescription description, BasicProfileViolationCollection violations)
	{
		foreach (XmlSchema schema in description.Types.Schemas)
		{
			if (schema.TargetNamespace != null && schema.TargetNamespace.Length != 0)
			{
				continue;
			}
			foreach (XmlSchemaObject item in schema.Items)
			{
				if (!(item is XmlSchemaAnnotation))
				{
					violations.Add("R2105", Res.GetString("Element", "schema", description.TargetNamespace));
					return;
				}
			}
		}
	}

	private static void CheckMessagePart(MessagePart part, bool element, string message, string operation, string binding, string ns, Hashtable wireSignatures, BasicProfileViolationCollection violations)
	{
		if (part == null)
		{
			if (!element)
			{
				AddSignature(wireSignatures, operation, ns, message, ns, violations);
			}
			else
			{
				AddSignature(wireSignatures, null, null, message, ns, violations);
			}
			return;
		}
		if (part.Type != null && !part.Type.IsEmpty && part.Element != null && !part.Element.IsEmpty)
		{
			violations.Add("R2306", Res.GetString("Part", part.Name, message, ns));
		}
		else
		{
			XmlQualifiedName xmlQualifiedName = ((part.Type == null || part.Type.IsEmpty) ? part.Element : part.Type);
			if (xmlQualifiedName.Namespace == null || xmlQualifiedName.Namespace.Length == 0)
			{
				violations.Add("R1014", Res.GetString("Part", part.Name, message, ns));
			}
		}
		if (!element && (part.Type == null || part.Type.IsEmpty))
		{
			violations.Add("R2203", Res.GetString("Part", part.Name, message, ns));
		}
		if (element && (part.Element == null || part.Element.IsEmpty))
		{
			violations.Add("R2204", Res.GetString("Part", part.Name, message, ns));
		}
		if (!element)
		{
			AddSignature(wireSignatures, operation, ns, message, ns, violations);
		}
		else if (part.Element != null)
		{
			AddSignature(wireSignatures, part.Element.Name, part.Element.Namespace, message, ns, violations);
		}
	}

	private static void AddSignature(Hashtable wireSignatures, string name, string ns, string message, string messageNs, BasicProfileViolationCollection violations)
	{
		if (wireSignatures == null)
		{
			return;
		}
		string key = ns + ":" + name;
		string text = (string)wireSignatures[key];
		string text2 = ((ns == null && name == null) ? Res.GetString("WireSignatureEmpty", message, messageNs) : Res.GetString("WireSignature", message, messageNs, ns, name));
		if (text != null)
		{
			if (text.Length > 0)
			{
				violations.Add("R2710", text);
				violations.Add("R2710", text2);
				wireSignatures[key] = string.Empty;
			}
		}
		else
		{
			wireSignatures[key] = text2;
		}
	}

	private static void CheckMessageParts(Message message, string[] parts, bool element, string operation, string binding, string ns, Hashtable wireSignatures, BasicProfileViolationCollection violations)
	{
		if (message == null)
		{
			return;
		}
		if (message.Parts == null || message.Parts.Count == 0)
		{
			if (!element)
			{
				AddSignature(wireSignatures, operation, ns, message.Name, ns, violations);
			}
			else
			{
				AddSignature(wireSignatures, null, null, message.Name, ns, violations);
			}
			return;
		}
		if (parts == null || parts.Length == 0)
		{
			for (int i = 0; i < message.Parts.Count; i++)
			{
				CheckMessagePart(message.Parts[i], element, message.Name, operation, binding, ns, (i == 0) ? wireSignatures : null, violations);
			}
			return;
		}
		for (int j = 0; j < parts.Length; j++)
		{
			if (parts[j] != null)
			{
				_ = message.Parts[parts[j]];
				CheckMessagePart(message.Parts[j], element, message.Name, operation, binding, ns, (j == 0) ? wireSignatures : null, violations);
			}
		}
	}

	private static SoapBodyBinding FindSoapBodyBinding(bool input, ServiceDescriptionFormatExtensionCollection extensions, BasicProfileViolationCollection violations, bool documentBinding, string operationName, string bindingName, string bindingNs)
	{
		SoapBodyBinding soapBodyBinding = null;
		for (int i = 0; i < extensions.Count; i++)
		{
			object obj = extensions[i];
			string text = null;
			bool flag = false;
			bool flag2 = false;
			if (obj is SoapBodyBinding)
			{
				flag = true;
				soapBodyBinding = (SoapBodyBinding)obj;
				text = soapBodyBinding.Namespace;
				flag2 = soapBodyBinding.Use == SoapBindingUse.Encoded;
			}
			else if (obj is SoapHeaderBinding)
			{
				flag = true;
				SoapHeaderBinding soapHeaderBinding = (SoapHeaderBinding)obj;
				text = soapHeaderBinding.Namespace;
				flag2 = soapHeaderBinding.Use == SoapBindingUse.Encoded;
				if (!flag2 && (soapHeaderBinding.Part == null || soapHeaderBinding.Part.Length == 0))
				{
					violations.Add("R2720", MessageString(soapHeaderBinding, operationName, bindingName, bindingNs, input, null));
				}
				if (soapHeaderBinding.Fault != null)
				{
					flag2 |= soapHeaderBinding.Fault.Use == SoapBindingUse.Encoded;
					if (!flag2)
					{
						if (soapHeaderBinding.Fault.Part == null || soapHeaderBinding.Fault.Part.Length == 0)
						{
							violations.Add("R2720", MessageString(soapHeaderBinding.Fault, operationName, bindingName, bindingNs, input, null));
						}
						if (soapHeaderBinding.Fault.Namespace != null && soapHeaderBinding.Fault.Namespace.Length > 0)
						{
							violations.Add(documentBinding ? "R2716" : "R2726", MessageString(obj, operationName, bindingName, bindingNs, input, null));
						}
					}
				}
			}
			if (flag2)
			{
				violations.Add("R2706", MessageString(obj, operationName, bindingName, bindingNs, input, null));
			}
			else
			{
				if (!flag)
				{
					continue;
				}
				Uri result;
				if (text == null || text.Length == 0)
				{
					if (!documentBinding && obj is SoapBodyBinding)
					{
						violations.Add("R2717", MessageString(obj, operationName, bindingName, bindingNs, input, null));
					}
				}
				else if (documentBinding || !(obj is SoapBodyBinding))
				{
					violations.Add(documentBinding ? "R2716" : "R2726", MessageString(obj, operationName, bindingName, bindingNs, input, null));
				}
				else if (!Uri.TryCreate(text, UriKind.Absolute, out result))
				{
					violations.Add("R2717", MessageString(obj, operationName, bindingName, bindingNs, input, Res.GetString("UriValueRelative", text)));
				}
			}
		}
		return soapBodyBinding;
	}

	private static string MessageString(object item, string operation, string binding, string ns, bool input, string details)
	{
		string text = null;
		string text2 = null;
		if (item is SoapBodyBinding)
		{
			text = (input ? "InputElement" : "OutputElement");
			text2 = "soapbind:body";
		}
		else if (item is SoapHeaderBinding)
		{
			text = (input ? "InputElement" : "OutputElement");
			text2 = "soapbind:header";
		}
		else if (item is SoapFaultBinding)
		{
			text = "Fault";
			text2 = ((SoapFaultBinding)item).Name;
		}
		else if (item is SoapHeaderFaultBinding)
		{
			text = "HeaderFault";
			text2 = "soapbind:headerfault";
		}
		if (text == null)
		{
			return null;
		}
		return Res.GetString(text, text2, operation, binding, ns, details);
	}

	private static bool CheckExtensions(ServiceDescriptionFormatExtensionCollection extensions)
	{
		foreach (ServiceDescriptionFormatExtension extension in extensions)
		{
			if (extension.Required)
			{
				return false;
			}
		}
		return true;
	}

	private static void CheckExtensions(Binding binding, ServiceDescription description, BasicProfileViolationCollection violations)
	{
		SoapBinding soapBinding = (SoapBinding)binding.Extensions.Find(typeof(SoapBinding));
		if (soapBinding != null && !(soapBinding.GetType() != typeof(SoapBinding)) && !CheckExtensions(binding.Extensions))
		{
			violations.Add("R2026", Res.GetString("BindingInvalidAttribute", binding.Name, description.TargetNamespace, "wsdl:required", "true"));
		}
	}

	private static void CheckExtensions(ServiceDescriptionCollection descriptions, BasicProfileViolationCollection violations)
	{
		Hashtable hashtable = new Hashtable();
		foreach (ServiceDescription description in descriptions)
		{
			if (ServiceDescription.GetConformanceClaims(description.Types.DocumentationElement) == WsiProfiles.BasicProfile1_1 && !CheckExtensions(description.Extensions))
			{
				violations.Add("R2026", Res.GetString("Element", "wsdl:types", description.TargetNamespace));
			}
			foreach (Service service in description.Services)
			{
				foreach (Port port in service.Ports)
				{
					if (ServiceDescription.GetConformanceClaims(port.DocumentationElement) == WsiProfiles.BasicProfile1_1)
					{
						if (!CheckExtensions(port.Extensions))
						{
							violations.Add("R2026", Res.GetString("Port", port.Name, service.Name, description.TargetNamespace));
						}
						Binding binding = descriptions.GetBinding(port.Binding);
						if (hashtable[binding] != null)
						{
							CheckExtensions(binding, description, violations);
							hashtable.Add(binding, binding);
						}
					}
				}
			}
			foreach (Binding binding2 in description.Bindings)
			{
				SoapBinding soapBinding = (SoapBinding)binding2.Extensions.Find(typeof(SoapBinding));
				if (soapBinding != null && !(soapBinding.GetType() != typeof(SoapBinding)) && hashtable[binding2] == null && ServiceDescription.GetConformanceClaims(binding2.DocumentationElement) == WsiProfiles.BasicProfile1_1)
				{
					CheckExtensions(binding2, description, violations);
					hashtable.Add(binding2, binding2);
				}
			}
		}
	}

	private static Operation FindOperation(OperationCollection operations, OperationBinding bindingOperation)
	{
		foreach (Operation operation in operations)
		{
			if (operation.IsBoundBy(bindingOperation))
			{
				return operation;
			}
		}
		return null;
	}
}
