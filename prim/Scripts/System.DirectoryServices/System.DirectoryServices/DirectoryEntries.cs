using System.Collections;
using Novell.Directory.Ldap;

namespace System.DirectoryServices;

/// <summary>Contains a collection of <see cref="T:System.DirectoryServices.DirectoryEntry" /> objects.</summary>
public class DirectoryEntries : IEnumerable
{
	private LdapConnection _Conn;

	private string _Bpath;

	private string _Buser;

	private string _Bpass;

	private string _Basedn;

	private ArrayList m_oValues;

	internal string Basedn
	{
		get
		{
			if (_Basedn == null)
			{
				string dN = new LdapUrl(_Bpath).getDN();
				if (dN != null)
				{
					_Basedn = dN;
				}
				else
				{
					_Basedn = "";
				}
			}
			return _Basedn;
		}
	}

	internal string Bpath
	{
		get
		{
			return _Bpath;
		}
		set
		{
			_Bpath = value;
		}
	}

	internal LdapConnection Conn
	{
		get
		{
			if (_Conn == null)
			{
				InitBlock();
			}
			return _Conn;
		}
		set
		{
			_Conn = value;
		}
	}

	/// <summary>Gets the schemas that specify which child objects are contained in the collection.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.SchemaNameCollection" /> that specifies which child objects are contained in the <see cref="T:System.DirectoryServices.DirectoryEntries" /> instance.</returns>
	public SchemaNameCollection SchemaFilter
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException("System.DirectoryServices.DirectoryEntries.SchemaFilter");
		}
	}

	private void InitBlock()
	{
		try
		{
			LdapUrl ldapUrl = new LdapUrl(_Bpath);
			_Conn = new LdapConnection();
			_Conn.Connect(ldapUrl.Host, ldapUrl.Port);
			_Conn.Bind(_Buser, _Bpass);
		}
		catch (LdapException ex)
		{
			throw ex;
		}
		catch (Exception ex2)
		{
			throw ex2;
		}
	}

	internal DirectoryEntries(string path, string uname, string passwd)
	{
		_Bpath = path;
		_Buser = uname;
		_Bpass = passwd;
	}

	internal DirectoryEntries(string path, LdapConnection lc)
	{
		_Bpath = path;
		_Conn = lc;
	}

	/// <summary>Returns an enumerator that iterates through the collection.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
	public IEnumerator GetEnumerator()
	{
		m_oValues = new ArrayList();
		string[] attrs = new string[1] { "objectClass" };
		LdapSearchResults ldapSearchResults = Conn.Search(Basedn, 1, "objectClass=*", attrs, typesOnly: false);
		LdapUrl ldapUrl = new LdapUrl(_Bpath);
		string host = ldapUrl.Host;
		int port = ldapUrl.Port;
		while (ldapSearchResults.hasMore())
		{
			LdapEntry ldapEntry = null;
			try
			{
				ldapEntry = ldapSearchResults.next();
			}
			catch (LdapException)
			{
				continue;
			}
			DirectoryEntry directoryEntry = new DirectoryEntry(Conn);
			string dN = ldapEntry.DN;
			LdapUrl ldapUrl2 = new LdapUrl(host, port, dN);
			directoryEntry.Path = ldapUrl2.ToString();
			m_oValues.Add(directoryEntry);
		}
		return m_oValues.GetEnumerator();
	}

	/// <summary>Creates a new entry in the container.</summary>
	/// <param name="name">The name of the new entry.</param>
	/// <param name="schemaClassName">The name of the schema that is used for the new entry.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the new entry.</returns>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public DirectoryEntry Add(string name, string schemaClassName)
	{
		DirectoryEntry directoryEntry = new DirectoryEntry(Conn);
		LdapUrl ldapUrl = new LdapUrl(_Bpath);
		string dN = ldapUrl.getDN();
		LdapUrl ldapUrl2 = new LdapUrl(dn: (dN != null && dN.Length != 0) ? (name + "," + dN) : name, host: ldapUrl.Host, port: ldapUrl.Port);
		directoryEntry.Path = ldapUrl2.ToString();
		directoryEntry.Nflag = true;
		return directoryEntry;
	}

	/// <summary>Deletes a member of this collection.</summary>
	/// <param name="entry">The name of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to delete.</param>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public void Remove(DirectoryEntry entry)
	{
		LdapUrl ldapUrl = new LdapUrl(_Bpath);
		string dn = entry.Name + "," + ldapUrl.getDN();
		Conn.Delete(dn);
	}

	/// <summary>Returns the member of this collection with the specified name.</summary>
	/// <param name="name">Contains the name of the child object for which to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> that represents the child object that was found.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Active Directory Domain Services object is not a container.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public DirectoryEntry Find(string name)
	{
		return CheckEntry(name);
	}

	/// <summary>Returns the member of this collection with the specified name and of the specified type.</summary>
	/// <param name="name">The name of the child directory object for which to search.</param>
	/// <param name="schemaClassName">The class name of the child directory object for which to search.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the child object that was found.</returns>
	/// <exception cref="T:System.InvalidOperationException">The Active Directory Domain Services object is not a container.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred during the call to the underlying interface.</exception>
	public DirectoryEntry Find(string name, string schemaClassName)
	{
		DirectoryEntry directoryEntry = CheckEntry(name);
		if (directoryEntry != null)
		{
			if (directoryEntry.Properties["objectclass"].ContainsCaselessStringValue(schemaClassName))
			{
				return directoryEntry;
			}
			throw new SystemException("An unknown directory object was requested");
		}
		return directoryEntry;
	}

	private DirectoryEntry CheckEntry(string rdn)
	{
		string text = null;
		DirectoryEntry directoryEntry = null;
		text = rdn + "," + Basedn;
		string[] attrs = new string[1] { "objectClass" };
		try
		{
			LdapSearchResults ldapSearchResults = Conn.Search(text, 0, "objectClass=*", attrs, typesOnly: false);
			if (ldapSearchResults.hasMore())
			{
				try
				{
					ldapSearchResults.next();
					directoryEntry = new DirectoryEntry(Conn);
					LdapUrl ldapUrl = new LdapUrl(_Bpath);
					LdapUrl ldapUrl2 = new LdapUrl(ldapUrl.Host, ldapUrl.Port, text);
					directoryEntry.Path = ldapUrl2.ToString();
				}
				catch (LdapException ex)
				{
					throw ex;
				}
			}
		}
		catch (LdapException ex2)
		{
			if (ex2.ResultCode == 32)
			{
				return null;
			}
			throw ex2;
		}
		catch (Exception ex3)
		{
			throw ex3;
		}
		return directoryEntry;
	}
}
