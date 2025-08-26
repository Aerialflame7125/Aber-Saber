using System.Globalization;

namespace System.ComponentModel.Design;

/// <summary>Provides design-time support for localization features to a root designer.</summary>
[Obsolete("use CodeDomLocalizationProvider")]
[ProvideProperty("Localizable", typeof(object))]
[ProvideProperty("Language", typeof(object))]
[ProvideProperty("LoadLanguage", typeof(object))]
public class LocalizationExtenderProvider : IExtenderProvider, IDisposable
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" /> class using the specified service provider and base component.</summary>
	/// <param name="serviceProvider">A service provider for the specified component.</param>
	/// <param name="baseComponent">The base component to localize.</param>
	[System.MonoTODO]
	public LocalizationExtenderProvider(ISite serviceProvider, IComponent baseComponent)
	{
	}

	/// <summary>Indicates whether this object can provide its extender properties to the specified object.</summary>
	/// <param name="o">The object to receive the extender properties.</param>
	/// <returns>
	///   <see langword="true" /> if this object can provide extender properties to the specified object; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool CanExtend(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" />.</summary>
	[System.MonoTODO]
	public void Dispose()
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.LocalizationExtenderProvider" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	[System.MonoTODO]
	protected virtual void Dispose(bool disposing)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the current resource culture for the specified object.</summary>
	/// <param name="o">The object to get the current resource culture for.</param>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> indicating the resource variety.</returns>
	[System.MonoTODO]
	[Localizable(true)]
	[DesignOnly(true)]
	public CultureInfo GetLanguage(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the default resource culture to use when initializing the values of a localized object at design time.</summary>
	/// <param name="o">The object to get the resource culture for.</param>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> indicating the resource culture to use to initialize the values of the specified object.</returns>
	[System.MonoTODO]
	[DesignOnly(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public CultureInfo GetLoadLanguage(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the specified object supports resource localization.</summary>
	/// <param name="o">The object to check for localization support.</param>
	/// <returns>
	///   <see langword="true" /> if the specified object supports resource localization; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	[Localizable(true)]
	[DesignOnly(true)]
	public bool GetLocalizable(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>Resets the resource culture for the specified object.</summary>
	/// <param name="o">The object to reset the resource culture for.</param>
	[System.MonoTODO]
	public void ResetLanguage(object o)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the current resource culture for the specified object to the specified resource culture.</summary>
	/// <param name="o">The base component to set the resource culture for.</param>
	/// <param name="language">A <see cref="T:System.Globalization.CultureInfo" /> that indicates the resource culture to use.</param>
	[System.MonoTODO]
	public void SetLanguage(object o, CultureInfo language)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets a value indicating whether the specified object supports localized resources.</summary>
	/// <param name="o">The base component to set as localizable or not localizable.</param>
	/// <param name="localizable">
	///   <see langword="true" /> if the object supports resource localization; otherwise, <see langword="false" />.</param>
	[System.MonoTODO]
	public void SetLocalizable(object o, bool localizable)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a value indicating whether the specified object must have its localizable values persisted in a resource.</summary>
	/// <param name="o">The object to get the language support persistence flag for.</param>
	/// <returns>
	///   <see langword="true" /> if the localizable values should be persisted in resources; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool ShouldSerializeLanguage(object o)
	{
		throw new NotImplementedException();
	}
}
