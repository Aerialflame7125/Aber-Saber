using System.Runtime.InteropServices;

namespace System.Web.Configuration;

/// <summary>Used internally to support remote access to configuration data.</summary>
[ComVisible(true)]
[Guid("A99B591A-23C6-4238-8452-C7B0E895063D")]
public interface IRemoteWebConfigurationHostServer
{
	/// <summary>Used internally to support remote access to configuration data.</summary>
	/// <param name="fileName">Path to the remote configuration file to be accessed.</param>
	/// <param name="getReadTimeOnly">A Boolean value specifying whether only the <paramref name="readTime" /> is returned.</param>
	/// <param name="readTime">Time the file was last accessed.</param>
	/// <returns>An <see langword="array" /> of 8-bit unsigned integers (bytes) containing the configuration data.</returns>
	byte[] GetData(string fileName, bool getReadTimeOnly, out long readTime);

	/// <summary>Used internally to support remote access to configuration data.</summary>
	/// <param name="fileName">Path to the remote configuration file to be accessed.</param>
	/// <param name="templateFileName">File to duplicate file attributes from.</param>
	/// <param name="data">Data to be written.</param>
	/// <param name="readTime">Time the file was last accessed.</param>
	void WriteData(string fileName, string templateFileName, byte[] data, ref long readTime);

	/// <summary>Used internally to support remote access to configuration data.</summary>
	/// <param name="webLevel">The level of the configuration file.</param>
	/// <param name="path">Path to the remote configuration file to be accessed.</param>
	/// <param name="site">Path to the remote machine.</param>
	/// <param name="locationSubPath">The subpath of the location of the configuration file.</param>
	/// <returns>A concatenated string representing the file path of the configuration file.</returns>
	string GetFilePaths(int webLevel, string path, string site, string locationSubPath);

	/// <summary>Conditionally encrypts or decrypts the value of the string referenced by the <paramref name="xmlString" /> parameter.</summary>
	/// <param name="doEncrypt">
	///       <see langword="True" /> to encrypt; <see langword="false" /> to decrypt.</param>
	/// <param name="xmlString">The XML to be encrypted or decrypted.</param>
	/// <param name="protectionProviderName">The provider used to protect the configuration data. </param>
	/// <param name="protectionProviderType">The <see cref="T:System.Type" /> of the protection provider.</param>
	/// <param name="parameterKeys">The keys of optional parameters for the protection provider.</param>
	/// <param name="parameterValues">The values of optional parameters for the protection provider.</param>
	/// <returns>A string containing either the encrypted or decrypted value of the <paramref name="xmlString" />.</returns>
	string DoEncryptOrDecrypt(bool doEncrypt, string xmlString, string protectionProviderName, string protectionProviderType, string[] parameterKeys, string[] parameterValues);

	/// <summary>Gets the details of the configuration file.</summary>
	/// <param name="name">The name of the file.</param>
	/// <param name="exists">
	///       <see langword="true" /> if the file exists; otherwise, <see langword="false" />.</param>
	/// <param name="size">The size of the file.</param>
	/// <param name="createDate">The date the file was created.</param>
	/// <param name="lastWriteDate">The date the file was last written.</param>
	void GetFileDetails(string name, out bool exists, out long size, out long createDate, out long lastWriteDate);
}
