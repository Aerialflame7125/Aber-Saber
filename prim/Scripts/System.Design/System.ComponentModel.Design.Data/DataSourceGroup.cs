using System.Drawing;

namespace System.ComponentModel.Design.Data;

/// <summary>Implements the basic functionality required by a single data source at the <see langword="EnvDTE.Project" /> level.</summary>
public abstract class DataSourceGroup
{
	/// <summary>When overridden in a derived class, gets the collection of descriptors for the data sources in this group.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.Data.DataSourceDescriptorCollection" /> that represents the collection of descriptors for the data sources in this group.</returns>
	public abstract DataSourceDescriptorCollection DataSources { get; }

	/// <summary>When overridden in a derived class, gets the <see cref="T:System.Drawing.Bitmap" /> image that represents the group.</summary>
	/// <returns>A <see cref="T:System.Drawing.Bitmap" /> image that represents the group.</returns>
	public abstract Bitmap Image { get; }

	/// <summary>When overridden in a derived class, gets the value indicating whether this group is the default group.</summary>
	/// <returns>
	///   <see langword="true" /> if this group is the default group; otherwise, <see langword="false" />.</returns>
	public abstract bool IsDefault { get; }

	/// <summary>When overridden in a derived class, gets the name of the group.</summary>
	/// <returns>The name of the group.</returns>
	public abstract string Name { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DataSourceGroup" /> class.</summary>
	protected DataSourceGroup()
	{
	}
}
