using System.Collections;
using System.ComponentModel.Design;
using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Provides information to the parser during design time.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class DesignTimeParseData
{
	private EventHandler db_handler;

	private string text;

	private IDesignerHost host;

	private string durl;

	private string filter;

	private bool theme;

	private ICollection collection;

	/// <summary>Gets or sets the delegate for data binding at design time.</summary>
	/// <returns>An <see cref="T:System.EventHandler" /> for data binding at design time.</returns>
	public EventHandler DataBindingHandler
	{
		get
		{
			return db_handler;
		}
		set
		{
			db_handler = value;
		}
	}

	/// <summary>Gets the object for managing designer transactions and components.</summary>
	/// <returns>An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> object for managing designer transactions and components.</returns>
	public IDesignerHost DesignerHost => host;

	/// <summary>Gets or sets the URL at which the document is located.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the URL.</returns>
	public string DocumentUrl
	{
		get
		{
			return durl;
		}
		set
		{
			durl = value;
		}
	}

	/// <summary>Gets the text to parse.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the text to parse.</returns>
	public string ParseText => text;

	/// <summary>Gets the filter used at design time.</summary>
	/// <returns>A <see cref="T:System.String" /> representing the filter.</returns>
	public string Filter => filter;

	/// <summary>Gets or sets a value that indicates whether a theme should be applied.</summary>
	/// <returns>
	///     <see langword="true" /> if a theme should be applied; otherwise, <see langword="false" />.</returns>
	public bool ShouldApplyTheme
	{
		get
		{
			return theme;
		}
		set
		{
			theme = value;
		}
	}

	/// <summary>Gets a collection of information about user control registrations.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the tag prefix, tag name, and location of the user control. The collection is populated automatically by the .NET Framework at parse time.</returns>
	public ICollection UserControlRegisterEntries => collection;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class without a specified filter. </summary>
	/// <param name="designerHost">The object for managing designer transactions and components.</param>
	/// <param name="parseText">The text to parse during design time.</param>
	public DesignTimeParseData(IDesignerHost designerHost, string parseText)
	{
		host = designerHost;
		text = parseText;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DesignTimeParseData" /> class with the specified filter. </summary>
	/// <param name="designerHost">The object for managing designer transactions and components.</param>
	/// <param name="parseText">The text to parse during design time.</param>
	/// <param name="filter">The filter to apply during design time.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="parseText" /> is <see langword="null" />.</exception>
	public DesignTimeParseData(IDesignerHost designerHost, string parseText, string filter)
		: this(designerHost, parseText)
	{
		this.filter = filter;
	}

	internal void SetCollection(ICollection collection)
	{
		this.collection = collection;
	}
}
