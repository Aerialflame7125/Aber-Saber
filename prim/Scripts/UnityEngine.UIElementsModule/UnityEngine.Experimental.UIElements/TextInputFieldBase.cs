using UnityEngine.Experimental.UIElements.StyleSheets;

namespace UnityEngine.Experimental.UIElements;

public abstract class TextInputFieldBase : BaseTextElement
{
	private const string SelectionColorProperty = "selection-color";

	private const string CursorColorProperty = "cursor-color";

	private StyleValue<Color> m_SelectionColor;

	private StyleValue<Color> m_CursorColor;

	internal const int kMaxLengthNone = -1;

	public virtual bool isPasswordField { get; set; }

	public Color selectionColor => m_SelectionColor.GetSpecifiedValueOrDefault(Color.clear);

	public Color cursorColor => m_CursorColor.GetSpecifiedValueOrDefault(Color.clear);

	public int maxLength { get; set; }

	public bool doubleClickSelectsWord { get; set; }

	public bool tripleClickSelectsLine { get; set; }

	private bool touchScreenTextField => TouchScreenKeyboard.isSupported;

	internal bool hasFocus => base.elementPanel != null && base.elementPanel.focusController.focusedElement == this;

	internal TextEditorEventHandler editorEventHandler { get; private set; }

	internal TextEditorEngine editorEngine { get; private set; }

	public char maskChar { get; set; }

	public override string text
	{
		set
		{
			base.text = value;
			editorEngine.text = value;
		}
	}

	public TextInputFieldBase(int maxLength, char maskChar)
	{
		this.maxLength = maxLength;
		this.maskChar = maskChar;
		editorEngine = new TextEditorEngine(this);
		if (touchScreenTextField)
		{
			editorEventHandler = new TouchScreenTextEditorEventHandler(editorEngine, this);
		}
		else
		{
			doubleClickSelectsWord = true;
			tripleClickSelectsLine = true;
			editorEventHandler = new KeyboardTextEditorEventHandler(editorEngine, this);
		}
		editorEngine.style = new GUIStyle(editorEngine.style);
		base.focusIndex = 0;
	}

	public void SelectAll()
	{
		if (editorEngine != null)
		{
			editorEngine.SelectAll();
		}
	}

	public void UpdateText(string value)
	{
		if (text != value)
		{
			using (InputEvent inputEvent = InputEvent.GetPooled(text, value))
			{
				inputEvent.target = this;
				text = value;
				UIElementsUtility.eventDispatcher.DispatchEvent(inputEvent, base.panel);
			}
		}
	}

	private ContextualMenu.MenuAction.StatusFlags CutCopyActionStatus(EventBase e)
	{
		return (!editorEngine.hasSelection || isPasswordField) ? ContextualMenu.MenuAction.StatusFlags.Disabled : ContextualMenu.MenuAction.StatusFlags.Normal;
	}

	private ContextualMenu.MenuAction.StatusFlags PasteActionStatus(EventBase e)
	{
		return (!editorEngine.CanPaste()) ? ContextualMenu.MenuAction.StatusFlags.Disabled : ContextualMenu.MenuAction.StatusFlags.Normal;
	}

	private void Cut(EventBase e)
	{
		editorEngine.Cut();
		editorEngine.text = CullString(editorEngine.text);
		UpdateText(editorEngine.text);
	}

	private void Copy(EventBase e)
	{
		editorEngine.Copy();
	}

	private void Paste(EventBase e)
	{
		editorEngine.Paste();
		editorEngine.text = CullString(editorEngine.text);
		UpdateText(editorEngine.text);
	}

	protected override void OnStyleResolved(ICustomStyle style)
	{
		base.OnStyleResolved(style);
		base.effectiveStyle.ApplyCustomProperty("selection-color", ref m_SelectionColor);
		base.effectiveStyle.ApplyCustomProperty("cursor-color", ref m_CursorColor);
		base.effectiveStyle.WriteToGUIStyle(editorEngine.style);
	}

	internal virtual void SyncTextEngine()
	{
		editorEngine.text = CullString(text);
		editorEngine.SaveBackup();
		editorEngine.position = base.layout;
		editorEngine.DetectFocusChange();
	}

	internal string CullString(string s)
	{
		if (maxLength >= 0 && s != null && s.Length > maxLength)
		{
			return s.Substring(0, maxLength);
		}
		return s;
	}

	internal override void DoRepaint(IStylePainter painter)
	{
		if (touchScreenTextField)
		{
			TouchScreenTextEditorEventHandler touchScreenTextEditorEventHandler = editorEventHandler as TouchScreenTextEditorEventHandler;
			if (touchScreenTextEditorEventHandler != null && editorEngine.keyboardOnScreen != null)
			{
				UpdateText(CullString(editorEngine.keyboardOnScreen.text));
				if (editorEngine.keyboardOnScreen.status != 0)
				{
					editorEngine.keyboardOnScreen = null;
					GUI.changed = true;
				}
			}
			string text = this.text;
			if (touchScreenTextEditorEventHandler != null && !string.IsNullOrEmpty(touchScreenTextEditorEventHandler.secureText))
			{
				text = "".PadRight(touchScreenTextEditorEventHandler.secureText.Length, maskChar);
			}
			base.DoRepaint(painter);
			this.text = text;
		}
		else if (!hasFocus)
		{
			base.DoRepaint(painter);
		}
		else
		{
			DrawWithTextSelectionAndCursor(painter, this.text);
		}
	}

	internal void DrawWithTextSelectionAndCursor(IStylePainter painter, string newText)
	{
		if (!(editorEventHandler is KeyboardTextEditorEventHandler keyboardTextEditorEventHandler))
		{
			return;
		}
		keyboardTextEditorEventHandler.PreDrawCursor(newText);
		int cursorIndex = editorEngine.cursorIndex;
		int selectIndex = editorEngine.selectIndex;
		Rect localPosition = editorEngine.localPosition;
		Vector2 scrollOffset = editorEngine.scrollOffset;
		IStyle style = base.style;
		TextStylePainterParameters defaultTextParameters = painter.GetDefaultTextParameters(this);
		defaultTextParameters.text = " ";
		defaultTextParameters.wordWrapWidth = 0f;
		defaultTextParameters.wordWrap = false;
		float num = painter.ComputeTextHeight(defaultTextParameters);
		float num2 = ((!style.wordWrap) ? 0f : base.contentRect.width);
		Input.compositionCursorPos = editorEngine.graphicalCursorPos - scrollOffset + new Vector2(localPosition.x, localPosition.y + num);
		Color specifiedValueOrDefault = m_CursorColor.GetSpecifiedValueOrDefault(Color.grey);
		int num3 = ((!string.IsNullOrEmpty(Input.compositionString)) ? (cursorIndex + Input.compositionString.Length) : selectIndex);
		painter.DrawBackground(this);
		if (cursorIndex != num3)
		{
			RectStylePainterParameters defaultRectParameters = painter.GetDefaultRectParameters(this);
			defaultRectParameters.color = selectionColor;
			defaultRectParameters.border.SetWidth(0f);
			defaultRectParameters.border.SetRadius(0f);
			int cursorIndex2 = ((cursorIndex >= num3) ? num3 : cursorIndex);
			int cursorIndex3 = ((cursorIndex <= num3) ? num3 : cursorIndex);
			CursorPositionStylePainterParameters defaultCursorPositionParameters = painter.GetDefaultCursorPositionParameters(this);
			defaultCursorPositionParameters.text = editorEngine.text;
			defaultCursorPositionParameters.wordWrapWidth = num2;
			defaultCursorPositionParameters.cursorIndex = cursorIndex2;
			Vector2 cursorPosition = painter.GetCursorPosition(defaultCursorPositionParameters);
			defaultCursorPositionParameters.cursorIndex = cursorIndex3;
			Vector2 cursorPosition2 = painter.GetCursorPosition(defaultCursorPositionParameters);
			cursorPosition -= scrollOffset;
			cursorPosition2 -= scrollOffset;
			if (Mathf.Approximately(cursorPosition.y, cursorPosition2.y))
			{
				defaultRectParameters.rect = new Rect(cursorPosition.x, cursorPosition.y, cursorPosition2.x - cursorPosition.x, num);
				painter.DrawRect(defaultRectParameters);
			}
			else
			{
				defaultRectParameters.rect = new Rect(cursorPosition.x, cursorPosition.y, num2 - cursorPosition.x, num);
				painter.DrawRect(defaultRectParameters);
				float num4 = cursorPosition2.y - cursorPosition.y - num;
				if (num4 > 0f)
				{
					defaultRectParameters.rect = new Rect(0f, cursorPosition.y + num, num2, num4);
					painter.DrawRect(defaultRectParameters);
				}
				defaultRectParameters.rect = new Rect(0f, cursorPosition2.y, cursorPosition2.x, num);
				painter.DrawRect(defaultRectParameters);
			}
		}
		painter.DrawBorder(this);
		if (!string.IsNullOrEmpty(editorEngine.text) && base.contentRect.width > 0f && base.contentRect.height > 0f)
		{
			defaultTextParameters = painter.GetDefaultTextParameters(this);
			defaultTextParameters.rect = new Rect(base.contentRect.x - scrollOffset.x, base.contentRect.y - scrollOffset.y, base.contentRect.width, base.contentRect.height);
			defaultTextParameters.text = editorEngine.text;
			painter.DrawText(defaultTextParameters);
		}
		if (cursorIndex == num3 && (Font)style.font != null)
		{
			CursorPositionStylePainterParameters defaultCursorPositionParameters = painter.GetDefaultCursorPositionParameters(this);
			defaultCursorPositionParameters.text = editorEngine.text;
			defaultCursorPositionParameters.wordWrapWidth = num2;
			defaultCursorPositionParameters.cursorIndex = cursorIndex;
			Vector2 cursorPosition3 = painter.GetCursorPosition(defaultCursorPositionParameters);
			cursorPosition3 -= scrollOffset;
			RectStylePainterParameters rectStylePainterParameters = default(RectStylePainterParameters);
			rectStylePainterParameters.rect = new Rect(cursorPosition3.x, cursorPosition3.y, 1f, num);
			rectStylePainterParameters.color = specifiedValueOrDefault;
			RectStylePainterParameters painterParams = rectStylePainterParameters;
			painter.DrawRect(painterParams);
		}
		if (editorEngine.altCursorPosition != -1)
		{
			CursorPositionStylePainterParameters defaultCursorPositionParameters = painter.GetDefaultCursorPositionParameters(this);
			defaultCursorPositionParameters.text = editorEngine.text.Substring(0, editorEngine.altCursorPosition);
			defaultCursorPositionParameters.wordWrapWidth = num2;
			defaultCursorPositionParameters.cursorIndex = editorEngine.altCursorPosition;
			Vector2 cursorPosition4 = painter.GetCursorPosition(defaultCursorPositionParameters);
			cursorPosition4 -= scrollOffset;
			RectStylePainterParameters rectStylePainterParameters = default(RectStylePainterParameters);
			rectStylePainterParameters.rect = new Rect(cursorPosition4.x, cursorPosition4.y, 1f, num);
			rectStylePainterParameters.color = specifiedValueOrDefault;
			RectStylePainterParameters painterParams2 = rectStylePainterParameters;
			painter.DrawRect(painterParams2);
		}
		keyboardTextEditorEventHandler.PostDrawCursor();
	}

	internal virtual bool AcceptCharacter(char c)
	{
		return true;
	}

	protected virtual void BuildContextualMenu(ContextualMenuPopulateEvent evt)
	{
		if (evt.target is TextInputFieldBase)
		{
			evt.menu.AppendAction("Cut", Cut, CutCopyActionStatus);
			evt.menu.AppendAction("Copy", Copy, CutCopyActionStatus);
			evt.menu.AppendAction("Paste", Paste, PasteActionStatus);
		}
	}

	protected internal override void ExecuteDefaultActionAtTarget(EventBase evt)
	{
		base.ExecuteDefaultActionAtTarget(evt);
		if (base.elementPanel != null && base.elementPanel.contextualMenuManager != null)
		{
			base.elementPanel.contextualMenuManager.DisplayMenuIfEventMatches(evt, this);
		}
		if (evt.GetEventTypeId() == EventBase<ContextualMenuPopulateEvent>.TypeId())
		{
			ContextualMenuPopulateEvent contextualMenuPopulateEvent = evt as ContextualMenuPopulateEvent;
			int count = contextualMenuPopulateEvent.menu.MenuItems().Count;
			BuildContextualMenu(contextualMenuPopulateEvent);
			if (count > 0 && contextualMenuPopulateEvent.menu.MenuItems().Count > count)
			{
				contextualMenuPopulateEvent.menu.InsertSeparator(count);
			}
		}
		editorEventHandler.ExecuteDefaultActionAtTarget(evt);
	}

	protected internal override void ExecuteDefaultAction(EventBase evt)
	{
		base.ExecuteDefaultAction(evt);
		editorEventHandler.ExecuteDefaultAction(evt);
	}
}
