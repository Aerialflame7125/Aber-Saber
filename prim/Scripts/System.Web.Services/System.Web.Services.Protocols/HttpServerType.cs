using System.Collections;
using System.Web.Services.Configuration;

namespace System.Web.Services.Protocols;

internal class HttpServerType : ServerType
{
	private Hashtable methods = new Hashtable();

	internal HttpServerType(Type type)
		: base(type)
	{
		WebServicesSection current = WebServicesSection.Current;
		Type[] returnWriterTypes = current.ReturnWriterTypes;
		Type[] parameterReaderTypes = current.ParameterReaderTypes;
		LogicalMethodInfo[] array = WebMethodReflector.GetMethods(type);
		HttpServerMethod[] array2 = new HttpServerMethod[array.Length];
		object[] array3 = new object[returnWriterTypes.Length];
		for (int i = 0; i < array3.Length; i++)
		{
			array3[i] = MimeFormatter.GetInitializers(returnWriterTypes[i], array);
		}
		for (int j = 0; j < array.Length; j++)
		{
			LogicalMethodInfo logicalMethodInfo = array[j];
			HttpServerMethod httpServerMethod = null;
			if (logicalMethodInfo.ReturnType == typeof(void))
			{
				httpServerMethod = new HttpServerMethod();
			}
			else
			{
				for (int k = 0; k < returnWriterTypes.Length; k++)
				{
					object[] array4 = (object[])array3[k];
					if (array4[j] != null)
					{
						httpServerMethod = new HttpServerMethod
						{
							writerInitializer = array4[j],
							writerType = returnWriterTypes[k]
						};
						break;
					}
				}
			}
			if (httpServerMethod != null)
			{
				httpServerMethod.methodInfo = logicalMethodInfo;
				array2[j] = httpServerMethod;
			}
		}
		array3 = new object[parameterReaderTypes.Length];
		for (int l = 0; l < array3.Length; l++)
		{
			array3[l] = MimeFormatter.GetInitializers(parameterReaderTypes[l], array);
		}
		for (int m = 0; m < array.Length; m++)
		{
			HttpServerMethod httpServerMethod2 = array2[m];
			if (httpServerMethod2 == null || array[m].InParameters.Length == 0)
			{
				continue;
			}
			int num = 0;
			for (int n = 0; n < parameterReaderTypes.Length; n++)
			{
				if (((object[])array3[n])[m] != null)
				{
					num++;
				}
			}
			if (num == 0)
			{
				array2[m] = null;
				continue;
			}
			httpServerMethod2.readerTypes = new Type[num];
			httpServerMethod2.readerInitializers = new object[num];
			num = 0;
			for (int num2 = 0; num2 < parameterReaderTypes.Length; num2++)
			{
				object[] array5 = (object[])array3[num2];
				if (array5[m] != null)
				{
					httpServerMethod2.readerTypes[num] = parameterReaderTypes[num2];
					httpServerMethod2.readerInitializers[num] = array5[m];
					num++;
				}
			}
		}
		foreach (HttpServerMethod httpServerMethod3 in array2)
		{
			if (httpServerMethod3 != null)
			{
				WebMethodAttribute methodAttribute = httpServerMethod3.methodInfo.MethodAttribute;
				httpServerMethod3.name = methodAttribute.MessageName;
				if (httpServerMethod3.name.Length == 0)
				{
					httpServerMethod3.name = httpServerMethod3.methodInfo.Name;
				}
				methods.Add(httpServerMethod3.name, httpServerMethod3);
			}
		}
	}

	internal HttpServerMethod GetMethod(string name)
	{
		return (HttpServerMethod)methods[name];
	}

	internal HttpServerMethod GetMethodIgnoreCase(string name)
	{
		foreach (DictionaryEntry method in methods)
		{
			HttpServerMethod httpServerMethod = (HttpServerMethod)method.Value;
			if (string.Compare(httpServerMethod.name, name, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return httpServerMethod;
			}
		}
		return null;
	}
}
