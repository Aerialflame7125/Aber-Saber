namespace System.Windows.Forms.Design;

internal interface IMessageReceiver
{
	void WndProc(ref Message m);
}
