using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Provides data for the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
/// <filterpriority>2</filterpriority>
[ComVisible(true)]
public class LinkLabelLinkClickedEventArgs : EventArgs
{
	private MouseButtons button;

	private LinkLabel.Link link;

	/// <summary>Gets the mouse button that causes the link to be clicked.</summary>
	/// <returns>One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</returns>
	public MouseButtons Button => button;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked.</summary>
	/// <returns>A link on the <see cref="T:System.Windows.Forms.LinkLabel" />.</returns>
	/// <filterpriority>1</filterpriority>
	public LinkLabel.Link Link => link;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link.</summary>
	/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
	public LinkLabelLinkClickedEventArgs(LinkLabel.Link link)
	{
		button = MouseButtons.Left;
		this.link = link;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> class with the specified link and the specified mouse button.</summary>
	/// <param name="link">The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that was clicked. </param>
	/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values.</param>
	public LinkLabelLinkClickedEventArgs(LinkLabel.Link link, MouseButtons button)
	{
		this.button = button;
		this.link = link;
	}
}
