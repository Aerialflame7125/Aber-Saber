using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class SoapParameters
{
	private XmlMemberMapping ret;

	private ArrayList parameters = new ArrayList();

	private ArrayList inParameters = new ArrayList();

	private ArrayList outParameters = new ArrayList();

	private int checkSpecifiedCount;

	private int inCheckSpecifiedCount;

	private int outCheckSpecifiedCount;

	internal XmlMemberMapping Return => ret;

	internal IList Parameters => parameters;

	internal IList InParameters => inParameters;

	internal IList OutParameters => outParameters;

	internal int CheckSpecifiedCount => checkSpecifiedCount;

	internal int InCheckSpecifiedCount => inCheckSpecifiedCount;

	internal int OutCheckSpecifiedCount => outCheckSpecifiedCount;

	internal SoapParameters(XmlMembersMapping request, XmlMembersMapping response, string[] parameterOrder, CodeIdentifiers identifiers)
	{
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		AddMappings(arrayList, request);
		if (response != null)
		{
			AddMappings(arrayList2, response);
		}
		if (parameterOrder != null)
		{
			foreach (string elementName in parameterOrder)
			{
				XmlMemberMapping xmlMemberMapping = FindMapping(arrayList, elementName);
				SoapParameter soapParameter = new SoapParameter();
				if (xmlMemberMapping != null)
				{
					if (RemoveByRefMapping(arrayList2, xmlMemberMapping))
					{
						soapParameter.codeFlags = CodeFlags.IsByRef;
					}
					soapParameter.mapping = xmlMemberMapping;
					arrayList.Remove(xmlMemberMapping);
					AddParameter(soapParameter);
					continue;
				}
				XmlMemberMapping xmlMemberMapping2 = FindMapping(arrayList2, elementName);
				if (xmlMemberMapping2 != null)
				{
					soapParameter.codeFlags = CodeFlags.IsOut;
					soapParameter.mapping = xmlMemberMapping2;
					arrayList2.Remove(xmlMemberMapping2);
					AddParameter(soapParameter);
				}
			}
		}
		foreach (XmlMemberMapping item in arrayList)
		{
			SoapParameter soapParameter2 = new SoapParameter();
			if (RemoveByRefMapping(arrayList2, item))
			{
				soapParameter2.codeFlags = CodeFlags.IsByRef;
			}
			soapParameter2.mapping = item;
			AddParameter(soapParameter2);
		}
		if (arrayList2.Count > 0)
		{
			if (!((XmlMemberMapping)arrayList2[0]).CheckSpecified)
			{
				ret = (XmlMemberMapping)arrayList2[0];
				arrayList2.RemoveAt(0);
			}
			foreach (XmlMemberMapping item2 in arrayList2)
			{
				AddParameter(new SoapParameter
				{
					mapping = item2,
					codeFlags = CodeFlags.IsOut
				});
			}
		}
		foreach (SoapParameter parameter in parameters)
		{
			parameter.name = identifiers.MakeUnique(CodeIdentifier.MakeValid(parameter.mapping.MemberName));
		}
	}

	private void AddParameter(SoapParameter parameter)
	{
		parameters.Add(parameter);
		if (parameter.mapping.CheckSpecified)
		{
			checkSpecifiedCount++;
		}
		if (parameter.IsByRef)
		{
			inParameters.Add(parameter);
			outParameters.Add(parameter);
			if (parameter.mapping.CheckSpecified)
			{
				inCheckSpecifiedCount++;
				outCheckSpecifiedCount++;
			}
		}
		else if (parameter.IsOut)
		{
			outParameters.Add(parameter);
			if (parameter.mapping.CheckSpecified)
			{
				outCheckSpecifiedCount++;
			}
		}
		else
		{
			inParameters.Add(parameter);
			if (parameter.mapping.CheckSpecified)
			{
				inCheckSpecifiedCount++;
			}
		}
	}

	private static bool RemoveByRefMapping(ArrayList responseList, XmlMemberMapping requestMapping)
	{
		XmlMemberMapping xmlMemberMapping = FindMapping(responseList, requestMapping.ElementName);
		if (xmlMemberMapping == null)
		{
			return false;
		}
		if (requestMapping.TypeFullName != xmlMemberMapping.TypeFullName)
		{
			return false;
		}
		if (requestMapping.Namespace != xmlMemberMapping.Namespace)
		{
			return false;
		}
		if (requestMapping.MemberName != xmlMemberMapping.MemberName)
		{
			return false;
		}
		responseList.Remove(xmlMemberMapping);
		return true;
	}

	private static void AddMappings(ArrayList mappingsList, XmlMembersMapping mappings)
	{
		for (int i = 0; i < mappings.Count; i++)
		{
			mappingsList.Add(mappings[i]);
		}
	}

	private static XmlMemberMapping FindMapping(ArrayList mappingsList, string elementName)
	{
		foreach (XmlMemberMapping mappings in mappingsList)
		{
			if (mappings.ElementName == elementName)
			{
				return mappings;
			}
		}
		return null;
	}
}
