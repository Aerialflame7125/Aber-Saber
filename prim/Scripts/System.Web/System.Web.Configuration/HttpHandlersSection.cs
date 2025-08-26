using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures an HTTP handler for a Web application. This class cannot be inherited.</summary>
public sealed class HttpHandlersSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty handlersProp;

	/// <summary>Gets the <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> collection of <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects contained by the <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</summary>
	/// <returns>An <see cref="T:System.Web.Configuration.HttpHandlerActionCollection" /> that contains the <see cref="T:System.Web.Configuration.HttpHandlerAction" /> objects, or handlers.</returns>
	[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
	public HttpHandlerActionCollection Handlers => (HttpHandlerActionCollection)base[handlersProp];

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static HttpHandlersSection()
	{
		handlersProp = new ConfigurationProperty("", typeof(HttpHandlerActionCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);
		properties = new ConfigurationPropertyCollection();
		properties.Add(handlersProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpHandlersSection" /> class. </summary>
	public HttpHandlersSection()
	{
	}

	internal object LocateHandler(string verb, string filepath, out bool allowCache)
	{
		int count = Handlers.Count;
		for (int i = 0; i < count; i++)
		{
			HttpHandlerAction httpHandlerAction = Handlers[i];
			string[] verbs = httpHandlerAction.Verbs;
			if (verbs == null)
			{
				if (httpHandlerAction.PathMatches(filepath))
				{
					allowCache = httpHandlerAction.Path != "*";
					return httpHandlerAction.GetHandlerInstance();
				}
				continue;
			}
			int num = verbs.Length;
			while (num > 0)
			{
				num--;
				if (!(verbs[num] != verb) && httpHandlerAction.PathMatches(filepath))
				{
					allowCache = httpHandlerAction.Path != "*";
					return httpHandlerAction.GetHandlerInstance();
				}
			}
		}
		allowCache = false;
		return null;
	}
}
