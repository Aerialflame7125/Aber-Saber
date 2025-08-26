using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design;

internal class AnchorSelector : UserControl
{
	private IContainer components;

	private AnchorStyles styles;

	public AnchorStyles AnchorStyles => styles;

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		base.SuspendLayout();
		base.Name = "AnchorSelector";
		base.Size = new System.Drawing.Size(150, 120);
		base.ResumeLayout(false);
	}

	public AnchorSelector(IWindowsFormsEditorService editor_service, AnchorStyles startup)
	{
		styles = startup;
		InitializeComponent();
		BackColor = Color.White;
	}

	private void PaintState(Graphics g, int x1, int y1, int x2, int y2, AnchorStyles v)
	{
		if ((styles & v) != 0)
		{
			g.DrawLine(SystemPens.MenuText, x1, y1, x2, y2);
			return;
		}
		int num = ((x1 == x2) ? 10 : 0);
		int num2 = ((y1 == y2) ? 10 : 0);
		g.DrawBezier(SystemPens.MenuText, new Point(x1, y1), new Point((x1 + x2) / 2 + num, (y1 + y2) / 2 - num2), new Point((x1 + x2) / 2 - num, (y1 + y2) / 2 + num2), new Point(x2, y2));
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		Graphics graphics = e.Graphics;
		int num = base.Width / 3;
		int num2 = base.Height / 3;
		int num3 = base.Width / 2;
		int num4 = base.Height / 2;
		graphics.FillRectangle(Brushes.Black, new Rectangle(num, num2, num, num2));
		PaintState(graphics, 0, num4, num, num4, AnchorStyles.Left);
		PaintState(graphics, num * 2, num4, base.Width, num4, AnchorStyles.Right);
		PaintState(graphics, num3, 0, num3, num2, AnchorStyles.Top);
		PaintState(graphics, num3, num2 * 2, num3, base.Height, AnchorStyles.Bottom);
	}

	protected override void OnClick(EventArgs ee)
	{
		Point point = PointToClient(Control.MousePosition);
		int num = base.Width / 3;
		int num2 = base.Height / 3;
		if (point.X <= num && point.Y > num2 && point.Y < num2 * 2)
		{
			styles ^= AnchorStyles.Left;
		}
		else if (point.Y < num2 && point.X > num && point.X < num * 2)
		{
			styles ^= AnchorStyles.Top;
		}
		else if (point.X > num * 2 && point.Y > num2 && point.Y < num2 * 2)
		{
			styles ^= AnchorStyles.Right;
		}
		else if (point.Y > num2 * 2 && point.X > num && point.X < num * 2)
		{
			styles ^= AnchorStyles.Bottom;
		}
		else
		{
			base.OnClick(ee);
		}
		Invalidate();
	}

	protected override void OnDoubleClick(EventArgs ee)
	{
		OnClick(ee);
	}
}
