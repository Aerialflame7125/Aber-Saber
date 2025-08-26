using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;

namespace System.Web.Services.Protocols;

/// <summary>Represents the attributes and metadata for an XML Web service method. This class cannot be inherited.</summary>
public sealed class LogicalMethodInfo
{
	private MethodInfo methodInfo;

	private MethodInfo endMethodInfo;

	private ParameterInfo[] inParams;

	private ParameterInfo[] outParams;

	private ParameterInfo[] parameters;

	private Hashtable attributes;

	private Type retType;

	private ParameterInfo callbackParam;

	private ParameterInfo stateParam;

	private ParameterInfo resultParam;

	private string methodName;

	private bool isVoid;

	private static object[] emptyObjectArray = new object[0];

	private WebServiceBindingAttribute binding;

	private WebMethodAttribute attribute;

	private MethodInfo declaration;

	private static HashAlgorithm hash;

	internal WebServiceBindingAttribute Binding => binding;

	internal MethodInfo Declaration => declaration;

	/// <summary>Gets the class that declares the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <returns>The <see cref="T:System.Type" /> for the class declaring the method represented by the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
	public Type DeclaringType => methodInfo.DeclaringType;

	/// <summary>Gets the name of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <returns>The name of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
	public string Name => methodName;

	/// <summary>Gets the return value of a <see langword="Begin" /> asynchronous method invocation.</summary>
	/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <see cref="T:System.IAsyncResult" /> returned from a <see langword="Begin" /> asynchronous method invocation.</returns>
	public ParameterInfo AsyncResultParameter => resultParam;

	/// <summary>Gets the parameter information for the <paramref name="AsyncCallback" /> parameter of a Begin method in an asynchronous invocation.</summary>
	/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <paramref name="AsyncCallback" /> parameter of a <see langword="Begin" /> asynchronous method invocation.</returns>
	public ParameterInfo AsyncCallbackParameter => callbackParam;

	/// <summary>Gets the parameter information for the <paramref name="AsyncState" /> parameter of a <see langword="Begin" /> method in an asynchronous invocation.</summary>
	/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> representing the <paramref name="AsyncState" /> parameter of a <see langword="Begin" /> method in an asynchronous invocation.</returns>
	public ParameterInfo AsyncStateParameter => stateParam;

	/// <summary>Gets the return type of this method.</summary>
	/// <returns>The <see cref="T:System.Type" /> returned by this method.</returns>
	public Type ReturnType => retType;

	/// <summary>Gets a value indicating whether the return type for the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> is <see langword="void" />.</summary>
	/// <returns>
	///     <see langword="true" /> if the return type is void; otherwise, <see langword="false" />.</returns>
	public bool IsVoid => isVoid;

	/// <summary>Gets a value indicating whether the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> is invoked asynchronously.</summary>
	/// <returns>
	///     <see langword="true" /> if the method is invoked asynchronously; otherwise, <see langword="false" />.</returns>
	public bool IsAsync => endMethodInfo != null;

	/// <summary>Gets the parameters passed into the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <returns>An array of type <see cref="T:System.Reflection.ParameterInfo" /> containing the parameters passed into the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
	public ParameterInfo[] InParameters => inParams;

	/// <summary>Gets the out parameters for the method.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> representing the out parameters for the method, in order.</returns>
	public ParameterInfo[] OutParameters => outParams;

	/// <summary>Gets the parameters for the method.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> representing the parameters for the method.</returns>
	public ParameterInfo[] Parameters => parameters;

	internal WebMethodAttribute MethodAttribute
	{
		get
		{
			if (attribute == null)
			{
				attribute = (WebMethodAttribute)GetCustomAttribute(typeof(WebMethodAttribute));
				if (attribute == null)
				{
					attribute = new WebMethodAttribute();
				}
			}
			return attribute;
		}
	}

	/// <summary>Gets the custom attributes applied to the method.</summary>
	/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes for the method.</returns>
	public ICustomAttributeProvider CustomAttributeProvider => methodInfo;

	/// <summary>Gets the custom attributes for the return type.</summary>
	/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes for the return type.</returns>
	public ICustomAttributeProvider ReturnTypeCustomAttributeProvider
	{
		get
		{
			if (declaration != null)
			{
				return declaration.ReturnTypeCustomAttributes;
			}
			return methodInfo.ReturnTypeCustomAttributes;
		}
	}

	/// <summary>Gets the attributes and metadata for a synchronous method.</summary>
	/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for a method. If <see cref="P:System.Web.Services.Protocols.LogicalMethodInfo.IsAsync" /> is <see langword="true" />, then the value of this property is <see langword="null" />.</returns>
	public MethodInfo MethodInfo
	{
		get
		{
			if (!(endMethodInfo == null))
			{
				return null;
			}
			return methodInfo;
		}
	}

	/// <summary>Gets the attributes and metadata for a <see langword="Begin" /> method in an asynchronous invocation.</summary>
	/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for a <see langword="Begin" /> asynchronous method invocation.</returns>
	public MethodInfo BeginMethodInfo => methodInfo;

	/// <summary>Gets the attributes and metadata for an <see langword="End" /> method of an asynchronous invocation to a method.</summary>
	/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the attributes and metadata for an <see langword="End" /> asynchronous method invocation.</returns>
	public MethodInfo EndMethodInfo => endMethodInfo;

	internal static HashAlgorithm HashAlgorithm
	{
		get
		{
			if (hash == null)
			{
				hash = SHA1.Create();
			}
			return hash;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> class with the <see cref="T:System.Reflection.MethodInfo" /> passed in.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Reflection.MethodInfo" /> to initialize the properties of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> common to the <see cref="T:System.Reflection.MethodInfo" />. </param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Reflection.MethodBase.IsStatic" /> property of the <paramref name="methodInfo" /> parameter is <see langword="true" />.-or- The <see cref="M:System.Reflection.MethodBase.GetParameters" /> method of the <paramref name="methodInfo" /> parameter does not contain all the parameters required by the method represented by the instance of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />. </exception>
	public LogicalMethodInfo(MethodInfo methodInfo)
		: this(methodInfo, null)
	{
	}

	internal LogicalMethodInfo(MethodInfo methodInfo, WebMethod webMethod)
	{
		if (methodInfo.IsStatic)
		{
			throw new InvalidOperationException(Res.GetString("WebMethodStatic", methodInfo.Name));
		}
		this.methodInfo = methodInfo;
		if (webMethod != null)
		{
			binding = webMethod.binding;
			attribute = webMethod.attribute;
			declaration = webMethod.declaration;
		}
		MethodInfo methodInfo2 = ((declaration != null) ? declaration : methodInfo);
		parameters = methodInfo2.GetParameters();
		inParams = GetInParameters(methodInfo2, parameters, 0, parameters.Length, mustBeIn: false);
		outParams = GetOutParameters(methodInfo2, parameters, 0, parameters.Length, mustBeOut: false);
		retType = methodInfo2.ReturnType;
		isVoid = retType == typeof(void);
		methodName = methodInfo2.Name;
		attributes = new Hashtable();
	}

	private LogicalMethodInfo(MethodInfo beginMethodInfo, MethodInfo endMethodInfo, WebMethod webMethod)
	{
		methodInfo = beginMethodInfo;
		this.endMethodInfo = endMethodInfo;
		methodName = beginMethodInfo.Name.Substring(5);
		if (webMethod != null)
		{
			binding = webMethod.binding;
			attribute = webMethod.attribute;
			declaration = webMethod.declaration;
		}
		ParameterInfo[] array = beginMethodInfo.GetParameters();
		if (array.Length < 2 || array[array.Length - 1].ParameterType != typeof(object) || array[array.Length - 2].ParameterType != typeof(AsyncCallback))
		{
			throw new InvalidOperationException(Res.GetString("WebMethodMissingParams", beginMethodInfo.DeclaringType.FullName, beginMethodInfo.Name, typeof(AsyncCallback).FullName, typeof(object).FullName));
		}
		stateParam = array[array.Length - 1];
		callbackParam = array[array.Length - 2];
		inParams = GetInParameters(beginMethodInfo, array, 0, array.Length - 2, mustBeIn: true);
		ParameterInfo[] array2 = endMethodInfo.GetParameters();
		resultParam = array2[0];
		outParams = GetOutParameters(endMethodInfo, array2, 1, array2.Length - 1, mustBeOut: true);
		parameters = new ParameterInfo[inParams.Length + outParams.Length];
		inParams.CopyTo(parameters, 0);
		outParams.CopyTo(parameters, inParams.Length);
		retType = endMethodInfo.ReturnType;
		isVoid = retType == typeof(void);
		attributes = new Hashtable();
	}

	/// <summary>Returns a string that represents the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</returns>
	public override string ToString()
	{
		return methodInfo.ToString();
	}

	/// <summary>Invokes the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <param name="target">The instance of the <see cref="T:System.Object" /> to invoke the method. </param>
	/// <param name="values">An argument list for the invoked method. This is an array of objects with the same number, order, and type as the parameters of the method. If the method does not require any parameters, the <paramref name="values" /> parameter should be <see langword="null" />. </param>
	/// <returns>An array of type <see cref="T:System.Object" /> representing the return value and out parameters of the invoked method.</returns>
	/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">The number, type, and order of parameters in the <paramref name="values" /> parameter do not match the signature of the invoked method. </exception>
	/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method throws an exception. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public object[] Invoke(object target, object[] values)
	{
		if (outParams.Length != 0)
		{
			object[] array = new object[parameters.Length];
			for (int i = 0; i < inParams.Length; i++)
			{
				array[inParams[i].Position] = values[i];
			}
			values = array;
		}
		object obj = methodInfo.Invoke(target, values);
		if (outParams.Length != 0)
		{
			int num = outParams.Length;
			if (!isVoid)
			{
				num++;
			}
			object[] array2 = new object[num];
			num = 0;
			if (!isVoid)
			{
				array2[num++] = obj;
			}
			for (int j = 0; j < outParams.Length; j++)
			{
				array2[num++] = values[outParams[j].Position];
			}
			return array2;
		}
		if (isVoid)
		{
			return emptyObjectArray;
		}
		return new object[1] { obj };
	}

	/// <summary>Begins an asynchronous invocation of the method represented by this <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <param name="target">The instance of the <see cref="T:System.Object" /> on which to invoke the method on. </param>
	/// <param name="values">An argument list for the invoked method. This is an array of objects with the same number, order, and type as the parameters of the method. If the method does not require any parameters, <paramref name="values" /> should be <see langword="null" />. </param>
	/// <param name="callback">The delegate to call when the asynchronous invoke is complete. If <paramref name="callback" /> is <see langword="null" />, the delegate is not called. </param>
	/// <param name="asyncState">State information that is passed on to the delegate. </param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> which is passed to <see cref="M:System.Web.Services.Protocols.LogicalMethodInfo.EndInvoke(System.Object,System.IAsyncResult)" /> to obtain the return values from the remote method call.</returns>
	/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameteris <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">The number, type, and order of parameters in <paramref name="values" /> do not match the signature of the invoked method. </exception>
	/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public IAsyncResult BeginInvoke(object target, object[] values, AsyncCallback callback, object asyncState)
	{
		object[] array = new object[values.Length + 2];
		values.CopyTo(array, 0);
		array[values.Length] = callback;
		array[values.Length + 1] = asyncState;
		return (IAsyncResult)methodInfo.Invoke(target, array);
	}

	/// <summary>Ends an asynchronous invocation of the method represented by the current <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <param name="target">The instance of the <see cref="T:System.Object" /> on which to invoke the method. </param>
	/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned from <see cref="M:System.Web.Services.Protocols.LogicalMethodInfo.BeginInvoke(System.Object,System.Object[],System.AsyncCallback,System.Object)" />. </param>
	/// <returns>An array of objects containing the return value and any by-reference or out parameters of the derived class method.</returns>
	/// <exception cref="T:System.Reflection.TargetException">The <paramref name="target" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.MemberAccessException">The caller does not have permission to invoke the method. </exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method throws an exception. </exception>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public object[] EndInvoke(object target, IAsyncResult asyncResult)
	{
		object[] array = new object[outParams.Length + 1];
		array[0] = asyncResult;
		object obj = endMethodInfo.Invoke(target, array);
		if (!isVoid)
		{
			array[0] = obj;
			return array;
		}
		if (outParams.Length != 0)
		{
			object[] array2 = new object[outParams.Length];
			Array.Copy(array, 1, array2, 0, array2.Length);
			return array2;
		}
		return emptyObjectArray;
	}

	/// <summary>Returns the custom attributes applied to the specified type.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> to which the custom attributes are applied. </param>
	/// <returns>An array of <see cref="T:System.Object" /> containing the custom attributes applied to <paramref name="type" />.</returns>
	/// <exception cref="T:System.TypeLoadException">The custom attribute type can not be loaded. </exception>
	public object[] GetCustomAttributes(Type type)
	{
		object[] array = null;
		array = (object[])attributes[type];
		if (array != null)
		{
			return array;
		}
		lock (attributes)
		{
			array = (object[])attributes[type];
			if (array == null)
			{
				if (declaration != null)
				{
					object[] customAttributes = declaration.GetCustomAttributes(type, inherit: false);
					object[] customAttributes2 = methodInfo.GetCustomAttributes(type, inherit: false);
					if (customAttributes2.Length != 0)
					{
						if (!CanMerge(type))
						{
							throw new InvalidOperationException(Res.GetString("ContractOverride", methodInfo.Name, methodInfo.DeclaringType.FullName, declaration.DeclaringType.FullName, declaration.ToString(), customAttributes2[0].ToString()));
						}
						ArrayList arrayList = new ArrayList();
						for (int i = 0; i < customAttributes.Length; i++)
						{
							arrayList.Add(customAttributes[i]);
						}
						for (int j = 0; j < customAttributes2.Length; j++)
						{
							arrayList.Add(customAttributes2[j]);
						}
						array = (object[])arrayList.ToArray(type);
					}
					else
					{
						array = customAttributes;
					}
				}
				else
				{
					array = methodInfo.GetCustomAttributes(type, inherit: false);
				}
				attributes[type] = array;
			}
		}
		return array;
	}

	/// <summary>Returns the first custom attribute applied to the type, if any custom attributes are applied to the type.</summary>
	/// <param name="type">The <see cref="T:System.Type" /> to which the custom attributes are applied. </param>
	/// <returns>An <see cref="T:System.Object" /> containing the first custom attribute applied to the <paramref name="type" /> parameter.</returns>
	/// <exception cref="T:System.TypeLoadException">The custom attribute type can not be loaded. </exception>
	public object GetCustomAttribute(Type type)
	{
		object[] customAttributes = GetCustomAttributes(type);
		if (customAttributes.Length == 0)
		{
			return null;
		}
		return customAttributes[0];
	}

	private static ParameterInfo[] GetInParameters(MethodInfo methodInfo, ParameterInfo[] paramInfos, int start, int length, bool mustBeIn)
	{
		int num = 0;
		for (int i = 0; i < length; i++)
		{
			ParameterInfo parameterInfo = paramInfos[i + start];
			if (IsInParameter(parameterInfo))
			{
				num++;
			}
			else if (mustBeIn)
			{
				throw new InvalidOperationException(Res.GetString("WebBadOutParameter", parameterInfo.Name, methodInfo.DeclaringType.FullName, parameterInfo.Name));
			}
		}
		ParameterInfo[] array = new ParameterInfo[num];
		num = 0;
		for (int j = 0; j < length; j++)
		{
			ParameterInfo parameterInfo2 = paramInfos[j + start];
			if (IsInParameter(parameterInfo2))
			{
				array[num++] = parameterInfo2;
			}
		}
		return array;
	}

	private static ParameterInfo[] GetOutParameters(MethodInfo methodInfo, ParameterInfo[] paramInfos, int start, int length, bool mustBeOut)
	{
		int num = 0;
		for (int i = 0; i < length; i++)
		{
			ParameterInfo parameterInfo = paramInfos[i + start];
			if (IsOutParameter(parameterInfo))
			{
				num++;
			}
			else if (mustBeOut)
			{
				throw new InvalidOperationException(Res.GetString("WebInOutParameter", parameterInfo.Name, methodInfo.DeclaringType.FullName, parameterInfo.Name));
			}
		}
		ParameterInfo[] array = new ParameterInfo[num];
		num = 0;
		for (int j = 0; j < length; j++)
		{
			ParameterInfo parameterInfo2 = paramInfos[j + start];
			if (IsOutParameter(parameterInfo2))
			{
				array[num++] = parameterInfo2;
			}
		}
		return array;
	}

	private static bool IsInParameter(ParameterInfo paramInfo)
	{
		return !paramInfo.IsOut;
	}

	private static bool IsOutParameter(ParameterInfo paramInfo)
	{
		if (!paramInfo.IsOut)
		{
			return paramInfo.ParameterType.IsByRef;
		}
		return true;
	}

	/// <summary>Returns a value indicating whether the method passed in represents a <see langword="Begin" /> method of an asynchronous invocation.</summary>
	/// <param name="methodInfo">The <see cref="T:System.Reflection.MethodInfo" /> that might be a <see langword="Begin" /> method of an asynchronous invocation. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="methodInfo" /> parameter is a <see langword="Begin" /> method of an asynchronous invocation; otherwise, <see langword="false" />.</returns>
	public static bool IsBeginMethod(MethodInfo methodInfo)
	{
		if (typeof(IAsyncResult).IsAssignableFrom(methodInfo.ReturnType))
		{
			return methodInfo.Name.StartsWith("Begin", StringComparison.Ordinal);
		}
		return false;
	}

	/// <summary>Returns a value indicating whether the method passed in represents an <see langword="End" /> method of an asynchronous invocation.</summary>
	/// <param name="methodInfo">The <see cref="T:System.Reflection.MethodInfo" /> that might be an <see langword="End" /> method of an asynchronous invocation. </param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="methodInfo" /> parameter is an <see langword="End" /> method of an asynchronous invocation; otherwise, <see langword="false" />.</returns>
	public static bool IsEndMethod(MethodInfo methodInfo)
	{
		ParameterInfo[] array = methodInfo.GetParameters();
		if (array.Length != 0 && typeof(IAsyncResult).IsAssignableFrom(array[0].ParameterType))
		{
			return methodInfo.Name.StartsWith("End", StringComparison.Ordinal);
		}
		return false;
	}

	/// <summary>Given an array of <see cref="T:System.Reflection.MethodInfo" /> that can contain information about both asynchronous and synchronous methods, creates an array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <param name="methodInfos">An array of <see cref="T:System.Reflection.MethodInfo" /> representing the asynchronous and synchronous methods for which to create <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> objects. </param>
	/// <returns>An array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />, representing the methods within <paramref name="methodInfos" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">A <see langword="Begin" /> asynchronous method is included in <paramref name="methodInfos" /> without a corresponding <see langword="End" /> method. </exception>
	public static LogicalMethodInfo[] Create(MethodInfo[] methodInfos)
	{
		return Create(methodInfos, (LogicalMethodTypes)3, null);
	}

	/// <summary>Given an array of <see cref="T:System.Reflection.MethodInfo" />, where the returned array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> can be restricted to only asynchronous or synchronous methods, creates an array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />.</summary>
	/// <param name="methodInfos">An array of <see cref="T:System.Reflection.MethodInfo" /> representing the asynchronous and synchronous methods for which to create <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> objects. </param>
	/// <param name="types">A bitwise combination of the <see cref="T:System.Web.Services.Protocols.LogicalMethodTypes" /> values. Determines whether just asynchronous or synchronous methods or both are included in the returned array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />. </param>
	/// <returns>An array of <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />, representing the methods within <paramref name="methodInfos" />, filtered by the value of <paramref name="types" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">A <see langword="Begin" /> asynchronous method is included in <paramref name="methodInfos" /> without a corresponding <see langword="End" /> method. </exception>
	public static LogicalMethodInfo[] Create(MethodInfo[] methodInfos, LogicalMethodTypes types)
	{
		return Create(methodInfos, types, null);
	}

	internal static LogicalMethodInfo[] Create(MethodInfo[] methodInfos, LogicalMethodTypes types, Hashtable declarations)
	{
		ArrayList arrayList = (((types & LogicalMethodTypes.Async) != 0) ? new ArrayList() : null);
		Hashtable hashtable = (((types & LogicalMethodTypes.Async) != 0) ? new Hashtable() : null);
		ArrayList arrayList2 = (((types & LogicalMethodTypes.Sync) != 0) ? new ArrayList() : null);
		foreach (MethodInfo methodInfo in methodInfos)
		{
			if (IsBeginMethod(methodInfo))
			{
				arrayList?.Add(methodInfo);
			}
			else if (IsEndMethod(methodInfo))
			{
				hashtable?.Add(methodInfo.Name, methodInfo);
			}
			else
			{
				arrayList2?.Add(methodInfo);
			}
		}
		int num = arrayList?.Count ?? 0;
		int num2 = arrayList2?.Count ?? 0;
		int num3 = num2 + num;
		LogicalMethodInfo[] array = new LogicalMethodInfo[num3];
		num3 = 0;
		for (int j = 0; j < num2; j++)
		{
			MethodInfo key = (MethodInfo)arrayList2[j];
			WebMethod webMethod = ((declarations == null) ? null : ((WebMethod)declarations[key]));
			array[num3] = new LogicalMethodInfo(key, webMethod);
			array[num3].CheckContractOverride();
			num3++;
		}
		for (int k = 0; k < num; k++)
		{
			MethodInfo methodInfo2 = (MethodInfo)arrayList[k];
			string text = "End" + methodInfo2.Name.Substring(5);
			MethodInfo methodInfo3 = (MethodInfo)hashtable[text];
			if (methodInfo3 == null)
			{
				throw new InvalidOperationException(Res.GetString("WebAsyncMissingEnd", methodInfo2.DeclaringType.FullName, methodInfo2.Name, text));
			}
			WebMethod webMethod2 = ((declarations == null) ? null : ((WebMethod)declarations[methodInfo2]));
			array[num3++] = new LogicalMethodInfo(methodInfo2, methodInfo3, webMethod2);
		}
		return array;
	}

	internal string GetKey()
	{
		if (methodInfo == null)
		{
			return string.Empty;
		}
		string text = methodInfo.DeclaringType.FullName + ":" + methodInfo.ToString();
		if (text.Length > 1024)
		{
			text = Convert.ToBase64String(HashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text)));
		}
		return text;
	}

	internal void CheckContractOverride()
	{
		if (declaration == null)
		{
			return;
		}
		methodInfo.GetParameters();
		ParameterInfo[] array = methodInfo.GetParameters();
		for (int i = 0; i < array.Length; i++)
		{
			object[] customAttributes = array[i].GetCustomAttributes(inherit: false);
			foreach (object obj in customAttributes)
			{
				if (obj.GetType().Namespace == "System.Xml.Serialization")
				{
					throw new InvalidOperationException(Res.GetString("ContractOverride", methodInfo.Name, methodInfo.DeclaringType.FullName, declaration.DeclaringType.FullName, declaration.ToString(), obj.ToString()));
				}
			}
		}
	}

	internal static bool CanMerge(Type type)
	{
		if (type == typeof(SoapHeaderAttribute))
		{
			return true;
		}
		if (typeof(SoapExtensionAttribute).IsAssignableFrom(type))
		{
			return true;
		}
		return false;
	}
}
