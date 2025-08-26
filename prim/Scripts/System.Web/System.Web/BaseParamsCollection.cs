using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Web;

internal abstract class BaseParamsCollection : WebROCollection
{
	protected HttpRequest _request;

	protected bool _loaded;

	public override string[] AllKeys
	{
		get
		{
			LoadInfo();
			return base.AllKeys;
		}
	}

	public override int Count
	{
		get
		{
			LoadInfo();
			return base.Count;
		}
	}

	public override KeysCollection Keys
	{
		get
		{
			LoadInfo();
			return base.Keys;
		}
	}

	public BaseParamsCollection(HttpRequest request)
	{
		_request = request;
		base.IsReadOnly = true;
	}

	private void LoadInfo()
	{
		if (!_loaded)
		{
			base.IsReadOnly = false;
			InsertInfo();
			base.IsReadOnly = true;
			_loaded = true;
		}
	}

	protected abstract void InsertInfo();

	public override string Get(int index)
	{
		LoadInfo();
		return base.Get(index);
	}

	protected abstract string InternalGet(string name);

	public override string Get(string name)
	{
		if (!_loaded)
		{
			string text = InternalGet(name);
			if (text != null && text.Length > 0)
			{
				return text;
			}
			LoadInfo();
		}
		return base.Get(name);
	}

	public override string GetKey(int index)
	{
		LoadInfo();
		return base.GetKey(index);
	}

	public override string[] GetValues(int index)
	{
		string text = Get(index);
		if (text == null)
		{
			return null;
		}
		return new string[1] { text };
	}

	public override string[] GetValues(string name)
	{
		string text = Get(name);
		if (text == null)
		{
			return null;
		}
		return new string[1] { text };
	}

	public override void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		throw new SerializationException();
	}

	public override IEnumerator GetEnumerator()
	{
		LoadInfo();
		return base.GetEnumerator();
	}
}
