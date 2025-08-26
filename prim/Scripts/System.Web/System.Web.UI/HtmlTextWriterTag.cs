namespace System.Web.UI;

/// <summary>Specifies the HTML tags that can be passed to an <see cref="T:System.Web.UI.HtmlTextWriter" /> or <see cref="T:System.Web.UI.Html32TextWriter" /> object output stream.</summary>
public enum HtmlTextWriterTag
{
	/// <summary>The string passed as an HTML tag is not recognized. </summary>
	Unknown,
	/// <summary>The HTML <see langword="a" /> element. </summary>
	A,
	/// <summary>The HTML <see langword="acronym" /> element. </summary>
	Acronym,
	/// <summary>The HTML <see langword="address" /> element. </summary>
	Address,
	/// <summary>The HTML <see langword="area" /> element. </summary>
	Area,
	/// <summary>The HTML <see langword="b" /> element. </summary>
	B,
	/// <summary>The HTML <see langword="base" /> element. </summary>
	Base,
	/// <summary>The HTML <see langword="basefont" /> element. </summary>
	Basefont,
	/// <summary>The HTML <see langword="bdo" /> element. </summary>
	Bdo,
	/// <summary>The HTML <see langword="bgsound" /> element. </summary>
	Bgsound,
	/// <summary>The HTML <see langword="big" /> element. </summary>
	Big,
	/// <summary>The HTML <see langword="blockquote" /> element. </summary>
	Blockquote,
	/// <summary>The HTML <see langword="body" /> element. </summary>
	Body,
	/// <summary>The HTML <see langword="br" /> element. </summary>
	Br,
	/// <summary>The HTML <see langword="button" /> element. </summary>
	Button,
	/// <summary>The HTML <see langword="caption" /> element. </summary>
	Caption,
	/// <summary>The HTML <see langword="center" /> element. </summary>
	Center,
	/// <summary>The HTML <see langword="cite" /> element. </summary>
	Cite,
	/// <summary>The HTML <see langword="code" /> element. </summary>
	Code,
	/// <summary>The HTML <see langword="col" /> element. </summary>
	Col,
	/// <summary>The HTML <see langword="colgroup" /> element. </summary>
	Colgroup,
	/// <summary>The HTML <see langword="dd" /> element. </summary>
	Dd,
	/// <summary>The HTML <see langword="del" /> element. </summary>
	Del,
	/// <summary>The HTML <see langword="dfn" /> element. </summary>
	Dfn,
	/// <summary>The HTML <see langword="dir" /> element. </summary>
	Dir,
	/// <summary>The HTML <see langword="div" /> element. </summary>
	Div,
	/// <summary>The HTML <see langword="dl" /> element. </summary>
	Dl,
	/// <summary>The HTML <see langword="dt" /> element. </summary>
	Dt,
	/// <summary>The HTML <see langword="em" /> element. </summary>
	Em,
	/// <summary>The HTML <see langword="embed" /> element. </summary>
	Embed,
	/// <summary>The HTML <see langword="fieldset" /> element. </summary>
	Fieldset,
	/// <summary>The HTML <see langword="font" /> element. </summary>
	Font,
	/// <summary>The HTML <see langword="form" /> element. </summary>
	Form,
	/// <summary>The HTML <see langword="frame" /> element. </summary>
	Frame,
	/// <summary>The HTML <see langword="frameset" /> element. </summary>
	Frameset,
	/// <summary>The HTML <see langword="H1" /> element. </summary>
	H1,
	/// <summary>The HTML <see langword="H2" /> element. </summary>
	H2,
	/// <summary>The HTML <see langword="H3" /> element. </summary>
	H3,
	/// <summary>The HTML <see langword="H4" /> element. </summary>
	H4,
	/// <summary>The HTML <see langword="H5" /> element. </summary>
	H5,
	/// <summary>The HTML <see langword="H6" /> element. </summary>
	H6,
	/// <summary>The HTML <see langword="head" /> element. </summary>
	Head,
	/// <summary>The HTML <see langword="hr" /> element. </summary>
	Hr,
	/// <summary>The HTML <see langword="html" /> element. </summary>
	Html,
	/// <summary>The HTML <see langword="i" /> element. </summary>
	I,
	/// <summary>The HTML <see langword="iframe" /> element. </summary>
	Iframe,
	/// <summary>The HTML <see langword="img" /> element. </summary>
	Img,
	/// <summary>The HTML <see langword="input" /> element. </summary>
	Input,
	/// <summary>The HTML <see langword="ins" /> element. </summary>
	Ins,
	/// <summary>The HTML <see langword="isindex" /> element. </summary>
	Isindex,
	/// <summary>The HTML <see langword="kbd" /> element. </summary>
	Kbd,
	/// <summary>The HTML <see langword="label" /> element. </summary>
	Label,
	/// <summary>The HTML <see langword="legend" /> element. </summary>
	Legend,
	/// <summary>The HTML <see langword="li" /> element. </summary>
	Li,
	/// <summary>The HTML <see langword="link" /> element. </summary>
	Link,
	/// <summary>The HTML <see langword="map" /> element. </summary>
	Map,
	/// <summary>The HTML <see langword="marquee" /> element. </summary>
	Marquee,
	/// <summary>The HTML <see langword="menu" /> element. </summary>
	Menu,
	/// <summary>The HTML <see langword="meta" /> element. </summary>
	Meta,
	/// <summary>The HTML <see langword="nobr" /> element. </summary>
	Nobr,
	/// <summary>The HTML <see langword="noframes" /> element. </summary>
	Noframes,
	/// <summary>The HTML <see langword="noscript" /> element. </summary>
	Noscript,
	/// <summary>The HTML <see langword="object" /> element. </summary>
	Object,
	/// <summary>The HTML <see langword="ol" /> element. </summary>
	Ol,
	/// <summary>The HTML <see langword="option" /> element. </summary>
	Option,
	/// <summary>The HTML <see langword="p" /> element. </summary>
	P,
	/// <summary>The HTML <see langword="param" /> element. </summary>
	Param,
	/// <summary>The HTML <see langword="pre" /> element. </summary>
	Pre,
	/// <summary>The HTML <see langword="q" /> element. </summary>
	Q,
	/// <summary>The DHTML <see langword="rt" /> element, which specifies text for the <see langword="ruby" /> element. </summary>
	Rt,
	/// <summary>The DHTML <see langword="ruby" /> element. </summary>
	Ruby,
	/// <summary>The HTML <see langword="s" /> element. </summary>
	S,
	/// <summary>The HTML <see langword="samp" /> element. </summary>
	Samp,
	/// <summary>The HTML <see langword="script" /> element. </summary>
	Script,
	/// <summary>The HTML <see langword="select" /> element. </summary>
	Select,
	/// <summary>The HTML <see langword="small" /> element. </summary>
	Small,
	/// <summary>The HTML <see langword="span" /> element. </summary>
	Span,
	/// <summary>The HTML <see langword="strike" /> element. </summary>
	Strike,
	/// <summary>The HTML <see langword="strong" /> element. </summary>
	Strong,
	/// <summary>The HTML <see langword="style" /> element. </summary>
	Style,
	/// <summary>The HTML <see langword="sub" /> element. </summary>
	Sub,
	/// <summary>The HTML <see langword="sup" /> element. </summary>
	Sup,
	/// <summary>The HTML <see langword="table" /> element. </summary>
	Table,
	/// <summary>The HTML <see langword="tbody" /> element. </summary>
	Tbody,
	/// <summary>The HTML <see langword="td" /> element. </summary>
	Td,
	/// <summary>The HTML <see langword="textarea" /> element. </summary>
	Textarea,
	/// <summary>The HTML <see langword="tfoot" /> element. </summary>
	Tfoot,
	/// <summary>The HTML <see langword="th" /> element. </summary>
	Th,
	/// <summary>The HTML <see langword="thead" /> element. </summary>
	Thead,
	/// <summary>The HTML <see langword="title" /> element. </summary>
	Title,
	/// <summary>The HTML <see langword="tr" /> element. </summary>
	Tr,
	/// <summary>The HTML <see langword="tt" /> element. </summary>
	Tt,
	/// <summary>The HTML <see langword="u" /> element. </summary>
	U,
	/// <summary>The HTML <see langword="ul" /> element. </summary>
	Ul,
	/// <summary>The HTML <see langword="var" /> element. </summary>
	Var,
	/// <summary>The HTML <see langword="wbr" /> element. </summary>
	Wbr,
	/// <summary>The HTML <see langword="xml" /> element. </summary>
	Xml
}
