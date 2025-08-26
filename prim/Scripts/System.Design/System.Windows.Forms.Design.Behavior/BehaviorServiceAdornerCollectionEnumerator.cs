using System.Collections;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Supports iteration over a <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
public class BehaviorServiceAdornerCollectionEnumerator : IEnumerator
{
	private BehaviorServiceAdornerCollection mappings;

	private int index;

	private int state;

	/// <summary>Gets the current element in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <returns>The current element in the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</returns>
	public Adorner Current
	{
		get
		{
			if (index >= 0)
			{
				return mappings[index];
			}
			return null;
		}
	}

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.IEnumerator.Current" /> property.</summary>
	/// <returns>The current <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> in the collection.</returns>
	object IEnumerator.Current => Current;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollectionEnumerator" /> class.</summary>
	/// <param name="mappings">The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" /> for which to create the enumerator.</param>
	public BehaviorServiceAdornerCollectionEnumerator(BehaviorServiceAdornerCollection mappings)
	{
		if (mappings == null)
		{
			throw new ArgumentNullException("mappings");
		}
		this.mappings = mappings;
		Reset();
	}

	private void CheckState()
	{
		if (mappings.State != state)
		{
			throw new InvalidOperationException("Collection has changed");
		}
	}

	/// <summary>Advances the enumerator to the next element of the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorServiceAdornerCollection" />.</summary>
	/// <returns>
	///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator was past the end of the collection.</returns>
	public bool MoveNext()
	{
		CheckState();
		if (index++ < mappings.Count)
		{
			return true;
		}
		index--;
		return false;
	}

	/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
	public void Reset()
	{
		index = -1;
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerator.MoveNext" /> method.</summary>
	/// <returns>
	///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator was past the end of the collection.</returns>
	bool IEnumerator.MoveNext()
	{
		return MoveNext();
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerator.Reset" /> method.</summary>
	void IEnumerator.Reset()
	{
		Reset();
	}
}
