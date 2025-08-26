using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine;

[NativeHeader("Modules/IMGUI/GUIUtility.h")]
[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
public class GUIUtility
{
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal struct ManualTex2SRGBScope : IDisposable
	{
		private bool m_Disposed;

		private bool m_WasEnabled;

		public ManualTex2SRGBScope(bool enabled)
		{
			m_Disposed = false;
			m_WasEnabled = manualTex2SRGBEnabled;
			manualTex2SRGBEnabled = enabled;
		}

		public void Dispose()
		{
			if (!m_Disposed)
			{
				m_Disposed = true;
				manualTex2SRGBEnabled = m_WasEnabled;
			}
		}
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static int s_SkinMode;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static int s_OriginalID;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Action takeCapture;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Action releaseCapture;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Func<int, IntPtr, bool> processEvent;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Action cleanupRoots;

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Func<Exception, bool> endContainerGUIFromException;

	internal static Vector2 s_EditorScreenPointOffset = Vector2.zero;

	public static extern string systemCopyBuffer
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern bool mouseUsed
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	public static extern bool hasModalWindow
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern bool textFieldInput
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern bool manualTex2SRGBEnabled
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		get;
		[MethodImpl(MethodImplOptions.InternalCall)]
		[GeneratedByOldBindingsGenerator]
		set;
	}

	internal static float pixelsPerPoint
	{
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		get
		{
			return Internal_GetPixelsPerPoint();
		}
	}

	internal static bool guiIsExiting { get; set; }

	public static int hotControl
	{
		get
		{
			return Internal_GetHotControl();
		}
		set
		{
			Internal_SetHotControl(value);
		}
	}

	public static int keyboardControl
	{
		get
		{
			return Internal_GetKeyboardControl();
		}
		set
		{
			Internal_SetKeyboardControl(value);
		}
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern float Internal_GetPixelsPerPoint();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	public static extern int GetControlID(int hint, FocusType focus);

	private static int Internal_GetNextControlID2(int hint, FocusType focusType, Rect rect)
	{
		return INTERNAL_CALL_Internal_GetNextControlID2(hint, focusType, ref rect);
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int INTERNAL_CALL_Internal_GetNextControlID2(int hint, FocusType focusType, ref Rect rect);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern int GetPermanentControlID();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int Internal_GetHotControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetHotControl(int value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern void UpdateUndoName();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern bool GetChanged();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern void SetChanged(bool changed);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern int Internal_GetKeyboardControl();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_SetKeyboardControl(int value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern GUISkin Internal_GetDefaultSkin(int skinMode);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern Object Internal_GetBuiltinSkin(int skin);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void Internal_ExitGUI();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern int Internal_GetGUIDepth();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern int CheckForTabEvent(Event evt);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern void SetKeyboardControlToFirstControlId();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern void SetKeyboardControlToLastControlId();

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern bool HasFocusableControls();

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Rect Internal_AlignRectToDevice(Rect rect, Matrix4x4 transform)
	{
		INTERNAL_CALL_Internal_AlignRectToDevice(ref rect, ref transform, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Internal_AlignRectToDevice(ref Rect rect, ref Matrix4x4 transform, out Rect value);

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static Vector3 Internal_MultiplyPoint(Vector3 point, Matrix4x4 transform)
	{
		INTERNAL_CALL_Internal_MultiplyPoint(ref point, ref transform, out var value);
		return value;
	}

	[MethodImpl(MethodImplOptions.InternalCall)]
	[GeneratedByOldBindingsGenerator]
	private static extern void INTERNAL_CALL_Internal_MultiplyPoint(ref Vector3 point, ref Matrix4x4 transform, out Vector3 value);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	[GeneratedByOldBindingsGenerator]
	internal static extern float Internal_Roundf(float f);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern void BeginContainerFromOwner(ScriptableObject owner);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static extern void BeginContainer(ObjectGUIState objectGUIState);

	[MethodImpl(MethodImplOptions.InternalCall)]
	[NativeMethod("EndContainer")]
	internal static extern void Internal_EndContainer();

	public static int GetControlID(FocusType focus)
	{
		return GetControlID(0, focus);
	}

	public static int GetControlID(GUIContent contents, FocusType focus)
	{
		return GetControlID(contents.hash, focus);
	}

	public static int GetControlID(FocusType focus, Rect position)
	{
		return Internal_GetNextControlID2(0, focus, position);
	}

	public static int GetControlID(int hint, FocusType focus, Rect position)
	{
		return Internal_GetNextControlID2(hint, focus, position);
	}

	public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
	{
		return Internal_GetNextControlID2(contents.hash, focus, position);
	}

	public static object GetStateObject(Type t, int controlID)
	{
		return GUIStateObjects.GetStateObject(t, controlID);
	}

	public static object QueryStateObject(Type t, int controlID)
	{
		return GUIStateObjects.QueryStateObject(t, controlID);
	}

	[RequiredByNativeCode]
	internal static void TakeCapture()
	{
		if (takeCapture != null)
		{
			takeCapture();
		}
	}

	[RequiredByNativeCode]
	internal static void RemoveCapture()
	{
		if (releaseCapture != null)
		{
			releaseCapture();
		}
	}

	public static void ExitGUI()
	{
		guiIsExiting = true;
		throw new ExitGUIException();
	}

	internal static GUISkin GetDefaultSkin(int skinMode)
	{
		return Internal_GetDefaultSkin(skinMode);
	}

	internal static GUISkin GetDefaultSkin()
	{
		return Internal_GetDefaultSkin(s_SkinMode);
	}

	internal static GUISkin GetBuiltinSkin(int skin)
	{
		return Internal_GetBuiltinSkin(skin) as GUISkin;
	}

	[RequiredByNativeCode]
	internal static bool ProcessEvent(int instanceID, IntPtr nativeEventPtr)
	{
		if (processEvent != null)
		{
			return processEvent(instanceID, nativeEventPtr);
		}
		return false;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static void EndContainer()
	{
		Internal_EndContainer();
		Internal_ExitGUI();
	}

	internal static void CleanupRoots()
	{
		if (cleanupRoots != null)
		{
			cleanupRoots();
		}
	}

	[RequiredByNativeCode]
	internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
	{
		s_SkinMode = skinMode;
		s_OriginalID = instanceID;
		ResetGlobalState();
		if (useGUILayout != 0)
		{
			GUILayoutUtility.Begin(instanceID);
		}
	}

	[RequiredByNativeCode]
	internal static void EndGUI(int layoutType)
	{
		try
		{
			if (Event.current.type == EventType.Layout)
			{
				switch (layoutType)
				{
				case 1:
					GUILayoutUtility.Layout();
					break;
				case 2:
					GUILayoutUtility.LayoutFromEditorWindow();
					break;
				}
			}
			GUILayoutUtility.SelectIDList(s_OriginalID, isWindow: false);
			GUIContent.ClearStaticCache();
		}
		finally
		{
			Internal_ExitGUI();
		}
	}

	[RequiredByNativeCode]
	internal static bool EndGUIFromException(Exception exception)
	{
		Internal_ExitGUI();
		return ShouldRethrowException(exception);
	}

	[RequiredByNativeCode]
	internal static bool EndContainerGUIFromException(Exception exception)
	{
		if (endContainerGUIFromException != null)
		{
			return endContainerGUIFromException(exception);
		}
		return false;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static void ResetGlobalState()
	{
		GUI.skin = null;
		guiIsExiting = false;
		GUI.changed = false;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static bool IsExitGUIException(Exception exception)
	{
		while (exception is TargetInvocationException && exception.InnerException != null)
		{
			exception = exception.InnerException;
		}
		return exception is ExitGUIException;
	}

	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static bool ShouldRethrowException(Exception exception)
	{
		return IsExitGUIException(exception);
	}

	internal static void CheckOnGUI()
	{
		if (Internal_GetGUIDepth() <= 0)
		{
			throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
		}
	}

	public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
	{
		return GUIClip.UnclipToWindow(guiPoint) + s_EditorScreenPointOffset;
	}

	internal static Rect GUIToScreenRect(Rect guiRect)
	{
		Vector2 vector = GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
		guiRect.x = vector.x;
		guiRect.y = vector.y;
		return guiRect;
	}

	public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
	{
		return GUIClip.ClipToWindow(screenPoint) - s_EditorScreenPointOffset;
	}

	public static Rect ScreenToGUIRect(Rect screenRect)
	{
		Vector2 vector = ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
		screenRect.x = vector.x;
		screenRect.y = vector.y;
		return screenRect;
	}

	public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
	{
		Matrix4x4 matrix = GUI.matrix;
		GUI.matrix = Matrix4x4.identity;
		Vector2 vector = GUIClip.Unclip(pivotPoint);
		Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
		GUI.matrix = matrix4x * matrix;
	}

	public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
	{
		Matrix4x4 matrix = GUI.matrix;
		Vector2 vector = GUIClip.Unclip(pivotPoint);
		Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
		GUI.matrix = matrix4x * matrix;
	}
}
