using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Implements the basic functionality that represents the appearance and behavior of a table layout.</summary>
[TypeConverter(typeof(TableLayoutSettings.StyleConverter))]
public abstract class TableLayoutStyle
{
	private SizeType size_type;

	private TableLayoutPanel owner;

	/// <summary>Gets or sets a flag indicating how a row or column should be sized relative to its containing table.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.SizeType" /> values that specifies how rows or columns of user interface (UI) elements should be sized relative to their container. The default is <see cref="F:System.Windows.Forms.SizeType.AutoSize" />.</returns>
	[DefaultValue(SizeType.AutoSize)]
	public SizeType SizeType
	{
		get
		{
			return size_type;
		}
		set
		{
			if (size_type != value)
			{
				size_type = value;
				if (owner != null)
				{
					owner.PerformLayout();
				}
			}
		}
	}

	internal TableLayoutPanel Owner
	{
		get
		{
			return owner;
		}
		set
		{
			owner = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TableLayoutStyle" /> class.</summary>
	protected TableLayoutStyle()
	{
		size_type = SizeType.AutoSize;
	}
}
