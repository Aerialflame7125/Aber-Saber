using System.Runtime.CompilerServices;

namespace System.Collections.Specialized;

/// <summary>Notifies listeners of dynamic changes, such as when an item is added and removed or the whole list is cleared.</summary>
[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public interface INotifyCollectionChanged
{
	/// <summary>Occurs when the collection changes.</summary>
	event NotifyCollectionChangedEventHandler CollectionChanged;
}
