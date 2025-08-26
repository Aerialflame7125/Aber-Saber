using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design;

internal class UISelectionService : IUISelectionService
{
	private IServiceProvider _serviceProvider;

	private DesignerTransaction _transaction;

	private ISelectionService _selectionService;

	private bool _dragging;

	private Point _prevMousePosition;

	private bool _firstMove;

	private bool _selecting;

	private Control _selectionContainer;

	private Point _initialMousePosition;

	private Rectangle _selectionRectangle;

	private ArrayList _selectionFrames = new ArrayList();

	private SelectionFrame _selectionFrame;

	private bool _resizing;

	private ISelectionService SelectionService => _selectionService;

	public bool SelectionInProgress => _selecting;

	public bool DragDropInProgress => _dragging;

	public bool ResizeInProgress => _resizing;

	public Rectangle SelectionBounds => _selectionRectangle;

	public UISelectionService(IServiceProvider serviceProvider)
	{
		if (serviceProvider == null)
		{
			throw new ArgumentNullException("serviceProvider");
		}
		_serviceProvider = serviceProvider;
		_transaction = null;
		_selectionService = serviceProvider.GetService(typeof(ISelectionService)) as ISelectionService;
		if (_selectionService == null)
		{
			IServiceContainer serviceContainer = serviceProvider.GetService(typeof(IServiceContainer)) as IServiceContainer;
			_selectionService = new SelectionService(serviceContainer);
			serviceContainer.AddService(typeof(ISelectionService), _selectionService);
		}
		_selectionService.SelectionChanged += OnSelectionChanged;
	}

	private object GetService(Type service)
	{
		return _serviceProvider.GetService(service);
	}

	public bool SetCursor(int x, int y)
	{
		bool result = false;
		SelectionFrame selectionFrameAt = GetSelectionFrameAt(x, y);
		if (selectionFrameAt != null && selectionFrameAt.HitTest(x, y) && selectionFrameAt.SetCursor(x, y))
		{
			result = true;
		}
		return result;
	}

	public void MouseDragBegin(Control container, int x, int y)
	{
		SelectionFrame selectionFrameAt = GetSelectionFrameAt(x, y);
		if (selectionFrameAt != null && selectionFrameAt.HitTest(x, y))
		{
			SelectionService.SetSelectedComponents(new IComponent[1] { selectionFrameAt.Control });
			if (_transaction == null)
			{
				IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
				_transaction = designerHost.CreateTransaction("Resize " + ((SelectionService.SelectionCount == 1) ? ((IComponent)SelectionService.PrimarySelection).Site.Name : "controls"));
			}
			ResizeBegin(x, y);
		}
		else
		{
			SelectionBegin(container, x, y);
		}
	}

	public void MouseDragMove(int x, int y)
	{
		if (_selecting)
		{
			SelectionContinue(x, y);
		}
		else if (_resizing)
		{
			ResizeContinue(x, y);
		}
	}

	public void MouseDragEnd(bool cancel)
	{
		if (_selecting)
		{
			SelectionEnd(cancel);
		}
		else if (_resizing)
		{
			ResizeEnd(cancel);
			if (_transaction != null)
			{
				if (cancel)
				{
					_transaction.Cancel();
				}
				else
				{
					_transaction.Commit();
				}
				_transaction = null;
			}
		}
		if (Cursor.Current != Cursors.Default)
		{
			Cursor.Current = Cursors.Default;
		}
	}

	public void DragBegin()
	{
		if (_transaction == null)
		{
			IDesignerHost designerHost = GetService(typeof(IDesignerHost)) as IDesignerHost;
			_transaction = designerHost.CreateTransaction("Move " + ((SelectionService.SelectionCount == 1) ? ((IComponent)SelectionService.PrimarySelection).Site.Name : "controls"));
		}
		_dragging = true;
		_firstMove = true;
		if (SelectionService.PrimarySelection != null)
		{
			((Control)SelectionService.PrimarySelection).DoDragDrop(new ControlDataObject((Control)SelectionService.PrimarySelection), DragDropEffects.All);
		}
	}

	public void DragOver(Control container, int x, int y)
	{
		if (!_dragging)
		{
			return;
		}
		if (_firstMove)
		{
			_prevMousePosition = new Point(x, y);
			_firstMove = false;
			return;
		}
		int dx = x - _prevMousePosition.X;
		int dy = y - _prevMousePosition.Y;
		MoveSelection(container, dx, dy);
		_prevMousePosition = new Point(x, y);
		if (GetService(typeof(IDesignerHost)) is IDesignerHost { RootComponent: not null } designerHost)
		{
			((Control)designerHost.RootComponent).Refresh();
		}
	}

	public void DragDrop(bool cancel, Control container, int x, int y)
	{
		if (!_dragging)
		{
			return;
		}
		int dx = x - _prevMousePosition.X;
		int dy = y - _prevMousePosition.Y;
		MoveSelection(container, dx, dy);
		_dragging = false;
		if (GetService(typeof(IDesignerHost)) is IDesignerHost { RootComponent: not null } designerHost)
		{
			((Control)designerHost.RootComponent).Refresh();
		}
		Native.SendMessage(((Control)SelectionService.PrimarySelection).Handle, Native.Msg.WM_LBUTTONUP, (IntPtr)0, (IntPtr)0);
		if (_transaction != null)
		{
			if (cancel)
			{
				_transaction.Cancel();
			}
			else
			{
				_transaction.Commit();
			}
			_transaction = null;
		}
	}

	private void MoveSelection(Control container, int dx, int dy)
	{
		bool flag = false;
		Control control = null;
		if (((Control)SelectionService.PrimarySelection).Parent != container && !SelectionService.GetComponentSelected(container))
		{
			flag = true;
			control = ((Control)SelectionService.PrimarySelection).Parent;
		}
		foreach (Component selectedComponent in SelectionService.GetSelectedComponents())
		{
			Control component = selectedComponent as Control;
			if (flag)
			{
				TypeDescriptor.GetProperties(component)["Parent"].SetValue(component, container);
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)["Location"];
			Point point = (Point)propertyDescriptor.GetValue(component);
			point.X += dx;
			point.Y += dy;
			propertyDescriptor.SetValue(component, point);
		}
		if (flag)
		{
			control.Invalidate(invalidateChildren: false);
			control.Update();
		}
	}

	private void SelectionBegin(Control container, int x, int y)
	{
		_selecting = true;
		_selectionContainer = container;
		_prevMousePosition = new Point(x, y);
		_initialMousePosition = _prevMousePosition;
		_selectionRectangle = new Rectangle(x, y, 0, 0);
	}

	private void SelectionContinue(int x, int y)
	{
		if (!_selecting)
		{
			return;
		}
		if (x > _selectionRectangle.Right)
		{
			_selectionRectangle.Width = x - _selectionRectangle.X;
		}
		else if (x > _selectionRectangle.X && x < _selectionRectangle.Right && x < _prevMousePosition.X)
		{
			_selectionRectangle.Width = x - _selectionRectangle.X;
		}
		else if (x < _selectionRectangle.X)
		{
			if (_prevMousePosition.X > _selectionRectangle.X)
			{
				_selectionRectangle.X = _initialMousePosition.X;
				_selectionRectangle.Width = 0;
			}
			else
			{
				_selectionRectangle.Width += _selectionRectangle.X - x;
				_selectionRectangle.X = x;
			}
		}
		else if (x > _selectionRectangle.X && x < _selectionRectangle.Right && x > _prevMousePosition.X)
		{
			if (_prevMousePosition.X < _selectionRectangle.X)
			{
				_selectionRectangle.X = _initialMousePosition.X;
				_selectionRectangle.Width = 0;
			}
			else
			{
				_selectionRectangle.Width -= x - _selectionRectangle.X;
				_selectionRectangle.X = x;
			}
		}
		if (y > _selectionRectangle.Bottom)
		{
			_selectionRectangle.Height = y - _selectionRectangle.Y;
		}
		else if (y > _selectionRectangle.Y && y < _selectionRectangle.Bottom && y < _prevMousePosition.Y)
		{
			_selectionRectangle.Height = y - _selectionRectangle.Y;
		}
		else if (y < _selectionRectangle.Y)
		{
			if (_prevMousePosition.Y > _selectionRectangle.Y)
			{
				_selectionRectangle.Y = _initialMousePosition.Y;
				_selectionRectangle.Height = 0;
			}
			else
			{
				_selectionRectangle.Height += _selectionRectangle.Y - y;
				_selectionRectangle.Y = y;
			}
		}
		else if (y > _selectionRectangle.Y && y < _selectionRectangle.Bottom && y > _prevMousePosition.Y)
		{
			if (_prevMousePosition.Y < _selectionRectangle.Y)
			{
				_selectionRectangle.Y = _initialMousePosition.Y;
				_selectionRectangle.Height = 0;
			}
			else
			{
				_selectionRectangle.Height -= y - _selectionRectangle.Y;
				_selectionRectangle.Y = y;
			}
		}
		_prevMousePosition.X = x;
		_prevMousePosition.Y = y;
		_selectionContainer.Refresh();
	}

	private void SelectionEnd(bool cancel)
	{
		_selecting = false;
		ICollection controlsIn = GetControlsIn(_selectionRectangle);
		if (controlsIn.Count != 0)
		{
			SelectionService.SetSelectedComponents(controlsIn, SelectionTypes.Replace);
		}
		_selectionContainer.Refresh();
	}

	private void ResizeBegin(int x, int y)
	{
		_resizing = true;
		_selectionFrame = GetSelectionFrameAt(x, y);
		_selectionFrame.ResizeBegin(x, y);
	}

	private void ResizeContinue(int x, int y)
	{
		Rectangle deltaBounds = _selectionFrame.ResizeContinue(x, y);
		foreach (IComponent selectedComponent in SelectionService.GetSelectedComponents())
		{
			if (selectedComponent is Control)
			{
				SelectionFrame selectionFrameFor = GetSelectionFrameFor((Control)selectedComponent);
				if (selectionFrameFor != _selectionFrame)
				{
					selectionFrameFor.Resize(deltaBounds);
				}
			}
		}
	}

	private void ResizeEnd(bool cancel)
	{
		_selectionFrame.ResizeEnd(cancel);
		_resizing = false;
	}

	private SelectionFrame GetSelectionFrameAt(int x, int y)
	{
		SelectionFrame result = null;
		foreach (SelectionFrame selectionFrame in _selectionFrames)
		{
			if (selectionFrame.Bounds.Contains(new Point(x, y)))
			{
				result = selectionFrame;
				break;
			}
		}
		return result;
	}

	private SelectionFrame GetSelectionFrameFor(Control control)
	{
		foreach (SelectionFrame selectionFrame in _selectionFrames)
		{
			if (control == selectionFrame.Control)
			{
				return selectionFrame;
			}
		}
		return null;
	}

	public bool AdornmentsHitTest(Control control, int x, int y)
	{
		return GetSelectionFrameAt(x, y)?.HitTest(x, y) ?? false;
	}

	public void PaintAdornments(Control container, Graphics gfx)
	{
		if (!(GetService(typeof(IDesignerHost)) is IDesignerHost) || !(SelectionService.PrimarySelection is Control))
		{
			return;
		}
		if ((Control)SelectionService.PrimarySelection == container)
		{
			if (_selecting)
			{
				Color color = Color.FromArgb((byte)(~_selectionContainer.BackColor.R), (byte)(~_selectionContainer.BackColor.G), (byte)(~_selectionContainer.BackColor.B));
				DrawSelectionRectangle(gfx, _selectionRectangle, color);
			}
		}
		else
		{
			if (((Control)SelectionService.PrimarySelection).Parent != container)
			{
				return;
			}
			foreach (SelectionFrame selectionFrame in _selectionFrames)
			{
				selectionFrame.OnPaint(gfx);
			}
		}
	}

	private void DrawSelectionRectangle(Graphics gfx, Rectangle frame, Color color)
	{
		Pen pen = new Pen(color);
		pen.DashStyle = DashStyle.Dash;
		gfx.DrawRectangle(pen, frame);
	}

	private void OnSelectionChanged(object sender, EventArgs args)
	{
		ICollection selectedComponents = SelectionService.GetSelectedComponents();
		if (_selectionFrames.Count == 0)
		{
			foreach (Component item in selectedComponents)
			{
				_selectionFrames.Add(new SelectionFrame((Control)item));
			}
		}
		else
		{
			int num = 0;
			foreach (Component item2 in selectedComponents)
			{
				if (num >= _selectionFrames.Count)
				{
					_selectionFrames.Add(new SelectionFrame((Control)item2));
				}
				else
				{
					((SelectionFrame)_selectionFrames[num]).Control = (Control)item2;
				}
				num++;
			}
			if (num < _selectionFrames.Count)
			{
				_selectionFrames.RemoveRange(num, _selectionFrames.Count - num);
			}
		}
		if ((GetService(typeof(IDesignerHost)) as IDesignerHost).RootComponent is Control control)
		{
			if (control.Parent != null)
			{
				control.Parent.Refresh();
			}
			else
			{
				control.Refresh();
			}
		}
	}

	private ICollection GetControlsIn(Rectangle rect)
	{
		ArrayList arrayList = new ArrayList();
		foreach (Control control in _selectionContainer.Controls)
		{
			if (rect.Contains(control.Bounds) || rect.IntersectsWith(control.Bounds))
			{
				arrayList.Add(control);
			}
		}
		return arrayList;
	}
}
