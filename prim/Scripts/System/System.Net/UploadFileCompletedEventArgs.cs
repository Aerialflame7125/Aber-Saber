using System.ComponentModel;

namespace System.Net;

/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
{
	private byte[] m_Result;

	/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
	/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
	public byte[] Result
	{
		get
		{
			RaiseExceptionIfNecessary();
			return m_Result;
		}
	}

	internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
		: base(exception, cancelled, userToken)
	{
		m_Result = result;
	}
}
