using System.Collections;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Stores <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects in a strongly typed collection.</summary>
public class GlyphCollection : CollectionBase
{
	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element.</param>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> element at the specified index.</returns>
	public Glyph this[int index]
	{
		get
		{
			return (Glyph)base.InnerList[index];
		}
		set
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			base.InnerList[index] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class.</summary>
	public GlyphCollection()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class with the given <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> array.</summary>
	/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to populate the collection.</param>
	public GlyphCollection(Glyph[] value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> class based on another <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to populate the collection.</param>
	public GlyphCollection(GlyphCollection value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> with the specified value to the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to add to the end of the collection.</param>
	/// <returns>The index at which the new element was inserted.</returns>
	public int Add(Glyph value)
	{
		return base.InnerList.Add(value);
	}

	/// <summary>Copies the elements of an array to the end of the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
	/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to copy to the end of the collection.</param>
	public void AddRange(Glyph[] value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds the contents of another <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to the end of the collection.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> to add to the end of the collection.</param>
	public void AddRange(GlyphCollection value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> contains the specified <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to locate.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> is contained in the collection; otherwise, <see langword="false" />.</returns>
	public bool Contains(Glyph value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>Copies the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</param>
	/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
	public void CopyTo(Glyph[] array, int index)
	{
		base.InnerList.CopyTo(array, index);
	}

	/// <summary>Returns the index of a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to locate.</param>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> of <paramref name="value" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(Glyph value)
	{
		return base.InnerList.IndexOf(value);
	}

	/// <summary>Inserts a <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> into the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to insert.</param>
	public void Insert(int index, Glyph value)
	{
		base.InnerList.Insert(index, value);
	}

	/// <summary>Removes a specific <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> from the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> to remove from the <see cref="T:System.Windows.Forms.Design.Behavior.GlyphCollection" />.</param>
	public void Remove(Glyph value)
	{
		base.InnerList.Remove(value);
	}
}
