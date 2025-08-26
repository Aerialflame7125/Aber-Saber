using System.Drawing;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Manages user interface in the designer. This class cannot be inherited.</summary>
public sealed class BehaviorService : IDisposable
{
	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <returns>A collection of adorner.</returns>
	[System.MonoTODO]
	public BehaviorServiceAdornerCollection Adorners
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> for the adorner window.</summary>
	/// <returns>The <see cref="T:System.Drawing.Graphics" /> for the adorner window.</returns>
	[System.MonoTODO]
	public Graphics AdornerWindowGraphics
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the behavior stack without removing it.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the behavior stack.</returns>
	[System.MonoTODO]
	public Behavior CurrentBehavior
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> starts a drag-and-drop operation.</summary>
	public event BehaviorDragDropEventHandler BeginDrag;

	/// <summary>Occurs when the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> completes a drag operation.</summary>
	public event BehaviorDragDropEventHandler EndDrag;

	/// <summary>Occurs when the current selection should be refreshed.</summary>
	public event EventHandler Synchronize;

	internal BehaviorService()
	{
	}

	/// <summary>Translates a <see cref="T:System.Drawing.Point" /> in the adorner window to screen coordinates.</summary>
	/// <param name="p">The <see cref="T:System.Drawing.Point" /> value to transform.</param>
	/// <returns>The transformed <see cref="T:System.Drawing.Point" /> value, in screen coordinates.</returns>
	[System.MonoTODO]
	public Point AdornerWindowPointToScreen(Point p)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the location of the adorner window in screen coordinates.</summary>
	/// <returns>The location, from the upper-left corner of the adorner window, in screen coordinates.</returns>
	[System.MonoTODO]
	public Point AdornerWindowToScreen()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the bounding <see cref="T:System.Drawing.Rectangle" /> of a <see cref="T:System.Windows.Forms.Control" />.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to translate.</param>
	/// <returns>The bounding <see cref="T:System.Drawing.Rectangle" /> of a <see cref="T:System.Windows.Forms.Control" /> translated to the adorner window coordinates.</returns>
	[System.MonoTODO]
	public Rectangle ControlRectInAdornerWindow(Control c)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the location of a <see cref="T:System.Windows.Forms.Control" /> translated to adorner window coordinates.</summary>
	/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> to translate.</param>
	/// <returns>A <see cref="T:System.Drawing.Point" /> value indicating the location of <paramref name="c" /> in adorner window coordinates.</returns>
	[System.MonoTODO]
	public Point ControlToAdornerWindow(Control c)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	[System.MonoTODO]
	public void Dispose()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> immediately after the given <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> in the behavior stack.</summary>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> preceding the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to be returned.</param>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> immediately after <paramref name="behavior" /> in the behavior stack, or <see langword="null" /> if there is no following behavior.</returns>
	[System.MonoTODO]
	public Behavior GetNextBehavior(Behavior behavior)
	{
		throw new NotImplementedException();
	}

	/// <summary>Invalidates the adorner window of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	[System.MonoTODO]
	public void Invalidate()
	{
		throw new NotImplementedException();
	}

	/// <summary>Invalidates, within the adorner window, the specified area of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	/// <param name="rect">The rectangular area to invalidate.</param>
	[System.MonoTODO]
	public void Invalidate(Rectangle rect)
	{
		throw new NotImplementedException();
	}

	/// <summary>Invalidates, within the adorner window, the specified area of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	/// <param name="r">The region to invalidate.</param>
	[System.MonoTODO]
	public void Invalidate(Region r)
	{
		throw new NotImplementedException();
	}

	/// <summary>Converts a point in a handle's coordinate system to the adorner window coordinates.</summary>
	/// <param name="handle">An adorner window's handle.</param>
	/// <param name="pt">A <see cref="T:System.Drawing.Point" /> in a handle's coordinate system.</param>
	/// <returns>A <see cref="T:System.Drawing.Point" /> in the adorner window coordinates.</returns>
	[System.MonoTODO]
	public Point MapAdornerWindowPoint(IntPtr handle, Point pt)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes and returns the <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> at the top of the stack.</summary>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to remove from the stack.</param>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> that was removed from the stack.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> stack is empty.</exception>
	[System.MonoTODO]
	public Behavior PopBehavior(Behavior behavior)
	{
		throw new NotImplementedException();
	}

	/// <summary>Pushes a <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> onto the behavior stack.</summary>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to push.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="behavior" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public void PushBehavior(Behavior behavior)
	{
		throw new NotImplementedException();
	}

	/// <summary>Pushes a <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> onto the behavior stack and assigns mouse capture to the behavior.</summary>
	/// <param name="behavior">The <see cref="T:System.Windows.Forms.Design.Behavior.Behavior" /> to push.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="behavior" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	public void PushCaptureBehavior(Behavior behavior)
	{
		throw new NotImplementedException();
	}

	/// <summary>Translates a point in screen coordinates into the adorner window coordinates of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" />.</summary>
	/// <param name="p">The <see cref="T:System.Drawing.Point" /> value to transform.</param>
	/// <returns>The transformed <see cref="T:System.Drawing.Point" /> value, in adorner window coordinates.</returns>
	[System.MonoTODO]
	public Point ScreenToAdornerWindow(Point p)
	{
		throw new NotImplementedException();
	}

	/// <summary>Synchronizes all selection glyphs.</summary>
	[System.MonoTODO]
	public void SyncSelection()
	{
		throw new NotImplementedException();
	}
}
