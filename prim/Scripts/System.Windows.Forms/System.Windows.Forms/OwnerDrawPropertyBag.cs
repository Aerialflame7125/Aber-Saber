using System.Drawing;
using System.Runtime.Serialization;

namespace System.Windows.Forms;

/// <summary>Contains values of properties that a component might need only occasionally.</summary>
/// <filterpriority>2</filterpriority>
[Serializable]
public class OwnerDrawPropertyBag : MarshalByRefObject, ISerializable
{
	private Color fore_color;

	private Color back_color;

	private Font font;

	/// <summary>Gets or sets the foreground color of the component.</summary>
	/// <returns>The foreground color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Color ForeColor
	{
		get
		{
			return fore_color;
		}
		set
		{
			fore_color = value;
		}
	}

	/// <summary>Gets or sets the background color for the component.</summary>
	/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the component. The default is <see cref="F:System.Drawing.Color.Empty" />.</returns>
	/// <filterpriority>1</filterpriority>
	public Color BackColor
	{
		get
		{
			return back_color;
		}
		set
		{
			back_color = value;
		}
	}

	/// <summary>Gets or sets the font of the text displayed by the component.</summary>
	/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the component. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	public Font Font
	{
		get
		{
			return font;
		}
		set
		{
			font = value;
		}
	}

	internal OwnerDrawPropertyBag()
	{
		fore_color = (back_color = Color.Empty);
	}

	private OwnerDrawPropertyBag(Color fore_color, Color back_color, Font font)
	{
		this.fore_color = fore_color;
		this.back_color = back_color;
		this.font = font;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> class. </summary>
	/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> value.</param>
	/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> value.</param>
	protected OwnerDrawPropertyBag(SerializationInfo info, StreamingContext context)
	{
		SerializationInfoEnumerator enumerator = info.GetEnumerator();
		while (enumerator.MoveNext())
		{
			SerializationEntry current = enumerator.Current;
			switch (current.Name)
			{
			case "Font":
				font = (Font)current.Value;
				break;
			case "ForeColor":
				fore_color = (Color)current.Value;
				break;
			case "BackColor":
				back_color = (Color)current.Value;
				break;
			}
		}
	}

	/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
	/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
	/// <param name="context">The destination for this serialization.</param>
	void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
	{
		si.AddValue("BackColor", BackColor);
		si.AddValue("ForeColor", ForeColor);
		si.AddValue("Font", Font);
	}

	/// <summary>Returns whether the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> contains all default values; otherwise, false.</returns>
	/// <filterpriority>1</filterpriority>
	public virtual bool IsEmpty()
	{
		return font == null && fore_color.IsEmpty && back_color.IsEmpty;
	}

	/// <summary>Copies an <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</summary>
	/// <returns>A new <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" />.</returns>
	/// <param name="value">The <see cref="T:System.Windows.Forms.OwnerDrawPropertyBag" /> to be copied.</param>
	/// <filterpriority>1</filterpriority>
	public static OwnerDrawPropertyBag Copy(OwnerDrawPropertyBag value)
	{
		return new OwnerDrawPropertyBag(value.ForeColor, value.BackColor, value.Font);
	}
}
