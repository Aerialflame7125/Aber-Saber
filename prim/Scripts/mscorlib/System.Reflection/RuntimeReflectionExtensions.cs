using System.Collections.Generic;

namespace System.Reflection;

/// <summary>Provides methods that retrieve information about types at run time.</summary>
public static class RuntimeReflectionExtensions
{
	private const BindingFlags everything = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

	private static void CheckAndThrow(Type t)
	{
		if (t == null)
		{
			throw new ArgumentNullException("type");
		}
		if (!(t is RuntimeType))
		{
			throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
		}
	}

	private static void CheckAndThrow(MethodInfo m)
	{
		if (m == null)
		{
			throw new ArgumentNullException("method");
		}
		if (!(m is RuntimeMethodInfo))
		{
			throw new ArgumentException(Environment.GetResourceString("MethodInfo must be a runtime MethodInfo object."));
		}
	}

	/// <summary>Retrieves a collection that represents all the properties defined on a specified type.</summary>
	/// <param name="type">The type that contains the properties.</param>
	/// <returns>A collection of properties for the specified type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	public static IEnumerable<PropertyInfo> GetRuntimeProperties(this Type type)
	{
		CheckAndThrow(type);
		return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}

	/// <summary>Retrieves a collection that represents all the events defined on a specified type.</summary>
	/// <param name="type">The type that contains the events.</param>
	/// <returns>A collection of events for the specified type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	public static IEnumerable<EventInfo> GetRuntimeEvents(this Type type)
	{
		CheckAndThrow(type);
		return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}

	/// <summary>Retrieves a collection that represents all methods defined on a specified type.</summary>
	/// <param name="type">The type that contains the methods.</param>
	/// <returns>A collection of methods for the specified type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	public static IEnumerable<MethodInfo> GetRuntimeMethods(this Type type)
	{
		CheckAndThrow(type);
		return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}

	/// <summary>Retrieves a collection that represents all the fields defined on a specified type.</summary>
	/// <param name="type">The type that contains the fields.</param>
	/// <returns>A collection of fields for the specified type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.</exception>
	public static IEnumerable<FieldInfo> GetRuntimeFields(this Type type)
	{
		CheckAndThrow(type);
		return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
	}

	/// <summary>Retrieves an object that represents a specified property.</summary>
	/// <param name="type">The type that contains the property.</param>
	/// <param name="name">The name of the property.</param>
	/// <returns>An object that represents the specified property, or <see langword="null" /> if the property is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	/// -or-
	/// <paramref name="name" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="type" /> is not a <see langword="RuntimeType" />.</exception>
	/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property with the requested name was found.</exception>
	public static PropertyInfo GetRuntimeProperty(this Type type, string name)
	{
		CheckAndThrow(type);
		return type.GetProperty(name);
	}

	/// <summary>Retrieves an object that represents the specified event.</summary>
	/// <param name="type">The type that contains the event.</param>
	/// <param name="name">The name of the event.</param>
	/// <returns>An object that represents the specified event, or <see langword="null" /> if the event is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	/// -or-
	/// <paramref name="name" /> is <see langword="null" />.</exception>
	public static EventInfo GetRuntimeEvent(this Type type, string name)
	{
		CheckAndThrow(type);
		return type.GetEvent(name);
	}

	/// <summary>Retrieves an object that represents a specified method.</summary>
	/// <param name="type">The type that contains the method.</param>
	/// <param name="name">The name of the method.</param>
	/// <param name="parameters">An array that contains the method's parameters.</param>
	/// <returns>An object that represents the specified method, or <see langword="null" /> if the method is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	/// -or-
	/// <paramref name="name" /> is <see langword="null" />.</exception>
	/// <exception cref="">More than one method is found with the specified name.</exception>
	public static MethodInfo GetRuntimeMethod(this Type type, string name, Type[] parameters)
	{
		CheckAndThrow(type);
		return type.GetMethod(name, parameters);
	}

	/// <summary>Retrieves an object that represents a specified field.</summary>
	/// <param name="type">The type that contains the field.</param>
	/// <param name="name">The name of the field.</param>
	/// <returns>An object that represents the specified field, or <see langword="null" /> if the field is not found.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="type" /> is <see langword="null" />.
	/// -or-
	/// <paramref name="name" /> is <see langword="null" />.</exception>
	public static FieldInfo GetRuntimeField(this Type type, string name)
	{
		CheckAndThrow(type);
		return type.GetField(name);
	}

	/// <summary>Retrieves an object that represents the specified method on the direct or indirect base class where the method was first declared.</summary>
	/// <param name="method">The method to retrieve information about.</param>
	/// <returns>An object that represents the specified method's initial declaration on a base class.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="method" /> is <see langword="null" />.</exception>
	public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
	{
		CheckAndThrow(method);
		return method.GetBaseDefinition();
	}

	/// <summary>Returns an interface mapping for the specified type and the specified interface.</summary>
	/// <param name="typeInfo">The type to retrieve a mapping for.</param>
	/// <param name="interfaceType">The interface to retrieve a mapping for.</param>
	/// <returns>An object that represents the interface mapping for the specified interface and type.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="typeInfo" /> is <see langword="null" />.
	/// -or-
	/// <paramref name="interfaceType" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="interfaceType" /> is not implemented by <paramref name="typeInfo" />.
	/// -or-
	/// <paramref name="interfaceType" /> does not refer to an interface.
	/// -or-
	/// <paramref name="typeInfo" /> or <paramref name="interfaceType" /> is an open generic type.
	/// -or-
	/// <paramref name="interfaceType" /> is a generic interface, and <paramref name="typeInfo" /> is an array type.</exception>
	/// <exception cref="T:System.InvalidOperationException">
	///   <paramref name="typeInfo" /> represents a generic type parameter.</exception>
	/// <exception cref="T:System.NotSupportedException">
	///   <paramref name="typeInfo" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> instance whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called.
	/// -or-
	/// The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
	public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
	{
		if (typeInfo == null)
		{
			throw new ArgumentNullException("typeInfo");
		}
		if (!(typeInfo is RuntimeType))
		{
			throw new ArgumentException(Environment.GetResourceString("Type must be a runtime Type object."));
		}
		return typeInfo.GetInterfaceMap(interfaceType);
	}

	/// <summary>Gets an object that represents the method represented by the specified delegate.</summary>
	/// <param name="del">The delegate to examine.</param>
	/// <returns>An object that represents the method.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="del" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.MemberAccessException">The caller does not have access to the method represented by the delegate (for example, if the method is private).</exception>
	public static MethodInfo GetMethodInfo(this Delegate del)
	{
		if ((object)del == null)
		{
			throw new ArgumentNullException("del");
		}
		return del.Method;
	}
}
