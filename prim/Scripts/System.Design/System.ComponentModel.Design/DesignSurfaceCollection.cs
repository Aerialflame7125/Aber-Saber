using System.Collections;

namespace System.ComponentModel.Design;

/// <summary>Contains a collection of design surfaces. This class cannot be inherited.</summary>
public sealed class DesignSurfaceCollection : ICollection, IEnumerable
{
	private class DesignSurfaceEnumerator : IEnumerator
	{
		private IEnumerator _designerCollectionEnumerator;

		public object Current => (((IDesignerHost)_designerCollectionEnumerator.Current).GetService(typeof(DesignSurface)) as DesignSurface) ?? throw new NotSupportedException();

		public DesignSurfaceEnumerator(IEnumerator designerCollectionEnumerator)
		{
			_designerCollectionEnumerator = designerCollectionEnumerator;
		}

		public bool MoveNext()
		{
			return _designerCollectionEnumerator.MoveNext();
		}

		public void Reset()
		{
			_designerCollectionEnumerator.Reset();
		}
	}

	private DesignerCollection _designers;

	/// <summary>Gets the total number of design surfaces in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</summary>
	/// <returns>The total number of elements in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
	public int Count => _designers.Count;

	/// <summary>Gets the design surface at the specified index.</summary>
	/// <param name="index">The index of the design surface to return.</param>
	/// <returns>The design surface at the specified index.</returns>
	/// <exception cref="T:System.NotSupportedException">The design surface specified by <paramref name="index" /> is not supported.</exception>
	public DesignSurface this[int index] => (_designers[index].GetService(typeof(DesignSurface)) as DesignSurface) ?? throw new NotSupportedException();

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.Count" /> property.</summary>
	/// <returns>The number of elements contained in the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
	int ICollection.Count => Count;

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.IsSynchronized" /> property.</summary>
	/// <returns>
	///   <see langword="true" /> if access to the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool ICollection.IsSynchronized => false;

	/// <summary>For a description of this member, see the <see cref="P:System.Collections.ICollection.SyncRoot" /> property.</summary>
	/// <returns>An object that can be used to synchronize access to the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</returns>
	object ICollection.SyncRoot => null;

	internal DesignSurfaceCollection(DesignerCollection designers)
	{
		if (designers == null)
		{
			designers = new DesignerCollection(null);
		}
		_designers = designers;
	}

	/// <summary>Copies the collection members to the specified <see cref="T:System.ComponentModel.Design.DesignSurface" /> array beginning at the specified destination index.</summary>
	/// <param name="array">The array to copy collection members to.</param>
	/// <param name="index">The destination index to begin copying to.</param>
	public void CopyTo(DesignSurface[] array, int index)
	{
		((ICollection)this).CopyTo((Array)array, index);
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method.</summary>
	/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" />.</param>
	/// <param name="index">The index in <paramref name="array" /> where copying begins.</param>
	void ICollection.CopyTo(Array array, int index)
	{
		IEnumerator enumerator = GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				DesignSurface value = (DesignSurface)enumerator.Current;
				array.SetValue(value, index);
				index++;
			}
		}
		finally
		{
			IDisposable disposable = enumerator as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}

	/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</returns>
	public IEnumerator GetEnumerator()
	{
		return new DesignSurfaceEnumerator(_designers.GetEnumerator());
	}

	/// <summary>For a description of this member, see the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.ComponentModel.Design.DesignSurfaceCollection" /> instance.</returns>
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
