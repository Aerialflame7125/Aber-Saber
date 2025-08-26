using System.Reflection;

namespace System.Windows.Forms;

/// <summary>Provides static methods for retrieving feature information from the current system.</summary>
/// <filterpriority>2</filterpriority>
public abstract class FeatureSupport : IFeatureSupport
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.FeatureSupport" /> class. </summary>
	protected FeatureSupport()
	{
	}

	private static IFeatureSupport FeatureObject(string class_name)
	{
		Type type = Type.GetType(class_name);
		if ((object)type != null && typeof(IFeatureSupport).IsAssignableFrom(type))
		{
			ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
			if ((object)constructor != null)
			{
				return (IFeatureSupport)constructor.Invoke(new object[0]);
			}
		}
		return null;
	}

	/// <summary>Gets the version of the specified feature that is available on the system.</summary>
	/// <returns>A <see cref="T:System.Version" /> with the version number of the specified feature available on the system; or null if the feature is not installed.</returns>
	/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
	/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	public static Version GetVersionPresent(string featureClassName, string featureConstName)
	{
		return FeatureObject(featureClassName)?.GetVersionPresent(featureConstName);
	}

	/// <summary>Determines whether any version of the specified feature is installed in the system. This method is static.</summary>
	/// <returns>true if the specified feature is present; false if the specified feature is not present or if the product containing the feature is not installed.</returns>
	/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
	/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	public static bool IsPresent(string featureClassName, string featureConstName)
	{
		return FeatureObject(featureClassName)?.IsPresent(featureConstName) ?? false;
	}

	/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system. This method is static.</summary>
	/// <returns>true if the feature is present and its version number is greater than or equal to the specified minimum version number; false if the feature is not installed or its version number is below the specified minimum number.</returns>
	/// <param name="featureClassName">The fully qualified name of the class to query for information about the specified feature. This class must implement the <see cref="T:System.Windows.Forms.IFeatureSupport" /> interface or inherit from a class that implements this interface. </param>
	/// <param name="featureConstName">The fully qualified name of the feature to look for. </param>
	/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature. </param>
	/// <filterpriority>1</filterpriority>
	public static bool IsPresent(string featureClassName, string featureConstName, Version minimumVersion)
	{
		return FeatureObject(featureClassName)?.IsPresent(featureConstName, minimumVersion) ?? false;
	}

	/// <summary>When overridden in a derived class, gets the version of the specified feature that is available on the system.</summary>
	/// <returns>A <see cref="T:System.Version" /> representing the version number of the specified feature available on the system; or null if the feature is not installed.</returns>
	/// <param name="feature">The feature whose version is requested. </param>
	/// <filterpriority>1</filterpriority>
	public abstract Version GetVersionPresent(object feature);

	/// <summary>Determines whether any version of the specified feature is installed in the system.</summary>
	/// <returns>true if the feature is present; otherwise, false.</returns>
	/// <param name="feature">The feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool IsPresent(object feature)
	{
		if (GetVersionPresent(feature) != null)
		{
			return true;
		}
		return false;
	}

	/// <summary>Determines whether the specified or newer version of the specified feature is installed in the system.</summary>
	/// <returns>true if the feature is present and its version number is greater than or equal to the specified minimum version number; false if the feature is not installed or its version number is below the specified minimum number.</returns>
	/// <param name="feature">The feature to look for. </param>
	/// <param name="minimumVersion">A <see cref="T:System.Version" /> representing the minimum version number of the feature to look for. </param>
	/// <filterpriority>1</filterpriority>
	public virtual bool IsPresent(object feature, Version minimumVersion)
	{
		bool result = false;
		Version versionPresent = GetVersionPresent(feature);
		if (versionPresent != null && versionPresent >= minimumVersion)
		{
			result = true;
		}
		return result;
	}
}
