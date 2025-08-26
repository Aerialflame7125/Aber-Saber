namespace System.Web;

/// <summary>Provides expanded support for application startup.</summary>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public sealed class PreApplicationStartMethodAttribute : Attribute
{
	private readonly Type _type;

	private readonly string _methodName;

	/// <summary>Gets the type that is returned by the associated startup method.</summary>
	/// <returns>An object that describes the type of the startup method.</returns>
	public Type Type => _type;

	/// <summary>Gets the associated startup method.</summary>
	/// <returns>A string that contains the name of the associated startup method.</returns>
	public string MethodName => _methodName;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.PreApplicationStartMethodAttribute" /> class.</summary>
	/// <param name="type">An object that describes the type of the startup method..</param>
	/// <param name="methodName">An empty parameter signature that has no return value. </param>
	public PreApplicationStartMethodAttribute(Type type, string methodName)
	{
		_type = type;
		_methodName = methodName;
	}
}
