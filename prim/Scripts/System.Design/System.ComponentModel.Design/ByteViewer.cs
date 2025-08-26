using System.Windows.Forms;

namespace System.ComponentModel.Design;

/// <summary>Displays byte arrays in hexadecimal, ANSI, and Unicode formats.</summary>
[DesignTimeVisible(false)]
[ToolboxItem(false)]
public class ByteViewer : TableLayoutPanel
{
	/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.ByteViewer" /> class.</summary>
	[System.MonoTODO]
	public ByteViewer()
	{
	}

	/// <summary>Gets the display mode for the control.</summary>
	/// <returns>The display mode that this control uses. The returned value is defined in <see cref="T:System.ComponentModel.Design.DisplayMode" />.</returns>
	[System.MonoTODO]
	public virtual DisplayMode GetDisplayMode()
	{
		throw new NotImplementedException();
	}

	/// <summary>Writes the raw data from the data buffer to a file.</summary>
	/// <param name="path">The file path to save to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">The file write failed.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
	[System.MonoTODO]
	public virtual void SaveToFile(string path)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the bytes in the buffer.</summary>
	/// <returns>The unsigned byte array reference.</returns>
	[System.MonoTODO]
	public virtual byte[] GetBytes()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the byte array to display in the viewer.</summary>
	/// <param name="bytes">The byte array to display.</param>
	/// <exception cref="T:System.ArgumentNullException">The specified byte array is <see langword="null" />.</exception>
	[System.MonoTODO]
	public virtual void SetBytes(byte[] bytes)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the current display mode.</summary>
	/// <param name="mode">The display mode to set.</param>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The specified display mode is not from the <see cref="T:System.ComponentModel.Design.DisplayMode" /> enumeration.</exception>
	[System.MonoTODO]
	public virtual void SetDisplayMode(DisplayMode mode)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the file to display in the viewer.</summary>
	/// <param name="path">The file path to load from.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="path" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="path" /> is an empty string (""), contains only white space, or contains one or more invalid characters.</exception>
	/// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path is invalid, such as being on an unmapped drive.</exception>
	/// <exception cref="T:System.IO.IOException">The file load failed.</exception>
	/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length.</exception>
	/// <exception cref="T:System.UnauthorizedAccessException">The access requested is not permitted by the operating system for the specified <paramref name="path" />, such as when access is <see langword="Write" /> or <see langword="ReadWrite" /> and the file or directory is set for read-only access.</exception>
	[System.MonoTODO]
	public virtual void SetFile(string path)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the current line for the <see cref="F:System.ComponentModel.Design.DisplayMode.Hexdump" /> view.</summary>
	/// <param name="line">The current line to display from.</param>
	[System.MonoTODO]
	public virtual void SetStartLine(int line)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnKeyDown(KeyEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnPaint(PaintEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected override void OnLayout(LayoutEventArgs e)
	{
		throw new NotImplementedException();
	}

	/// <summary>Handles the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event on the <see cref="T:System.ComponentModel.Design.ByteViewer" /> control's <see cref="T:System.Windows.Forms.ScrollBar" />.</summary>
	/// <param name="source">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
	[System.MonoTODO]
	protected virtual void ScrollChanged(object source, EventArgs e)
	{
		throw new NotImplementedException();
	}
}
