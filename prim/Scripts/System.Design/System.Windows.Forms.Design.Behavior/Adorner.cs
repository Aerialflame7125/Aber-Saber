using System.Drawing;

namespace System.Windows.Forms.Design.Behavior;

/// <summary>Manages a collection of user-interface related <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects. This class cannot be inherited.</summary>
public sealed class Adorner
{
	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> associated with the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" />.</returns>
	[System.MonoTODO]
	public BehaviorService BehaviorService
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is enabled.</summary>
	/// <returns>
	///   <see langword="true" />, if the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> is enabled; otherwise, <see langword="false" />.</returns>
	[System.MonoTODO]
	public bool Enabled
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> collection.</summary>
	/// <returns>A collection of <see cref="T:System.Windows.Forms.Design.Behavior.Glyph" /> objects.</returns>
	[System.MonoTODO]
	public GlyphCollection Glyphs
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.Behavior.Adorner" /> class.</summary>
	public Adorner()
	{
	}

	/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window.</summary>
	[System.MonoTODO]
	public void Invalidate()
	{
		throw new NotImplementedException();
	}

	/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window within the given <see cref="T:System.Drawing.Rectangle" />.</summary>
	/// <param name="rectangle">The area to invalidate.</param>
	[System.MonoTODO]
	public void Invalidate(Rectangle rectangle)
	{
		throw new NotImplementedException();
	}

	/// <summary>Forces the <see cref="T:System.Windows.Forms.Design.Behavior.BehaviorService" /> to refresh its adorner window within the given <see cref="T:System.Drawing.Region" />.</summary>
	/// <param name="region">The <see cref="T:System.Drawing.Region" /> to invalidate.</param>
	[System.MonoTODO]
	public void Invalidate(Region region)
	{
		throw new NotImplementedException();
	}
}
