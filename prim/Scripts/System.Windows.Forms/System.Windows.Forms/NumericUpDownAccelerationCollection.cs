using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Represents a sorted collection of <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> objects in the <see cref="T:System.Windows.Forms.NumericUpDown" /> control.</summary>
[ListBindable(false)]
public class NumericUpDownAccelerationCollection : MarshalByRefObject, ICollection<NumericUpDownAcceleration>, IEnumerable<NumericUpDownAcceleration>, IEnumerable
{
	private List<NumericUpDownAcceleration> items;

	/// <summary>Gets the number of objects in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
	/// <returns>The number of objects in the collection.</returns>
	public int Count => items.Count;

	/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> is read-only.</summary>
	/// <returns>true if the collection is read-only; otherwise, false.</returns>
	public bool IsReadOnly => false;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> at the specified index number.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> with the specified index.</returns>
	/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to get from the collection.</param>
	public NumericUpDownAcceleration this[int index] => items[index];

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> class.</summary>
	public NumericUpDownAccelerationCollection()
	{
		items = new List<NumericUpDownAcceleration>();
	}

	IEnumerator<NumericUpDownAcceleration> IEnumerable<NumericUpDownAcceleration>.GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Gets the enumerator for the collection.</summary>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return items.GetEnumerator();
	}

	/// <summary>Adds a new <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
	/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to add to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="acceleration" /> is null.</exception>
	public void Add(NumericUpDownAcceleration acceleration)
	{
		if (acceleration == null)
		{
			throw new ArgumentNullException("Acceleration cannot be null");
		}
		int i;
		for (i = 0; i < items.Count && acceleration.Seconds >= items[i].Seconds; i++)
		{
		}
		items.Insert(i, acceleration);
	}

	/// <summary>Adds the elements of the specified array to the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />, keeping the collection sorted.</summary>
	/// <param name="accelerations">An array of type <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" />  containing the objects to add to the collection.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="accelerations" /> is null, or one of the entries in the <paramref name="accelerations" /> array is null.</exception>
	public void AddRange(params NumericUpDownAcceleration[] accelerations)
	{
		for (int i = 0; i < accelerations.Length; i++)
		{
			Add(accelerations[i]);
		}
	}

	/// <summary>Removes all elements from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
	public void Clear()
	{
		items.Clear();
	}

	/// <summary>Determines whether the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> contains a specific <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is found in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, false.</returns>
	/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to locate in the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</param>
	public bool Contains(NumericUpDownAcceleration acceleration)
	{
		return items.Contains(acceleration);
	}

	/// <summary>Copies the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" /> values to a one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> instance at the specified index.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> that is the destination of the values copied from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />. </param>
	/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
	public void CopyTo(NumericUpDownAcceleration[] array, int index)
	{
		items.CopyTo(array, index);
	}

	/// <summary>Removes the first occurrence of the specified <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> from the <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />.</summary>
	/// <returns>true if the <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> is removed from <see cref="T:System.Windows.Forms.NumericUpDownAccelerationCollection" />; otherwise, false.</returns>
	/// <param name="acceleration">The <see cref="T:System.Windows.Forms.NumericUpDownAcceleration" /> to remove from the collection.</param>
	public bool Remove(NumericUpDownAcceleration acceleration)
	{
		return items.Remove(acceleration);
	}
}
