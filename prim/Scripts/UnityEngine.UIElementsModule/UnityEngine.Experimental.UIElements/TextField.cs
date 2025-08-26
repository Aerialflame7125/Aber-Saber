using System.Collections.Generic;

namespace UnityEngine.Experimental.UIElements;

public class TextField : TextInputFieldBase, INotifyValueChanged<string>
{
	private bool m_Multiline;

	protected string m_Value;

	public bool multiline
	{
		get
		{
			return m_Multiline;
		}
		set
		{
			m_Multiline = value;
			if (!value)
			{
				text = text.Replace("\n", "");
			}
		}
	}

	public override bool isPasswordField
	{
		set
		{
			base.isPasswordField = value;
			if (value)
			{
				multiline = false;
			}
		}
	}

	public string value
	{
		get
		{
			return m_Value;
		}
		set
		{
			m_Value = value;
			text = m_Value;
		}
	}

	public TextField()
		: this(-1, multiline: false, isPasswordField: false, '\0')
	{
	}

	public TextField(int maxLength, bool multiline, bool isPasswordField, char maskChar)
		: base(maxLength, maskChar)
	{
		this.multiline = multiline;
		this.isPasswordField = isPasswordField;
	}

	public void SetValueAndNotify(string newValue)
	{
		if (!EqualityComparer<string>.Default.Equals(value, newValue))
		{
			using (ChangeEvent<string> changeEvent = ChangeEvent<string>.GetPooled(value, newValue))
			{
				changeEvent.target = this;
				value = newValue;
				UIElementsUtility.eventDispatcher.DispatchEvent(changeEvent, base.panel);
			}
		}
	}

	public void OnValueChanged(EventCallback<ChangeEvent<string>> callback)
	{
		RegisterCallback(callback);
	}

	public override void OnPersistentDataReady()
	{
		base.OnPersistentDataReady();
		string fullHierarchicalPersistenceKey = GetFullHierarchicalPersistenceKey();
		OverwriteFromPersistedData(this, fullHierarchicalPersistenceKey);
	}

	internal override void SyncTextEngine()
	{
		base.editorEngine.multiline = multiline;
		base.editorEngine.isPasswordField = isPasswordField;
		base.SyncTextEngine();
	}

	internal override void DoRepaint(IStylePainter painter)
	{
		if (isPasswordField)
		{
			string newText = "".PadRight(text.Length, base.maskChar);
			if (!base.hasFocus)
			{
				painter.DrawBackground(this);
				painter.DrawBorder(this);
				if (!string.IsNullOrEmpty(newText) && base.contentRect.width > 0f && base.contentRect.height > 0f)
				{
					TextStylePainterParameters defaultTextParameters = painter.GetDefaultTextParameters(this);
					defaultTextParameters.text = newText;
					painter.DrawText(defaultTextParameters);
				}
			}
			else
			{
				DrawWithTextSelectionAndCursor(painter, newText);
			}
		}
		else
		{
			base.DoRepaint(painter);
		}
	}

	protected internal override void ExecuteDefaultActionAtTarget(EventBase evt)
	{
		base.ExecuteDefaultActionAtTarget(evt);
		if (evt.GetEventTypeId() == EventBase<KeyDownEvent>.TypeId())
		{
			KeyDownEvent keyDownEvent = evt as KeyDownEvent;
			if (keyDownEvent.character == '\n')
			{
				SetValueAndNotify(text);
			}
		}
	}

	protected internal override void ExecuteDefaultAction(EventBase evt)
	{
		base.ExecuteDefaultAction(evt);
		if (evt.GetEventTypeId() == EventBase<BlurEvent>.TypeId())
		{
			SetValueAndNotify(text);
		}
	}
}
