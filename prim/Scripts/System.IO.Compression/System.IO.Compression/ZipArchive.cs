using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace System.IO.Compression;

/// <summary>Represents a package of compressed files in the zip archive format.</summary>
public class ZipArchive : IDisposable
{
	private Stream _archiveStream;

	private ZipArchiveEntry _archiveStreamOwner;

	private BinaryReader _archiveReader;

	private ZipArchiveMode _mode;

	private List<ZipArchiveEntry> _entries;

	private ReadOnlyCollection<ZipArchiveEntry> _entriesCollection;

	private Dictionary<string, ZipArchiveEntry> _entriesDictionary;

	private bool _readEntries;

	private bool _leaveOpen;

	private long _centralDirectoryStart;

	private bool _isDisposed;

	private uint _numberOfThisDisk;

	private long _expectedNumberOfEntries;

	private Stream _backingStream;

	private byte[] _archiveComment;

	private Encoding _entryNameEncoding;

	/// <summary>Gets the collection of entries that are currently in the zip archive.</summary>
	/// <returns>The collection of entries that are currently in the zip archive.</returns>
	/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
	public ReadOnlyCollection<ZipArchiveEntry> Entries
	{
		get
		{
			if (_mode == ZipArchiveMode.Create)
			{
				throw new NotSupportedException("Cannot access entries in Create mode.");
			}
			ThrowIfDisposed();
			EnsureCentralDirectoryRead();
			return _entriesCollection;
		}
	}

	/// <summary>Gets a value that describes the type of action the zip archive can perform on entries.</summary>
	/// <returns>One of the enumeration values that describes the type of action (read, create, or update) the zip archive can perform on entries.</returns>
	public ZipArchiveMode Mode => _mode;

	internal BinaryReader ArchiveReader => _archiveReader;

	internal Stream ArchiveStream => _archiveStream;

	internal uint NumberOfThisDisk => _numberOfThisDisk;

	internal Encoding EntryNameEncoding
	{
		get
		{
			return _entryNameEncoding;
		}
		private set
		{
			if (value != null && (value.Equals(Encoding.BigEndianUnicode) || value.Equals(Encoding.Unicode)))
			{
				throw new ArgumentException("The specified entry name encoding is not supported.", "EntryNameEncoding");
			}
			_entryNameEncoding = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream.</summary>
	/// <param name="stream">The stream that contains the archive to be read.</param>
	/// <exception cref="T:System.ArgumentException">The stream is already closed or does not support reading.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="stream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream are not in the zip archive format.</exception>
	public ZipArchive(Stream stream)
		: this(stream, ZipArchiveMode.Read, leaveOpen: false, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class from the specified stream and with the specified mode.</summary>
	/// <param name="stream">The input or output stream.</param>
	/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
	/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="stream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="mode" /> is an invalid value.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
	public ZipArchive(Stream stream, ZipArchiveMode mode)
		: this(stream, mode, leaveOpen: false, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, and optionally leaves the stream open.</summary>
	/// <param name="stream">The input or output stream.</param>
	/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
	/// <param name="leaveOpen">
	///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="stream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="mode" /> is an invalid value.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
	public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen)
		: this(stream, mode, leaveOpen, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class on the specified stream for the specified mode, uses the specified encoding for entry names, and optionally leaves the stream open.</summary>
	/// <param name="stream">The input or output stream.</param>
	/// <param name="mode">One of the enumeration values that indicates whether the zip archive is used to read, create, or update entries.</param>
	/// <param name="leaveOpen">
	///       <see langword="true" /> to leave the stream open after the <see cref="T:System.IO.Compression.ZipArchive" /> object is disposed; otherwise, <see langword="false" />.</param>
	/// <param name="entryNameEncoding">The encoding to use when reading or writing entry names in this archive. Specify a value for this parameter only when an encoding is required for interoperability with zip archive tools and libraries that do not support UTF-8 encoding for entry names.</param>
	/// <exception cref="T:System.ArgumentException">The stream is already closed, or the capabilities of the stream do not match the mode.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="stream" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="mode" /> is an invalid value.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The contents of the stream could not be interpreted as a zip archive.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is missing from the archive or is corrupt and cannot be read.-or-
	///         <paramref name="mode" /> is <see cref="F:System.IO.Compression.ZipArchiveMode.Update" /> and an entry is too large to fit into memory.</exception>
	public ZipArchive(Stream stream, ZipArchiveMode mode, bool leaveOpen, Encoding entryNameEncoding)
	{
		if (stream == null)
		{
			throw new ArgumentNullException("stream");
		}
		EntryNameEncoding = entryNameEncoding;
		Init(stream, mode, leaveOpen);
	}

	/// <summary>Creates an empty entry that has the specified path and entry name in the zip archive.</summary>
	/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
	/// <returns>An empty entry in the zip archive.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="entryName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
	public ZipArchiveEntry CreateEntry(string entryName)
	{
		return DoCreateEntry(entryName, null);
	}

	/// <summary>Creates an empty entry that has the specified entry name and compression level in the zip archive.</summary>
	/// <param name="entryName">A path, relative to the root of the archive, that specifies the name of the entry to be created.</param>
	/// <param name="compressionLevel">One of the enumeration values that indicates whether to emphasize speed or compression effectiveness when creating the entry.</param>
	/// <returns>An empty entry in the zip archive.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="entryName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The zip archive does not support writing.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
	public ZipArchiveEntry CreateEntry(string entryName, CompressionLevel compressionLevel)
	{
		return DoCreateEntry(entryName, compressionLevel);
	}

	/// <summary>Called by the <see cref="M:System.IO.Compression.ZipArchive.Dispose" /> and <see cref="M:System.Object.Finalize" /> methods to release the unmanaged resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class, and optionally finishes writing the archive and releases the managed resources.</summary>
	/// <param name="disposing">
	///       <see langword="true" /> to finish writing the archive and release unmanaged and managed resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!disposing || _isDisposed)
		{
			return;
		}
		try
		{
			ZipArchiveMode mode = _mode;
			if (mode != 0)
			{
				_ = mode - 1;
				_ = 1;
				WriteFile();
			}
		}
		finally
		{
			CloseStreams();
			_isDisposed = true;
		}
	}

	/// <summary>Releases the resources used by the current instance of the <see cref="T:System.IO.Compression.ZipArchive" /> class.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	/// <summary>Retrieves a wrapper for the specified entry in the zip archive.</summary>
	/// <param name="entryName">A path, relative to the root of the archive, that identifies the entry to retrieve.</param>
	/// <returns>A wrapper for the specified entry in the archive; <see langword="null" /> if the entry does not exist in the archive.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="entryName" /> is <see cref="F:System.String.Empty" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="entryName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The zip archive does not support reading.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The zip archive has been disposed.</exception>
	/// <exception cref="T:System.IO.InvalidDataException">The zip archive is corrupt, and its entries cannot be retrieved.</exception>
	public ZipArchiveEntry GetEntry(string entryName)
	{
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		if (_mode == ZipArchiveMode.Create)
		{
			throw new NotSupportedException("Cannot access entries in Create mode.");
		}
		EnsureCentralDirectoryRead();
		_entriesDictionary.TryGetValue(entryName, out var value);
		return value;
	}

	private ZipArchiveEntry DoCreateEntry(string entryName, CompressionLevel? compressionLevel)
	{
		if (entryName == null)
		{
			throw new ArgumentNullException("entryName");
		}
		if (string.IsNullOrEmpty(entryName))
		{
			throw new ArgumentException("String cannot be empty.", "entryName");
		}
		if (_mode == ZipArchiveMode.Read)
		{
			throw new NotSupportedException("Cannot create entries on an archive opened in read mode.");
		}
		ThrowIfDisposed();
		ZipArchiveEntry zipArchiveEntry = (compressionLevel.HasValue ? new ZipArchiveEntry(this, entryName, compressionLevel.Value) : new ZipArchiveEntry(this, entryName));
		AddEntry(zipArchiveEntry);
		return zipArchiveEntry;
	}

	internal void AcquireArchiveStream(ZipArchiveEntry entry)
	{
		if (_archiveStreamOwner != null)
		{
			if (_archiveStreamOwner.EverOpenedForWrite)
			{
				throw new IOException("Entries cannot be created while previously created entries are still open.");
			}
			_archiveStreamOwner.WriteAndFinishLocalEntry();
		}
		_archiveStreamOwner = entry;
	}

	private void AddEntry(ZipArchiveEntry entry)
	{
		_entries.Add(entry);
		string fullName = entry.FullName;
		if (!_entriesDictionary.ContainsKey(fullName))
		{
			_entriesDictionary.Add(fullName, entry);
		}
	}

	[Conditional("DEBUG")]
	internal void DebugAssertIsStillArchiveStreamOwner(ZipArchiveEntry entry)
	{
	}

	internal void ReleaseArchiveStream(ZipArchiveEntry entry)
	{
		_archiveStreamOwner = null;
	}

	internal void RemoveEntry(ZipArchiveEntry entry)
	{
		_entries.Remove(entry);
		_entriesDictionary.Remove(entry.FullName);
	}

	internal void ThrowIfDisposed()
	{
		if (_isDisposed)
		{
			throw new ObjectDisposedException(GetType().ToString());
		}
	}

	private void CloseStreams()
	{
		if (!_leaveOpen)
		{
			_archiveStream.Dispose();
			_backingStream?.Dispose();
			_archiveReader?.Dispose();
		}
		else if (_backingStream != null)
		{
			_archiveStream.Dispose();
		}
	}

	private void EnsureCentralDirectoryRead()
	{
		if (!_readEntries)
		{
			ReadCentralDirectory();
			_readEntries = true;
		}
	}

	private void Init(Stream stream, ZipArchiveMode mode, bool leaveOpen)
	{
		Stream stream2 = null;
		try
		{
			_backingStream = null;
			switch (mode)
			{
			case ZipArchiveMode.Create:
				if (!stream.CanWrite)
				{
					throw new ArgumentException("Cannot use create mode on a non-writable stream.");
				}
				break;
			case ZipArchiveMode.Read:
				if (!stream.CanRead)
				{
					throw new ArgumentException("Cannot use read mode on a non-readable stream.");
				}
				if (!stream.CanSeek)
				{
					_backingStream = stream;
					stream2 = (stream = new MemoryStream());
					_backingStream.CopyTo(stream);
					stream.Seek(0L, SeekOrigin.Begin);
				}
				break;
			case ZipArchiveMode.Update:
				if (!stream.CanRead || !stream.CanWrite || !stream.CanSeek)
				{
					throw new ArgumentException("Update mode requires a stream with read, write, and seek capabilities.");
				}
				break;
			default:
				throw new ArgumentOutOfRangeException("mode");
			}
			_mode = mode;
			if (mode == ZipArchiveMode.Create && !stream.CanSeek)
			{
				_archiveStream = new PositionPreservingWriteOnlyStreamWrapper(stream);
			}
			else
			{
				_archiveStream = stream;
			}
			_archiveStreamOwner = null;
			if (mode == ZipArchiveMode.Create)
			{
				_archiveReader = null;
			}
			else
			{
				_archiveReader = new BinaryReader(_archiveStream);
			}
			_entries = new List<ZipArchiveEntry>();
			_entriesCollection = new ReadOnlyCollection<ZipArchiveEntry>(_entries);
			_entriesDictionary = new Dictionary<string, ZipArchiveEntry>();
			_readEntries = false;
			_leaveOpen = leaveOpen;
			_centralDirectoryStart = 0L;
			_isDisposed = false;
			_numberOfThisDisk = 0u;
			_archiveComment = null;
			switch (mode)
			{
			case ZipArchiveMode.Create:
				_readEntries = true;
				return;
			case ZipArchiveMode.Read:
				ReadEndOfCentralDirectory();
				return;
			}
			if (_archiveStream.Length == 0L)
			{
				_readEntries = true;
				return;
			}
			ReadEndOfCentralDirectory();
			EnsureCentralDirectoryRead();
			foreach (ZipArchiveEntry entry in _entries)
			{
				entry.ThrowIfNotOpenable(needToUncompress: false, needToLoadIntoMemory: true);
			}
		}
		catch
		{
			stream2?.Dispose();
			throw;
		}
	}

	private void ReadCentralDirectory()
	{
		try
		{
			_archiveStream.Seek(_centralDirectoryStart, SeekOrigin.Begin);
			long num = 0L;
			bool saveExtraFieldsAndComments = Mode == ZipArchiveMode.Update;
			ZipCentralDirectoryFileHeader header;
			while (ZipCentralDirectoryFileHeader.TryReadBlock(_archiveReader, saveExtraFieldsAndComments, out header))
			{
				AddEntry(new ZipArchiveEntry(this, header));
				num++;
			}
			if (num != _expectedNumberOfEntries)
			{
				throw new InvalidDataException("Number of entries expected in End Of Central Directory does not correspond to number of entries in Central Directory.");
			}
		}
		catch (EndOfStreamException p)
		{
			throw new InvalidDataException(global::SR.Format("Central Directory is invalid.", p));
		}
	}

	private void ReadEndOfCentralDirectory()
	{
		try
		{
			_archiveStream.Seek(-18L, SeekOrigin.End);
			if (!ZipHelper.SeekBackwardsToSignature(_archiveStream, 101010256u))
			{
				throw new InvalidDataException("End of Central Directory record could not be found.");
			}
			long position = _archiveStream.Position;
			ZipEndOfCentralDirectoryBlock.TryReadBlock(_archiveReader, out var eocdBlock);
			if (eocdBlock.NumberOfThisDisk != eocdBlock.NumberOfTheDiskWithTheStartOfTheCentralDirectory)
			{
				throw new InvalidDataException("Split or spanned archives are not supported.");
			}
			_numberOfThisDisk = eocdBlock.NumberOfThisDisk;
			_centralDirectoryStart = eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber;
			if (eocdBlock.NumberOfEntriesInTheCentralDirectory != eocdBlock.NumberOfEntriesInTheCentralDirectoryOnThisDisk)
			{
				throw new InvalidDataException("Split or spanned archives are not supported.");
			}
			_expectedNumberOfEntries = eocdBlock.NumberOfEntriesInTheCentralDirectory;
			if (_mode == ZipArchiveMode.Update)
			{
				_archiveComment = eocdBlock.ArchiveComment;
			}
			if (eocdBlock.NumberOfThisDisk == ushort.MaxValue || eocdBlock.OffsetOfStartOfCentralDirectoryWithRespectToTheStartingDiskNumber == uint.MaxValue || eocdBlock.NumberOfEntriesInTheCentralDirectory == ushort.MaxValue)
			{
				_archiveStream.Seek(position - 16, SeekOrigin.Begin);
				if (ZipHelper.SeekBackwardsToSignature(_archiveStream, 117853008u))
				{
					Zip64EndOfCentralDirectoryLocator.TryReadBlock(_archiveReader, out var zip64EOCDLocator);
					if (zip64EOCDLocator.OffsetOfZip64EOCD > long.MaxValue)
					{
						throw new InvalidDataException("Offset to Zip64 End Of Central Directory record cannot be held in an Int64.");
					}
					long offsetOfZip64EOCD = (long)zip64EOCDLocator.OffsetOfZip64EOCD;
					_archiveStream.Seek(offsetOfZip64EOCD, SeekOrigin.Begin);
					if (!Zip64EndOfCentralDirectoryRecord.TryReadBlock(_archiveReader, out var zip64EOCDRecord))
					{
						throw new InvalidDataException("Zip 64 End of Central Directory Record not where indicated.");
					}
					_numberOfThisDisk = zip64EOCDRecord.NumberOfThisDisk;
					if (zip64EOCDRecord.NumberOfEntriesTotal > long.MaxValue)
					{
						throw new InvalidDataException("Number of Entries cannot be held in an Int64.");
					}
					if (zip64EOCDRecord.OffsetOfCentralDirectory > long.MaxValue)
					{
						throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
					}
					if (zip64EOCDRecord.NumberOfEntriesTotal != zip64EOCDRecord.NumberOfEntriesOnThisDisk)
					{
						throw new InvalidDataException("Split or spanned archives are not supported.");
					}
					_expectedNumberOfEntries = (long)zip64EOCDRecord.NumberOfEntriesTotal;
					_centralDirectoryStart = (long)zip64EOCDRecord.OffsetOfCentralDirectory;
				}
			}
			if (_centralDirectoryStart > _archiveStream.Length)
			{
				throw new InvalidDataException("Offset to Central Directory cannot be held in an Int64.");
			}
		}
		catch (EndOfStreamException innerException)
		{
			throw new InvalidDataException("Central Directory corrupt.", innerException);
		}
		catch (IOException innerException2)
		{
			throw new InvalidDataException("Central Directory corrupt.", innerException2);
		}
	}

	private void WriteFile()
	{
		if (_mode == ZipArchiveMode.Update)
		{
			List<ZipArchiveEntry> list = new List<ZipArchiveEntry>();
			foreach (ZipArchiveEntry entry in _entries)
			{
				if (!entry.LoadLocalHeaderExtraFieldAndCompressedBytesIfNeeded())
				{
					list.Add(entry);
				}
			}
			foreach (ZipArchiveEntry item in list)
			{
				item.Delete();
			}
			_archiveStream.Seek(0L, SeekOrigin.Begin);
			_archiveStream.SetLength(0L);
		}
		foreach (ZipArchiveEntry entry2 in _entries)
		{
			entry2.WriteAndFinishLocalEntry();
		}
		long position = _archiveStream.Position;
		foreach (ZipArchiveEntry entry3 in _entries)
		{
			entry3.WriteCentralDirectoryFileHeader();
		}
		long sizeOfCentralDirectory = _archiveStream.Position - position;
		WriteArchiveEpilogue(position, sizeOfCentralDirectory);
	}

	private void WriteArchiveEpilogue(long startOfCentralDirectory, long sizeOfCentralDirectory)
	{
		if (startOfCentralDirectory >= uint.MaxValue || sizeOfCentralDirectory >= uint.MaxValue || _entries.Count >= 65535)
		{
			long position = _archiveStream.Position;
			Zip64EndOfCentralDirectoryRecord.WriteBlock(_archiveStream, _entries.Count, startOfCentralDirectory, sizeOfCentralDirectory);
			Zip64EndOfCentralDirectoryLocator.WriteBlock(_archiveStream, position);
		}
		ZipEndOfCentralDirectoryBlock.WriteBlock(_archiveStream, _entries.Count, startOfCentralDirectory, sizeOfCentralDirectory, _archiveComment);
	}
}
