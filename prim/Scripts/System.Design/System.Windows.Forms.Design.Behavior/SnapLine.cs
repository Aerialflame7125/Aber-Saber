namespace System.Windows.Forms.Design.Behavior;

/// <summary>Represents the horizontal and vertical line segments that are dynamically created in the user interface (UI) to assist in the design-time layout of controls in a container. This class cannot be inherited.</summary>
public sealed class SnapLine
{
	private SnapLineType type;

	private int offset;

	private string filter;

	private SnapLinePriority priority;

	/// <summary>Gets the programmer-defined filter category associated with this snapline.</summary>
	/// <returns>A <see cref="T:System.String" /> that defines the filter category. The default is <see langword="null" />.</returns>
	public string Filter => filter;

	/// <summary>Gets a value indicating whether the snapline has a horizontal orientation.</summary>
	/// <returns>
	///   <see langword="true" /> if the snapline is horizontal; otherwise, <see langword="false" />.</returns>
	public bool IsHorizontal
	{
		get
		{
			switch (SnapLineType)
			{
			case SnapLineType.Top:
			case SnapLineType.Bottom:
			case SnapLineType.Horizontal:
			case SnapLineType.Baseline:
				return true;
			default:
				return false;
			}
		}
	}

	/// <summary>Gets a value indicating whether the snapline has a vertical orientation.</summary>
	/// <returns>
	///   <see langword="true" /> if the snapline is vertical; otherwise, <see langword="false" />.</returns>
	public bool IsVertical
	{
		get
		{
			SnapLineType snapLineType = SnapLineType;
			if ((uint)(snapLineType - 2) <= 1u || snapLineType == SnapLineType.Vertical)
			{
				return true;
			}
			return false;
		}
	}

	/// <summary>Gets the number of pixels that the snapline is offset from the origin of the associated control.</summary>
	/// <returns>The offset, in pixels, of the snapline.</returns>
	public int Offset => offset;

	/// <summary>Gets a value indicating the relative importance of the snapline.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> that represents the priority category of a snapline.</returns>
	public SnapLinePriority Priority => priority;

	/// <summary>Gets the type of a snapline, which indicates the general location and orientation.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> that represents the orientation and general location, relative to control edges, of a snapline.</returns>
	public SnapLineType SnapLineType => type;

	/// <summary>Returns a value indicating whether the specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> should snap to another <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</summary>
	/// <param name="line1">The specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</param>
	/// <param name="line2">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> to which the specified <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> is expected to snap.</param>
	/// <returns>
	///   <see langword="true" /> if <paramref name="line1" /> should snap to <paramref name="line2" />; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public static bool ShouldSnap(SnapLine line1, SnapLine line2)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type and offset.</summary>
	/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
	/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
	[System.MonoTODO]
	public SnapLine(SnapLineType type, int offset)
		: this(type, offset, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, and filter name.</summary>
	/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
	/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
	/// <param name="filter">A <see cref="T:System.String" /> used to specify a programmer-defined category of snaplines.</param>
	[System.MonoTODO]
	public SnapLine(SnapLineType type, int offset, string filter)
		: this(type, offset, filter, (SnapLinePriority)0)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, and priority.</summary>
	/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
	/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
	/// <param name="priority">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> of the snapline.</param>
	[System.MonoTODO]
	public SnapLine(SnapLineType type, int offset, SnapLinePriority priority)
		: this(type, offset, null, priority)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" /> class using the specified snapline type, offset, filter name, and priority.</summary>
	/// <param name="type">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLineType" /> to create. Describes the relative position and orientation of the snapline.</param>
	/// <param name="offset">The position of the snapline, in pixels, relative to the upper-left origin of the owning control.</param>
	/// <param name="filter">A <see cref="T:System.String" /> used to specify a programmer-defined category of snaplines.</param>
	/// <param name="priority">The <see cref="T:System.Windows.Forms.Design.Behavior.SnapLinePriority" /> of the snapline.</param>
	[System.MonoTODO]
	public SnapLine(SnapLineType type, int offset, string filter, SnapLinePriority priority)
	{
		this.type = type;
		this.offset = offset;
		this.filter = filter;
		this.priority = priority;
	}

	/// <summary>Adjusts the <see cref="P:System.Windows.Forms.Design.Behavior.SnapLine.Offset" /> property of the snapline.</summary>
	/// <param name="adjustment">The number of pixels to change the snapline offset by.</param>
	[System.MonoTODO]
	public void AdjustOffset(int adjustment)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string representation of the current snapline.</summary>
	/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Windows.Forms.Design.Behavior.SnapLine" />.</returns>
	[System.MonoTODO]
	public override string ToString()
	{
		return base.ToString();
	}
}
