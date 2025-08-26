using System.Reflection;

namespace System.Web.UI;

internal interface IScriptResourceMapping
{
	IScriptResourceDefinition GetDefinition(string resourceName);

	IScriptResourceDefinition GetDefinition(string resourceName, Assembly resourceAssembly);
}
