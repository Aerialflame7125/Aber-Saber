public class AVProLiveCameraDeviceMode
{
	private AVProLiveCameraDevice _device;

	private int _internalIndex;

	private int _width;

	private int _height;

	private float _fps;

	private string _format;

	public int Width => _width;

	public int Height => _height;

	public float FPS => _fps;

	public string Format => _format;

	public int InternalIndex => _internalIndex;

	public AVProLiveCameraDevice Device => _device;

	public AVProLiveCameraDeviceMode(AVProLiveCameraDevice device, int internalIndex, int width, int height, float fps, string format)
	{
		_device = device;
		_internalIndex = internalIndex;
		_width = width;
		_height = height;
		_fps = fps;
		_format = format;
	}
}
