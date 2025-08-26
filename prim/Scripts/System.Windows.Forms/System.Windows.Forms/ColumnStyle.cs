namespace System.Windows.Forms;

/// <summary>Represents the look and feel of a column in a table layout.</summary>
/// <filterpriority>2</filterpriority>
public class ColumnStyle : TableLayoutStyle
{
	private float width;

	/// <summary>Gets or sets the width value for a column.</summary>
	/// <returns>The preferred width, in pixels or percentage, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public float Width
	{
		get
		{
			return width;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (width != value)
			{
				width = value;
				if (base.Owner != null)
				{
					base.Owner.PerformLayout();
				}
			}
		}
	}

	/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class to its default state.</summary>
	public ColumnStyle()
	{
		width = 0f;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
	/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
	public ColumnStyle(SizeType sizeType)
	{
		width = 0f;
		base.SizeType = sizeType;
	}

	/// <summary>Initializes and instance of the <see cref="T:System.Windows.Forms.ColumnStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and width values.</summary>
	/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the column should be should be sized relative to its containing table.</param>
	/// <param name="width">The preferred width, in pixels or percentage, depending on the <paramref name="sizeType" /> parameter.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="width" /> is less than 0.</exception>
	public ColumnStyle(SizeType sizeType, float width)
	{
		if (width < 0f)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		base.SizeType = sizeType;
		this.width = width;
	}
}
