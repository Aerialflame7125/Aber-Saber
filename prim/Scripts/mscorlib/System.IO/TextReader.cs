using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.IO;

/// <summary>Represents a reader that can read a sequential series of characters.</summary>
[Serializable]
[ComVisible(true)]
public abstract class TextReader : MarshalByRefObject, IDisposable
{
	[Serializable]
	private sealed class NullTextReader : TextReader
	{
		public override int Read(char[] buffer, int index, int count)
		{
			return 0;
		}

		public override string ReadLine()
		{
			return null;
		}
	}

	[Serializable]
	internal sealed class SyncTextReader : TextReader
	{
		internal TextReader _in;

		internal SyncTextReader(TextReader t)
		{
			_in = t;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Close()
		{
			_in.Close();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				((IDisposable)_in).Dispose();
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override int Peek()
		{
			return _in.Peek();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override int Read()
		{
			return _in.Read();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override int Read([In][Out] char[] buffer, int index, int count)
		{
			return _in.Read(buffer, index, count);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override int ReadBlock([In][Out] char[] buffer, int index, int count)
		{
			return _in.ReadBlock(buffer, index, count);
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override string ReadLine()
		{
			return _in.ReadLine();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public override string ReadToEnd()
		{
			return _in.ReadToEnd();
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		[ComVisible(false)]
		public override Task<string> ReadLineAsync()
		{
			return Task.FromResult(ReadLine());
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		[ComVisible(false)]
		public override Task<string> ReadToEndAsync()
		{
			return Task.FromResult(ReadToEnd());
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		[ComVisible(false)]
		public override Task<int> ReadBlockAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			return Task.FromResult(ReadBlock(buffer, index, count));
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		[ComVisible(false)]
		public override Task<int> ReadAsync(char[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
			}
			return Task.FromResult(Read(buffer, index, count));
		}
	}

	[NonSerialized]
	private static Func<object, string> _ReadLineDelegate = (object state) => ((TextReader)state).ReadLine();

	[NonSerialized]
	private static Func<object, int> _ReadDelegate = delegate(object state)
	{
		Tuple<TextReader, char[], int, int> tuple = (Tuple<TextReader, char[], int, int>)state;
		return tuple.Item1.Read(tuple.Item2, tuple.Item3, tuple.Item4);
	};

	/// <summary>Provides a <see langword="TextReader" /> with no data to read from.</summary>
	public static readonly TextReader Null = new NullTextReader();

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.TextReader" /> class.</summary>
	protected TextReader()
	{
	}

	/// <summary>Closes the <see cref="T:System.IO.TextReader" /> and releases any system resources associated with the <see langword="TextReader" />.</summary>
	public virtual void Close()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases all resources used by the <see cref="T:System.IO.TextReader" /> object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Releases the unmanaged resources used by the <see cref="T:System.IO.TextReader" /> and optionally releases the managed resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
	}

	/// <summary>Reads the next character without changing the state of the reader or the character source. Returns the next available character without actually reading it from the reader.</summary>
	/// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the reader does not support seeking.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	public virtual int Peek()
	{
		return -1;
	}

	/// <summary>Reads the next character from the text reader and advances the character position by one character.</summary>
	/// <returns>The next character from the text reader, or -1 if no more characters are available. The default implementation returns -1.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	public virtual int Read()
	{
		return -1;
	}

	/// <summary>Reads a specified maximum number of characters from the current reader and writes the data to a buffer, beginning at the specified index.</summary>
	/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
	/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
	/// <param name="count">The maximum number of characters to read. If the end of the reader is reached before the specified number of characters is read into the buffer, the method returns.</param>
	/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether the data is available within the reader. This method returns 0 (zero) if it is called when no more characters are left to read.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="buffer" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	public virtual int Read([In][Out] char[] buffer, int index, int count)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
		}
		if (index < 0)
		{
			throw new ArgumentOutOfRangeException("index", Environment.GetResourceString("Non-negative number required."));
		}
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("Non-negative number required."));
		}
		if (buffer.Length - index < count)
		{
			throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
		}
		int num = 0;
		do
		{
			int num2 = Read();
			if (num2 == -1)
			{
				break;
			}
			buffer[index + num++] = (char)num2;
		}
		while (num < count);
		return num;
	}

	/// <summary>Reads all characters from the current position to the end of the text reader and returns them as one string.</summary>
	/// <returns>A string that contains all characters from the current position to the end of the text reader.</returns>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
	public virtual string ReadToEnd()
	{
		char[] array = new char[4096];
		StringBuilder stringBuilder = new StringBuilder(4096);
		int charCount;
		while ((charCount = Read(array, 0, array.Length)) != 0)
		{
			stringBuilder.Append(array, 0, charCount);
		}
		return stringBuilder.ToString();
	}

	/// <summary>Reads a specified maximum number of characters from the current text reader and writes the data to a buffer, beginning at the specified index.</summary>
	/// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> -1) replaced by the characters read from the current source.</param>
	/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
	/// <param name="count">The maximum number of characters to read.</param>
	/// <returns>The number of characters that have been read. The number will be less than or equal to <paramref name="count" />, depending on whether all input characters have been read.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="buffer" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The buffer length minus <paramref name="index" /> is less than <paramref name="count" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	public virtual int ReadBlock([In][Out] char[] buffer, int index, int count)
	{
		int num = 0;
		int num2;
		do
		{
			num += (num2 = Read(buffer, index + num, count - num));
		}
		while (num2 > 0 && num < count);
		return num;
	}

	/// <summary>Reads a line of characters from the text reader and returns the data as a string.</summary>
	/// <returns>The next line from the reader, or <see langword="null" /> if all characters have been read.</returns>
	/// <exception cref="T:System.IO.IOException">An I/O error occurs.</exception>
	/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.IO.TextReader" /> is closed.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" /></exception>
	public virtual string ReadLine()
	{
		StringBuilder stringBuilder = new StringBuilder();
		while (true)
		{
			int num = Read();
			switch (num)
			{
			case 10:
			case 13:
				if (num == 13 && Peek() == 10)
				{
					Read();
				}
				return stringBuilder.ToString();
			case -1:
				if (stringBuilder.Length > 0)
				{
					return stringBuilder.ToString();
				}
				return null;
			}
			stringBuilder.Append((char)num);
		}
	}

	/// <summary>Reads a line of characters asynchronously and returns the data as a string.</summary>
	/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the next line from the text reader, or is <see langword="null" /> if all of the characters have been read.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
	/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
	public virtual Task<string> ReadLineAsync()
	{
		return Task<string>.Factory.StartNew(_ReadLineDelegate, this, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
	}

	/// <summary>Reads all characters from the current position to the end of the text reader asynchronously and returns them as one string.</summary>
	/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains a string with the characters from the current position to the end of the text reader.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
	/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
	public virtual async Task<string> ReadToEndAsync()
	{
		char[] chars = new char[4096];
		StringBuilder sb = new StringBuilder(4096);
		while (true)
		{
			int num;
			int len = (num = await ReadAsyncInternal(chars, 0, chars.Length).ConfigureAwait(continueOnCapturedContext: false));
			if (num == 0)
			{
				break;
			}
			sb.Append(chars, 0, len);
		}
		return sb.ToString();
	}

	/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
	/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
	/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
	/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
	/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="buffer" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
	/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
	/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
	public virtual Task<int> ReadAsync(char[] buffer, int index, int count)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
		}
		if (index < 0 || count < 0)
		{
			throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
		}
		if (buffer.Length - index < count)
		{
			throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
		}
		return ReadAsyncInternal(buffer, index, count);
	}

	internal virtual Task<int> ReadAsyncInternal(char[] buffer, int index, int count)
	{
		Tuple<TextReader, char[], int, int> state = new Tuple<TextReader, char[], int, int>(this, buffer, index, count);
		return Task<int>.Factory.StartNew(_ReadDelegate, state, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
	}

	/// <summary>Reads a specified maximum number of characters from the current text reader asynchronously and writes the data to a buffer, beginning at the specified index.</summary>
	/// <param name="buffer">When this method returns, contains the specified character array with the values between <paramref name="index" /> and (<paramref name="index" /> + <paramref name="count" /> - 1) replaced by the characters read from the current source.</param>
	/// <param name="index">The position in <paramref name="buffer" /> at which to begin writing.</param>
	/// <param name="count">The maximum number of characters to read. If the end of the text is reached before the specified number of characters is read into the buffer, the current method returns.</param>
	/// <returns>A task that represents the asynchronous read operation. The value of the <paramref name="TResult" /> parameter contains the total number of bytes read into the buffer. The result value can be less than the number of bytes requested if the number of bytes currently available is less than the requested number, or it can be 0 (zero) if the end of the text has been reached.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="buffer" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///   <paramref name="index" /> or <paramref name="count" /> is negative.</exception>
	/// <exception cref="T:System.ArgumentException">The sum of <paramref name="index" /> and <paramref name="count" /> is larger than the buffer length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The text reader has been disposed.</exception>
	/// <exception cref="T:System.InvalidOperationException">The reader is currently in use by a previous read operation.</exception>
	[ComVisible(false)]
	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
	public virtual Task<int> ReadBlockAsync(char[] buffer, int index, int count)
	{
		if (buffer == null)
		{
			throw new ArgumentNullException("buffer", Environment.GetResourceString("Buffer cannot be null."));
		}
		if (index < 0 || count < 0)
		{
			throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", Environment.GetResourceString("Non-negative number required."));
		}
		if (buffer.Length - index < count)
		{
			throw new ArgumentException(Environment.GetResourceString("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection."));
		}
		return ReadBlockAsyncInternal(buffer, index, count);
	}

	[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
	private async Task<int> ReadBlockAsyncInternal(char[] buffer, int index, int count)
	{
		int n = 0;
		int num;
		do
		{
			num = await ReadAsyncInternal(buffer, index + n, count - n).ConfigureAwait(continueOnCapturedContext: false);
			n += num;
		}
		while (num > 0 && n < count);
		return n;
	}

	/// <summary>Creates a thread-safe wrapper around the specified <see langword="TextReader" />.</summary>
	/// <param name="reader">The <see langword="TextReader" /> to synchronize.</param>
	/// <returns>A thread-safe <see cref="T:System.IO.TextReader" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="reader" /> is <see langword="null" />.</exception>
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public static TextReader Synchronized(TextReader reader)
	{
		if (reader == null)
		{
			throw new ArgumentNullException("reader");
		}
		if (reader is SyncTextReader)
		{
			return reader;
		}
		return new SyncTextReader(reader);
	}
}
