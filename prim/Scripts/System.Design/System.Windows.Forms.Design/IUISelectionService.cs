using System.Drawing;

namespace System.Windows.Forms.Design;

internal interface IUISelectionService
{
	bool SelectionInProgress { get; }

	bool DragDropInProgress { get; }

	bool ResizeInProgress { get; }

	Rectangle SelectionBounds { get; }

	void MouseDragBegin(Control container, int x, int y);

	void MouseDragMove(int x, int y);

	void MouseDragEnd(bool cancel);

	void DragBegin();

	void DragOver(Control container, int x, int y);

	void DragDrop(bool cancel, Control container, int x, int y);

	void PaintAdornments(Control container, Graphics gfx);

	bool SetCursor(int x, int y);

	bool AdornmentsHitTest(Control control, int x, int y);
}
