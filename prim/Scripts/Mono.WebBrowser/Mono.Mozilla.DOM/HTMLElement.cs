using System.IO;
using System.Text;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla.DOM;

internal class HTMLElement : Element, IElement, INode
{
	protected new nsIDOMHTMLElement node
	{
		get
		{
			return base.node as nsIDOMHTMLElement;
		}
		set
		{
			base.node = value;
		}
	}

	public new string InnerHTML
	{
		get
		{
			if (!(node is nsIDOMNSHTMLElement nsIDOMNSHTMLElement))
			{
				return null;
			}
			nsIDOMNSHTMLElement.getInnerHTML(storage);
			return Base.StringGet(storage);
		}
		set
		{
			if (node is nsIDOMNSHTMLElement nsIDOMNSHTMLElement)
			{
				Base.StringSet(storage, value);
				nsIDOMNSHTMLElement.setInnerHTML(storage);
			}
		}
	}

	public override string OuterHTML
	{
		get
		{
			try
			{
				control.DocEncoder.Flags = DocumentEncoderFlags.OutputRaw;
				if (Equals(base.Owner.DocumentElement))
				{
					return control.DocEncoder.EncodeToString((Document)base.Owner);
				}
				return control.DocEncoder.EncodeToString(this);
			}
			catch
			{
				string tagName = TagName;
				string text = "<" + tagName;
				foreach (IAttribute attribute in Attributes)
				{
					text = text + " " + attribute.Name + "=\"" + attribute.Value + "\"";
				}
				(node as nsIDOMNSHTMLElement).getInnerHTML(storage);
				return text + ">" + Base.StringGet(storage) + "</" + tagName + ">";
			}
		}
		set
		{
			(((Document)control.Document).XPComObject as nsIDOMDocumentRange).createRange(out var ret);
			ret.setStartBefore(node);
			nsIDOMNSRange obj = ret as nsIDOMNSRange;
			Base.StringSet(storage, value);
			obj.createContextualFragment(storage, out var ret2);
			node.getParentNode(out var ret3);
			ret3 = nsDOMNode.GetProxy(control, ret3);
			ret3.replaceChild(ret2, node, out var ret4);
			node = ret4 as nsIDOMHTMLElement;
		}
	}

	public override System.IO.Stream ContentStream
	{
		get
		{
			try
			{
				control.DocEncoder.Flags = DocumentEncoderFlags.OutputRaw;
				if (Equals(base.Owner.DocumentElement))
				{
					return control.DocEncoder.EncodeToStream((Document)base.Owner);
				}
				return control.DocEncoder.EncodeToStream(this);
			}
			catch
			{
				string tagName = TagName;
				string text = "<" + tagName;
				foreach (IAttribute attribute in Attributes)
				{
					text = text + " " + attribute.Name + "=\"" + attribute.Value + "\"";
				}
				(node as nsIDOMNSHTMLElement).getInnerHTML(storage);
				text = text + ">" + Base.StringGet(storage) + "</" + tagName + ">";
				return new MemoryStream(Encoding.UTF8.GetBytes(text));
			}
		}
	}

	public override bool Disabled
	{
		get
		{
			if (HasAttribute("disabled"))
			{
				return bool.Parse(GetAttribute("disabled"));
			}
			return false;
		}
		set
		{
			if (HasAttribute("disabled"))
			{
				SetAttribute("disabled", value.ToString());
			}
		}
	}

	public override int TabIndex
	{
		get
		{
			((nsIDOMNSHTMLElement)node).getTabIndex(out var ret);
			return ret;
		}
		set
		{
			((nsIDOMNSHTMLElement)node).setTabIndex(value);
		}
	}

	public HTMLElement(WebBrowser control, nsIDOMHTMLElement domHtmlElement)
		: base(control, domHtmlElement)
	{
		node = domHtmlElement;
	}

	protected override void Dispose(bool disposing)
	{
		if (!disposed && disposing)
		{
			node = null;
		}
		base.Dispose(disposing);
	}

	public override int GetHashCode()
	{
		return hashcode;
	}
}
