using System.IO;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Web;

/// <summary>Provides a <see cref="T:System.IO.TextWriter" /> object that is accessed through the intrinsic <see cref="T:System.Web.HttpResponse" /> object.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpWriter : TextWriter
{
	private const long MAX_TOTAL_BUFFERS_SIZE = 4194304L;

	private const uint SINGLE_BUFFER_SIZE = 131072u;

	private const uint MIN_SINGLE_BUFFER_SIZE = 32768u;

	private HttpResponseStream output_stream;

	private HttpResponse response;

	private Encoding encoding;

	[ThreadStatic]
	private static byte[] _bytebuffer;

	private static readonly uint byteBufferSize;

	private char[] chars = new char[1];

	private static char[] newline;

	/// <summary>Gets an <see cref="T:System.Text.Encoding" /> object for the <see cref="T:System.IO.TextWriter" />.</summary>
	/// <returns>An instance of the <see cref="T:System.Text.Encoding" /> class indicating the character set of the current response.</returns>
	public override Encoding Encoding => encoding;

	/// <summary>Gets a <see cref="T:System.IO.Stream" /> object to enable HTTP output directly from the <see cref="T:System.IO.Stream" />.</summary>
	/// <returns>An instance of the <see cref="T:System.IO.Stream" /> class containing the data to send to the client </returns>
	public Stream OutputStream => output_stream;

	internal HttpResponse Response => response;

	static HttpWriter()
	{
		newline = new char[2] { '\r', '\n' };
		ThreadPool.GetMinThreads(out var workerThreads, out var _);
		workerThreads *= 3;
		uint val = (uint)(4194304L / (long)workerThreads);
		byteBufferSize = Math.Min(131072u, val);
		if (byteBufferSize < 32768)
		{
			byteBufferSize = 32768u;
		}
	}

	internal HttpWriter(HttpResponse response)
	{
		this.response = response;
		encoding = response.ContentEncoding;
		output_stream = response.output_stream;
	}

	private byte[] GetByteBuffer(int length)
	{
		if (_bytebuffer == null)
		{
			_bytebuffer = new byte[byteBufferSize];
		}
		if (byteBufferSize >= length)
		{
			return _bytebuffer;
		}
		return new byte[length];
	}

	internal void SetEncoding(Encoding new_encoding)
	{
		encoding = new_encoding;
	}

	/// <summary>Sends all buffered output to the HTTP output stream and closes the socket connection.</summary>
	public override void Close()
	{
		output_stream.Close();
	}

	/// <summary>Sends all buffered output to the HTTP output stream.</summary>
	public override void Flush()
	{
		output_stream.Flush();
	}

	/// <summary>Sends a single character to the HTTP output stream.</summary>
	/// <param name="ch">The character to send to the HTTP output stream. </param>
	public override void Write(char ch)
	{
		chars[0] = ch;
		Write(chars, 0, 1);
	}

	/// <summary>Sends an <see cref="T:System.Object" /> to the HTTP output stream.</summary>
	/// <param name="obj">The <see cref="T:System.Object" /> to send to the HTTP output stream. </param>
	public override void Write(object obj)
	{
		if (obj != null)
		{
			Write(obj.ToString());
		}
	}

	/// <summary>Sends a string to the HTTP output stream.</summary>
	/// <param name="s">The string to send to the HTTP output stream. </param>
	public override void Write(string s)
	{
		if (s != null)
		{
			WriteString(s, 0, s.Length);
		}
	}

	/// <summary>Sends a stream of characters with the specified starting position and number of characters to the HTTP output stream.</summary>
	/// <param name="buffer">The memory buffer containing the characters to send to the HTTP output stream </param>
	/// <param name="index">The buffer position of the first character to send. </param>
	/// <param name="count">The number of characters to send beginning at the position specified by <paramref name="index" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="buffer" />, is <see langword="null" />.- or -
	///         <paramref name="index" /> is less than zero.- or - 
	///         <paramref name="count" /> is less than zero.- or -
	///         <paramref name="buffer" /> length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
	public override void Write(char[] buffer, int index, int count)
	{
		if (buffer == null || index < 0 || count < 0 || buffer.Length - index < count)
		{
			throw new ArgumentOutOfRangeException();
		}
		int maxByteCount = encoding.GetMaxByteCount(count);
		byte[] byteBuffer = GetByteBuffer(maxByteCount);
		int bytes = encoding.GetBytes(buffer, index, count, byteBuffer, 0);
		output_stream.Write(byteBuffer, 0, bytes);
		if (!response.buffer)
		{
			response.Flush();
		}
	}

	/// <summary>Sends a carriage return + line feed (CRLF) pair of characters to the HTTP output stream.</summary>
	public override void WriteLine()
	{
		Write(newline, 0, 2);
	}

	/// <summary>Sends a string with the specified starting position and number of characters to the HTTP output stream.</summary>
	/// <param name="s">The string to send to the HTTP output stream. </param>
	/// <param name="index">The character position of the first byte to send. </param>
	/// <param name="count">The number of characters to send, beginning at the character position specified by <paramref name="index" />. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> is less than zero.- or - The <paramref name="count" /> is less than zero. - or - The sum of the <paramref name="index" /> and the <paramref name="count" /> are greater than the string length.</exception>
	public void WriteString(string s, int index, int count)
	{
		if (s != null)
		{
			if (index < 0 || count < 0 || index + count > s.Length)
			{
				throw new ArgumentOutOfRangeException();
			}
			int maxByteCount = encoding.GetMaxByteCount(count);
			byte[] byteBuffer = GetByteBuffer(maxByteCount);
			int bytes = encoding.GetBytes(s, index, count, byteBuffer, 0);
			output_stream.Write(byteBuffer, 0, bytes);
			if (!response.buffer)
			{
				response.Flush();
			}
		}
	}

	internal void WriteUTF8Ptr(IntPtr ptr, int length)
	{
		output_stream.WritePtr(ptr, length);
	}

	/// <summary>Sends a stream of bytes with the specified starting position and number of bytes to the HTTP output stream.</summary>
	/// <param name="buffer">The memory buffer containing the bytes to send to the HTTP output stream. </param>
	/// <param name="index">The buffer position of the first byte to send. </param>
	/// <param name="count">The number of bytes to send, beginning at the byte position specified by <paramref name="index" />. </param>
	public void WriteBytes(byte[] buffer, int index, int count)
	{
		output_stream.Write(buffer, index, count);
		if (!response.buffer)
		{
			response.Flush();
		}
	}
}
