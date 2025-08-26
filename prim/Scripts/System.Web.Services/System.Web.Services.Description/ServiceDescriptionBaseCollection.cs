using System.Collections;
using System.Threading;

namespace System.Web.Services.Description;

/// <summary>Forms the basis for the strongly typed collections that are members of the <see cref="N:System.Web.Services.Description" /> namespace.</summary>
public abstract class ServiceDescriptionBaseCollection : CollectionBase
{
	private Hashtable table;

	private object parent;

	/// <summary>Gets an interface that implements the association of the keys and values in the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
	/// <returns>An interface that implements the association of the keys and values in the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</returns>
	protected virtual IDictionary Table
	{
		get
		{
			if (table == null)
			{
				table = new Hashtable();
			}
			return table;
		}
	}

	internal ServiceDescriptionBaseCollection(object parent)
	{
		this.parent = parent;
	}

	/// <summary>Returns the name of the key associated with the value passed by reference.</summary>
	/// <param name="value">An object for which to return the name of the key. </param>
	/// <returns>A null reference.</returns>
	protected virtual string GetKey(object value)
	{
		return null;
	}

	/// <summary>Sets the parent object of the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" /> instance.</summary>
	/// <param name="value">The object for which to set the parent object. </param>
	/// <param name="parent">The object to set as the parent. </param>
	protected virtual void SetParent(object value, object parent)
	{
	}

	/// <summary>Performs additional custom processes after inserting a new element into the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
	/// <param name="index">The zero-based index at which to insert the <paramref name="value" /> parameter. </param>
	/// <param name="value">The element to insert into the collection. </param>
	protected override void OnInsertComplete(int index, object value)
	{
		AddValue(value);
	}

	/// <summary>Removes an element from the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
	/// <param name="index">The zero-based index of the <paramref name="value" /> parameter to be removed. </param>
	/// <param name="value">The element to remove from the collection. </param>
	protected override void OnRemove(int index, object value)
	{
		RemoveValue(value);
	}

	/// <summary>Clears the contents of the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" /> instance.</summary>
	protected override void OnClear()
	{
		for (int i = 0; i < base.List.Count; i++)
		{
			RemoveValue(base.List[i]);
		}
	}

	/// <summary>Replaces one value with another within the <see cref="T:System.Web.Services.Description.ServiceDescriptionBaseCollection" />.</summary>
	/// <param name="index">The zero-based index where the <paramref name="oldValue" /> parameter can be found. </param>
	/// <param name="oldValue">The object to replace with the <paramref name="newValue" /> parameter. </param>
	/// <param name="newValue">The object that replaces the <paramref name="oldValue" /> parameter. </param>
	protected override void OnSet(int index, object oldValue, object newValue)
	{
		RemoveValue(oldValue);
		AddValue(newValue);
	}

	private void AddValue(object value)
	{
		string key = GetKey(value);
		if (key != null)
		{
			try
			{
				Table.Add(key, value);
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException || ex is StackOverflowException || ex is OutOfMemoryException)
				{
					throw;
				}
				if (Table[key] != null)
				{
					throw new ArgumentException(GetDuplicateMessage(value.GetType(), key), ex.InnerException);
				}
				throw ex;
			}
		}
		SetParent(value, parent);
	}

	private void RemoveValue(object value)
	{
		string key = GetKey(value);
		if (key != null)
		{
			Table.Remove(key);
		}
		SetParent(value, null);
	}

	private static string GetDuplicateMessage(Type type, string elemName)
	{
		string text = null;
		if (type == typeof(ServiceDescriptionFormatExtension))
		{
			return Res.GetString("WebDuplicateFormatExtension", elemName);
		}
		if (type == typeof(OperationMessage))
		{
			return Res.GetString("WebDuplicateOperationMessage", elemName);
		}
		if (type == typeof(Import))
		{
			return Res.GetString("WebDuplicateImport", elemName);
		}
		if (type == typeof(Message))
		{
			return Res.GetString("WebDuplicateMessage", elemName);
		}
		if (type == typeof(Port))
		{
			return Res.GetString("WebDuplicatePort", elemName);
		}
		if (type == typeof(PortType))
		{
			return Res.GetString("WebDuplicatePortType", elemName);
		}
		if (type == typeof(Binding))
		{
			return Res.GetString("WebDuplicateBinding", elemName);
		}
		if (type == typeof(Service))
		{
			return Res.GetString("WebDuplicateService", elemName);
		}
		if (type == typeof(MessagePart))
		{
			return Res.GetString("WebDuplicateMessagePart", elemName);
		}
		if (type == typeof(OperationBinding))
		{
			return Res.GetString("WebDuplicateOperationBinding", elemName);
		}
		if (type == typeof(FaultBinding))
		{
			return Res.GetString("WebDuplicateFaultBinding", elemName);
		}
		if (type == typeof(Operation))
		{
			return Res.GetString("WebDuplicateOperation", elemName);
		}
		if (type == typeof(OperationFault))
		{
			return Res.GetString("WebDuplicateOperationFault", elemName);
		}
		return Res.GetString("WebDuplicateUnknownElement", type, elemName);
	}
}
