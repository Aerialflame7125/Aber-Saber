using System.Collections;

namespace System.Web.Profile;

/// <summary>A collection of <see cref="T:System.Web.Profile.ProfileInfo" /> objects.</summary>
[Serializable]
public sealed class ProfileInfoCollection : IEnumerable, ICollection
{
	private Hashtable _Hashtable;

	private ArrayList _ArrayList;

	private bool _ReadOnly;

	private int _CurPos;

	private int _NumBlanks;

	/// <summary>Gets the <see cref="T:System.Web.Profile.ProfileInfo" /> object in the collection, referenced by the specified <see cref="P:System.Web.Profile.ProfileInfo.UserName" />.</summary>
	/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to retrieve from the collection.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfo" /> object for the specified user name. If name is not found in the collection, <see langword="null" /> is returned.</returns>
	public ProfileInfo this[string name]
	{
		get
		{
			object obj = _Hashtable[name];
			if (obj == null)
			{
				return null;
			}
			return _ArrayList[(int)obj] as ProfileInfo;
		}
	}

	/// <summary>Gets the number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</summary>
	/// <returns>The number of <see cref="T:System.Web.Profile.ProfileInfo" /> objects in the collection.</returns>
	public int Count => _Hashtable.Count;

	/// <summary>Gets a value indicating whether the profile info collection is thread safe.</summary>
	/// <returns>Always <see langword="false" />, because thread-safe profile info collections are not supported.</returns>
	public bool IsSynchronized => false;

	/// <summary>Gets the synchronization root.</summary>
	/// <returns>Always <see langword="this" /> (<see langword="Me" /> in Visual Basic), because synchronization of <see cref="T:System.Web.Profile.ProfileInfoCollection" /> objects is not supported.</returns>
	public object SyncRoot => this;

	/// <summary>Creates a new, empty <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
	public ProfileInfoCollection()
	{
		_Hashtable = new Hashtable(10, StringComparer.CurrentCultureIgnoreCase);
		_ArrayList = new ArrayList();
	}

	/// <summary>Adds the specified <see cref="T:System.Web.Profile.ProfileInfo" /> object to the collection.</summary>
	/// <param name="profileInfo">A <see cref="T:System.Web.Profile.ProfileInfo" /> object to add to the collection.</param>
	/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
	/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Web.Profile.ProfileInfo" /> object with the same <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> value as <paramref name="profileInfo" /> already exists in the collection.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="profileInfo" /> is <see langword="null" />.-or-The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> property of <paramref name="profileInfo" /> is <see langword="null" />.</exception>
	public void Add(ProfileInfo profileInfo)
	{
		if (_ReadOnly)
		{
			throw new NotSupportedException();
		}
		if (profileInfo == null || profileInfo.UserName == null)
		{
			throw new ArgumentNullException("profileInfo");
		}
		_Hashtable.Add(profileInfo.UserName, _CurPos);
		_ArrayList.Add(profileInfo);
		_CurPos++;
	}

	/// <summary>Removes the <see cref="T:System.Web.Profile.ProfileInfo" /> object with the specified user name from the collection.</summary>
	/// <param name="name">The <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> of the <see cref="T:System.Web.Profile.ProfileInfo" /> object to remove from the collection.</param>
	/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
	public void Remove(string name)
	{
		if (_ReadOnly)
		{
			throw new NotSupportedException();
		}
		object obj = _Hashtable[name];
		if (obj != null)
		{
			_Hashtable.Remove(name);
			_ArrayList[(int)obj] = null;
			_NumBlanks++;
		}
	}

	/// <summary>Gets an enumerator that can iterate through the <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the entire <see cref="T:System.Web.Profile.ProfileInfoCollection" />.</returns>
	public IEnumerator GetEnumerator()
	{
		DoCompact();
		return _ArrayList.GetEnumerator();
	}

	/// <summary>Makes the contents of the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> read-only.</summary>
	public void SetReadOnly()
	{
		if (!_ReadOnly)
		{
			_ReadOnly = true;
		}
	}

	/// <summary>Removes all <see cref="T:System.Web.Profile.ProfileInfo" /> objects from the collection.</summary>
	/// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
	public void Clear()
	{
		if (_ReadOnly)
		{
			throw new NotSupportedException();
		}
		_Hashtable.Clear();
		_ArrayList.Clear();
		_CurPos = 0;
		_NumBlanks = 0;
	}

	/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array.</summary>
	/// <param name="array">A one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in array at which copying begins.</param>
	public void CopyTo(Array array, int index)
	{
		DoCompact();
		_ArrayList.CopyTo(array, index);
	}

	/// <summary>Copies the <see cref="T:System.Web.Profile.ProfileInfoCollection" /> to a one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" />.</summary>
	/// <param name="array">A one-dimensional array of type <see cref="T:System.Web.Profile.ProfileInfo" /> that is the destination of the elements copied from the <see cref="T:System.Web.Profile.ProfileInfoCollection" />. The array must have zero-based indexing.</param>
	/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
	public void CopyTo(ProfileInfo[] array, int index)
	{
		DoCompact();
		_ArrayList.CopyTo(array, index);
	}

	private void DoCompact()
	{
		if (_NumBlanks < 1)
		{
			return;
		}
		ArrayList arrayList = new ArrayList(_CurPos - _NumBlanks);
		int num = -1;
		for (int i = 0; i < _CurPos; i++)
		{
			if (_ArrayList[i] != null)
			{
				arrayList.Add(_ArrayList[i]);
			}
			else if (num == -1)
			{
				num = i;
			}
		}
		_NumBlanks = 0;
		_ArrayList = arrayList;
		_CurPos = _ArrayList.Count;
		for (int j = num; j < _CurPos; j++)
		{
			ProfileInfo profileInfo = _ArrayList[j] as ProfileInfo;
			_Hashtable[profileInfo.UserName] = j;
		}
	}
}
