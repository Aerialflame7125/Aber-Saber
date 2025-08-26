using System.Collections;
using System.Collections.Generic;

namespace System.Web.Services.Description;

/// <summary>Enumerates the elements in a <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
public class BasicProfileViolationEnumerator : IEnumerator<BasicProfileViolation>, IDisposable, IEnumerator
{
	private BasicProfileViolationCollection list;

	private int idx;

	private int end;

	/// <summary>Gets the current <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
	/// <returns>A <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object representing the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</returns>
	public BasicProfileViolation Current => list[idx];

	/// <summary>Gets the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />. </summary>
	/// <returns>The current element in the collection.</returns>
	object IEnumerator.Current => list[idx];

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.</summary>
	/// <param name="list">The <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> to be enumerated using this class.</param>
	public BasicProfileViolationEnumerator(BasicProfileViolationCollection list)
	{
		this.list = list;
		idx = -1;
		end = list.Count - 1;
	}

	/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.Releases all resources used by the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" />. </summary>
	public void Dispose()
	{
	}

	/// <summary>Enumerates to the next element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
	/// <returns>
	///     <see langword="false" /> if the end of the collection is reached; otherwise <see langword="true" />.</returns>
	public bool MoveNext()
	{
		if (idx >= end)
		{
			return false;
		}
		idx++;
		return true;
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
	void IEnumerator.Reset()
	{
		idx = -1;
	}
}
