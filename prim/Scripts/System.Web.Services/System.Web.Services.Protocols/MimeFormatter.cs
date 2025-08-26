using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>Provides an abstract base class for all readers and writers for Web services and clients implemented using HTTP but without SOAP.</summary>
public abstract class MimeFormatter
{
	/// <summary>When overridden in a derived class, returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>An <see cref="T:System.Object" /> that contains the initializer for the specified method.</returns>
	public abstract object GetInitializer(LogicalMethodInfo methodInfo);

	/// <summary>When overridden in a derived class, initializes an instance.</summary>
	/// <param name="initializer">An object of a type appropriate to the particular class that is implementing the method. </param>
	public abstract void Initialize(object initializer);

	/// <summary>When overridden in a derived class, returns an array of initializer objects corresponding to an input array of method definitions.</summary>
	/// <param name="methodInfos">An array of Web methods where, for each method, the object of the corresponding index in the returned initializer array is obtained.</param>
	/// <returns>An array of initializer objects corresponding to the input array of method definitions.</returns>
	public virtual object[] GetInitializers(LogicalMethodInfo[] methodInfos)
	{
		object[] array = new object[methodInfos.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = GetInitializer(methodInfos[i]);
		}
		return array;
	}

	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="type">The Type, derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />,- for which an initializer is obtained.</param>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>A <see cref="T:System.Object" /> object that contains the initializer for the specified method.</returns>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public static object GetInitializer(Type type, LogicalMethodInfo methodInfo)
	{
		return ((MimeFormatter)Activator.CreateInstance(type)).GetInitializer(methodInfo);
	}

	/// <summary>Returns an array of initializer objects corresponding to an input array of method definitions for a specified class derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />.</summary>
	/// <param name="type">The Type, derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />, for which initializers are obtained.</param>
	/// <param name="methodInfos">An array of type <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web methods for which the initializers are obtained.</param>
	/// <returns>An array of initializer objects corresponding to the input array of method definitions for a specified class derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />.</returns>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public static object[] GetInitializers(Type type, LogicalMethodInfo[] methodInfos)
	{
		return ((MimeFormatter)Activator.CreateInstance(type)).GetInitializers(methodInfos);
	}

	/// <summary>Creates and initializes an instance of a concrete class derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />.</summary>
	/// <param name="type">The Type, derived from <see cref="T:System.Web.Services.Protocols.MimeFormatter" />, of which to create an instance.</param>
	/// <param name="initializer">An object used to initialize the instance obtained earlier through the derived class's implementation of the <see cref="M:System.Web.Services.Protocols.MimeFormatter.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo)" /> method.</param>
	/// <returns>A <see cref="T:System.Web.Services.Protocols.MimeFormatter" /> object.</returns>
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public static MimeFormatter CreateInstance(Type type, object initializer)
	{
		MimeFormatter obj = (MimeFormatter)Activator.CreateInstance(type);
		obj.Initialize(initializer);
		return obj;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MimeFormatter" /> class. </summary>
	protected MimeFormatter()
	{
	}
}
