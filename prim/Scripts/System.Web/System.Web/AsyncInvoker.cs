using System.Threading;

namespace System.Web;

internal class AsyncInvoker
{
	public BeginEventHandler begin;

	public EndEventHandler end;

	public object data;

	private HttpApplication app;

	private AsyncCallback callback;

	public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication a, object d)
	{
		begin = bh;
		end = eh;
		data = d;
		app = a;
		callback = doAsyncCallback;
	}

	public AsyncInvoker(BeginEventHandler bh, EndEventHandler eh, HttpApplication app)
		: this(bh, eh, app, null)
	{
	}

	public void Invoke(object sender, EventArgs e)
	{
		begin(app, e, callback, data);
	}

	private void doAsyncCallback(IAsyncResult res)
	{
		ThreadPool.QueueUserWorkItem(delegate(object ores)
		{
			IAsyncResult ar = (IAsyncResult)ores;
			try
			{
				end(ar);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.ToString());
			}
		}, res);
	}
}
