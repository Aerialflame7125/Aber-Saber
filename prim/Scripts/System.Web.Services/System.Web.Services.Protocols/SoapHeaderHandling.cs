using System.Collections;
using System.Security.Permissions;
using System.Threading;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>The <see cref="T:System.Web.Services.Protocols.SoapHeaderHandling" /> class is used to get, set, write, and read SOAP header content to and from SOAP messages.</summary>
[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
public sealed class SoapHeaderHandling
{
	private SoapHeaderCollection unknownHeaders;

	private SoapHeaderCollection unreferencedHeaders;

	private int currentThread;

	private string envelopeNS;

	private void OnUnknownElement(object sender, XmlElementEventArgs e)
	{
		if (Thread.CurrentThread.GetHashCode() == currentThread && e.Element != null)
		{
			SoapUnknownHeader soapUnknownHeader = new SoapUnknownHeader();
			soapUnknownHeader.Element = e.Element;
			unknownHeaders.Add(soapUnknownHeader);
		}
	}

	private void OnUnreferencedObject(object sender, UnreferencedObjectEventArgs e)
	{
		if (Thread.CurrentThread.GetHashCode() == currentThread)
		{
			object unreferencedObject = e.UnreferencedObject;
			if (unreferencedObject != null && typeof(SoapHeader).IsAssignableFrom(unreferencedObject.GetType()))
			{
				unreferencedHeaders.Add((SoapHeader)unreferencedObject);
			}
		}
	}

	/// <summary>Returns a <see cref="T:System.String" /> that contains the SOAP header content of the SOAP message.</summary>
	/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> to be used in writing the headers.</param>
	/// <param name="serializer">The <see cref="T:System.Xml.Serialization.XmlSerializer" /> to be used in reading the headers.</param>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
	/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
	/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
	/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
	/// <param name="encodingStyle">A <see cref="T:System.String" /> that contains the encoding style for the SOAP headers.</param>
	/// <param name="checkRequiredHeaders">A <see cref="T:System.Boolean" /> that indicates whether to check for the required SOAP headers.</param>
	/// <returns>A <see cref="T:System.String" /> that contains the SOAP header content of the SOAP message.</returns>
	public string ReadHeaders(XmlReader reader, XmlSerializer serializer, SoapHeaderCollection headers, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, string envelopeNS, string encodingStyle, bool checkRequiredHeaders)
	{
		string text = null;
		reader.MoveToContent();
		if (!reader.IsStartElement("Header", envelopeNS))
		{
			if (checkRequiredHeaders && mappings != null && mappings.Length != 0)
			{
				text = GetHeaderElementName(mappings[0].headerType);
			}
			return text;
		}
		if (reader.IsEmptyElement)
		{
			reader.Skip();
			return text;
		}
		unknownHeaders = new SoapHeaderCollection();
		unreferencedHeaders = new SoapHeaderCollection();
		currentThread = Thread.CurrentThread.GetHashCode();
		this.envelopeNS = envelopeNS;
		int depth = reader.Depth;
		reader.ReadStartElement();
		reader.MoveToContent();
		XmlDeserializationEvents events = default(XmlDeserializationEvents);
		events.OnUnknownElement = OnUnknownElement;
		events.OnUnreferencedObject = OnUnreferencedObject;
		TraceMethod caller = (Tracing.On ? new TraceMethod(this, "ReadHeaders") : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceReadHeaders"), caller, new TraceMethod(serializer, "Deserialize", reader, encodingStyle));
		}
		object[] array = (object[])serializer.Deserialize(reader, encodingStyle, events);
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceReadHeaders"), caller);
		}
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null)
			{
				SoapHeader soapHeader = (SoapHeader)array[i];
				soapHeader.DidUnderstand = true;
				headers.Add(soapHeader);
			}
			else if (checkRequiredHeaders && text == null)
			{
				text = GetHeaderElementName(mappings[i].headerType);
			}
		}
		currentThread = 0;
		this.envelopeNS = null;
		foreach (SoapHeader unreferencedHeader in unreferencedHeaders)
		{
			headers.Add(unreferencedHeader);
		}
		unreferencedHeaders = null;
		foreach (SoapHeader unknownHeader in unknownHeaders)
		{
			headers.Add(unknownHeader);
		}
		unknownHeaders = null;
		while (depth < reader.Depth && reader.Read())
		{
		}
		if (reader.NodeType == XmlNodeType.EndElement)
		{
			reader.Read();
		}
		return text;
	}

	/// <summary>Writes the specified SOAP header content to the SOAP message.</summary>
	/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to be used in writing the headers.</param>
	/// <param name="serializer">The <see cref="T:System.Xml.Serialization.XmlSerializer" /> to be used in writing the headers.</param>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers to be written.</param>
	/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
	/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
	/// <param name="isEncoded">A <see cref="T:System.Boolean" /> that indicates whether the SOAP headers are encoded.</param>
	/// <param name="defaultNS">A <see cref="T:System.String" /> that contains the default namespace for the XML Web service.</param>
	/// <param name="serviceDefaultIsEncoded">A <see cref="T:System.Boolean" /> that indicates whether data sent to and from the XML Web service is encoded by default.</param>
	/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
	public static void WriteHeaders(XmlWriter writer, XmlSerializer serializer, SoapHeaderCollection headers, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool isEncoded, string defaultNS, bool serviceDefaultIsEncoded, string envelopeNS)
	{
		if (headers.Count == 0)
		{
			return;
		}
		writer.WriteStartElement("Header", envelopeNS);
		SoapProtocolVersion version;
		string text;
		if (envelopeNS == "http://www.w3.org/2003/05/soap-envelope")
		{
			version = SoapProtocolVersion.Soap12;
			text = "http://www.w3.org/2003/05/soap-encoding";
		}
		else
		{
			version = SoapProtocolVersion.Soap11;
			text = "http://schemas.xmlsoap.org/soap/encoding/";
		}
		int num = 0;
		ArrayList arrayList = new ArrayList();
		SoapHeader[] array = new SoapHeader[mappings.Length];
		bool[] array2 = new bool[array.Length];
		for (int i = 0; i < headers.Count; i++)
		{
			SoapHeader soapHeader = headers[i];
			if (soapHeader != null)
			{
				soapHeader.version = version;
				int num2;
				if (soapHeader is SoapUnknownHeader)
				{
					arrayList.Add(soapHeader);
					num++;
				}
				else if ((num2 = FindMapping(mappings, soapHeader, direction)) >= 0 && !array2[num2])
				{
					array[num2] = soapHeader;
					array2[num2] = true;
				}
				else
				{
					arrayList.Add(soapHeader);
				}
			}
		}
		int num3 = arrayList.Count - num;
		if (isEncoded && num3 > 0)
		{
			SoapHeader[] array3 = new SoapHeader[mappings.Length + num3];
			array.CopyTo(array3, 0);
			int num4 = mappings.Length;
			for (int j = 0; j < arrayList.Count; j++)
			{
				if (!(arrayList[j] is SoapUnknownHeader))
				{
					array3[num4++] = (SoapHeader)arrayList[j];
				}
			}
			array = array3;
		}
		TraceMethod caller = (Tracing.On ? new TraceMethod(typeof(SoapHeaderHandling), "WriteHeaders") : null);
		if (Tracing.On)
		{
			Tracing.Enter(Tracing.TraceId("TraceWriteHeaders"), caller, new TraceMethod(serializer, "Serialize", writer, array, null, isEncoded ? text : null, "h_"));
		}
		serializer.Serialize(writer, array, null, isEncoded ? text : null, "h_");
		if (Tracing.On)
		{
			Tracing.Exit(Tracing.TraceId("TraceWriteHeaders"), caller);
		}
		foreach (SoapHeader item in arrayList)
		{
			if (item is SoapUnknownHeader)
			{
				SoapUnknownHeader soapUnknownHeader = (SoapUnknownHeader)item;
				if (soapUnknownHeader.Element != null)
				{
					soapUnknownHeader.Element.WriteTo(writer);
				}
			}
			else if (!isEncoded)
			{
				string literalNamespace = SoapReflector.GetLiteralNamespace(defaultNS, serviceDefaultIsEncoded);
				XmlSerializer xmlSerializer = new XmlSerializer(item.GetType(), literalNamespace);
				if (Tracing.On)
				{
					Tracing.Enter(Tracing.TraceId("TraceWriteHeaders"), caller, new TraceMethod(xmlSerializer, "Serialize", writer, item));
				}
				xmlSerializer.Serialize(writer, item);
				if (Tracing.On)
				{
					Tracing.Exit(Tracing.TraceId("TraceWriteHeaders"), caller);
				}
			}
		}
		for (int k = 0; k < headers.Count; k++)
		{
			SoapHeader soapHeader3 = headers[k];
			if (soapHeader3 != null)
			{
				soapHeader3.version = SoapProtocolVersion.Default;
			}
		}
		writer.WriteEndElement();
		writer.Flush();
	}

	/// <summary>Writes the specified SOAP header content to the SOAP message.</summary>
	/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> to be used in writing the headers.</param>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers to be written.</param>
	/// <param name="envelopeNS">A <see cref="T:System.String" /> that contains the namespace for the SOAP message envelope.</param>
	public static void WriteUnknownHeaders(XmlWriter writer, SoapHeaderCollection headers, string envelopeNS)
	{
		bool flag = true;
		foreach (SoapHeader header in headers)
		{
			if (header is SoapUnknownHeader soapUnknownHeader)
			{
				if (flag)
				{
					writer.WriteStartElement("Header", envelopeNS);
					flag = false;
				}
				if (soapUnknownHeader.Element != null)
				{
					soapUnknownHeader.Element.WriteTo(writer);
				}
			}
		}
		if (!flag)
		{
			writer.WriteEndElement();
		}
	}

	/// <summary>Sets the SOAP header content for the specified SOAP message.</summary>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
	/// <param name="target">A <see cref="T:System.Object" /> that represents the SOAP message.</param>
	/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
	/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
	/// <param name="client">This parameter is currently not used.</param>
	public static void SetHeaderMembers(SoapHeaderCollection headers, object target, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool client)
	{
		bool[] array = new bool[headers.Count];
		if (mappings != null)
		{
			foreach (SoapHeaderMapping soapHeaderMapping in mappings)
			{
				if ((soapHeaderMapping.direction & direction) == 0)
				{
					continue;
				}
				if (soapHeaderMapping.repeats)
				{
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < headers.Count; j++)
					{
						SoapHeader soapHeader = headers[j];
						if (!array[j] && soapHeaderMapping.headerType.IsAssignableFrom(soapHeader.GetType()))
						{
							arrayList.Add(soapHeader);
							array[j] = true;
						}
					}
					MemberHelper.SetValue(soapHeaderMapping.memberInfo, target, arrayList.ToArray(soapHeaderMapping.headerType));
					continue;
				}
				bool flag = false;
				for (int k = 0; k < headers.Count; k++)
				{
					SoapHeader soapHeader2 = headers[k];
					if (!array[k] && soapHeaderMapping.headerType.IsAssignableFrom(soapHeader2.GetType()))
					{
						if (flag)
						{
							soapHeader2.DidUnderstand = false;
							continue;
						}
						flag = true;
						MemberHelper.SetValue(soapHeaderMapping.memberInfo, target, soapHeader2);
						array[k] = true;
					}
				}
			}
		}
		for (int l = 0; l < array.Length; l++)
		{
			if (!array[l])
			{
				SoapHeader soapHeader3 = headers[l];
				if (soapHeader3.MustUnderstand && !soapHeader3.DidUnderstand)
				{
					throw new SoapHeaderException(Res.GetString("WebCannotUnderstandHeader", GetHeaderElementName(soapHeader3)), new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/"));
				}
			}
		}
	}

	/// <summary>Gets the SOAP header content for the specified SOAP message.</summary>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
	/// <param name="target">A <see cref="T:System.Object" /> that represents the SOAP message.</param>
	/// <param name="mappings">An array of type <see cref="T:System.Web.Services.Protocols.SoapHeaderMapping" /> that contains the mappings for the SOAP headers.</param>
	/// <param name="direction">A <see cref="T:System.Web.Services.Protocols.SoapHeaderDirection" /> value that indicates the direction of the SOAP headers.</param>
	/// <param name="client">This parameter is currently not used.</param>
	public static void GetHeaderMembers(SoapHeaderCollection headers, object target, SoapHeaderMapping[] mappings, SoapHeaderDirection direction, bool client)
	{
		if (mappings == null || mappings.Length == 0)
		{
			return;
		}
		foreach (SoapHeaderMapping soapHeaderMapping in mappings)
		{
			if ((soapHeaderMapping.direction & direction) == 0)
			{
				continue;
			}
			object value = MemberHelper.GetValue(soapHeaderMapping.memberInfo, target);
			if (soapHeaderMapping.repeats)
			{
				object[] array = (object[])value;
				if (array == null)
				{
					continue;
				}
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] != null)
					{
						headers.Add((SoapHeader)array[j]);
					}
				}
			}
			else if (value != null)
			{
				headers.Add((SoapHeader)value);
			}
		}
	}

	/// <summary>Checks to ensure that the SOAP headers that must be understood have been understood; if not, this method throws an exception.</summary>
	/// <param name="headers">The <see cref="T:System.Web.Services.Protocols.SoapHeaderCollection" /> that contains the SOAP headers.</param>
	/// <exception cref="T:System.Web.Services.Protocols.SoapHeaderException">A SOAP header that must be understood was not understood.</exception>
	public static void EnsureHeadersUnderstood(SoapHeaderCollection headers)
	{
		for (int i = 0; i < headers.Count; i++)
		{
			SoapHeader soapHeader = headers[i];
			if (soapHeader.MustUnderstand && !soapHeader.DidUnderstand)
			{
				throw new SoapHeaderException(Res.GetString("WebCannotUnderstandHeader", GetHeaderElementName(soapHeader)), new XmlQualifiedName("MustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/"));
			}
		}
	}

	private static int FindMapping(SoapHeaderMapping[] mappings, SoapHeader header, SoapHeaderDirection direction)
	{
		if (mappings == null || mappings.Length == 0)
		{
			return -1;
		}
		Type type = header.GetType();
		for (int i = 0; i < mappings.Length; i++)
		{
			SoapHeaderMapping soapHeaderMapping = mappings[i];
			if ((soapHeaderMapping.direction & direction) != 0 && soapHeaderMapping.custom && soapHeaderMapping.headerType.IsAssignableFrom(type))
			{
				return i;
			}
		}
		return -1;
	}

	private static string GetHeaderElementName(Type headerType)
	{
		return SoapReflector.CreateXmlImporter(null, serviceDefaultIsEncoded: false).ImportTypeMapping(headerType).XsdElementName;
	}

	private static string GetHeaderElementName(SoapHeader header)
	{
		if (header is SoapUnknownHeader)
		{
			return ((SoapUnknownHeader)header).Element.LocalName;
		}
		return GetHeaderElementName(header.GetType());
	}

	/// <summary>Initializes a new instance of <see cref="T:System.Web.Services.Protocols.SoapHeaderHandling" />.</summary>
	public SoapHeaderHandling()
	{
	}
}
