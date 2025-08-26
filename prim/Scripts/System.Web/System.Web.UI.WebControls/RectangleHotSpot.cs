using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Defines a rectangular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
public sealed class RectangleHotSpot : HotSpot
{
	protected internal override string MarkupName => "rect";

	/// <summary>Gets or sets the x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
	/// <returns>The x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int Left
	{
		get
		{
			object obj = base.ViewState["Left"];
			if (obj == null)
			{
				return 0;
			}
			return (int)obj;
		}
		set
		{
			base.ViewState["Left"] = value;
		}
	}

	/// <summary>Gets or sets the y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
	/// <returns>The y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int Top
	{
		get
		{
			object obj = base.ViewState["Top"];
			if (obj == null)
			{
				return 0;
			}
			return (int)obj;
		}
		set
		{
			base.ViewState["Top"] = value;
		}
	}

	/// <summary>Gets or sets the x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
	/// <returns>The x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int Right
	{
		get
		{
			object obj = base.ViewState["Right"];
			if (obj == null)
			{
				return 0;
			}
			return (int)obj;
		}
		set
		{
			base.ViewState["Right"] = value;
		}
	}

	/// <summary>Gets or sets the y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
	/// <returns>The y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int Bottom
	{
		get
		{
			object obj = base.ViewState["Bottom"];
			if (obj == null)
			{
				return 0;
			}
			return (int)obj;
		}
		set
		{
			base.ViewState["Bottom"] = value;
		}
	}

	/// <summary>Returns a string that represents the x -and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</summary>
	/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</returns>
	public override string GetCoordinates()
	{
		return Left + "," + Top + "," + Right + "," + Bottom;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> class.</summary>
	public RectangleHotSpot()
	{
	}
}
