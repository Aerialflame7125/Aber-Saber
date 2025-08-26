using System.Collections.Specialized;
using System.Reflection;

namespace System.Web.Services.Protocols;

/// <summary>Serves as a base class for readers of incoming request parameters for Web services implemented using HTTP but without SOAP.</summary>
public abstract class ValueCollectionParameterReader : MimeParameterReader
{
	private ParameterInfo[] paramInfos;

	/// <summary>Initializes an instance.</summary>
	/// <param name="o">A <see cref="T:System.Reflection.ParameterInfo" /> array, obtained through the <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.InParameters" /> property of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class.</param>
	public override void Initialize(object o)
	{
		paramInfos = (ParameterInfo[])o;
	}

	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> representing the Web method.</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		if (!IsSupported(methodInfo))
		{
			return null;
		}
		return methodInfo.InParameters;
	}

	/// <summary>Translates a collection of name/value pairs into an array of objects representing method parameter values.</summary>
	/// <param name="collection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that specifies the collection of name/value pairs containing method parameter names and values.</param>
	/// <returns>An array of <see cref="T:System.Object" /> objects representing method parameter values.</returns>
	protected object[] Read(NameValueCollection collection)
	{
		object[] array = new object[paramInfos.Length];
		for (int i = 0; i < paramInfos.Length; i++)
		{
			ParameterInfo parameterInfo = paramInfos[i];
			if (parameterInfo.ParameterType.IsArray)
			{
				string[] values = collection.GetValues(parameterInfo.Name);
				Type elementType = parameterInfo.ParameterType.GetElementType();
				Array array2 = Array.CreateInstance(elementType, values.Length);
				for (int j = 0; j < values.Length; j++)
				{
					string value = values[j];
					array2.SetValue(ScalarFormatter.FromString(value, elementType), j);
				}
				array[i] = array2;
			}
			else
			{
				string text = collection[parameterInfo.Name];
				if (text == null)
				{
					throw new InvalidOperationException(Res.GetString("WebMissingParameter", parameterInfo.Name));
				}
				array[i] = ScalarFormatter.FromString(text, parameterInfo.ParameterType);
			}
		}
		return array;
	}

	/// <summary>Determines whether a method definition's parameter definitions are supported by the <see cref="T:System.Web.Services.Protocols.ValueCollectionParameterReader" /> class.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the method to check.</param>
	/// <returns>
	///     <see langword="true" /> if a method's parameter definitions are supported by the reader; otherwise, <see langword="false" />.</returns>
	public static bool IsSupported(LogicalMethodInfo methodInfo)
	{
		if (methodInfo.OutParameters.Length != 0)
		{
			return false;
		}
		ParameterInfo[] inParameters = methodInfo.InParameters;
		for (int i = 0; i < inParameters.Length; i++)
		{
			if (!IsSupported(inParameters[i]))
			{
				return false;
			}
		}
		return true;
	}

	/// <summary>Determines whether a particular parameter type is supported by the <see cref="T:System.Web.Services.Protocols.ValueCollectionParameterReader" /> class.</summary>
	/// <param name="paramInfo">A <see cref="T:System.Reflection.ParameterInfo" /> that specifies the parameter to check.</param>
	/// <returns>
	///     <see langword="true" /> if a method's parameter definitions are supported by the reader; otherwise, <see langword="false" />.</returns>
	public static bool IsSupported(ParameterInfo paramInfo)
	{
		Type type = paramInfo.ParameterType;
		if (type.IsArray)
		{
			type = type.GetElementType();
		}
		return ScalarFormatter.IsTypeSupported(type);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.ValueCollectionParameterReader" /> class. </summary>
	protected ValueCollectionParameterReader()
	{
	}
}
