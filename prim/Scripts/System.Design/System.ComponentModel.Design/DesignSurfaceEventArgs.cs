namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurfaceManager.DesignSurfaceCreated" /> event.</summary>
public class DesignSurfaceEventArgs : EventArgs
{
	private DesignSurface _surface;

	/// <summary>Gets the design surface that is being created.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is being created.</returns>
	public DesignSurface Surface => _surface;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.DesignSurfaceEventArgs" /> class.</summary>
	/// <param name="surface">The design surface that is being created.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="surface" /> is <see langword="null" />.</exception>
	public DesignSurfaceEventArgs(DesignSurface surface)
	{
		_surface = surface;
	}
}
