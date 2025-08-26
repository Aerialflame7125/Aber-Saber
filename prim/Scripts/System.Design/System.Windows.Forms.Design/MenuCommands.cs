using System.ComponentModel.Design;

namespace System.Windows.Forms.Design;

/// <summary>Defines a set of <see cref="T:System.ComponentModel.Design.CommandID" /> fields that each correspond to a command function provided by the host environment.</summary>
public sealed class MenuCommands : StandardCommands
{
	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the component tray menu.</summary>
	public static readonly CommandID ComponentTrayMenu;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the container menu.</summary>
	public static readonly CommandID ContainerMenu;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the properties page for the designer.</summary>
	public static readonly CommandID DesignerProperties;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the cancel key handler.</summary>
	public static readonly CommandID KeyCancel;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the default key handler.</summary>
	public static readonly CommandID KeyDefaultAction;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move down key handler.</summary>
	public static readonly CommandID KeyMoveDown;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move left key handler.</summary>
	public static readonly CommandID KeyMoveLeft;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move right key handler.</summary>
	public static readonly CommandID KeyMoveRight;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the move up key handler.</summary>
	public static readonly CommandID KeyMoveUp;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge down key handler.</summary>
	public static readonly CommandID KeyNudgeDown;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge height decrease key handler.</summary>
	public static readonly CommandID KeyNudgeHeightDecrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge height increase key handler.</summary>
	public static readonly CommandID KeyNudgeHeightIncrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge left key handler.</summary>
	public static readonly CommandID KeyNudgeLeft;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge right key handler.</summary>
	public static readonly CommandID KeyNudgeRight;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge up key handler.</summary>
	public static readonly CommandID KeyNudgeUp;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge width decrease key handler.</summary>
	public static readonly CommandID KeyNudgeWidthDecrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the nudge width increase key handler.</summary>
	public static readonly CommandID KeyNudgeWidthIncrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the reverse cancel key handler.</summary>
	public static readonly CommandID KeyReverseCancel;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the select next key handler.</summary>
	public static readonly CommandID KeySelectNext;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the select previous key handler.</summary>
	public static readonly CommandID KeySelectPrevious;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size height decrease key handler.</summary>
	public static readonly CommandID KeySizeHeightDecrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size height increase key handler.</summary>
	public static readonly CommandID KeySizeHeightIncrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size width decrease key handler.</summary>
	public static readonly CommandID KeySizeWidthDecrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the size width increase key handler.</summary>
	public static readonly CommandID KeySizeWidthIncrease;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the tab order select key handler.</summary>
	public static readonly CommandID KeyTabOrderSelect;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the selection menu.</summary>
	public static readonly CommandID SelectionMenu;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the tray selection menu.</summary>
	public static readonly CommandID TraySelectionMenu;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the edit label handler.</summary>
	public static readonly CommandID EditLabel;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the end key handler.</summary>
	public static readonly CommandID KeyEnd;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the home key handler.</summary>
	public static readonly CommandID KeyHome;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the smart tag invocation handler.</summary>
	public static readonly CommandID KeyInvokeSmartTag;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the SHIFT-END key handler.</summary>
	public static readonly CommandID KeyShiftEnd;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to access the SHIFT-HOME key handler.</summary>
	public static readonly CommandID KeyShiftHome;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to set the status rectangle.</summary>
	public static readonly CommandID SetStatusRectangle;

	/// <summary>A <see cref="T:System.ComponentModel.Design.CommandID" /> that can be used to set the status rectangle text.</summary>
	public static readonly CommandID SetStatusText;

	private static readonly Guid guidVSStd97;

	private static readonly Guid guidVSStd2K;

	private static readonly Guid wfCommandSet;

	private static readonly Guid wfMenuGroup;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.MenuCommands" /> class.</summary>
	public MenuCommands()
	{
	}

	static MenuCommands()
	{
		wfMenuGroup = new Guid("{74D21312-2AEE-11d1-8BFB-00A0C90F26F7}");
		wfCommandSet = new Guid("{74D21313-2AEE-11d1-8BFB-00A0C90F26F7}");
		guidVSStd2K = new Guid("{1496A755-94DE-11D0-8C3F-00C04FC2AAE2}");
		guidVSStd97 = new Guid("{5efc7975-14bc-11cf-9b2b-00aa00573819}");
		SelectionMenu = new CommandID(wfMenuGroup, 1280);
		ContainerMenu = new CommandID(wfMenuGroup, 1281);
		TraySelectionMenu = new CommandID(wfMenuGroup, 1283);
		ComponentTrayMenu = new CommandID(wfMenuGroup, 1286);
		DesignerProperties = new CommandID(wfCommandSet, 4097);
		KeyCancel = new CommandID(guidVSStd2K, 103);
		KeyReverseCancel = new CommandID(wfCommandSet, 16385);
		KeyDefaultAction = new CommandID(guidVSStd2K, 3);
		KeyMoveUp = new CommandID(guidVSStd2K, 11);
		KeyMoveDown = new CommandID(guidVSStd2K, 13);
		KeyMoveLeft = new CommandID(guidVSStd2K, 7);
		KeyMoveRight = new CommandID(guidVSStd2K, 9);
		KeyNudgeUp = new CommandID(guidVSStd2K, 1227);
		KeyNudgeDown = new CommandID(guidVSStd2K, 1225);
		KeyNudgeLeft = new CommandID(guidVSStd2K, 1224);
		KeyNudgeRight = new CommandID(guidVSStd2K, 1226);
		KeySizeWidthIncrease = new CommandID(guidVSStd2K, 10);
		KeySizeHeightIncrease = new CommandID(guidVSStd2K, 12);
		KeySizeWidthDecrease = new CommandID(guidVSStd2K, 8);
		KeySizeHeightDecrease = new CommandID(guidVSStd2K, 14);
		KeyNudgeWidthIncrease = new CommandID(guidVSStd2K, 1231);
		KeyNudgeHeightIncrease = new CommandID(guidVSStd2K, 1228);
		KeyNudgeWidthDecrease = new CommandID(guidVSStd2K, 1230);
		KeyNudgeHeightDecrease = new CommandID(guidVSStd2K, 1229);
		KeySelectNext = new CommandID(guidVSStd2K, 4);
		KeySelectPrevious = new CommandID(guidVSStd2K, 5);
		KeyTabOrderSelect = new CommandID(wfCommandSet, 16405);
		KeyHome = new CommandID(guidVSStd2K, 15);
		KeyShiftHome = new CommandID(guidVSStd2K, 16);
		KeyEnd = new CommandID(guidVSStd2K, 17);
		KeyShiftEnd = new CommandID(guidVSStd2K, 18);
		KeyInvokeSmartTag = new CommandID(guidVSStd2K, 147);
		EditLabel = new CommandID(guidVSStd97, 338);
		SetStatusText = new CommandID(wfCommandSet, 16387);
		SetStatusRectangle = new CommandID(wfCommandSet, 16388);
	}
}
