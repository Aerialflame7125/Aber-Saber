using System.Drawing;

namespace System.ComponentModel.Design.Data;

/// <summary>Implements the basic functionality required by a single data source at the <see langword="EnvDTE.Project" /> level.</summary>
public abstract class DataSourceDescriptor
{
	/// <summary>When overridden in a derived class, closes this stream and the underlying stream gets the <see cref="T:System.Drawing.Bitmap" /> image that represents the data source.</summary>
	/// <returns>A <see cref="T:System.Drawing.Bitmap" /> image that represents the data source.</returns>
	public abstract Bitmap Image { get; }

	/// <summary>When overridden in a derived class, gets the value indicating whether the data source is designable.</summary>
	/// <returns>
	///   <see langword="true" /> if the data source is designable; otherwise, <see langword="false" />.</returns>
	public abstract bool IsDesignable { get; }

	/// <summary>When overridden in a derived class, gets the name of the data source.</summary>
	/// <returns>The name of the data source.</returns>
	public abstract string Name { get; }

	/// <summary>When overridden in a derived class, gets the fully qualified type name of the data source.</summary>
	/// <returns>The fully qualified type name of the data source.</returns>
	public abstract string TypeName { get; }

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Data.DataSourceDescriptor" /> class.</summary>
	protected DataSourceDescriptor()
	{
	}
}
