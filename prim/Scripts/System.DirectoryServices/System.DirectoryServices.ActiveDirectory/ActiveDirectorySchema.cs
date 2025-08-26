using System.Security.Permissions;

namespace System.DirectoryServices.ActiveDirectory;

/// <summary>The <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> class represents the schema partition for a particular domain.</summary>
[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
public class ActiveDirectorySchema : ActiveDirectoryPartition
{
	/// <summary>Gets the schema master role owner.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryServer" /> object that represents the server that is the schema master.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public DirectoryServer SchemaRoleOwner
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Releases the managed resources that are used by the <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object and, optionally, releases unmanaged resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
	}

	/// <summary>Retrieves the schema object for the specified context.</summary>
	/// <param name="context">A <see cref="T:System.DirectoryServices.ActiveDirectory.DirectoryContext" /> object that contains the target and credentials to use to retrieve the object. The target of the context must be a forest, directory server, or configuration set.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema for the specified context.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the target specified in <paramref name="context" /> cannot be made.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="context" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="context" /> is invalid.</exception>
	public static ActiveDirectorySchema GetSchema(DirectoryContext context)
	{
		throw new NotImplementedException();
	}

	/// <summary>Refreshes the schema cache on the client computer.</summary>
	public void RefreshSchema()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the class with the specified name.</summary>
	/// <param name="ldapDisplayName">The LDAP display name of the class to find.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that represents the class with the specified name.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A class with the specified name cannot be found.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="ldapDisplayName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="ldapDisplayName" /> is zero length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ActiveDirectorySchemaClass FindClass(string ldapDisplayName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the defunct class that has the specified common name.</summary>
	/// <param name="commonName">The common name of the defunct class to find.</param>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> object that represents the class with the specified common name.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A class with the specified name cannot be found.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="commonName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="commonName" /> is zero length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ActiveDirectorySchemaClass FindDefunctClass(string commonName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all Active Directory Domain Services classes in the schema.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all Active Directory Domain Services classes in the schema that are of the specified type.</summary>
	/// <param name="type">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.SchemaClassType" /> members that identifies which type of classes to retrieve.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaClassCollection FindAllClasses(SchemaClassType type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the defunct Active Directory Domain Services classes in the schema.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaClassCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaClass" /> objects for the classes that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaClassCollection FindAllDefunctClasses()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the property with the specified name.</summary>
	/// <param name="ldapDisplayName">The LDAP display name of the property to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that represents the property with the specified name.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A property with the specified name cannot be found.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="propertyName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="propertyName" /> is zero length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ActiveDirectorySchemaProperty FindProperty(string ldapDisplayName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves the defunct property that has the specified common name.</summary>
	/// <param name="commonName">The common name of the defunct property to find.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> object that represents the property.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A property with the specified name cannot be found.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="commonName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="commonName" /> is zero length.</exception>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ActiveDirectorySchemaProperty FindDefunctProperty(string commonName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the Active Directory Domain Services properties in the schema.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the Active Directory Domain Services properties in the schema of the specified type.</summary>
	/// <param name="type">One of the <see cref="T:System.DirectoryServices.ActiveDirectory.PropertyTypes" /> members that identifies which type of properties to retrieve.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllProperties(PropertyTypes type)
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves all of the defunct Active Directory Domain Services properties in the schema.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.ActiveDirectory.ReadOnlyActiveDirectorySchemaPropertyCollection" /> object that contains a collection of <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchemaProperty" /> objects for the properties that were retrieved.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	public ReadOnlyActiveDirectorySchemaPropertyCollection FindAllDefunctProperties()
	{
		throw new NotImplementedException();
	}

	/// <summary>Retrieves a <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the directory partition.</returns>
	/// <exception cref="T:System.ObjectDisposedException">The current object has been disposed.</exception>
	[DirectoryServicesPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	[DirectoryServicesPermission(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public override DirectoryEntry GetDirectoryEntry()
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the schema object for the forest that the currently logged-on user is a member of.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectorySchema" /> object that represents the schema for the domain that the local computer is a member of.</returns>
	/// <exception cref="T:System.DirectoryServices.ActiveDirectory.ActiveDirectoryObjectNotFoundException">A connection to the local domain cannot be made.</exception>
	public static ActiveDirectorySchema GetCurrentSchema()
	{
		throw new NotImplementedException();
	}
}
