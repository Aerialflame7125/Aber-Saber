using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels;

/// <summary>Stores channel data for the remoting channels.</summary>
[Serializable]
[ComVisible(true)]
public class ChannelDataStore : IChannelDataStore
{
	private string[] _channelURIs;

	private DictionaryEntry[] _extraData;

	/// <summary>Gets or sets an array of channel URIs that the current channel maps to.</summary>
	/// <returns>An array of channel URIs that the current channel maps to.</returns>
	public string[] ChannelUris
	{
		get
		{
			return _channelURIs;
		}
		set
		{
			_channelURIs = value;
		}
	}

	/// <summary>Gets or sets the data object that is associated with the specified key for the implementing channel.</summary>
	/// <param name="key">The key that the data object is associated with.</param>
	/// <returns>The specified data object for the implementing channel.</returns>
	public object this[object key]
	{
		get
		{
			if (_extraData == null)
			{
				return null;
			}
			DictionaryEntry[] extraData = _extraData;
			for (int i = 0; i < extraData.Length; i++)
			{
				DictionaryEntry dictionaryEntry = extraData[i];
				if (dictionaryEntry.Key.Equals(key))
				{
					return dictionaryEntry.Value;
				}
			}
			return null;
		}
		set
		{
			if (_extraData == null)
			{
				_extraData = new DictionaryEntry[1]
				{
					new DictionaryEntry(key, value)
				};
				return;
			}
			DictionaryEntry[] array = new DictionaryEntry[_extraData.Length + 1];
			_extraData.CopyTo(array, 0);
			array[_extraData.Length] = new DictionaryEntry(key, value);
			_extraData = array;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ChannelDataStore" /> class with the URIs that the current channel maps to.</summary>
	/// <param name="channelURIs">An array of channel URIs that the current channel maps to.</param>
	public ChannelDataStore(string[] channelURIs)
	{
		_channelURIs = channelURIs;
	}
}
