using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Channels;

/// <summary>Holds the stack of server channel sinks.</summary>
[ComVisible(true)]
public class ServerChannelSinkStack : IServerChannelSinkStack, IServerResponseChannelSinkStack
{
	private ChanelSinkStackEntry _sinkStack;

	/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ServerChannelSinkStack" /> class.</summary>
	public ServerChannelSinkStack()
	{
	}

	/// <summary>Returns the <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</summary>
	/// <param name="msg">The message to be serialized onto the requested stream.</param>
	/// <param name="headers">The headers retrieved from the server response stream.</param>
	/// <returns>The <see cref="T:System.IO.Stream" /> onto which the specified message is to be serialized.</returns>
	/// <exception cref="T:System.Runtime.Remoting.RemotingException">The sink stack is empty.</exception>
	public Stream GetResponseStream(IMessage msg, ITransportHeaders headers)
	{
		if (_sinkStack == null)
		{
			throw new RemotingException("The sink stack is empty");
		}
		return ((IServerChannelSink)_sinkStack.Sink).GetResponseStream(this, _sinkStack.State, msg, headers);
	}

	/// <summary>Pops the information associated with all the sinks from the sink stack up to and including the specified sink.</summary>
	/// <param name="sink">The sink to remove and return from the sink stack.</param>
	/// <returns>Information generated on the request side and associated with the specified sink.</returns>
	/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty, or the specified sink was never pushed onto the current stack.</exception>
	public object Pop(IServerChannelSink sink)
	{
		while (_sinkStack != null)
		{
			ChanelSinkStackEntry sinkStack = _sinkStack;
			_sinkStack = _sinkStack.Next;
			if (sinkStack.Sink == sink)
			{
				return sinkStack.State;
			}
		}
		throw new RemotingException("The current sink stack is empty, or the specified sink was never pushed onto the current stack");
	}

	/// <summary>Pushes the specified sink and information associated with it onto the sink stack.</summary>
	/// <param name="sink">The sink to push onto the sink stack.</param>
	/// <param name="state">Information generated on the request side that is needed on the response side.</param>
	public void Push(IServerChannelSink sink, object state)
	{
		_sinkStack = new ChanelSinkStackEntry(sink, state, _sinkStack);
	}

	/// <summary>Provides a <see cref="T:System.AsyncCallback" /> delegate to handle a callback after a message has been dispatched asynchronously.</summary>
	/// <param name="ar">The status and state of an asynchronous operation on a remote object.</param>
	[MonoTODO]
	public void ServerCallback(IAsyncResult ar)
	{
		throw new NotImplementedException();
	}

	/// <summary>Stores a message sink and its associated state for later asynchronous processing.</summary>
	/// <param name="sink">A server channel sink.</param>
	/// <param name="state">The state associated with <paramref name="sink" />.</param>
	/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.  
	///  -or-  
	///  The specified sink was never pushed onto the current stack.</exception>
	[MonoTODO]
	public void Store(IServerChannelSink sink, object state)
	{
		throw new NotImplementedException();
	}

	/// <summary>Stores a message sink and its associated state, and then dispatches a message asynchronously, using the sink just stored and any other stored sinks.</summary>
	/// <param name="sink">A server channel sink.</param>
	/// <param name="state">The state associated with <paramref name="sink" />.</param>
	[MonoTODO]
	public void StoreAndDispatch(IServerChannelSink sink, object state)
	{
		throw new NotImplementedException();
	}

	/// <summary>Requests asynchronous processing of a method call on the sinks in the current sink stack.</summary>
	/// <param name="msg">The message to be serialized onto the requested stream.</param>
	/// <param name="headers">The headers retrieved from the server response stream.</param>
	/// <param name="stream">The stream coming back from the transport sink.</param>
	/// <exception cref="T:System.Runtime.Remoting.RemotingException">The current sink stack is empty.</exception>
	public void AsyncProcessResponse(IMessage msg, ITransportHeaders headers, Stream stream)
	{
		if (_sinkStack == null)
		{
			throw new RemotingException("The current sink stack is empty");
		}
		ChanelSinkStackEntry sinkStack = _sinkStack;
		_sinkStack = _sinkStack.Next;
		((IServerChannelSink)sinkStack.Sink).AsyncProcessResponse(this, sinkStack.State, msg, headers, stream);
	}
}
