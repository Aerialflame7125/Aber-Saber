using System.ComponentModel;
using System.Windows.Forms;

namespace System.Web.UI.Design;

/// <summary>Starts a URL editor that allows a user to select or create a URL. This class cannot be inherited.</summary>
public sealed class UrlBuilder
{
	private UrlBuilder()
	{
	}

	/// <summary>Creates a UI to create or pick a URL.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services.</param>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window.</param>
	/// <param name="initialUrl">The initial URL to be shown in the picker window.</param>
	/// <param name="caption">The caption of the picker window.</param>
	/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window.</param>
	/// <returns>The URL returned from the UI.</returns>
	[System.MonoTODO]
	public static string BuildUrl(IComponent component, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter)
	{
		return BuildUrl(component, owner, initialUrl, caption, filter, UrlBuilderOptions.None);
	}

	/// <summary>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</summary>
	/// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services.</param>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window.</param>
	/// <param name="initialUrl">The initial URL to be shown in the picker window.</param>
	/// <param name="caption">The caption of the picker window.</param>
	/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window.</param>
	/// <param name="options">A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> indicating the options for URL selection.</param>
	/// <returns>The URL returned from the UI.</returns>
	[System.MonoTODO]
	public static string BuildUrl(IComponent component, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</summary>
	/// <param name="serviceProvider">The <see cref="T:System.IServiceProvider" /> to be used to access design-time services.</param>
	/// <param name="owner">The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window.</param>
	/// <param name="initialUrl">The initial URL to be shown in the picker window.</param>
	/// <param name="caption">The caption of the picker window.</param>
	/// <param name="filter">The filter string to use to optionally filter the files displayed in the picker window.</param>
	/// <param name="options">A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> indicating the options for URL selection.</param>
	/// <returns>The URL returned from the UI.</returns>
	[System.MonoTODO]
	public static string BuildUrl(IServiceProvider serviceProvider, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter, UrlBuilderOptions options)
	{
		throw new NotImplementedException();
	}
}
