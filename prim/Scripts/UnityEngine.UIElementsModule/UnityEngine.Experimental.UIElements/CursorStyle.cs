namespace UnityEngine.Experimental.UIElements;

public struct CursorStyle
{
	public Texture2D texture { get; set; }

	public Vector2 hotspot { get; set; }

	internal int defaultCursorId { get; set; }

	public override int GetHashCode()
	{
		return texture.GetHashCode() ^ hotspot.GetHashCode() ^ defaultCursorId.GetHashCode();
	}

	public override bool Equals(object other)
	{
		if (!(other is CursorStyle cursorStyle))
		{
			return false;
		}
		return texture.Equals(cursorStyle.texture) && hotspot.Equals(cursorStyle.hotspot) && defaultCursorId == cursorStyle.defaultCursorId;
	}

	public static bool operator ==(CursorStyle lhs, CursorStyle rhs)
	{
		return lhs.texture == rhs.texture && lhs.hotspot == rhs.hotspot;
	}

	public static bool operator !=(CursorStyle lhs, CursorStyle rhs)
	{
		return !(lhs == rhs);
	}
}
