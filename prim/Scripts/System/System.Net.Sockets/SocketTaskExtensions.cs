using System.Collections.Generic;
using System.Threading.Tasks;

namespace System.Net.Sockets;

/// <summary>This class contains extension methods to the <see cref="T:System.Net.Sockets.Socket" /> class.</summary>
public static class SocketTaskExtensions
{
	/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
	/// <param name="socket">The socket that is listening for connections.</param>
	/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
	public static Task<Socket> AcceptAsync(this Socket socket)
	{
		return Task<Socket>.Factory.FromAsync((AsyncCallback callback, object state) => ((Socket)state).BeginAccept(callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), socket);
	}

	/// <summary>Performs an asynchronous operation on to accept an incoming connection attempt on the socket.</summary>
	/// <param name="socket">The socket that is listening for incoming connections.</param>
	/// <param name="acceptSocket">The accepted <see cref="T:System.Net.Sockets.Socket" /> object. This value may be <see langword="null" />.</param>
	/// <returns>An asynchronous task that completes with a <see cref="T:System.Net.Sockets.Socket" /> to handle communication with the remote host.</returns>
	public static Task<Socket> AcceptAsync(this Socket socket, Socket acceptSocket)
	{
		return Task<Socket>.Factory.FromAsync((Socket socketForAccept, int receiveSize, AsyncCallback callback, object state) => ((Socket)state).BeginAccept(socketForAccept, receiveSize, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndAccept(asyncResult), acceptSocket, 0, socket);
	}

	/// <summary>Establishes a connection to a remote host.</summary>
	/// <param name="socket">The socket that is used for establishing a connection.</param>
	/// <param name="remoteEP">An EndPoint that represents the remote device.</param>
	/// <returns>An asynchronous Task.</returns>
	public static Task ConnectAsync(this Socket socket, EndPoint remoteEP)
	{
		return Task.Factory.FromAsync((EndPoint targetEndPoint, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetEndPoint, callback, state), delegate(IAsyncResult asyncResult)
		{
			((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
		}, remoteEP, socket);
	}

	/// <summary>Establishes a connection to a remote host. The host is specified by an IP address and a port number.</summary>
	/// <param name="socket">The socket to perform the connect operation on.</param>
	/// <param name="address">The IP address of the remote host.</param>
	/// <param name="port">The port number of the remote host.</param>
	public static Task ConnectAsync(this Socket socket, IPAddress address, int port)
	{
		return Task.Factory.FromAsync((IPAddress targetAddress, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddress, targetPort, callback, state), delegate(IAsyncResult asyncResult)
		{
			((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
		}, address, port, socket);
	}

	/// <summary>Establishes a connection to a remote host. The host is specified by an array of IP addresses and a port number.</summary>
	/// <param name="socket">The socket that the connect operation is performed on.</param>
	/// <param name="addresses">The IP addresses of the remote host.</param>
	/// <param name="port">The port number of the remote host.</param>
	/// <returns>A task that represents the asynchronous connect operation.</returns>
	public static Task ConnectAsync(this Socket socket, IPAddress[] addresses, int port)
	{
		return Task.Factory.FromAsync((IPAddress[] targetAddresses, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetAddresses, targetPort, callback, state), delegate(IAsyncResult asyncResult)
		{
			((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
		}, addresses, port, socket);
	}

	/// <summary>Establishes a connection to a remote host. The host is specified by a host name and a port number.</summary>
	/// <param name="socket">The socket to perform the connect operation on.</param>
	/// <param name="host">The name of the remote host.</param>
	/// <param name="port">The port number of the remote host.</param>
	/// <returns>An asynchronous task.</returns>
	public static Task ConnectAsync(this Socket socket, string host, int port)
	{
		return Task.Factory.FromAsync((string targetHost, int targetPort, AsyncCallback callback, object state) => ((Socket)state).BeginConnect(targetHost, targetPort, callback, state), delegate(IAsyncResult asyncResult)
		{
			((Socket)asyncResult.AsyncState).EndConnect(asyncResult);
		}, host, port, socket);
	}

	/// <summary>Receives data from a connected socket.</summary>
	/// <param name="socket">The socket to perform the receive operation on.</param>
	/// <param name="buffer">An array that is the storage location for the received data.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
	public static Task<int> ReceiveAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
	{
		return Task<int>.Factory.FromAsync((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffer, socketFlags, socket);
	}

	/// <summary>Receives data from a connected socket.</summary>
	/// <param name="socket">The socket to perform the receive operation on.</param>
	/// <param name="buffers">An array that is the storage location for the received data.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <returns>A task that represents the asynchronous receive operation. The value of the <paramref name="TResult" /> parameter contains the number of bytes received.</returns>
	public static Task<int> ReceiveAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
	{
		return Task<int>.Factory.FromAsync((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginReceive(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndReceive(asyncResult), buffers, socketFlags, socket);
	}

	/// <summary>Receives data from a specified network device.</summary>
	/// <param name="socket">The socket to perform the ReceiveFrom operation on.</param>
	/// <param name="buffer">An array of type Byte that is the storage location for the received data.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <param name="remoteEndPoint">An EndPoint that represents the source of the data.</param>
	/// <returns>An asynchronous Task that completes with a SocketReceiveFromResult struct.</returns>
	public static Task<SocketReceiveFromResult> ReceiveFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
	{
		object[] state2 = new object[2] { socket, remoteEndPoint };
		return Task<SocketReceiveFromResult>.Factory.FromAsync(delegate(ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state)
		{
			object[] array = (object[])state;
			Socket socket2 = (Socket)array[0];
			EndPoint remoteEP = (EndPoint)array[1];
			IAsyncResult result = socket2.BeginReceiveFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, ref remoteEP, callback, state);
			array[1] = remoteEP;
			return result;
		}, delegate(IAsyncResult asyncResult)
		{
			object[] obj = (object[])asyncResult.AsyncState;
			Socket socket3 = (Socket)obj[0];
			EndPoint endPoint = (EndPoint)obj[1];
			int receivedBytes = socket3.EndReceiveFrom(asyncResult, ref endPoint);
			SocketReceiveFromResult result2 = default(SocketReceiveFromResult);
			result2.ReceivedBytes = receivedBytes;
			result2.RemoteEndPoint = endPoint;
			return result2;
		}, buffer, socketFlags, state2);
	}

	/// <summary>Receives the specified number of bytes of data into the specified location of the data buffer, using the specified <see cref="T:System.Net.Sockets.SocketFlags" />, and stores the endpoint and packet information.</summary>
	/// <param name="socket">The socket to perform the operation on.</param>
	/// <param name="buffer">An array that is the storage location for received data.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <param name="remoteEndPoint">An <see cref="T:System.Net.EndPoint" />, that represents the remote server.</param>
	/// <returns>An asynchronous Task that completes with a <see cref="T:System.Net.Sockets.SocketReceiveMessageFromResult" /> struct.</returns>
	public static Task<SocketReceiveMessageFromResult> ReceiveMessageFromAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEndPoint)
	{
		object[] state2 = new object[3] { socket, socketFlags, remoteEndPoint };
		return Task<SocketReceiveMessageFromResult>.Factory.FromAsync(delegate(ArraySegment<byte> targetBuffer, AsyncCallback callback, object state)
		{
			object[] array = (object[])state;
			Socket socket2 = (Socket)array[0];
			SocketFlags socketFlags2 = (SocketFlags)array[1];
			EndPoint remoteEP = (EndPoint)array[2];
			IAsyncResult result = socket2.BeginReceiveMessageFrom(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, socketFlags2, ref remoteEP, callback, state);
			array[2] = remoteEP;
			return result;
		}, delegate(IAsyncResult asyncResult)
		{
			object[] obj = (object[])asyncResult.AsyncState;
			Socket socket3 = (Socket)obj[0];
			SocketFlags socketFlags3 = (SocketFlags)obj[1];
			EndPoint endPoint = (EndPoint)obj[2];
			IPPacketInformation ipPacketInformation;
			int receivedBytes = socket3.EndReceiveMessageFrom(asyncResult, ref socketFlags3, ref endPoint, out ipPacketInformation);
			SocketReceiveMessageFromResult result2 = default(SocketReceiveMessageFromResult);
			result2.PacketInformation = ipPacketInformation;
			result2.ReceivedBytes = receivedBytes;
			result2.RemoteEndPoint = endPoint;
			result2.SocketFlags = socketFlags3;
			return result2;
		}, buffer, state2);
	}

	/// <summary>Sends data to a connected socket.</summary>
	/// <param name="socket">The socket to perform the operation on.</param>
	/// <param name="buffer">An array of type Byte that contains the data to send.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
	public static Task<int> SendAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags)
	{
		return Task<int>.Factory.FromAsync((ArraySegment<byte> targetBuffer, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffer, socketFlags, socket);
	}

	/// <summary>Sends data to a connected socket.</summary>
	/// <param name="socket">The socket to perform the operation on.</param>
	/// <param name="buffers">An array that contains the data to send.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <returns>An asynchronous task that completes with number of bytes sent to the socket if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
	public static Task<int> SendAsync(this Socket socket, IList<ArraySegment<byte>> buffers, SocketFlags socketFlags)
	{
		return Task<int>.Factory.FromAsync((IList<ArraySegment<byte>> targetBuffers, SocketFlags flags, AsyncCallback callback, object state) => ((Socket)state).BeginSend(targetBuffers, flags, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSend(asyncResult), buffers, socketFlags, socket);
	}

	/// <summary>Sends data asynchronously to a specific remote host.</summary>
	/// <param name="socket">The socket to perform the operation on.</param>
	/// <param name="buffer">An array that contains the data to send.</param>
	/// <param name="socketFlags">A bitwise combination of the <see cref="T:System.Net.Sockets.SocketFlags" /> values.</param>
	/// <param name="remoteEP">An <see cref="T:System.Net.EndPoint" /> that represents the remote device.</param>
	/// <returns>An asynchronous task that completes with number of bytes sent if the operation was successful. Otherwise, the task will complete with an invalid socket error.</returns>
	public static Task<int> SendToAsync(this Socket socket, ArraySegment<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
	{
		return Task<int>.Factory.FromAsync((ArraySegment<byte> targetBuffer, SocketFlags flags, EndPoint endPoint, AsyncCallback callback, object state) => ((Socket)state).BeginSendTo(targetBuffer.Array, targetBuffer.Offset, targetBuffer.Count, flags, endPoint, callback, state), (IAsyncResult asyncResult) => ((Socket)asyncResult.AsyncState).EndSendTo(asyncResult), buffer, socketFlags, remoteEP, socket);
	}
}
