namespace System.Drawing;

/// <summary>Provides access to the main buffered graphics context object for the application domain.</summary>
public sealed class BufferedGraphicsManager
{
	private static BufferedGraphicsContext graphics_context;

	/// <summary>Gets the <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</summary>
	/// <returns>The <see cref="T:System.Drawing.BufferedGraphicsContext" /> for the current application domain.</returns>
	public static BufferedGraphicsContext Current => graphics_context;

	static BufferedGraphicsManager()
	{
		graphics_context = new BufferedGraphicsContext();
	}

	private BufferedGraphicsManager()
	{
	}
}
