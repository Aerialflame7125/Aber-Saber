namespace System.Web.Services.Protocols;

internal class SoapReflectedExtension : IComparable
{
	private Type type;

	private SoapExtensionAttribute attribute;

	private int priority;

	internal SoapReflectedExtension(Type type, SoapExtensionAttribute attribute)
		: this(type, attribute, attribute.Priority)
	{
	}

	internal SoapReflectedExtension(Type type, SoapExtensionAttribute attribute, int priority)
	{
		if (priority < 0)
		{
			throw new ArgumentException(Res.GetString("WebConfigInvalidExtensionPriority", priority), "priority");
		}
		this.type = type;
		this.attribute = attribute;
		this.priority = priority;
	}

	internal SoapExtension CreateInstance(object initializer)
	{
		SoapExtension obj = (SoapExtension)Activator.CreateInstance(type);
		obj.Initialize(initializer);
		return obj;
	}

	internal object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return ((SoapExtension)Activator.CreateInstance(type)).GetInitializer(methodInfo, attribute);
	}

	internal object GetInitializer(Type serviceType)
	{
		return ((SoapExtension)Activator.CreateInstance(type)).GetInitializer(serviceType);
	}

	internal static object[] GetInitializers(LogicalMethodInfo methodInfo, SoapReflectedExtension[] extensions)
	{
		object[] array = new object[extensions.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = extensions[i].GetInitializer(methodInfo);
		}
		return array;
	}

	internal static object[] GetInitializers(Type serviceType, SoapReflectedExtension[] extensions)
	{
		object[] array = new object[extensions.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = extensions[i].GetInitializer(serviceType);
		}
		return array;
	}

	public int CompareTo(object o)
	{
		return priority - ((SoapReflectedExtension)o).priority;
	}
}
