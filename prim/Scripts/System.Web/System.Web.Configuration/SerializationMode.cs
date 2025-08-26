namespace System.Web.Configuration;

/// <summary>Determines the serialization method used for a <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object.</summary>
public enum SerializationMode
{
	/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized to a simple string.</summary>
	String,
	/// <summary>The profile <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> is serialized as XML using XML serialization.</summary>
	Xml,
	/// <summary>The <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object is serialized using binary serialization.</summary>
	Binary,
	/// <summary>The provider has implicit knowledge of the type and is responsible for deciding how to serialize the <see cref="T:System.Web.Configuration.ProfilePropertySettings" /> object into the data store.</summary>
	ProviderSpecific
}
