using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design;

internal class SelectionFrame
{
	private enum GrabHandle
	{
		None = -1,
		TopLeft,
		TopMiddle,
		TopRight,
		Right,
		BottomRight,
		BottomMiddle,
		BottomLeft,
		Left,
		Border
	}

	private Rectangle _bounds;

	private Control _control;

	private Rectangle[] _handles = new Rectangle[8];

	private GrabHandle _handle = GrabHandle.None;

	private const int BORDER_SIZE = 7;

	private bool _resizing;

	public Rectangle Bounds
	{
		get
		{
			_bounds.X = _control.Location.X - 7;
			_bounds.Y = _control.Location.Y - 7;
			_bounds.Width = _control.Width + 14;
			_bounds.Height = _control.Height + 14;
			return _bounds;
		}
		set
		{
			_bounds = value;
			_control.Bounds = _bounds;
		}
	}

	private SelectionRules SelectionRules
	{
		get
		{
			SelectionRules result = SelectionRules.AllSizeable;
			if (_control.Site != null && _control.Site.GetService(typeof(IDesignerHost)) is IDesignerHost designerHost && designerHost.GetDesigner(_control) is ControlDesigner controlDesigner)
			{
				result = controlDesigner.SelectionRules;
			}
			return result;
		}
	}

	public Control Control
	{
		get
		{
			return _control;
		}
		set
		{
			if (value != null)
			{
				_control = value;
			}
		}
	}

	public Control Parent
	{
		get
		{
			if (_control.Parent == null)
			{
				return _control;
			}
			return _control.Parent;
		}
	}

	private GrabHandle GrabHandleSelected
	{
		get
		{
			return _handle;
		}
		set
		{
			_handle = value;
		}
	}

	private bool PrimarySelection
	{
		get
		{
			bool result = false;
			if (Control != null && Control.Site != null && Control.Site.GetService(typeof(ISelectionService)) is ISelectionService selectionService && selectionService.PrimarySelection == Control)
			{
				result = true;
			}
			return result;
		}
	}

	public SelectionFrame(Control control)
	{
		if (control == null)
		{
			throw new ArgumentNullException("control");
		}
		_control = control;
	}

	public void OnPaint(Graphics gfx)
	{
		DrawFrame(gfx);
		DrawGrabHandles(gfx);
	}

	private void DrawGrabHandles(Graphics gfx)
	{
		GraphicsState gstate = gfx.Save();
		gfx.TranslateTransform(Bounds.X, Bounds.Y);
		for (int i = 0; i < _handles.Length; i++)
		{
			_handles[i].Width = 7;
			_handles[i].Height = 7;
		}
		SelectionRules selectionRules = SelectionRules;
		bool primarySelection = PrimarySelection;
		bool enabled = false;
		_handles[0].Location = new Point(0, 0);
		if (CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable | SelectionRules.TopSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[0], primarySelection, enabled);
		enabled = false;
		_handles[1].Location = new Point((Bounds.Width - 7) / 2, 0);
		if (CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[1], primarySelection, enabled);
		enabled = false;
		_handles[2].Location = new Point(Bounds.Width - 7, 0);
		if (CheckSelectionRules(selectionRules, SelectionRules.RightSizeable | SelectionRules.TopSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[2], primarySelection, enabled);
		enabled = false;
		_handles[3].Location = new Point(Bounds.Width - 7, (Bounds.Height - 7) / 2);
		if (CheckSelectionRules(selectionRules, SelectionRules.RightSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[3], primarySelection, enabled);
		enabled = false;
		_handles[4].Location = new Point(Bounds.Width - 7, Bounds.Height - 7);
		if (CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.RightSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[4], primarySelection, enabled);
		enabled = false;
		_handles[5].Location = new Point((Bounds.Width - 7) / 2, Bounds.Height - 7);
		if (CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[5], primarySelection, enabled);
		enabled = false;
		_handles[6].Location = new Point(0, Bounds.Height - 7);
		if (CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.LeftSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[6], primarySelection, enabled);
		enabled = false;
		_handles[7].Location = new Point(0, (Bounds.Height - 7) / 2);
		if (CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
		{
			enabled = true;
		}
		ControlPaint.DrawGrabHandle(gfx, _handles[7], primarySelection, enabled);
		gfx.Restore(gstate);
	}

	protected void DrawFrame(Graphics gfx)
	{
		Color foreColor = Color.FromArgb((byte)(~_control.Parent.BackColor.R), (byte)(~_control.Parent.BackColor.G), (byte)(~_control.Parent.BackColor.B));
		Pen pen = new Pen(new HatchBrush(HatchStyle.Percent30, foreColor, Color.FromArgb(0)), 7f);
		gfx.DrawRectangle(pen, Control.Bounds);
	}

	public bool SetCursor(int x, int y)
	{
		bool result = false;
		if (!_resizing)
		{
			GrabHandle grabHandle = PointToGrabHandle(PointToClient(Control.MousePosition));
			if (grabHandle != GrabHandle.None)
			{
				result = true;
			}
			switch (grabHandle)
			{
			case GrabHandle.TopLeft:
				Cursor.Current = Cursors.SizeNWSE;
				break;
			case GrabHandle.TopMiddle:
				Cursor.Current = Cursors.SizeNS;
				break;
			case GrabHandle.TopRight:
				Cursor.Current = Cursors.SizeNESW;
				break;
			case GrabHandle.Right:
				Cursor.Current = Cursors.SizeWE;
				break;
			case GrabHandle.BottomRight:
				Cursor.Current = Cursors.SizeNWSE;
				break;
			case GrabHandle.BottomMiddle:
				Cursor.Current = Cursors.SizeNS;
				break;
			case GrabHandle.BottomLeft:
				Cursor.Current = Cursors.SizeNESW;
				break;
			case GrabHandle.Left:
				Cursor.Current = Cursors.SizeWE;
				break;
			default:
				Cursor.Current = Cursors.Default;
				break;
			}
		}
		return result;
	}

	public void ResizeBegin(int x, int y)
	{
		GrabHandleSelected = PointToGrabHandle(PointToClient(Parent.PointToScreen(new Point(x, y))));
		if (GrabHandleSelected != GrabHandle.None)
		{
			_resizing = true;
		}
	}

	private bool CheckSelectionRules(SelectionRules rules, SelectionRules toCheck)
	{
		return (rules & toCheck) == toCheck;
	}

	public Rectangle ResizeContinue(int x, int y)
	{
		Rectangle rectangle = (Rectangle)TypeDescriptor.GetProperties(_control)["Bounds"].GetValue(_control);
		Rectangle result = rectangle;
		Point point = new Point(x, y);
		SelectionRules selectionRules = SelectionRules;
		int num = 0;
		if (_resizing && GrabHandleSelected != GrabHandle.None && selectionRules != SelectionRules.Locked)
		{
			if (GrabHandleSelected == GrabHandle.TopLeft && CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable | SelectionRules.TopSizeable))
			{
				int y2 = _control.Top;
				int height = _control.Height;
				int left = _control.Left;
				num = _control.Width;
				if (point.Y < _control.Bottom)
				{
					y2 = point.Y;
					height = _control.Bottom - point.Y;
				}
				if (point.X < _control.Right)
				{
					left = point.X;
					num = _control.Right - point.X;
					rectangle = new Rectangle(left, y2, num, height);
				}
			}
			else if (GrabHandleSelected == GrabHandle.TopRight && CheckSelectionRules(selectionRules, SelectionRules.RightSizeable | SelectionRules.TopSizeable))
			{
				int y2 = _control.Top;
				int height = _control.Height;
				num = _control.Width;
				if (point.Y < _control.Bottom)
				{
					y2 = point.Y;
					height = _control.Bottom - point.Y;
				}
				num = point.X - _control.Left;
				rectangle = new Rectangle(_control.Left, y2, num, height);
			}
			else if (GrabHandleSelected == GrabHandle.TopMiddle && CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				if (point.Y < _control.Bottom)
				{
					int y2 = point.Y;
					int height = _control.Bottom - point.Y;
					rectangle = new Rectangle(_control.Left, y2, _control.Width, height);
				}
			}
			else if (GrabHandleSelected == GrabHandle.Right && CheckSelectionRules(selectionRules, SelectionRules.RightSizeable))
			{
				num = point.X - _control.Left;
				rectangle = new Rectangle(_control.Left, _control.Top, num, _control.Height);
			}
			else if (GrabHandleSelected == GrabHandle.BottomRight && CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.RightSizeable))
			{
				num = point.X - _control.Left;
				int height = point.Y - _control.Top;
				rectangle = new Rectangle(_control.Left, _control.Top, num, height);
			}
			else if (GrabHandleSelected == GrabHandle.BottomMiddle && CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable))
			{
				int height = point.Y - _control.Top;
				rectangle = new Rectangle(_control.Left, _control.Top, _control.Width, height);
			}
			else if (GrabHandleSelected == GrabHandle.BottomLeft && CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable | SelectionRules.LeftSizeable))
			{
				int height = _control.Height;
				int left = _control.Left;
				num = _control.Width;
				if (point.X < _control.Right)
				{
					left = point.X;
					num = _control.Right - point.X;
				}
				height = point.Y - _control.Top;
				rectangle = new Rectangle(left, _control.Top, num, height);
			}
			else if (GrabHandleSelected == GrabHandle.Left && CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable) && point.X < _control.Right)
			{
				int left = point.X;
				num = _control.Right - point.X;
				rectangle = new Rectangle(left, _control.Top, num, _control.Height);
			}
			TypeDescriptor.GetProperties(_control)["Bounds"].SetValue(_control, rectangle);
		}
		Parent.Refresh();
		result.X = rectangle.X - result.X;
		result.Y = rectangle.Y - result.Y;
		result.Height = rectangle.Height - result.Height;
		result.Width = rectangle.Width - result.Width;
		return result;
	}

	public void ResizeEnd(bool cancel)
	{
		GrabHandleSelected = GrabHandle.None;
		_resizing = false;
	}

	public void Resize(Rectangle deltaBounds)
	{
		SelectionRules selectionRules = SelectionRules;
		if (!CheckSelectionRules(selectionRules, SelectionRules.Locked) && CheckSelectionRules(selectionRules, SelectionRules.Moveable))
		{
			Rectangle rectangle = (Rectangle)TypeDescriptor.GetProperties(_control)["Bounds"].GetValue(_control);
			if (CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
			{
				rectangle.X += deltaBounds.X;
				rectangle.Width += deltaBounds.Width;
			}
			if (CheckSelectionRules(selectionRules, SelectionRules.RightSizeable) && !CheckSelectionRules(selectionRules, SelectionRules.LeftSizeable))
			{
				rectangle.Y += deltaBounds.Y;
				rectangle.Width += deltaBounds.Width;
			}
			if (CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				rectangle.Y += deltaBounds.Y;
				rectangle.Height += deltaBounds.Height;
			}
			if (CheckSelectionRules(selectionRules, SelectionRules.BottomSizeable) && !CheckSelectionRules(selectionRules, SelectionRules.TopSizeable))
			{
				rectangle.Height += deltaBounds.Height;
			}
			TypeDescriptor.GetProperties(_control)["Bounds"].SetValue(_control, rectangle);
		}
	}

	public bool HitTest(int x, int y)
	{
		if (PointToGrabHandle(PointToClient(Parent.PointToScreen(new Point(x, y)))) != GrabHandle.None)
		{
			return true;
		}
		return false;
	}

	private GrabHandle PointToGrabHandle(Point pointerLocation)
	{
		GrabHandle grabHandle = GrabHandle.None;
		if (IsCursorOnGrabHandle(pointerLocation, _handles[0]))
		{
			return GrabHandle.TopLeft;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[1]))
		{
			return GrabHandle.TopMiddle;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[2]))
		{
			return GrabHandle.TopRight;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[3]))
		{
			return GrabHandle.Right;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[4]))
		{
			return GrabHandle.BottomRight;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[5]))
		{
			return GrabHandle.BottomMiddle;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[6]))
		{
			return GrabHandle.BottomLeft;
		}
		if (IsCursorOnGrabHandle(pointerLocation, _handles[7]))
		{
			return GrabHandle.Left;
		}
		return GrabHandle.None;
	}

	private bool IsCursorOnGrabHandle(Point pointerLocation, Rectangle handleRectangle)
	{
		if (pointerLocation.X >= handleRectangle.X && pointerLocation.X <= handleRectangle.X + handleRectangle.Width && pointerLocation.Y >= handleRectangle.Y && pointerLocation.Y <= handleRectangle.Y + handleRectangle.Height)
		{
			return true;
		}
		return false;
	}

	private Point PointToClient(Point screenPoint)
	{
		Point result = Parent.PointToClient(screenPoint);
		result.X -= Bounds.X;
		result.Y -= Bounds.Y;
		return result;
	}
}
