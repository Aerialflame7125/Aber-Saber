using System.Collections;

namespace System.Diagnostics;

/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceData" /> objects.</summary>
public class InstanceDataCollection : DictionaryBase
{
	private string counterName;

	/// <summary>Gets the name of the performance counter whose instance data you want to get.</summary>
	/// <returns>The performance counter name.</returns>
	public string CounterName => counterName;

	/// <summary>Gets the instance data associated with this counter. This is typically a set of raw counter values.</summary>
	/// <param name="instanceName">The name of the performance counter category instance, or an empty string ("") if the category contains a single instance.</param>
	/// <returns>An <see cref="T:System.Diagnostics.InstanceData" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollection" /> object is indexed.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
	public InstanceData this[string instanceName]
	{
		get
		{
			CheckNull(instanceName, "instanceName");
			return (InstanceData)base.Dictionary[instanceName];
		}
	}

	/// <summary>Gets the object and counter registry keys for the objects associated with this instance data.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
	public ICollection Keys => base.Dictionary.Keys;

	/// <summary>Gets the raw counter values that comprise the instance data for the counter.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's raw data values.</returns>
	public ICollection Values => base.Dictionary.Values;

	private static void CheckNull(object value, string name)
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollection" /> class, using the specified performance counter (which defines a performance instance).</summary>
	/// <param name="counterName">The name of the counter, which often describes the quantity that is being counted.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
	[Obsolete("Use InstanceDataCollectionCollection indexer instead.")]
	public InstanceDataCollection(string counterName)
	{
		CheckNull(counterName, "counterName");
		this.counterName = counterName;
	}

	/// <summary>Determines whether a performance instance with a specified name (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceData" /> objects) exists in the collection.</summary>
	/// <param name="instanceName">The name of the instance to find in this collection.</param>
	/// <returns>
	///   <see langword="true" /> if the instance exists in the collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="instanceName" /> parameter is <see langword="null" />.</exception>
	public bool Contains(string instanceName)
	{
		CheckNull(instanceName, "instanceName");
		return base.Dictionary.Contains(instanceName);
	}

	/// <summary>Copies the items in the collection to the specified one-dimensional array at the specified index.</summary>
	/// <param name="instances">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
	/// <param name="index">The zero-based index value at which to add the new instances.</param>
	public void CopyTo(InstanceData[] instances, int index)
	{
		base.Dictionary.CopyTo(instances, index);
	}
}
