using System;
using System.Reflection;

namespace UnityEngine;

public class AndroidJavaProxy
{
	public readonly AndroidJavaClass javaInterface;

	internal AndroidJavaObject proxyObject;

	private static readonly GlobalJavaObjectRef s_JavaLangSystemClass = new GlobalJavaObjectRef(AndroidJNISafe.FindClass("java/lang/System"));

	private static readonly IntPtr s_HashCodeMethodID = AndroidJNIHelper.GetMethodID(s_JavaLangSystemClass, "identityHashCode", "(Ljava/lang/Object;)I", isStatic: true);

	public AndroidJavaProxy(string javaInterface)
		: this(new AndroidJavaClass(javaInterface))
	{
	}

	public AndroidJavaProxy(AndroidJavaClass javaInterface)
	{
		this.javaInterface = javaInterface;
	}

	public virtual AndroidJavaObject Invoke(string methodName, object[] args)
	{
		Exception ex = null;
		BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		Type[] array = new Type[args.Length];
		for (int i = 0; i < args.Length; i++)
		{
			array[i] = ((args[i] != null) ? args[i].GetType() : typeof(AndroidJavaObject));
		}
		try
		{
			MethodInfo method = GetType().GetMethod(methodName, bindingAttr, null, array, null);
			if ((object)method != null)
			{
				return _AndroidJNIHelper.Box(method.Invoke(this, args));
			}
		}
		catch (TargetInvocationException ex2)
		{
			ex = ex2.InnerException;
		}
		catch (Exception ex3)
		{
			ex = ex3;
		}
		string[] array2 = new string[args.Length];
		for (int j = 0; j < array.Length; j++)
		{
			array2[j] = array[j].ToString();
		}
		if (ex != null)
		{
			throw new TargetInvocationException(string.Concat(GetType(), ".", methodName, "(", string.Join(",", array2), ")"), ex);
		}
		throw new Exception(string.Concat("No such proxy method: ", GetType(), ".", methodName, "(", string.Join(",", array2), ")"));
	}

	public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
	{
		object[] array = new object[javaArgs.Length];
		for (int i = 0; i < javaArgs.Length; i++)
		{
			array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
		}
		return Invoke(methodName, array);
	}

	public virtual bool equals(AndroidJavaObject obj)
	{
		IntPtr obj2 = obj?.GetRawObject() ?? IntPtr.Zero;
		return AndroidJNI.IsSameObject(GetProxy().GetRawObject(), obj2);
	}

	public virtual int hashCode()
	{
		jvalue[] array = new jvalue[1];
		array[0].l = GetProxy().GetRawObject();
		return AndroidJNISafe.CallStaticIntMethod(s_JavaLangSystemClass, s_HashCodeMethodID, array);
	}

	public virtual string toString()
	{
		return ToString() + " <c# proxy java object>";
	}

	internal AndroidJavaObject GetProxy()
	{
		if (proxyObject == null)
		{
			proxyObject = AndroidJavaObject.AndroidJavaObjectDeleteLocalRef(AndroidJNIHelper.CreateJavaProxy(this));
		}
		return proxyObject;
	}
}
