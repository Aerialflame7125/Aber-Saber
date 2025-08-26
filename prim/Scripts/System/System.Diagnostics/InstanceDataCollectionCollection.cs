using System.Collections;

namespace System.Diagnostics;

/// <summary>Provides a strongly typed collection of <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects.</summary>
public class InstanceDataCollectionCollection : DictionaryBase
{
	/// <summary>Gets the instance data for the specified counter.</summary>
	/// <param name="counterName">The name of the performance counter.</param>
	/// <returns>An <see cref="T:System.Diagnostics.InstanceDataCollection" /> item, by which the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> object is indexed.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
	public InstanceDataCollection this[string counterName]
	{
		get
		{
			CheckNull(counterName, "counterName");
			return (InstanceDataCollection)base.Dictionary[counterName];
		}
	}

	/// <summary>Gets the object and counter registry keys for the objects associated with this instance data collection.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents a set of object-specific registry keys.</returns>
	public ICollection Keys => base.Dictionary.Keys;

	/// <summary>Gets the instance data values that comprise the collection of instances for the counter.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> that represents the counter's instances and their associated data values.</returns>
	public ICollection Values => base.Dictionary.Values;

	private static void CheckNull(object value, string name)
	{
		if (value == null)
		{
			throw new ArgumentNullException(name);
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.InstanceDataCollectionCollection" /> class.</summary>
	[Obsolete("Use PerformanceCounterCategory.ReadCategory()")]
	public InstanceDataCollectionCollection()
	{
	}

	/// <summary>Determines whether an instance data collection for the specified counter (identified by one of the indexed <see cref="T:System.Diagnostics.InstanceDataCollection" /> objects) exists in the collection.</summary>
	/// <param name="counterName">The name of the performance counter.</param>
	/// <returns>
	///   <see langword="true" /> if an instance data collection containing the specified counter exists in the collection; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="counterName" /> parameter is <see langword="null" />.</exception>
	public bool Contains(string counterName)
	{
		CheckNull(counterName, "counterName");
		return base.Dictionary.Contains(counterName);
	}

	/// <summary>Copies an array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances to the collection, at the specified index.</summary>
	/// <param name="counters">An array of <see cref="T:System.Diagnostics.InstanceDataCollection" /> instances (identified by the counters they contain) to add to the collection.</param>
	/// <param name="index">The location at which to add the new instances.</param>
	public void CopyTo(InstanceDataCollection[] counters, int index)
	{
		base.Dictionary.CopyTo(counters, index);
	}
}
