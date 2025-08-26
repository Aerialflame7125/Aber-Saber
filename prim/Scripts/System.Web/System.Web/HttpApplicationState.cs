using System.Collections.Specialized;
using System.Security.Permissions;
using System.Threading;

namespace System.Web;

/// <summary>Enables sharing of global information across multiple sessions and requests within an ASP.NET application.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpApplicationState : NameObjectCollectionBase
{
	private HttpStaticObjectsCollection _AppObjects;

	private HttpStaticObjectsCollection _SessionObjects;

	private ReaderWriterLockSlim _Lock;

	private bool IsLockHeld
	{
		get
		{
			if (!_Lock.IsReadLockHeld)
			{
				return _Lock.IsWriteLockHeld;
			}
			return true;
		}
	}

	/// <summary>Gets the access keys in the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	/// <returns>A string array of <see cref="T:System.Web.HttpApplicationState" /> object names.</returns>
	public string[] AllKeys
	{
		get
		{
			bool flag = false;
			try
			{
				if (!IsLockHeld)
				{
					_Lock.EnterReadLock();
					flag = true;
				}
				return BaseGetAllKeys();
			}
			finally
			{
				if (flag && IsLockHeld)
				{
					_Lock.ExitReadLock();
				}
			}
		}
	}

	/// <summary>Gets a reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</summary>
	/// <returns>A reference to the <see cref="T:System.Web.HttpApplicationState" /> object.</returns>
	public HttpApplicationState Contents => this;

	/// <summary>Gets the number of objects in the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	/// <returns>The number of item objects in the collection. The default is 0.</returns>
	public override int Count
	{
		get
		{
			bool flag = false;
			try
			{
				if (!IsLockHeld)
				{
					_Lock.EnterReadLock();
					flag = true;
				}
				return base.Count;
			}
			finally
			{
				if (flag && IsLockHeld)
				{
					_Lock.ExitReadLock();
				}
			}
		}
	}

	/// <summary>Gets the value of a single <see cref="T:System.Web.HttpApplicationState" /> object by name.</summary>
	/// <param name="name">The name of the object in the collection. </param>
	/// <returns>The object referenced by <paramref name="name" />.</returns>
	public object this[string name]
	{
		get
		{
			return Get(name);
		}
		set
		{
			Set(name, value);
		}
	}

	/// <summary>Gets a single <see cref="T:System.Web.HttpApplicationState" /> object by index.</summary>
	/// <param name="index">The numerical index of the object in the collection. </param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	public object this[int index] => Get(index);

	internal HttpStaticObjectsCollection SessionObjects
	{
		get
		{
			if (_SessionObjects == null)
			{
				_SessionObjects = new HttpStaticObjectsCollection();
			}
			return _SessionObjects;
		}
	}

	/// <summary>Gets all objects declared by an <see langword="&lt;object&gt;" /> tag where the scope is set to "Application" within the ASP.NET application.</summary>
	/// <returns>A collection of objects on the page.</returns>
	public HttpStaticObjectsCollection StaticObjects
	{
		get
		{
			if (_AppObjects == null)
			{
				_AppObjects = new HttpStaticObjectsCollection();
			}
			return _AppObjects;
		}
	}

	internal HttpApplicationState()
	{
		_Lock = new ReaderWriterLockSlim();
	}

	internal HttpApplicationState(HttpStaticObjectsCollection AppObj, HttpStaticObjectsCollection SessionObj)
	{
		_AppObjects = AppObj;
		_SessionObjects = SessionObj;
		_Lock = new ReaderWriterLockSlim();
	}

	/// <summary>Adds a new object to the <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	/// <param name="name">The name of the object to be added to the collection. </param>
	/// <param name="value">The value of the object. </param>
	public void Add(string name, object value)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterWriteLock();
				flag = true;
			}
			BaseAdd(name, value);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitWriteLock();
			}
		}
	}

	/// <summary>Removes all objects from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	public void Clear()
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterWriteLock();
				flag = true;
			}
			BaseClear();
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitWriteLock();
			}
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object by name.</summary>
	/// <param name="name">The name of the object. </param>
	/// <returns>The object referenced by <paramref name="name" />.</returns>
	public object Get(string name)
	{
		object obj = null;
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterReadLock();
				flag = true;
			}
			return BaseGet(name);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitReadLock();
			}
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object by numerical index.</summary>
	/// <param name="index">The index of the application state object. </param>
	/// <returns>The object referenced by <paramref name="index" />.</returns>
	public object Get(int index)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterReadLock();
				flag = true;
			}
			return BaseGet(index);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitReadLock();
			}
		}
	}

	/// <summary>Gets an <see cref="T:System.Web.HttpApplicationState" /> object name by index.</summary>
	/// <param name="index">The index of the application state object. </param>
	/// <returns>The name under which the application state object was saved.</returns>
	public string GetKey(int index)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterReadLock();
				flag = true;
			}
			return BaseGetKey(index);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitReadLock();
			}
		}
	}

	/// <summary>Locks access to an <see cref="T:System.Web.HttpApplicationState" /> variable to facilitate access synchronization.</summary>
	public void Lock()
	{
		if (!_Lock.IsWriteLockHeld)
		{
			_Lock.EnterWriteLock();
		}
	}

	/// <summary>Removes the named object from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	/// <param name="name">The name of the object to be removed from the collection. </param>
	public void Remove(string name)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterWriteLock();
				flag = true;
			}
			BaseRemove(name);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitWriteLock();
			}
		}
	}

	/// <summary>Removes all objects from an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	public void RemoveAll()
	{
		Clear();
	}

	/// <summary>Removes an <see cref="T:System.Web.HttpApplicationState" /> object from a collection by index.</summary>
	/// <param name="index">The position in the collection of the item to remove. </param>
	public void RemoveAt(int index)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterWriteLock();
				flag = true;
			}
			BaseRemoveAt(index);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitWriteLock();
			}
		}
	}

	/// <summary>Updates the value of an object in an <see cref="T:System.Web.HttpApplicationState" /> collection.</summary>
	/// <param name="name">The name of the object to be updated. </param>
	/// <param name="value">The updated value of the object. </param>
	public void Set(string name, object value)
	{
		bool flag = false;
		try
		{
			if (!IsLockHeld)
			{
				_Lock.EnterWriteLock();
				flag = true;
			}
			BaseSet(name, value);
		}
		finally
		{
			if (flag && IsLockHeld)
			{
				_Lock.ExitWriteLock();
			}
		}
	}

	/// <summary>Unlocks access to an <see cref="T:System.Web.HttpApplicationState" /> variable to facilitate access synchronization.</summary>
	public void UnLock()
	{
		if (_Lock.IsWriteLockHeld)
		{
			_Lock.ExitWriteLock();
		}
	}
}
