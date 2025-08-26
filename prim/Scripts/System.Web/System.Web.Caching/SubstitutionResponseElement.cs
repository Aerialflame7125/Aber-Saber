using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Caching;

/// <summary>Represents a managed delegate that can be called to insert dynamically generated output into an output-cache response. </summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Unrestricted)]
public class SubstitutionResponseElement : ResponseElement
{
	private string typeName;

	private string methodName;

	/// <summary>Gets a reference to the substitution callback method.</summary>
	/// <returns>A callback method reference.</returns>
	public HttpResponseSubstitutionCallback Callback { get; private set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Caching.SubstitutionResponseElement" /> class.</summary>
	/// <param name="callback">The static substitution callback that was set as part of the response for an output-cached page.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="callback" /> is <see langword="null" />. </exception>
	public SubstitutionResponseElement(HttpResponseSubstitutionCallback callback)
	{
		if (callback == null)
		{
			throw new ArgumentNullException("callback");
		}
		Callback = callback;
		MethodInfo method = callback.Method;
		typeName = method.DeclaringType.AssemblyQualifiedName;
		methodName = method.Name;
	}

	[OnDeserialized]
	private void ObjectDeserialized(StreamingContext context)
	{
		Type type = Type.GetType(typeName, throwOnError: true);
		Callback = Delegate.CreateDelegate(typeof(HttpResponseSubstitutionCallback), type, methodName, ignoreCase: false, throwOnBindFailure: true) as HttpResponseSubstitutionCallback;
	}
}
