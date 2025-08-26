using UnityEngine;

public class StretchableObstacle : MonoBehaviour
{
	[SerializeField]
	private StretchableCube _stretchableCoreOutside;

	[SerializeField]
	private StretchableCube _stretchableCoreInside;

	[SerializeField]
	private Transform _stretchableCore;

	[SerializeField]
	private Transform _stretchableFrontTop;

	[SerializeField]
	private Transform _stretchableFrontBottom;

	[SerializeField]
	private Transform _stretchableFrontLeft;

	[SerializeField]
	private Transform _stretchableFrontRight;

	[SerializeField]
	private Transform _stretchableBackTop;

	[SerializeField]
	private Transform _stretchableBackBottom;

	[SerializeField]
	private Transform _stretchableBackLeft;

	[SerializeField]
	private Transform _stretchableBackRight;

	[SerializeField]
	private Transform _stretchableMiddleTopLeft;

	[SerializeField]
	private Transform _stretchableMiddleTopRight;

	[SerializeField]
	private Transform _stretchableMiddleBottomLeft;

	[SerializeField]
	private Transform _stretchableMiddleBottomRight;

	[SerializeField]
	private Transform _cornerFrontTopLeft;

	[SerializeField]
	private Transform _cornerFrontTopRight;

	[SerializeField]
	private Transform _cornerFrontBottomLeft;

	[SerializeField]
	private Transform _cornerFrontBottomRight;

	[SerializeField]
	private Transform _cornerBackTopLeft;

	[SerializeField]
	private Transform _cornerBackTopRight;

	[SerializeField]
	private Transform _cornerBackBottomLeft;

	[SerializeField]
	private Transform _cornerBackBottomRight;

	private Bounds _bounds;

	public Bounds bounds
	{
		get
		{
			return _bounds;
		}
	}

	public void SetSize(float width, float height, float length)
	{
		_bounds = new Bounds(new Vector3(0f, height * 0.5f, length * 0.5f), new Vector3(width, height, length));
		_stretchableCore.localScale = new Vector3(width, height, length);
		_stretchableCore.localPosition = new Vector3(0f, height * 0.5f, length * 0.5f);
		_stretchableCoreOutside.RefreshUVs();
		_stretchableCoreInside.RefreshUVs();
		Vector3 localScale = _stretchableFrontTop.localScale;
		localScale.x = width;
		_stretchableFrontTop.localScale = localScale;
		_stretchableFrontTop.localPosition = new Vector3(0f, height, 0f);
		_stretchableFrontBottom.localScale = localScale;
		_stretchableFrontBottom.localPosition = new Vector3(0f, 0f, 0f);
		_stretchableBackTop.localScale = localScale;
		_stretchableBackTop.localPosition = new Vector3(0f, height, length);
		_stretchableBackBottom.localScale = localScale;
		_stretchableBackBottom.localPosition = new Vector3(0f, 0f, length);
		localScale = _stretchableFrontLeft.localScale;
		localScale.y = height;
		_stretchableFrontLeft.localScale = localScale;
		_stretchableFrontLeft.localPosition = new Vector3((0f - width) * 0.5f, height * 0.5f, 0f);
		_stretchableFrontRight.localScale = localScale;
		_stretchableFrontRight.localPosition = new Vector3(width * 0.5f, height * 0.5f, 0f);
		_stretchableBackLeft.localScale = localScale;
		_stretchableBackLeft.localPosition = new Vector3((0f - width) * 0.5f, height * 0.5f, length);
		_stretchableBackRight.localScale = localScale;
		_stretchableBackRight.localPosition = new Vector3(width * 0.5f, height * 0.5f, length);
		localScale = _stretchableMiddleBottomLeft.localScale;
		localScale.z = length;
		_stretchableMiddleTopLeft.localScale = localScale;
		_stretchableMiddleTopLeft.localPosition = new Vector3((0f - width) * 0.5f, height, length * 0.5f);
		_stretchableMiddleTopRight.localScale = localScale;
		_stretchableMiddleTopRight.localPosition = new Vector3(width * 0.5f, height, length * 0.5f);
		_stretchableMiddleBottomLeft.localScale = localScale;
		_stretchableMiddleBottomLeft.localPosition = new Vector3((0f - width) * 0.5f, 0f, length * 0.5f);
		_stretchableMiddleBottomRight.localScale = localScale;
		_stretchableMiddleBottomRight.localPosition = new Vector3(width * 0.5f, 0f, length * 0.5f);
		_cornerFrontTopLeft.localPosition = new Vector3((0f - width) * 0.5f, height, 0f);
		_cornerFrontTopRight.localPosition = new Vector3(width * 0.5f, height, 0f);
		_cornerFrontBottomLeft.localPosition = new Vector3((0f - width) * 0.5f, 0f, 0f);
		_cornerFrontBottomRight.localPosition = new Vector3(width * 0.5f, 0f, 0f);
		_cornerBackTopLeft.localPosition = new Vector3((0f - width) * 0.5f, height, length);
		_cornerBackTopRight.localPosition = new Vector3(width * 0.5f, height, length);
		_cornerBackBottomLeft.localPosition = new Vector3((0f - width) * 0.5f, 0f, length);
		_cornerBackBottomRight.localPosition = new Vector3(width * 0.5f, 0f, length);
	}
}
