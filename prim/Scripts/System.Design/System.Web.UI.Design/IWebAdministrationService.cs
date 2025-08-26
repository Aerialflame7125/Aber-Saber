using System.Collections;

namespace System.Web.UI.Design;

/// <summary>Provides an interface for creating services for administering a Web site at design time.</summary>
public interface IWebAdministrationService
{
	/// <summary>Starts the Web administration facility in the design host.</summary>
	/// <param name="arguments">An <see cref="T:System.Collections.IDictionary" />.</param>
	void Start(IDictionary arguments);
}
