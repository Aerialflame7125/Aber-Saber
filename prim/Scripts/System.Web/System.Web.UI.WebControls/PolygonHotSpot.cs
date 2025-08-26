using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Defines a polygon-shaped hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
public sealed class PolygonHotSpot : HotSpot
{
	protected internal override string MarkupName => "poly";

	/// <summary>A string of coordinates that represents the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
	/// <returns>A string that represents the coordinates of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object's vertexes.</returns>
	[DefaultValue("")]
	public string Coordinates
	{
		get
		{
			object obj = base.ViewState["Coordinates"];
			if (obj == null)
			{
				return string.Empty;
			}
			return (string)obj;
		}
		set
		{
			base.ViewState["Coordinates"] = value;
		}
	}

	/// <summary>Returns a string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
	/// <returns>A string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object. The default value is an empty string ("").</returns>
	public override string GetCoordinates()
	{
		return Coordinates;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> class.</summary>
	public PolygonHotSpot()
	{
	}
}
