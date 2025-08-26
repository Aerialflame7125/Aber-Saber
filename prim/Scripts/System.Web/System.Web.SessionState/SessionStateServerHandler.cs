using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Runtime.Remoting;
using System.Web.Configuration;

namespace System.Web.SessionState;

internal class SessionStateServerHandler : SessionStateStoreProviderBase
{
	private const int lockAcquireTimeout = 30000;

	private SessionStateSection config;

	private RemoteStateServer stateServer;

	public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
	{
		return new SessionStateStoreData(new SessionStateItemCollection(), HttpApplicationFactory.ApplicationState.SessionObjects, timeout);
	}

	public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
	{
		EnsureGoodId(id, throwOnNull: true);
		stateServer.CreateUninitializedItem(id, timeout);
	}

	public override void Dispose()
	{
	}

	public override void EndRequest(HttpContext context)
	{
	}

	private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
	{
		locked = false;
		lockAge = TimeSpan.MinValue;
		lockId = int.MinValue;
		actions = SessionStateActions.None;
		if (id == null)
		{
			return null;
		}
		StateServerItem item = stateServer.GetItem(id, out locked, out lockAge, out lockId, out actions, exclusive);
		if (item == null)
		{
			return null;
		}
		if (actions == SessionStateActions.InitializeItem)
		{
			return CreateNewStoreData(context, item.Timeout);
		}
		SessionStateItemCollection sessionItems = null;
		HttpStaticObjectsCollection staticObjects = null;
		MemoryStream memoryStream = null;
		BinaryReader binaryReader = null;
		Stream stream = null;
		GZipStream gZipStream = null;
		try
		{
			if (item.CollectionData != null && item.CollectionData.Length != 0)
			{
				memoryStream = new MemoryStream(item.CollectionData);
				stream = ((!config.CompressionEnabled) ? ((Stream)memoryStream) : ((Stream)(gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress, leaveOpen: true))));
				binaryReader = new BinaryReader(stream);
				sessionItems = SessionStateItemCollection.Deserialize(binaryReader);
				gZipStream?.Close();
				binaryReader.Close();
			}
			else
			{
				sessionItems = new SessionStateItemCollection();
			}
			staticObjects = ((item.StaticObjectsData == null || item.StaticObjectsData.Length == 0) ? new HttpStaticObjectsCollection() : HttpStaticObjectsCollection.FromByteArray(item.StaticObjectsData));
		}
		catch (Exception innerException)
		{
			throw new HttpException("Failed to retrieve session state.", innerException);
		}
		finally
		{
			memoryStream?.Dispose();
			binaryReader?.Dispose();
			gZipStream?.Dispose();
		}
		return new SessionStateStoreData(sessionItems, staticObjects, item.Timeout);
	}

	public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
	{
		EnsureGoodId(id, throwOnNull: false);
		return GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, exclusive: false);
	}

	public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
	{
		EnsureGoodId(id, throwOnNull: false);
		return GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, exclusive: true);
	}

	public override void Initialize(string name, NameValueCollection config)
	{
		this.config = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
		if (string.IsNullOrEmpty(name))
		{
			name = "Session Server handler";
		}
		RemotingConfiguration.Configure(null);
		string text = null;
		string proto = null;
		string server = null;
		string port = null;
		GetConData(out proto, out server, out port);
		text = $"{proto}://{server}:{port}/StateServer";
		stateServer = Activator.GetObject(typeof(RemoteStateServer), text) as RemoteStateServer;
		base.Initialize(name, config);
	}

	public override void InitializeRequest(HttpContext context)
	{
	}

	public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
	{
		EnsureGoodId(id, throwOnNull: true);
		stateServer.ReleaseItemExclusive(id, lockId);
	}

	public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
	{
		EnsureGoodId(id, throwOnNull: true);
		stateServer.Remove(id, lockId);
	}

	public override void ResetItemTimeout(HttpContext context, string id)
	{
		EnsureGoodId(id, throwOnNull: true);
		stateServer.ResetItemTimeout(id);
	}

	public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
	{
		if (item == null)
		{
			return;
		}
		EnsureGoodId(id, throwOnNull: true);
		byte[] collection_data = null;
		byte[] sobjs_data = null;
		MemoryStream memoryStream = null;
		BinaryWriter binaryWriter = null;
		Stream stream = null;
		GZipStream gZipStream = null;
		try
		{
			if (item.Items is SessionStateItemCollection { Count: >0 } sessionStateItemCollection)
			{
				memoryStream = new MemoryStream();
				stream = ((!config.CompressionEnabled) ? ((Stream)memoryStream) : ((Stream)(gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, leaveOpen: true))));
				binaryWriter = new BinaryWriter(stream);
				sessionStateItemCollection.Serialize(binaryWriter);
				gZipStream?.Close();
				binaryWriter.Close();
				collection_data = memoryStream.ToArray();
			}
			HttpStaticObjectsCollection staticObjects = item.StaticObjects;
			if (staticObjects != null && staticObjects.Count > 0)
			{
				sobjs_data = staticObjects.ToByteArray();
			}
		}
		catch (Exception innerException)
		{
			throw new HttpException("Failed to store session data.", innerException);
		}
		finally
		{
			binaryWriter?.Dispose();
			gZipStream?.Dispose();
			memoryStream?.Dispose();
		}
		stateServer.SetAndReleaseItemExclusive(id, collection_data, sobjs_data, lockId, item.Timeout, newItem);
	}

	public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
	{
		return false;
	}

	private void EnsureGoodId(string id, bool throwOnNull)
	{
		if (id == null)
		{
			if (throwOnNull)
			{
				throw new HttpException("Session ID is invalid");
			}
		}
		else if (id.Length > SessionIDManager.SessionIDMaxLength)
		{
			throw new HttpException("Session ID too long");
		}
	}

	private void GetConData(out string proto, out string server, out string port)
	{
		string stateConnectionString = config.StateConnectionString;
		int num = stateConnectionString.IndexOf('=');
		int num2 = stateConnectionString.IndexOf(':');
		if (num < 0 || num2 < 0)
		{
			throw new HttpException("Invalid StateConnectionString");
		}
		proto = stateConnectionString.Substring(0, num);
		server = stateConnectionString.Substring(num + 1, num2 - num - 1);
		port = stateConnectionString.Substring(num2 + 1, stateConnectionString.Length - num2 - 1);
		if (proto == "tcpip")
		{
			proto = "tcp";
		}
	}
}
