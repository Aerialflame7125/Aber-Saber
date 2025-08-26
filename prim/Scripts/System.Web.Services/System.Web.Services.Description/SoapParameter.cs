using System.CodeDom.Compiler;
using System.Collections;
using System.Xml.Serialization;

namespace System.Web.Services.Description;

internal class SoapParameter
{
	internal CodeFlags codeFlags;

	internal string name;

	internal XmlMemberMapping mapping;

	internal string specifiedName;

	internal bool IsOut => (codeFlags & CodeFlags.IsOut) != 0;

	internal bool IsByRef => (codeFlags & CodeFlags.IsByRef) != 0;

	internal static string[] GetTypeFullNames(IList parameters, int specifiedCount, CodeDomProvider codeProvider)
	{
		string[] array = new string[parameters.Count + specifiedCount];
		GetTypeFullNames(parameters, array, 0, specifiedCount, codeProvider);
		return array;
	}

	internal static void GetTypeFullNames(IList parameters, string[] typeFullNames, int start, int specifiedCount, CodeDomProvider codeProvider)
	{
		int num = 0;
		for (int i = 0; i < parameters.Count; i++)
		{
			typeFullNames[i + start + num] = WebCodeGenerator.FullTypeName(((SoapParameter)parameters[i]).mapping, codeProvider);
			if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
			{
				num++;
				typeFullNames[i + start + num] = typeof(bool).FullName;
			}
		}
	}

	internal static string[] GetNames(IList parameters, int specifiedCount)
	{
		string[] array = new string[parameters.Count + specifiedCount];
		GetNames(parameters, array, 0, specifiedCount);
		return array;
	}

	internal static void GetNames(IList parameters, string[] names, int start, int specifiedCount)
	{
		int num = 0;
		for (int i = 0; i < parameters.Count; i++)
		{
			names[i + start + num] = ((SoapParameter)parameters[i]).name;
			if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
			{
				num++;
				names[i + start + num] = ((SoapParameter)parameters[i]).specifiedName;
			}
		}
	}

	internal static CodeFlags[] GetCodeFlags(IList parameters, int specifiedCount)
	{
		CodeFlags[] result = new CodeFlags[parameters.Count + specifiedCount];
		GetCodeFlags(parameters, result, 0, specifiedCount);
		return result;
	}

	internal static void GetCodeFlags(IList parameters, CodeFlags[] codeFlags, int start, int specifiedCount)
	{
		int num = 0;
		for (int i = 0; i < parameters.Count; i++)
		{
			codeFlags[i + start + num] = ((SoapParameter)parameters[i]).codeFlags;
			if (((SoapParameter)parameters[i]).mapping.CheckSpecified)
			{
				num++;
				codeFlags[i + start + num] = ((SoapParameter)parameters[i]).codeFlags;
			}
		}
	}
}
