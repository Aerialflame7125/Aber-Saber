using System;
using System.Runtime.InteropServices;
using Mono.Mozilla.DOM;
using Mono.WebBrowser;
using Mono.WebBrowser.DOM;

namespace Mono.Mozilla;

internal class Callback
{
	private WebBrowser owner;

	private string currentUri;

	private bool calledLoadStarted;

	public Callback(WebBrowser owner)
	{
		this.owner = owner;
	}

	public void OnWidgetLoaded()
	{
	}

	public void OnStateChange(nsIWebProgress progress, nsIRequest request, int status, uint state)
	{
		if (!owner.created)
		{
			owner.created = true;
		}
		bool num = (state & 1) != 0;
		bool flag = (state & 4) != 0;
		bool flag2 = (state & 2) != 0;
		bool flag3 = (state & 0x10) != 0;
		bool flag4 = (state & 0x10000) != 0;
		bool flag5 = (state & 0x20000) != 0;
		bool flag6 = (state & 0x40000) != 0;
		bool flag7 = (state & 0x80000) != 0;
		if (num && flag4 && flag5 && !calledLoadStarted)
		{
			progress.getDOMWindow(out var ret);
			((nsIChannel)request).getURI(out var ret2);
			if (ret2 == null)
			{
				currentUri = "about:blank";
			}
			else
			{
				AsciiString asciiString = new AsciiString(string.Empty);
				ret2.getSpec(asciiString.Handle);
				currentUri = asciiString.ToString();
			}
			calledLoadStarted = true;
			LoadStartedEventHandler loadStartedEventHandler = (LoadStartedEventHandler)owner.Events[WebBrowser.LoadStartedEvent];
			if (loadStartedEventHandler != null)
			{
				AsciiString asciiString2 = new AsciiString(string.Empty);
				ret.getName(asciiString2.Handle);
				LoadStartedEventArgs loadStartedEventArgs = new LoadStartedEventArgs(currentUri, asciiString2.ToString());
				loadStartedEventHandler(this, loadStartedEventArgs);
				if (loadStartedEventArgs.Cancel)
				{
					request.cancel(2152398850u);
				}
			}
		}
		else if (flag5 && flag4 && flag)
		{
			progress.getDOMWindow(out var ret3);
			((nsIChannel)request).getURI(out var ret4);
			if (ret4 == null)
			{
				currentUri = "about:blank";
			}
			else
			{
				AsciiString asciiString3 = new AsciiString(string.Empty);
				ret4.getSpec(asciiString3.Handle);
				currentUri = asciiString3.ToString();
			}
			ret3.getTop(out var ret5);
			if (ret5 == null || ret5.GetHashCode() == ret3.GetHashCode())
			{
				owner.Reset();
				ret3.getDocument(out var ret6);
				if (ret6 != null)
				{
					owner.document = new Document(owner, ret6);
				}
			}
			LoadCommitedEventHandler loadCommitedEventHandler = (LoadCommitedEventHandler)owner.Events[WebBrowser.LoadCommitedEvent];
			if (loadCommitedEventHandler != null)
			{
				LoadCommitedEventArgs e = new LoadCommitedEventArgs(currentUri);
				loadCommitedEventHandler(this, e);
			}
		}
		else if (flag5 && flag4 && flag2)
		{
			progress.getDOMWindow(out var _);
			((nsIChannel)request).getURI(out var ret8);
			if (ret8 == null)
			{
				currentUri = "about:blank";
				return;
			}
			AsciiString asciiString4 = new AsciiString(string.Empty);
			ret8.getSpec(asciiString4.Handle);
			currentUri = asciiString4.ToString();
		}
		else if (flag3 && !flag4 && !flag5 && flag6 && flag7)
		{
			calledLoadStarted = false;
			LoadFinishedEventHandler loadFinishedEventHandler = (LoadFinishedEventHandler)owner.Events[WebBrowser.LoadFinishedEvent];
			if (loadFinishedEventHandler != null)
			{
				progress.getDOMWindow(out var _);
				LoadFinishedEventArgs e2 = new LoadFinishedEventArgs(currentUri);
				loadFinishedEventHandler(this, e2);
			}
		}
		else
		{
			if (!(flag3 && !flag4 && flag5) || flag6 || flag7)
			{
				return;
			}
			progress.getDOMWindow(out var ret10);
			ret10.getDocument(out var ret11);
			if (ret11 != null)
			{
				int hashCode = ret11.GetHashCode();
				if (owner.documents.ContainsKey(hashCode))
				{
					((EventHandler)(owner.documents[hashCode] as Document).Events[Document.LoadStoppedEvent])?.Invoke(this, null);
				}
			}
			calledLoadStarted = false;
		}
	}

	public void OnProgress(nsIWebProgress progress, nsIRequest request, int currentTotalProgress, int maxTotalProgress)
	{
		ProgressChangedEventHandler progressChangedEventHandler = (ProgressChangedEventHandler)owner.Events[WebBrowser.ProgressChangedEvent];
		if (progressChangedEventHandler != null)
		{
			ProgressChangedEventArgs e = new ProgressChangedEventArgs(currentTotalProgress, maxTotalProgress);
			progressChangedEventHandler(this, e);
		}
	}

	public void OnLocationChanged(nsIWebProgress progress, nsIRequest request, nsIURI uri)
	{
	}

	public void OnStatusChange(nsIWebProgress progress, nsIRequest request, string message, int status)
	{
		StatusChangedEventHandler statusChangedEventHandler = (StatusChangedEventHandler)owner.Events[WebBrowser.StatusChangedEvent];
		if (statusChangedEventHandler != null)
		{
			StatusChangedEventArgs e = new StatusChangedEventArgs(message, status);
			statusChangedEventHandler(this, e);
		}
	}

	public void OnSecurityChange(nsIWebProgress progress, nsIRequest request, uint status)
	{
		SecurityChangedEventHandler securityChangedEventHandler = (SecurityChangedEventHandler)owner.Events[WebBrowser.SecurityChangedEvent];
		if (securityChangedEventHandler != null)
		{
			SecurityLevel state = SecurityLevel.Insecure;
			switch (status)
			{
			case 4u:
				state = SecurityLevel.Insecure;
				break;
			case 1u:
				state = SecurityLevel.Mixed;
				break;
			case 2u:
				state = SecurityLevel.Secure;
				break;
			}
			SecurityChangedEventArgs e = new SecurityChangedEventArgs(state);
			securityChangedEventHandler(this, e);
		}
	}

	public bool OnClientDomKeyDown(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":keydown");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.KeyDownEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientDomKeyUp(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":keyup");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.KeyUpEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientDomKeyPress(KeyInfo keyInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":keypress");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.KeyPressEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseDown(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":mousedown");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseDownEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseUp(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":mouseup");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseUpEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseClick(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":click");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseClickEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseDoubleClick(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":dblclick");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseDoubleClickEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseOver(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode typedNode = new DOMObject(owner).GetTypedNode(target);
		string key = string.Intern(typedNode.GetHashCode() + ":mouseover");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(typedNode, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseEnterEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(typedNode);
			nodeEventHandler(typedNode, e2);
			return true;
		}
		return false;
	}

	public bool OnClientMouseOut(MouseInfo mouseInfo, ModifierKeys modifiers, nsIDOMNode target)
	{
		INode node = new Node(owner, target);
		string key = string.Intern(node.GetHashCode() + ":mouseout");
		EventHandler eventHandler = (EventHandler)owner.DomEvents[key];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(node, e);
		}
		NodeEventHandler nodeEventHandler = (NodeEventHandler)owner.Events[WebBrowser.MouseLeaveEvent];
		if (nodeEventHandler != null)
		{
			NodeEventArgs e2 = new NodeEventArgs(node);
			nodeEventHandler(this, e2);
			return true;
		}
		return false;
	}

	public bool OnClientActivate()
	{
		return false;
	}

	public bool OnClientFocus()
	{
		EventHandler eventHandler = (EventHandler)owner.Events[WebBrowser.FocusEvent];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(this, e);
		}
		return false;
	}

	public bool OnClientBlur()
	{
		EventHandler eventHandler = (EventHandler)owner.Events[WebBrowser.BlurEvent];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(this, e);
		}
		return false;
	}

	public bool OnCreateNewWindow()
	{
		bool result = false;
		CreateNewWindowEventHandler createNewWindowEventHandler = (CreateNewWindowEventHandler)owner.Events[WebBrowser.CreateNewWindowEvent];
		if (createNewWindowEventHandler != null)
		{
			CreateNewWindowEventArgs e = new CreateNewWindowEventArgs(isModal: false);
			result = createNewWindowEventHandler(this, e);
		}
		return result;
	}

	public void OnAlert(IntPtr title, IntPtr text)
	{
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.Alert;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			alertEventHandler(this, alertEventArgs);
		}
	}

	public bool OnAlertCheck(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState)
	{
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.AlertCheck;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (chkMsg != IntPtr.Zero)
			{
				alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
			}
			alertEventArgs.CheckState = chkState;
			alertEventHandler(this, alertEventArgs);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnConfirm(IntPtr title, IntPtr text)
	{
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.Confirm;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			alertEventHandler(this, alertEventArgs);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnConfirmCheck(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState)
	{
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.ConfirmCheck;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (chkMsg != IntPtr.Zero)
			{
				alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
			}
			alertEventArgs.CheckState = chkState;
			alertEventHandler(this, alertEventArgs);
			chkState = alertEventArgs.CheckState;
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnConfirmEx(IntPtr title, IntPtr text, DialogButtonFlags flags, IntPtr title0, IntPtr title1, IntPtr title2, IntPtr chkMsg, ref bool chkState, out int retVal)
	{
		retVal = -1;
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.ConfirmEx;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (chkMsg != IntPtr.Zero)
			{
				alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
			}
			alertEventArgs.CheckState = chkState;
			alertEventHandler(this, alertEventArgs);
			chkState = alertEventArgs.CheckState;
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnPrompt(IntPtr title, IntPtr text, ref IntPtr retVal)
	{
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.Prompt;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (retVal != IntPtr.Zero)
			{
				alertEventArgs.Text2 = Marshal.PtrToStringUni(retVal);
			}
			alertEventHandler(this, alertEventArgs);
			retVal = Marshal.StringToHGlobalUni(alertEventArgs.StringReturn);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnPromptUsernameAndPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr username, out IntPtr password)
	{
		username = IntPtr.Zero;
		password = IntPtr.Zero;
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.PromptUsernamePassword;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (chkMsg != IntPtr.Zero)
			{
				alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
			}
			alertEventArgs.CheckState = chkState;
			alertEventHandler(this, alertEventArgs);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnPromptPassword(IntPtr title, IntPtr text, IntPtr chkMsg, ref bool chkState, out IntPtr password)
	{
		password = IntPtr.Zero;
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.PromptPassword;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			if (chkMsg != IntPtr.Zero)
			{
				alertEventArgs.CheckMessage = Marshal.PtrToStringUni(chkMsg);
			}
			alertEventArgs.CheckState = chkState;
			alertEventHandler(this, alertEventArgs);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public bool OnSelect(IntPtr title, IntPtr text, uint count, IntPtr list, out int retVal)
	{
		retVal = 0;
		AlertEventHandler alertEventHandler = (AlertEventHandler)owner.Events[WebBrowser.AlertEvent];
		if (alertEventHandler != null)
		{
			AlertEventArgs alertEventArgs = new AlertEventArgs();
			alertEventArgs.Type = DialogType.Select;
			if (title != IntPtr.Zero)
			{
				alertEventArgs.Title = Marshal.PtrToStringUni(title);
			}
			if (text != IntPtr.Zero)
			{
				alertEventArgs.Text = Marshal.PtrToStringUni(text);
			}
			alertEventHandler(this, alertEventArgs);
			return alertEventArgs.BoolReturn;
		}
		return false;
	}

	public void OnLoad()
	{
		((Window)owner.Window).OnLoad();
	}

	public void OnUnload()
	{
		((Window)owner.Window).OnUnload();
	}

	public void OnShowContextMenu(uint contextFlags, [MarshalAs(UnmanagedType.Interface)] nsIDOMEvent eve, [MarshalAs(UnmanagedType.Interface)] nsIDOMNode node)
	{
		ContextMenuEventHandler contextMenuEventHandler = (ContextMenuEventHandler)owner.Events[WebBrowser.ContextMenuEvent];
		if (contextMenuEventHandler != null)
		{
			nsIDOMMouseEvent obj = (nsIDOMMouseEvent)eve;
			obj.getClientX(out var ret);
			obj.getClientY(out var ret2);
			ContextMenuEventArgs e = new ContextMenuEventArgs(ret, ret2);
			contextMenuEventHandler(owner, e);
		}
	}

	public void OnGeneric(string type)
	{
		EventHandler eventHandler = (EventHandler)owner.Events[WebBrowser.GenericEvent];
		if (eventHandler != null)
		{
			EventArgs e = new EventArgs();
			eventHandler(type, e);
		}
	}
}
