namespace System.ComponentModel.Design;

/// <summary>Provides data for the <see cref="E:System.ComponentModel.Design.DesignSurfaceManager.ActiveDesignSurfaceChanged" /> event.</summary>
public class ActiveDesignSurfaceChangedEventArgs : EventArgs
{
	private DesignSurface _oldSurface;

	private DesignSurface _newSurface;

	/// <summary>Gets the design surface that is losing activation.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is losing activation.</returns>
	public DesignSurface OldSurface => _oldSurface;

	/// <summary>Gets the design surface that is gaining activation.</summary>
	/// <returns>A <see cref="T:System.ComponentModel.Design.DesignSurface" /> that is gaining activation.</returns>
	public DesignSurface NewSurface => _newSurface;

	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ActiveDesignSurfaceChangedEventArgs" /> class.</summary>
	/// <param name="oldSurface">The design surface that is losing activation.</param>
	/// <param name="newSurface">The design surface that is gaining activation.</param>
	public ActiveDesignSurfaceChangedEventArgs(DesignSurface oldSurface, DesignSurface newSurface)
	{
		_newSurface = newSurface;
		_oldSurface = oldSurface;
	}
}
