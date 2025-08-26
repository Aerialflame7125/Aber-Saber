using System.Collections;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Web.Configuration;

namespace System.Web;

internal sealed class QueueManager
{
	private int minFree = 8;

	private int minLocalFree = 4;

	private int queueLimit = 5000;

	private Queue queue;

	private bool disposing;

	private Exception initialException;

	private PerformanceCounter requestsQueuedCounter;

	public bool HasException => initialException != null;

	public Exception InitialException => initialException;

	public QueueManager()
	{
		Exception ex = null;
		try
		{
			HttpRuntimeSection section = HttpRuntime.Section;
			if (section != null)
			{
				minFree = section.MinFreeThreads;
				minLocalFree = section.MinLocalRequestFreeThreads;
				queueLimit = section.AppRequestQueueLimit;
			}
		}
		catch (Exception ex2)
		{
			ex = ex2;
		}
		try
		{
			queue = new Queue(queueLimit);
		}
		catch (Exception ex3)
		{
			if (ex == null)
			{
				initialException = ex3;
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder("Several exceptions occurred:\n");
				stringBuilder.AppendFormat("--- Exception Q1:\n{0}\n", ex.ToString());
				stringBuilder.AppendFormat("--- Exception Q2:\n{0}\n", ex3.ToString());
				initialException = new Exception(stringBuilder.ToString());
			}
		}
		if (initialException == null && ex != null)
		{
			initialException = ex;
		}
		requestsQueuedCounter = new PerformanceCounter("ASP.NET", "Requests Queued");
		requestsQueuedCounter.RawValue = 0L;
	}

	private bool CanExecuteRequest(HttpWorkerRequest req)
	{
		if (disposing)
		{
			return false;
		}
		ThreadPool.GetAvailableThreads(out var workerThreads, out var _);
		bool flag = req != null && req.GetLocalAddress() == "127.0.0.1";
		if (workerThreads <= minFree)
		{
			if (flag)
			{
				return workerThreads > minLocalFree;
			}
			return false;
		}
		return true;
	}

	public HttpWorkerRequest GetNextRequest(HttpWorkerRequest req)
	{
		if (!CanExecuteRequest(req))
		{
			if (!disposing && req != null)
			{
				lock (queue)
				{
					Queue(req);
				}
			}
			return null;
		}
		HttpWorkerRequest httpWorkerRequest;
		lock (queue)
		{
			httpWorkerRequest = Dequeue();
			if (httpWorkerRequest != null)
			{
				if (req != null)
				{
					Queue(req);
				}
			}
			else
			{
				httpWorkerRequest = req;
			}
		}
		return httpWorkerRequest;
	}

	private void Queue(HttpWorkerRequest wr)
	{
		if (queue.Count < queueLimit)
		{
			queue.Enqueue(wr);
			requestsQueuedCounter.Increment();
		}
		else
		{
			HttpRuntime.FinishUnavailable(wr);
		}
	}

	private HttpWorkerRequest Dequeue()
	{
		if (queue.Count > 0)
		{
			HttpWorkerRequest result = (HttpWorkerRequest)queue.Dequeue();
			requestsQueuedCounter.Decrement();
			return result;
		}
		return null;
	}

	public void Dispose()
	{
		if (!disposing)
		{
			disposing = true;
			HttpWorkerRequest nextRequest;
			while ((nextRequest = GetNextRequest(null)) != null)
			{
				HttpRuntime.FinishUnavailable(nextRequest);
			}
			queue = null;
		}
	}
}
