using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono;

namespace System.Reflection;

/// <summary>Discovers the attributes of a property and provides access to property metadata.</summary>
[Serializable]
[ClassInterface(ClassInterfaceType.None)]
[ComVisible(true)]
[ComDefaultInterface(typeof(_PropertyInfo))]
public abstract class PropertyInfo : MemberInfo, _PropertyInfo
{
	/// <summary>Gets the attributes for this property.</summary>
	/// <returns>The attributes of this property.</returns>
	public abstract PropertyAttributes Attributes { get; }

	/// <summary>Gets a value indicating whether the property can be read.</summary>
	/// <returns>
	///   <see langword="true" /> if this property can be read; otherwise, <see langword="false" />.</returns>
	public abstract bool CanRead { get; }

	/// <summary>Gets a value indicating whether the property can be written to.</summary>
	/// <returns>
	///   <see langword="true" /> if this property can be written to; otherwise, <see langword="false" />.</returns>
	public abstract bool CanWrite { get; }

	/// <summary>Gets the <see langword="get" /> accessor for this property.</summary>
	/// <returns>The <see langword="get" /> accessor for this property.</returns>
	public virtual MethodInfo GetMethod => GetGetMethod(nonPublic: true);

	/// <summary>Gets the <see langword="set" /> accessor for this property.</summary>
	/// <returns>The <see langword="set" /> accessor for this property, or <see langword="null" /> if the property is read-only.</returns>
	public virtual MethodInfo SetMethod => GetSetMethod(nonPublic: true);

	/// <summary>Gets a value indicating whether the property is the special name.</summary>
	/// <returns>
	///   <see langword="true" /> if this property is the special name; otherwise, <see langword="false" />.</returns>
	public bool IsSpecialName => (Attributes & PropertyAttributes.SpecialName) != 0;

	/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</summary>
	/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</returns>
	public override MemberTypes MemberType => MemberTypes.Property;

	/// <summary>Gets the type of this property.</summary>
	/// <returns>The type of this property.</returns>
	public abstract Type PropertyType { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.PropertyInfo" /> class.</summary>
	protected PropertyInfo()
	{
	}

	/// <summary>Returns an array whose elements reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
	/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects that reflect the public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.</returns>
	public MethodInfo[] GetAccessors()
	{
		return GetAccessors(nonPublic: false);
	}

	/// <summary>Returns an array whose elements reflect the public and, if specified, non-public <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance.</summary>
	/// <param name="nonPublic">Indicates whether non-public methods should be returned in the returned array. <see langword="true" /> if non-public methods are to be included; otherwise, <see langword="false" />.</param>
	/// <returns>An array whose elements reflect the <see langword="get" /> and <see langword="set" /> accessors of the property reflected by the current instance. If <paramref name="nonPublic" /> is <see langword="true" />, this array contains public and non-public <see langword="get" /> and <see langword="set" /> accessors. If <paramref name="nonPublic" /> is <see langword="false" />, this array contains only public <see langword="get" /> and <see langword="set" /> accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.</returns>
	public abstract MethodInfo[] GetAccessors(bool nonPublic);

	/// <summary>Returns the public <see langword="get" /> accessor for this property.</summary>
	/// <returns>A <see langword="MethodInfo" /> object representing the public <see langword="get" /> accessor for this property, or <see langword="null" /> if the <see langword="get" /> accessor is non-public or does not exist.</returns>
	public MethodInfo GetGetMethod()
	{
		return GetGetMethod(nonPublic: false);
	}

	/// <summary>When overridden in a derived class, returns the public or non-public <see langword="get" /> accessor for this property.</summary>
	/// <param name="nonPublic">Indicates whether a non-public <see langword="get" /> accessor should be returned. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
	/// <returns>A <see langword="MethodInfo" /> object representing the <see langword="get" /> accessor for this property, if <paramref name="nonPublic" /> is <see langword="true" />. Returns <see langword="null" /> if <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="get" /> accessor is non-public, or if <paramref name="nonPublic" /> is <see langword="true" /> but no <see langword="get" /> accessors exist.</returns>
	/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
	public abstract MethodInfo GetGetMethod(bool nonPublic);

	/// <summary>When overridden in a derived class, returns an array of all the index parameters for the property.</summary>
	/// <returns>An array of type <see langword="ParameterInfo" /> containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
	public abstract ParameterInfo[] GetIndexParameters();

	/// <summary>Returns the public <see langword="set" /> accessor for this property.</summary>
	/// <returns>The <see langword="MethodInfo" /> object representing the <see langword="Set" /> method for this property if the <see langword="set" /> accessor is public, or <see langword="null" /> if the <see langword="set" /> accessor is not public.</returns>
	public MethodInfo GetSetMethod()
	{
		return GetSetMethod(nonPublic: false);
	}

	/// <summary>When overridden in a derived class, returns the <see langword="set" /> accessor for this property.</summary>
	/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. <see langword="true" /> if a non-public accessor is to be returned; otherwise, <see langword="false" />.</param>
	/// <returns>This property's <see langword="Set" /> method, or <see langword="null" />, as shown in the following table.  
	///   Value  
	///
	///   Condition  
	///
	///   The <see langword="Set" /> method for this property.  
	///
	///   The <see langword="set" /> accessor is public.  
	///
	///  -or-  
	///
	///  <paramref name="nonPublic" /> is <see langword="true" /> and the <see langword="set" /> accessor is non-public.  
	///
	///  <see langword="null" /><paramref name="nonPublic" /> is <see langword="true" />, but the property is read-only.  
	///
	///  -or-  
	///
	///  <paramref name="nonPublic" /> is <see langword="false" /> and the <see langword="set" /> accessor is non-public.  
	///
	///  -or-  
	///
	///  There is no <see langword="set" /> accessor.</returns>
	/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method.</exception>
	public abstract MethodInfo GetSetMethod(bool nonPublic);

	/// <summary>Returns the property value of a specified object with optional index values for indexed properties.</summary>
	/// <param name="obj">The object whose property value will be returned.</param>
	/// <param name="index">Optional index values for indexed properties. The indexes of indexed properties are zero-based. This value should be <see langword="null" /> for non-indexed properties.</param>
	/// <returns>The property value of the specified object.</returns>
	/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
	///  -or-  
	///  The property's <see langword="get" /> accessor is not found.</exception>
	/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
	///
	///
	///
	///
	///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
	/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
	///
	///
	///
	///
	///  There was an illegal attempt to access a private or protected method inside a class.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
	[DebuggerHidden]
	[DebuggerStepThrough]
	public virtual object GetValue(object obj, object[] index)
	{
		return GetValue(obj, BindingFlags.Default, null, index, null);
	}

	/// <summary>Returns the property value of a specified object.</summary>
	/// <param name="obj">The object whose property value will be returned.</param>
	/// <returns>The property value of the specified object.</returns>
	[DebuggerHidden]
	[DebuggerStepThrough]
	public object GetValue(object obj)
	{
		return GetValue(obj, BindingFlags.Default, null, null, null);
	}

	/// <summary>When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.</summary>
	/// <param name="obj">The object whose property value will be returned.</param>
	/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, and <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
	/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
	/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
	/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
	/// <returns>The property value of the specified object.</returns>
	/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
	///  -or-  
	///  The property's <see langword="get" /> accessor is not found.</exception>
	/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
	/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
	public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

	/// <summary>Sets the property value of a specified object with optional index values for index properties.</summary>
	/// <param name="obj">The object whose property value will be set.</param>
	/// <param name="value">The new property value.</param>
	/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
	///  -or-  
	///  The property's <see langword="set" /> accessor is not found.  
	///  -or-  
	///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
	/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
	///
	///
	///
	///
	///  The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
	/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
	///
	///
	///
	///
	///  There was an illegal attempt to access a private or protected method inside a class.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
	[DebuggerHidden]
	[DebuggerStepThrough]
	public virtual void SetValue(object obj, object value, object[] index)
	{
		SetValue(obj, value, BindingFlags.Default, null, index, null);
	}

	/// <summary>Sets the property value of a specified object.</summary>
	/// <param name="obj">The object whose property value will be set.</param>
	/// <param name="value">The new property value.</param>
	/// <exception cref="T:System.ArgumentException">The property's <see langword="set" /> accessor is not found.  
	///  -or-  
	///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
	/// <exception cref="T:System.Reflection.TargetException">In the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.  
	///
	///
	///
	///
	///  The type of <paramref name="obj" /> does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.MethodAccessException">In the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.  
	///
	///
	///
	///
	///  There was an illegal attempt to access a private or protected method inside a class.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
	[DebuggerStepThrough]
	[DebuggerHidden]
	public void SetValue(object obj, object value)
	{
		SetValue(obj, value, BindingFlags.Default, null, null, null);
	}

	/// <summary>When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.</summary>
	/// <param name="obj">The object whose property value will be set.</param>
	/// <param name="value">The new property value.</param>
	/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, or <see langword="SetProperty" />. You must specify a suitable invocation attribute. For example, to invoke a static member, set the <see langword="Static" /> flag.</param>
	/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is <see langword="null" />, the default binder is used.</param>
	/// <param name="index">Optional index values for indexed properties. This value should be <see langword="null" /> for non-indexed properties.</param>
	/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is <see langword="null" />, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property.</param>
	/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.  
	///  -or-  
	///  The property's <see langword="set" /> accessor is not found.  
	///  -or-  
	///  <paramref name="value" /> cannot be converted to the type of <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.</exception>
	/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes.</exception>
	/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
	public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

	/// <summary>Returns an array of types representing the optional custom modifiers of the property.</summary>
	/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
	public virtual Type[] GetOptionalCustomModifiers()
	{
		return Type.EmptyTypes;
	}

	/// <summary>Returns an array of types representing the required custom modifiers of the property.</summary>
	/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
	public virtual Type[] GetRequiredCustomModifiers()
	{
		return Type.EmptyTypes;
	}

	private static NotImplementedException CreateNIE()
	{
		return new NotImplementedException();
	}

	/// <summary>Returns a literal value associated with the property by a compiler.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
	/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata.</exception>
	public virtual object GetConstantValue()
	{
		throw CreateNIE();
	}

	/// <summary>Returns a literal value associated with the property by a compiler.</summary>
	/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is <see langword="null" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
	/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures.</exception>
	public virtual object GetRawConstantValue()
	{
		throw CreateNIE();
	}

	/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
	/// <param name="obj">An object to compare with this instance, or <see langword="null" />.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="obj" /> equals the type and value of this instance; otherwise, <see langword="false" />.</returns>
	public override bool Equals(object obj)
	{
		return obj == this;
	}

	/// <summary>Returns the hash code for this instance.</summary>
	/// <returns>A 32-bit signed integer hash code.</returns>
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are equal.</summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
	public static bool operator ==(PropertyInfo left, PropertyInfo right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if (((object)left == null) ^ ((object)right == null))
		{
			return false;
		}
		return left.Equals(right);
	}

	/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are not equal.</summary>
	/// <param name="left">The first object to compare.</param>
	/// <param name="right">The second object to compare.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, <see langword="false" />.</returns>
	public static bool operator !=(PropertyInfo left, PropertyInfo right)
	{
		if ((object)left == right)
		{
			return false;
		}
		if (((object)left == null) ^ ((object)right == null))
		{
			return true;
		}
		return !left.Equals(right);
	}

	/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
	/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
	/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
	/// <param name="cNames">Count of the names to be mapped.</param>
	/// <param name="lcid">The locale context in which to interpret the names.</param>
	/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
	/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
	void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</summary>
	/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</returns>
	Type _PropertyInfo.GetType()
	{
		return GetType();
	}

	/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
	/// <param name="iTInfo">The type information to return.</param>
	/// <param name="lcid">The locale identifier for the type information.</param>
	/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
	/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
	void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
	/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
	/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
	void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides access to properties and methods exposed by an object.</summary>
	/// <param name="dispIdMember">Identifies the member.</param>
	/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
	/// <param name="lcid">The locale context in which to interpret arguments.</param>
	/// <param name="wFlags">Flags describing the context of the call.</param>
	/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
	/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
	/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
	/// <param name="puArgErr">The index of the first argument that has an error.</param>
	/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
	void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
	{
		throw new NotImplementedException();
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	private static extern PropertyInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

	internal static PropertyInfo GetPropertyFromHandle(RuntimePropertyHandle handle, RuntimeTypeHandle reflectedType)
	{
		if (handle.Value == IntPtr.Zero)
		{
			throw new ArgumentException("The handle is invalid.");
		}
		PropertyInfo propertyInfo = internal_from_handle_type(handle.Value, reflectedType.Value);
		if (propertyInfo == null)
		{
			throw new ArgumentException("The property handle and the type handle are incompatible.");
		}
		return propertyInfo;
	}
}
