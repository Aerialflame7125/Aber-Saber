using System.IO;

namespace System.Web.Services.Protocols;

internal class SoapExtensionStream : Stream
{
	internal Stream innerStream;

	private bool hasWritten;

	private bool streamReady;

	public override bool CanRead
	{
		get
		{
			EnsureStreamReady();
			return innerStream.CanRead;
		}
	}

	public override bool CanSeek
	{
		get
		{
			EnsureStreamReady();
			return innerStream.CanSeek;
		}
	}

	public override bool CanWrite
	{
		get
		{
			EnsureStreamReady();
			return innerStream.CanWrite;
		}
	}

	internal bool HasWritten => hasWritten;

	public override long Length
	{
		get
		{
			EnsureStreamReady();
			return innerStream.Length;
		}
	}

	public override long Position
	{
		get
		{
			EnsureStreamReady();
			return innerStream.Position;
		}
		set
		{
			EnsureStreamReady();
			hasWritten = true;
			innerStream.Position = value;
		}
	}

	internal SoapExtensionStream()
	{
	}

	private bool EnsureStreamReady()
	{
		if (streamReady)
		{
			return true;
		}
		throw new InvalidOperationException(Res.GetString("WebBadStreamState"));
	}

	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing)
			{
				EnsureStreamReady();
				hasWritten = true;
				innerStream.Close();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	public override void Flush()
	{
		EnsureStreamReady();
		hasWritten = true;
		innerStream.Flush();
	}

	public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
	{
		EnsureStreamReady();
		return innerStream.BeginRead(buffer, offset, count, callback, state);
	}

	public override int EndRead(IAsyncResult asyncResult)
	{
		EnsureStreamReady();
		return innerStream.EndRead(asyncResult);
	}

	public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
	{
		EnsureStreamReady();
		hasWritten = true;
		return innerStream.BeginWrite(buffer, offset, count, callback, state);
	}

	public override void EndWrite(IAsyncResult asyncResult)
	{
		EnsureStreamReady();
		hasWritten = true;
		innerStream.EndWrite(asyncResult);
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		EnsureStreamReady();
		return innerStream.Seek(offset, origin);
	}

	public override void SetLength(long value)
	{
		EnsureStreamReady();
		innerStream.SetLength(value);
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		EnsureStreamReady();
		return innerStream.Read(buffer, offset, count);
	}

	public override int ReadByte()
	{
		EnsureStreamReady();
		return innerStream.ReadByte();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		EnsureStreamReady();
		hasWritten = true;
		innerStream.Write(buffer, offset, count);
	}

	public override void WriteByte(byte value)
	{
		EnsureStreamReady();
		hasWritten = true;
		innerStream.WriteByte(value);
	}

	internal void SetInnerStream(Stream stream)
	{
		innerStream = stream;
		hasWritten = false;
	}

	internal void SetStreamReady()
	{
		streamReady = true;
	}
}
