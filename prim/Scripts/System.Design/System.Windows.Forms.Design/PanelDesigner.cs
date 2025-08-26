using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace System.Windows.Forms.Design;

internal class PanelDesigner : ParentControlDesigner
{
	public override void Initialize(IComponent component)
	{
		base.Initialize(component);
	}

	protected override void OnPaintAdornments(PaintEventArgs pe)
	{
		base.OnPaintAdornments(pe);
		GraphicsState gstate = pe.Graphics.Save();
		pe.Graphics.TranslateTransform(Control.ClientRectangle.X, Control.ClientRectangle.Y);
		ControlPaint.DrawBorder(pe.Graphics, Control.ClientRectangle, SystemColors.ControlDarkDark, ButtonBorderStyle.Dashed);
		pe.Graphics.Restore(gstate);
	}
}
