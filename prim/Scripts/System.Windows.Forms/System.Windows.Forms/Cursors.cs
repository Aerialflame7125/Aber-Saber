namespace System.Windows.Forms;

/// <summary>Provides a collection of <see cref="T:System.Windows.Forms.Cursor" /> objects for use by a Windows Forms application.</summary>
/// <filterpriority>2</filterpriority>
public sealed class Cursors
{
	internal static Cursor app_starting;

	internal static Cursor arrow;

	internal static Cursor cross;

	internal static Cursor def;

	internal static Cursor hand;

	internal static Cursor help;

	internal static Cursor hsplit;

	internal static Cursor ibeam;

	internal static Cursor no;

	internal static Cursor no_move_2d;

	internal static Cursor no_move_horiz;

	internal static Cursor no_move_vert;

	internal static Cursor pan_east;

	internal static Cursor pan_ne;

	internal static Cursor pan_north;

	internal static Cursor pan_nw;

	internal static Cursor pan_se;

	internal static Cursor pan_south;

	internal static Cursor pan_sw;

	internal static Cursor pan_west;

	internal static Cursor size_all;

	internal static Cursor size_nesw;

	internal static Cursor size_ns;

	internal static Cursor size_nwse;

	internal static Cursor size_we;

	internal static Cursor up_arrow;

	internal static Cursor vsplit;

	internal static Cursor wait_cursor;

	/// <summary>Gets the cursor that appears when an application starts.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when an application starts.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor AppStarting
	{
		get
		{
			if (app_starting == null)
			{
				app_starting = new Cursor(StdCursor.AppStarting);
				app_starting.name = "AppStarting";
			}
			return app_starting;
		}
	}

	/// <summary>Gets the arrow cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the arrow cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Arrow
	{
		get
		{
			if (arrow == null)
			{
				arrow = new Cursor(StdCursor.Arrow);
				arrow.name = "Arrow";
			}
			return arrow;
		}
	}

	/// <summary>Gets the crosshair cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the crosshair cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Cross
	{
		get
		{
			if (cross == null)
			{
				cross = new Cursor(StdCursor.Cross);
				cross.name = "Cross";
			}
			return cross;
		}
	}

	/// <summary>Gets the default cursor, which is usually an arrow cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the default cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Default
	{
		get
		{
			if (def == null)
			{
				def = new Cursor(StdCursor.Default);
				def.name = "Default";
			}
			return def;
		}
	}

	/// <summary>Gets the hand cursor, typically used when hovering over a Web link.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the hand cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Hand
	{
		get
		{
			if (hand == null)
			{
				hand = new Cursor(StdCursor.Hand);
				hand.name = "Hand";
			}
			return hand;
		}
	}

	/// <summary>Gets the Help cursor, which is a combination of an arrow and a question mark.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the Help cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor Help
	{
		get
		{
			if (help == null)
			{
				help = new Cursor(StdCursor.Help);
				help.name = "Help";
			}
			return help;
		}
	}

	/// <summary>Gets the cursor that appears when the mouse is positioned over a horizontal splitter bar.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a horizontal splitter bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor HSplit
	{
		get
		{
			if (hsplit == null)
			{
				hsplit = new Cursor(typeof(Splitter), "SplitterNS.cur");
				hsplit.name = "HSplit";
			}
			return hsplit;
		}
	}

	/// <summary>Gets the I-beam cursor, which is used to show where the text cursor appears when the mouse is clicked.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the I-beam cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor IBeam
	{
		get
		{
			if (ibeam == null)
			{
				ibeam = new Cursor(StdCursor.IBeam);
				ibeam.name = "IBeam";
			}
			return ibeam;
		}
	}

	/// <summary>Gets the cursor that indicates that a particular region is invalid for the current operation.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that indicates that a particular region is invalid for the current operation.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor No
	{
		get
		{
			if (no == null)
			{
				no = new Cursor(StdCursor.No);
				no.name = "No";
			}
			return no;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in both a horizontal and vertical direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor NoMove2D
	{
		get
		{
			if (no_move_2d == null)
			{
				no_move_2d = new Cursor(StdCursor.NoMove2D);
				no_move_2d.name = "NoMove2D";
			}
			return no_move_2d;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a horizontal direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor NoMoveHoriz
	{
		get
		{
			if (no_move_horiz == null)
			{
				no_move_horiz = new Cursor(StdCursor.NoMoveHoriz);
				no_move_horiz.name = "NoMoveHoriz";
			}
			return no_move_horiz;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is not moving, but the window can be scrolled in a vertical direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is not moving.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor NoMoveVert
	{
		get
		{
			if (no_move_vert == null)
			{
				no_move_vert = new Cursor(StdCursor.NoMoveVert);
				no_move_vert.name = "NoMoveVert";
			}
			return no_move_vert;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the right.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanEast
	{
		get
		{
			if (pan_east == null)
			{
				pan_east = new Cursor(StdCursor.PanEast);
				pan_east.name = "PanEast";
			}
			return pan_east;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the right.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanNE
	{
		get
		{
			if (pan_ne == null)
			{
				pan_ne = new Cursor(StdCursor.PanNE);
				pan_ne.name = "PanNE";
			}
			return pan_ne;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in an upward direction.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanNorth
	{
		get
		{
			if (pan_north == null)
			{
				pan_north = new Cursor(StdCursor.PanNorth);
				pan_north.name = "PanNorth";
			}
			return pan_north;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically upward and to the left.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanNW
	{
		get
		{
			if (pan_nw == null)
			{
				pan_nw = new Cursor(StdCursor.PanNW);
				pan_nw.name = "PanNW";
			}
			return pan_nw;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the right.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanSE
	{
		get
		{
			if (pan_se == null)
			{
				pan_se = new Cursor(StdCursor.PanSE);
				pan_se.name = "PanSE";
			}
			return pan_se;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling vertically in a downward direction.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanSouth
	{
		get
		{
			if (pan_south == null)
			{
				pan_south = new Cursor(StdCursor.PanSouth);
				pan_south.name = "PanSouth";
			}
			return pan_south;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally and vertically downward and to the left.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanSW
	{
		get
		{
			if (pan_sw == null)
			{
				pan_sw = new Cursor(StdCursor.PanSW);
				pan_sw.name = "PanSW";
			}
			return pan_sw;
		}
	}

	/// <summary>Gets the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears during wheel operations when the mouse is moving and the window is scrolling horizontally to the left.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor PanWest
	{
		get
		{
			if (pan_west == null)
			{
				pan_west = new Cursor(StdCursor.PanWest);
				pan_west.name = "PanWest";
			}
			return pan_west;
		}
	}

	/// <summary>Gets the four-headed sizing cursor, which consists of four joined arrows that point north, south, east, and west.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the four-headed sizing cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor SizeAll
	{
		get
		{
			if (size_all == null)
			{
				size_all = new Cursor(StdCursor.SizeAll);
				size_all.name = "SizeAll";
			}
			return size_all;
		}
	}

	/// <summary>Gets the two-headed diagonal (northeast/southwest) sizing cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents two-headed diagonal (northeast/southwest) sizing cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor SizeNESW
	{
		get
		{
			if (size_nesw == null)
			{
				if (XplatUI.RunningOnUnix)
				{
					size_nesw = new Cursor(typeof(Cursor), "NESW.cur");
					size_nesw.name = "SizeNESW";
				}
				else
				{
					size_nesw = new Cursor(StdCursor.SizeNWSE);
					size_nesw.name = "SizeNESW";
				}
			}
			return size_nesw;
		}
	}

	/// <summary>Gets the two-headed vertical (north/south) sizing cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed vertical (north/south) sizing cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor SizeNS
	{
		get
		{
			if (size_ns == null)
			{
				size_ns = new Cursor(StdCursor.SizeNS);
				size_ns.name = "SizeNS";
			}
			return size_ns;
		}
	}

	/// <summary>Gets the two-headed diagonal (northwest/southeast) sizing cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed diagonal (northwest/southeast) sizing cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor SizeNWSE
	{
		get
		{
			if (size_nwse == null)
			{
				if (XplatUI.RunningOnUnix)
				{
					size_nwse = new Cursor(typeof(Cursor), "NWSE.cur");
					size_nwse.name = "SizeNWSE";
				}
				else
				{
					size_nwse = new Cursor(StdCursor.SizeNWSE);
					size_nwse.name = "SizeNWSE";
				}
			}
			return size_nwse;
		}
	}

	/// <summary>Gets the two-headed horizontal (west/east) sizing cursor.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the two-headed horizontal (west/east) sizing cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor SizeWE
	{
		get
		{
			if (size_we == null)
			{
				size_we = new Cursor(StdCursor.SizeWE);
				size_we.name = "SizeWE";
			}
			return size_we;
		}
	}

	/// <summary>Gets the up arrow cursor, typically used to identify an insertion point.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the up arrow cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor UpArrow
	{
		get
		{
			if (up_arrow == null)
			{
				up_arrow = new Cursor(StdCursor.UpArrow);
				up_arrow.name = "UpArrow";
			}
			return up_arrow;
		}
	}

	/// <summary>Gets the cursor that appears when the mouse is positioned over a vertical splitter bar.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor that appears when the mouse is positioned over a vertical splitter bar.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor VSplit
	{
		get
		{
			if (vsplit == null)
			{
				vsplit = new Cursor(typeof(Cursor), "SplitterWE.cur");
				vsplit.name = "VSplit";
			}
			return vsplit;
		}
	}

	/// <summary>Gets the wait cursor, typically an hourglass shape.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> that represents the wait cursor.</returns>
	/// <filterpriority>1</filterpriority>
	public static Cursor WaitCursor
	{
		get
		{
			if (wait_cursor == null)
			{
				wait_cursor = new Cursor(StdCursor.WaitCursor);
				wait_cursor.name = "WaitCursor";
			}
			return wait_cursor;
		}
	}

	private Cursors()
	{
	}
}
