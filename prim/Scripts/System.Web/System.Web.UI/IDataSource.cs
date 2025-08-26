using System.Collections;

namespace System.Web.UI;

/// <summary>Represents an abstract data source that data-bound controls bind to.</summary>
public interface IDataSource
{
	/// <summary>Occurs when a data source control has changed in some way that affects data-bound controls. </summary>
	event EventHandler DataSourceChanged;

	/// <summary>Gets the named data source view associated with the data source control.</summary>
	/// <param name="viewName">The name of the view to retrieve. </param>
	/// <returns>Returns the named <see cref="T:System.Web.UI.DataSourceView" /> associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
	DataSourceView GetView(string viewName);

	/// <summary>Gets a collection of names representing the list of view objects associated with the <see cref="T:System.Web.UI.IDataSource" /> interface.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the names of the views associated with the <see cref="T:System.Web.UI.IDataSource" />.</returns>
	ICollection GetViewNames();
}
