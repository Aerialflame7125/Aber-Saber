using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace System.Web.UI;

/// <summary>Provides support for rapid application development (RAD) designers to parse data-binding expressions that use XPath expressions. This class cannot be inherited.</summary>
public sealed class XPathBinder
{
	private XPathBinder()
	{
	}

	/// <summary>Evaluates XPath data-binding expressions at run time.</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
	/// <param name="xPath">The XPath query from <paramref name="container" /> to the property value that is placed in the bound control property. </param>
	/// <returns>An <see cref="T:System.Object" /> that results from the evaluation of the data-binding expression.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" /> object.</exception>
	public static object Eval(object container, string xPath)
	{
		return Eval(container, xPath, (IXmlNamespaceResolver)null);
	}

	/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
	/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property.</param>
	/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
	/// <returns>A <see cref="T:System.Object" /> that results from the evaluation of the data-binding expression.</returns>
	public static object Eval(object container, string xPath, IXmlNamespaceResolver resolver)
	{
		if (xPath == null || xPath.Length == 0)
		{
			throw new ArgumentNullException("xPath");
		}
		object obj = ((container as IXPathNavigable) ?? throw new ArgumentException("container")).CreateNavigator().Evaluate(xPath, resolver);
		if (obj is XPathNodeIterator xPathNodeIterator)
		{
			if (xPathNodeIterator.MoveNext())
			{
				return xPathNodeIterator.Current.Value;
			}
			return null;
		}
		return obj;
	}

	/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser.</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
	/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property. </param>
	/// <param name="format">A .NET Framework format string, similar to those used by <see cref="M:System.String.Format(System.String,System.Object)" />, that converts the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object (which results from the evaluation of the data-binding expression) to a <see cref="T:System.String" /> that can be displayed by the requesting browser. </param>
	/// <returns>A <see cref="T:System.String" /> that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" />.</exception>
	public static string Eval(object container, string xPath, string format)
	{
		return Eval(container, xPath, format, null);
	}

	/// <summary>Evaluates XPath data-binding expressions at run time and formats the result as text to be displayed in the requesting browser, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression..</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
	/// <param name="xPath">The XPath query from the <paramref name="container" /> to the property value to be placed in the bound control property.</param>
	/// <param name="format">A .NET Framework format string, similar to those used by <see cref="M:System.String.Format(System.String,System.Object)" />, that converts the <see cref="T:System.Xml.XPath.IXPathNavigable" /> object (which results from the evaluation of the data-binding expression) to a <see cref="T:System.String" /> that can be displayed by the requesting browser.</param>
	/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
	/// <returns>A <see cref="T:System.String" /> that results from the evaluation of the data-binding expression and conversion to a string type.</returns>
	public static string Eval(object container, string xPath, string format, IXmlNamespaceResolver resolver)
	{
		object obj = Eval(container, xPath, resolver);
		if (obj == null)
		{
			return string.Empty;
		}
		if (format == null || format.Length == 0)
		{
			return obj.ToString();
		}
		return string.Format(format, obj);
	}

	/// <summary>Uses an XPath data-binding expression at run time to return a list of nodes.</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language. </param>
	/// <param name="xPath">The XPath query that retrieves a list of nodes. </param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="container" /> or <paramref name="xpath" /> parameter is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The object specified by <paramref name="container" /> is not an <see cref="T:System.Xml.XPath.IXPathNavigable" />. </exception>
	/// <exception cref="T:System.InvalidOperationException">The current node of the <see cref="T:System.Xml.XPath.XPathNodeIterator" /> does not have an associated XML node.</exception>
	public static IEnumerable Select(object container, string xPath)
	{
		return Select(container, xPath, null);
	}

	/// <summary>Uses an XPath data-binding expression at run time to return a list of nodes, using the <see cref="T:System.Xml.IXmlNamespaceResolver" /> object specified to resolve namespace prefixes in the XPath expression.</summary>
	/// <param name="container">The <see cref="T:System.Xml.XPath.IXPathNavigable" /> object reference that the expression is evaluated against. This must be a valid object identifier in the page's specified language.</param>
	/// <param name="xPath">The XPath query that retrieves a list of nodes.</param>
	/// <param name="resolver">The <see cref="T:System.Xml.IXmlNamespaceResolver" /> object used to resolve namespace prefixes in the XPath expression.</param>
	/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of nodes.</returns>
	public static IEnumerable Select(object container, string xPath, IXmlNamespaceResolver resolver)
	{
		if (xPath == null || xPath.Length == 0)
		{
			throw new ArgumentNullException("xPath");
		}
		XPathNodeIterator xPathNodeIterator = ((container as IXPathNavigable) ?? throw new ArgumentException("container")).CreateNavigator().Select(xPath, resolver);
		ArrayList arrayList = new ArrayList();
		while (xPathNodeIterator.MoveNext())
		{
			if (!(xPathNodeIterator.Current is IHasXmlNode hasXmlNode))
			{
				throw new InvalidOperationException();
			}
			arrayList.Add(hasXmlNode.GetNode());
		}
		return arrayList;
	}
}
