namespace System.Web.Util;

/// <summary>Provides the interface for implementing factories for Web objects.</summary>
public interface IWebObjectFactory
{
	/// <summary>Creates a new <see cref="T:System.Web.Util.IWebObjectFactory" /> instance.</summary>
	/// <returns>A new <see cref="T:System.Web.Util.IWebObjectFactory" /></returns>
	object CreateInstance();
}
