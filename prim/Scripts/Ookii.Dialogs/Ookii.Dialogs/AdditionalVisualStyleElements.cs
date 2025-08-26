using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs;

public static class AdditionalVisualStyleElements
{
	[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
	public static class TextStyle
	{
		private const string _className = "TEXTSTYLE";

		private static VisualStyleElement _mainInstruction;

		private static VisualStyleElement _bodyText;

		public static VisualStyleElement MainInstruction => _mainInstruction ?? (_mainInstruction = VisualStyleElement.CreateElement("TEXTSTYLE", 1, 0));

		public static VisualStyleElement BodyText => _bodyText ?? (_bodyText = VisualStyleElement.CreateElement("TEXTSTYLE", 4, 0));
	}

	[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
	public static class TaskDialog
	{
		private const string _className = "TASKDIALOG";

		private static VisualStyleElement _primaryPanel;

		private static VisualStyleElement _secondaryPanel;

		public static VisualStyleElement PrimaryPanel => _primaryPanel ?? (_primaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 1, 0));

		public static VisualStyleElement SecondaryPanel => _secondaryPanel ?? (_secondaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
	}
}
