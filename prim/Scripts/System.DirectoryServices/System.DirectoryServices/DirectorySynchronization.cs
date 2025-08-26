using System.ComponentModel;

namespace System.DirectoryServices;

/// <summary>Specifies how to synchronize a directory within a domain.</summary>
public class DirectorySynchronization
{
	/// <summary>Gets or sets the options for the directory synchronization search.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> object.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> values.</exception>
	[DefaultValue(DirectorySynchronizationOptions.None)]
	[DSDescription("DSDirectorySynchronizationFlag")]
	public DirectorySynchronizationOptions Option
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object.</summary>
	public DirectorySynchronization()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization option.</summary>
	/// <param name="option">A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> data type object that specifies how a directory synchronization search is performed.</param>
	public DirectorySynchronization(DirectorySynchronizationOptions option)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization object.</summary>
	/// <param name="sync">A <see cref="T:System.DirectoryServices.DirectorySynchronization" /> data type object.</param>
	public DirectorySynchronization(DirectorySynchronization sync)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a cookie.</summary>
	/// <param name="cookie">A Byte data type object that specifies the directory synchronization search cookie.</param>
	public DirectorySynchronization(byte[] cookie)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object with a directory synchronization option and cookie.</summary>
	/// <param name="option">A <see cref="T:System.DirectoryServices.DirectorySynchronizationOptions" /> data type object that specifies how a directory synchronization search is performed.</param>
	/// <param name="cookie">A Byte data type object that specifies the directory synchronization search cookie.</param>
	public DirectorySynchronization(DirectorySynchronizationOptions option, byte[] cookie)
	{
	}

	/// <summary>Gets the directory synchronization search cookie.</summary>
	/// <returns>The directory synchronization search cookie object.</returns>
	public byte[] GetDirectorySynchronizationCookie()
	{
		throw new NotImplementedException();
	}

	/// <summary>Resetss the directory synchronization search cookie.</summary>
	public void ResetDirectorySynchronizationCookie()
	{
	}

	/// <summary>Resets the directory synchronization search cookie.</summary>
	/// <param name="cookie">A Byte data type object that contains a directory synchronization search cookie.  This method resets the cookie for this <see cref="T:System.DirectoryServices.DirectorySynchronization" /> object instance to this value.</param>
	public void ResetDirectorySynchronizationCookie(byte[] cookie)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a copy of the current <see cref="T:System.DirectoryServices.DirectorySynchronization" /> instance.</summary>
	/// <returns>A copy of the current <see cref="T:System.DirectoryServices.DirectorySynchronization" /> instance.</returns>
	public DirectorySynchronization Copy()
	{
		throw new NotImplementedException();
	}
}
