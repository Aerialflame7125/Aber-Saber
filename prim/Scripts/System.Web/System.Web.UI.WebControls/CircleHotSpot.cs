using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Defines a circular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
public sealed class CircleHotSpot : HotSpot
{
	protected internal override string MarkupName => "circle";

	/// <summary>Gets or sets the distance from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
	/// <returns>An integer that represents the distance in pixels from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 0. </exception>
	[DefaultValue(0)]
	public int Radius
	{
		get
		{
			return base.ViewState.GetInt("Radius", 0);
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			base.ViewState["Radius"] = value;
		}
	}

	/// <summary>Gets or sets the x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
	/// <returns>The x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int X
	{
		get
		{
			return base.ViewState.GetInt("X", 0);
		}
		set
		{
			base.ViewState["X"] = value;
		}
	}

	/// <summary>Gets or sets the y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
	/// <returns>The y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
	[DefaultValue(0)]
	public int Y
	{
		get
		{
			return base.ViewState.GetInt("Y", 0);
		}
		set
		{
			base.ViewState["Y"] = value;
		}
	}

	/// <summary>Returns a string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</summary>
	/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</returns>
	public override string GetCoordinates()
	{
		return X + "," + Y + "," + Radius;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> class.</summary>
	public CircleHotSpot()
	{
	}
}
