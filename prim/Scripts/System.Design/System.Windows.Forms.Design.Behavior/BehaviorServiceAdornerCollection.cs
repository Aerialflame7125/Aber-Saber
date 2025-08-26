using System.Collections;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Stores <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> objects in a strongly typed collection.</summary>
public sealed class BehaviorServiceAdornerCollection : CollectionBase
{
	private int state;

	internal int State => state;

	/// <summary>Gets or sets the element at the specified index.</summary>
	/// <param name="index">The zero-based index of the element.</param>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> element specified by <paramref name="index" />.</returns>
	public Adorner this[int index]
	{
		get
		{
			return (Adorner)base.InnerList[index];
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

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class with the given <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> reference.</summary>
	/// <param name="behaviorService">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> reference.</param>
	public BehaviorServiceAdornerCollection(BehaviorService behaviorService)
		: this(behaviorService.Adorners)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class with the given array.</summary>
	/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to populate the collection.</param>
	public BehaviorServiceAdornerCollection(Adorner[] value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> class from an existing <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> from which to populate the collection.</param>
	public BehaviorServiceAdornerCollection(BehaviorServiceAdornerCollection value)
	{
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> with the specified value to the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <param name="value">An <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to add to the end of the collection.</param>
	/// <returns>The index at which the new element was inserted.</returns>
	public int Add(Adorner value)
	{
		state++;
		return base.InnerList.Add(value);
	}

	/// <summary>Copies the elements of an array to the end of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <param name="value">An array of type <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to copy to the end of the collection</param>
	public void AddRange(Adorner[] value)
	{
		state++;
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Adds the contents of another <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> to the end of the collection.</summary>
	/// <param name="value">A <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> to add to the end of the collection.</param>
	public void AddRange(BehaviorServiceAdornerCollection value)
	{
		state++;
		if (value == null)
		{
			throw new ArgumentNullException("value");
		}
		base.InnerList.AddRange(value);
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> contains the specified <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to locate.</param>
	/// <returns>
	///   <see langword="true" /> if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is contained in the collection; otherwise, <see langword="false" /></returns>
	public bool Contains(Adorner value)
	{
		return base.InnerList.Contains(value);
	}

	/// <summary>Copies the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> values to a one-dimensional <see cref="T:System.Array" /> at the specified index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</param>
	/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
	public void CopyTo(Adorner[] array, int index)
	{
		base.InnerList.CopyTo(array, index);
	}

	/// <summary>Returns the index of an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to locate.</param>
	/// <returns>The index of the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> of <paramref name="value" /> in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />, if found; otherwise, -1.</returns>
	public int IndexOf(Adorner value)
	{
		return base.InnerList.IndexOf(value);
	}

	/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> instance.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> instance.</returns>
	public new BehaviorServiceAdornerCollectionEnumerator GetEnumerator()
	{
		return new BehaviorServiceAdornerCollectionEnumerator(this);
	}

	/// <summary>Inserts an <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> into the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> at the specified index.</summary>
	/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted.</param>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to insert.</param>
	public void Insert(int index, Adorner value)
	{
		state++;
		base.InnerList.Insert(index, value);
	}

	/// <summary>Removes a specific <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> from the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <param name="value">The <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> to remove from the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</param>
	public void Remove(Adorner value)
	{
		state++;
		base.InnerList.Remove(value);
	}
}
