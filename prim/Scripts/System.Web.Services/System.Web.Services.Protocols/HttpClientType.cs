using System.Collections;
using System.Threading;

namespace System.Web.Services.Protocols;

internal class HttpClientType
{
	private Hashtable methods = new Hashtable();

	internal HttpClientType(Type type)
	{
		LogicalMethodInfo[] array = LogicalMethodInfo.Create(type.GetMethods(), LogicalMethodTypes.Sync);
		Hashtable hashtable = new Hashtable();
		foreach (LogicalMethodInfo logicalMethodInfo in array)
		{
			try
			{
				object[] customAttributes = logicalMethodInfo.GetCustomAttributes(typeof(HttpMethodAttribute));
				if (customAttributes.Length != 0)
				{
					HttpMethodAttribute httpMethodAttribute = (HttpMethodAttribute)customAttributes[0];
					HttpClientMethod httpClientMethod = new HttpClientMethod
					{
						readerType = httpMethodAttribute.ReturnFormatter,
						writerType = httpMethodAttribute.ParameterFormatter,
						methodInfo = logicalMethodInfo
					};
					AddFormatter(hashtable, httpClientMethod.readerType, httpClientMethod);
					AddFormatter(hashtable, httpClientMethod.writerType, httpClientMethod);
					methods.Add(logicalMethodInfo.Name, httpClientMethod);
				}
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				throw new InvalidOperationException(Res.GetString("WebReflectionError", logicalMethodInfo.DeclaringType.FullName, logicalMethodInfo.Name), ex);
			}
		}
		foreach (Type key in hashtable.Keys)
		{
			ArrayList arrayList = (ArrayList)hashtable[key];
			LogicalMethodInfo[] array2 = new LogicalMethodInfo[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array2[j] = ((HttpClientMethod)arrayList[j]).methodInfo;
			}
			object[] initializers = MimeFormatter.GetInitializers(key, array2);
			bool flag = typeof(MimeParameterWriter).IsAssignableFrom(key);
			for (int k = 0; k < arrayList.Count; k++)
			{
				if (flag)
				{
					((HttpClientMethod)arrayList[k]).writerInitializer = initializers[k];
				}
				else
				{
					((HttpClientMethod)arrayList[k]).readerInitializer = initializers[k];
				}
			}
		}
	}

	private static void AddFormatter(Hashtable formatterTypes, Type formatterType, HttpClientMethod method)
	{
		if (!(formatterType == null))
		{
			ArrayList arrayList = (ArrayList)formatterTypes[formatterType];
			if (arrayList == null)
			{
				arrayList = new ArrayList();
				formatterTypes.Add(formatterType, arrayList);
			}
			arrayList.Add(method);
		}
	}

	internal HttpClientMethod GetMethod(string name)
	{
		return (HttpClientMethod)methods[name];
	}
}
