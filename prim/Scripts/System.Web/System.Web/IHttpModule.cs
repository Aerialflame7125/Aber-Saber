namespace System.Web;

/// <summary>Provides module initialization and disposal events to the implementing class.</summary>
public interface IHttpModule
{
	/// <summary>Initializes a module and prepares it to handle requests.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpApplication" /> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
	void Init(HttpApplication context);

	/// <summary>Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule" />.</summary>
	void Dispose();
}
