using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data.Common;
using System.Data.SqlClient;

namespace System.Web.Configuration;

/// <summary>Provides methods for creating provider instances, either singly or in a batch.</summary>
public static class ProvidersHelper
{
	/// <summary>Initializes and returns a single provider of the given type using the supplied settings.</summary>
	/// <param name="providerSettings">The settings to be passed to the provider upon initialization.</param>
	/// <param name="providerType">The <see cref="T:System.Type" /> of the provider to be initialized.</param>
	/// <returns>A new provider of the given type using the supplied settings.</returns>
	/// <exception cref="T:System.ArgumentException">The provider type defined in configuration was <see langword="null" /> or an empty string ("").- or -The provider type defined in configuration is not compatible with the type used by the feature that is attempting to create a new instance of the provider.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The provider threw an exception while it was being initialized.- or -An error occurred while attempting to resolve a <see cref="T:System.Type" /> instance for the provider specified in <paramref name="providerSettings" />.</exception>
	public static ProviderBase InstantiateProvider(ProviderSettings providerSettings, Type providerType)
	{
		Type type = HttpApplication.LoadType(providerSettings.Type);
		if (type == null)
		{
			throw new ConfigurationErrorsException($"Could not find type: {providerSettings.Type}");
		}
		if (!providerType.IsAssignableFrom(type))
		{
			throw new ConfigurationErrorsException($"Provider '{providerSettings.Name}' must subclass from '{providerType}'");
		}
		ProviderBase obj = Activator.CreateInstance(type) as ProviderBase;
		obj.Initialize(config: new NameValueCollection(providerSettings.Parameters), name: providerSettings.Name);
		return obj;
	}

	/// <summary>Initializes a collection of providers of the given type using the supplied settings.</summary>
	/// <param name="configProviders">A collection of settings to be passed to the provider upon initialization.</param>
	/// <param name="providers">The collection used to contain the initialized providers after the method returns.</param>
	/// <param name="providerType">The <see cref="T:System.Type" /> of the providers to be initialized.</param>
	public static void InstantiateProviders(ProviderSettingsCollection configProviders, ProviderCollection providers, Type providerType)
	{
		if (!typeof(ProviderBase).IsAssignableFrom(providerType))
		{
			throw new ConfigurationErrorsException($"type '{providerType}' must subclass from ProviderBase");
		}
		foreach (ProviderSettings configProvider in configProviders)
		{
			providers.Add(InstantiateProvider(configProvider, providerType));
		}
	}

	internal static DbProviderFactory GetDbProviderFactory(string providerName)
	{
		DbProviderFactory dbProviderFactory = null;
		if (providerName != null && providerName != "")
		{
			try
			{
				dbProviderFactory = DbProviderFactories.GetFactory(providerName);
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
			if (dbProviderFactory != null)
			{
				return dbProviderFactory;
			}
		}
		return SqlClientFactory.Instance;
	}
}
