namespace System.Windows.Forms;

/// <summary>Represents the look and feel of a row in a table layout.</summary>
/// <filterpriority>2</filterpriority>
public class RowStyle : TableLayoutStyle
{
	private float height;

	/// <summary>Gets or sets the height of a row.</summary>
	/// <returns>The preferred height of a row in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on the <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> property.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0 when setting this property.</exception>
	/// <filterpriority>1</filterpriority>
	/// <PermissionSet>
	///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
	/// </PermissionSet>
	public float Height
	{
		get
		{
			return height;
		}
		set
		{
			if (value < 0f)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (height != value)
			{
				height = value;
				if (base.Owner != null)
				{
					base.Owner.PerformLayout();
				}
			}
		}
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class to its default state.</summary>
	public RowStyle()
	{
		height = 0f;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> value.</summary>
	/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
	public RowStyle(SizeType sizeType)
	{
		height = 0f;
		base.SizeType = sizeType;
	}

	/// <summary>Initializes an instance of the <see cref="T:System.Windows.Forms.RowStyle" /> class using the supplied <see cref="T:System.Windows.Forms.SizeType" /> and height values.</summary>
	/// <param name="sizeType">A <see cref="P:System.Windows.Forms.TableLayoutStyle.SizeType" /> indicating how the row should be should be sized relative to its containing table.</param>
	/// <param name="height">The preferred height in pixels or percentage of the <see cref="T:System.Windows.Forms.TableLayoutPanel" />, depending on <paramref name="sizeType" />.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="height" /> is less than 0.</exception>
	public RowStyle(SizeType sizeType, float height)
	{
		if (height < 0f)
		{
			throw new ArgumentOutOfRangeException("height");
		}
		base.SizeType = sizeType;
		this.height = height;
	}
}
