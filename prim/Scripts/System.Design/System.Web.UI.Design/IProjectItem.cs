namespace System.Web.UI.Design;

/// <summary>Provides an interface for an item that is retrieved at design time from a design host, such as Visual Studio 2005.</summary>
public interface IProjectItem
{
	/// <summary>Gets the URL for the item relative to the design host.</summary>
	/// <returns>The relative URL.</returns>
	string AppRelativeUrl { get; }

	/// <summary>Gets the name of the item.</summary>
	/// <returns>The name of the item.</returns>
	string Name { get; }

	/// <summary>Gets a reference to the containing item, if any.</summary>
	/// <returns>An <see cref="T:System.Web.UI.Design.IProjectItem" />, if the current item is contained within another item; otherwise, <see langword="null" />.</returns>
	IProjectItem Parent { get; }

	/// <summary>Gets the path for a project item.</summary>
	/// <returns>The path for the item.</returns>
	string PhysicalPath { get; }
}
