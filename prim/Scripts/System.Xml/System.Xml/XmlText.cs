using System.Xml.XPath;

namespace System.Xml;

/// <summary>Represents the text content of an element or attribute.</summary>
public class XmlText : XmlCharacterData
{
	/// <summary>Gets the qualified name of the node.</summary>
	/// <returns>For text nodes, this property returns <see langword="#text" />.</returns>
	public override string Name => OwnerDocument.strTextName;

	/// <summary>Gets the local name of the node.</summary>
	/// <returns>For text nodes, this property returns <see langword="#text" />.</returns>
	public override string LocalName => OwnerDocument.strTextName;

	/// <summary>Gets the type of the current node.</summary>
	/// <returns>For text nodes, this value is XmlNodeType.Text.</returns>
	public override XmlNodeType NodeType => XmlNodeType.Text;

	/// <summary>Gets the parent of this node (for nodes that can have parents).</summary>
	/// <returns>The <see langword="XmlNode" /> that is the parent of the current node. If a node has just been created and not yet added to the tree, or if it has been removed from the tree, the parent is <see langword="null" />. For all other nodes, the value returned depends on the <see cref="P:System.Xml.XmlNode.NodeType" /> of the node. The following table describes the possible return values for the <see langword="ParentNode" /> property.</returns>
	public override XmlNode ParentNode
	{
		get
		{
			switch (parentNode.NodeType)
			{
			case XmlNodeType.Document:
				return null;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			{
				XmlNode xmlNode = parentNode.parentNode;
				while (xmlNode.IsText)
				{
					xmlNode = xmlNode.parentNode;
				}
				return xmlNode;
			}
			default:
				return parentNode;
			}
		}
	}

	/// <summary>Gets or sets the value of the node.</summary>
	/// <returns>The content of the text node.</returns>
	public override string Value
	{
		get
		{
			return Data;
		}
		set
		{
			Data = value;
			XmlNode xmlNode = parentNode;
			if (xmlNode != null && xmlNode.NodeType == XmlNodeType.Attribute && xmlNode is XmlUnspecifiedAttribute { Specified: false } xmlUnspecifiedAttribute)
			{
				xmlUnspecifiedAttribute.SetSpecified(f: true);
			}
		}
	}

	internal override XPathNodeType XPNodeType => XPathNodeType.Text;

	internal override bool IsText => true;

	/// <summary>Gets the text node that immediately precedes this node.</summary>
	/// <returns>Returns <see cref="T:System.Xml.XmlNode" />.</returns>
	public override XmlNode PreviousText
	{
		get
		{
			if (parentNode.IsText)
			{
				return parentNode;
			}
			return null;
		}
	}

	internal XmlText(string strData)
		: this(strData, null)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlText" /> class.</summary>
	/// <param name="strData">The content of the node; see the <see cref="P:System.Xml.XmlText.Value" /> property.</param>
	/// <param name="doc">The parent XML document.</param>
	protected internal XmlText(string strData, XmlDocument doc)
		: base(strData, doc)
	{
	}

	/// <summary>Creates a duplicate of this node.</summary>
	/// <param name="deep">
	///       <see langword="true" /> to recursively clone the subtree under the specified node; <see langword="false" /> to clone only the node itself. </param>
	/// <returns>The cloned node.</returns>
	public override XmlNode CloneNode(bool deep)
	{
		return OwnerDocument.CreateTextNode(Data);
	}

	/// <summary>Splits the node into two nodes at the specified offset, keeping both in the tree as siblings.</summary>
	/// <param name="offset">The offset at which to split the node. </param>
	/// <returns>The new node.</returns>
	public virtual XmlText SplitText(int offset)
	{
		XmlNode xmlNode = ParentNode;
		int length = Length;
		if (offset > length)
		{
			throw new ArgumentOutOfRangeException("offset");
		}
		if (xmlNode == null)
		{
			throw new InvalidOperationException(Res.GetString("The 'Text' node is not connected in the DOM live tree. No 'SplitText' operation could be performed."));
		}
		int count = length - offset;
		string text = Substring(offset, count);
		DeleteData(offset, count);
		XmlText xmlText = OwnerDocument.CreateTextNode(text);
		xmlNode.InsertAfter(xmlText, this);
		return xmlText;
	}

	/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
	/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
	public override void WriteTo(XmlWriter w)
	{
		w.WriteString(Data);
	}

	/// <summary>Saves all the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. <see langword="XmlText" /> nodes do not have children, so this method has no effect.</summary>
	/// <param name="w">The <see langword="XmlWriter" /> to which you want to save. </param>
	public override void WriteContentTo(XmlWriter w)
	{
	}
}
