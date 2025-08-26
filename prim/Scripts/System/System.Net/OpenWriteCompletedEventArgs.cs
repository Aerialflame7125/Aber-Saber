using System.ComponentModel;
using System.IO;

namespace System.Net;

/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
{
	private Stream m_Result;

	/// <summary>Gets a writable stream that is used to send data to a server.</summary>
	/// <returns>A <see cref="T:System.IO.Stream" /> where you can write data to be uploaded.</returns>
	public Stream Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return m_Result;
		}
	}

	internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
		: base(exception, cancelled, userToken)
	{
		m_Result = result;
	}
}
