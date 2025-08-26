using System;
using System.Runtime.InteropServices;
using System.Text;

public class AVProLiveCameraPlugin
{
	public enum VideoFrameFormat
	{
		RAW_BGRA32,
		YUV_422_YUY2,
		YUV_422_UYVY,
		YUV_422_YVYU,
		YUV_422_HDYC,
		YUV_420_PLANAR_YV12,
		YUV_420_PLANAR_I420,
		RAW_RGB24,
		RAW_MONO8
	}

	public enum VideoInput
	{
		None,
		Video_Tuner,
		Video_Composite,
		Video_SVideo,
		Video_RGB,
		Video_YRYBY,
		Video_Serial_Digital,
		Video_Parallel_Digital,
		Video_SCSI,
		Video_AUX,
		Video_1394,
		Video_USB,
		Video_Decoder,
		Video_Encoder,
		Video_SCART,
		Video_Black
	}

	public enum PluginEvent
	{
		UpdateAllTextures
	}

	public const int PluginID = 262275072;

	[DllImport("AVProLiveCamera")]
	public static extern IntPtr GetRenderEventFunc();

	[DllImport("AVProLiveCamera")]
	public static extern bool Init(bool supportInternalConversion);

	[DllImport("AVProLiveCamera")]
	public static extern void Deinit();

	[DllImport("AVProLiveCamera")]
	private static extern IntPtr GetPluginVersion();

	public static string GetPluginVersionString()
	{
		return Marshal.PtrToStringAnsi(GetPluginVersion());
	}

	[DllImport("AVProLiveCamera")]
	public static extern int GetNumDevices();

	[DllImport("AVProLiveCamera")]
	public static extern bool HasDeviceConfigWindow(int index);

	[DllImport("AVProLiveCamera")]
	public static extern bool ShowDeviceConfigWindow(int index);

	[DllImport("AVProLiveCamera", CharSet = CharSet.Unicode)]
	private static extern bool GetDeviceName(int index, StringBuilder nameBuffer, int nameBufferLength);

	public static bool GetDeviceName(int index, out string name)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetDeviceName(index, stringBuilder, stringBuilder.Capacity))
		{
			name = stringBuilder.ToString();
			return true;
		}
		name = string.Empty;
		return false;
	}

	[DllImport("AVProLiveCamera")]
	private static extern bool GetDeviceGUID(int index, StringBuilder nameBuffer, int nameBufferLength);

	public static bool GetDeviceGUID(int index, out string name)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetDeviceGUID(index, stringBuilder, stringBuilder.Capacity))
		{
			name = stringBuilder.ToString();
			return true;
		}
		name = string.Empty;
		return false;
	}

	[DllImport("AVProLiveCamera")]
	public static extern bool IsDeviceConnected(int index);

	[DllImport("AVProLiveCamera")]
	public static extern bool UpdateDevicesConnected();

	[DllImport("AVProLiveCamera")]
	public static extern int GetNumModes(int index);

	[DllImport("AVProLiveCamera")]
	private static extern bool GetModeInfo(int deviceIndex, int modeIndex, out int width, out int height, out float fps, StringBuilder format);

	public static bool GetModeInfo(int deviceIndex, int modeIndex, out int width, out int height, out float fps, out string format)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetModeInfo(deviceIndex, modeIndex, out width, out height, out fps, stringBuilder))
		{
			format = stringBuilder.ToString();
			return true;
		}
		format = string.Empty;
		return false;
	}

	[DllImport("AVProLiveCamera")]
	public static extern int GetNumVideoInputs(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	private static extern bool GetVideoInputName(int deviceIndex, int inputIndex, StringBuilder name);

	public static bool GetVideoInputName(int deviceIndex, int inputIndex, out string name)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetVideoInputName(deviceIndex, inputIndex, stringBuilder))
		{
			name = stringBuilder.ToString();
			return true;
		}
		name = string.Empty;
		return false;
	}

	[DllImport("AVProLiveCamera")]
	public static extern void SetVideoInputByIndex(int deviceIndex, int inputIndex);

	[DllImport("AVProLiveCamera")]
	public static extern int GetNumDeviceVideoSettings(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	private static extern bool GetDeviceVideoSettingInfo(int deviceIndex, int settingIndex, out int settingType, out int dataType, StringBuilder name, out bool canAutomatic);

	public static bool GetDeviceVideoSettingInfo(int deviceIndex, int settingIndex, out int settingType, out int dataType, out string name, out bool canAutomatic)
	{
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetDeviceVideoSettingInfo(deviceIndex, settingIndex, out settingType, out dataType, stringBuilder, out canAutomatic))
		{
			name = stringBuilder.ToString();
			return true;
		}
		name = string.Empty;
		return false;
	}

	[DllImport("AVProLiveCamera")]
	public static extern bool GetDeviceVideoSettingBoolean(int deviceIndex, int settingIndex, out bool defaultValue, out bool currentValue, out bool isAutomatic);

	[DllImport("AVProLiveCamera")]
	public static extern bool GetDeviceVideoSettingFloat(int deviceIndex, int settingIndex, out float defaultValue, out float currentValue, out float minValue, out float maxValue, out bool isAutomatic);

	[DllImport("AVProLiveCamera")]
	public static extern bool UpdateDeviceVideoSettingValue(int deviceIndex, int settingIndex, out float currentValue, out bool isAutomatic);

	[DllImport("AVProLiveCamera")]
	public static extern void ApplyDeviceVideoSettingValue(int deviceIndex, int settingIndex, float currentValue, bool isAutomatic);

	[DllImport("AVProLiveCamera")]
	public static extern bool StartDevice(int index, int modeIndex, int videoInputIndex);

	[DllImport("AVProLiveCamera")]
	public static extern void StopDevice(int index);

	[DllImport("AVProLiveCamera")]
	public static extern bool Play(int index);

	[DllImport("AVProLiveCamera")]
	public static extern void Pause(int index);

	[DllImport("AVProLiveCamera")]
	public static extern void Stop(int index);

	[DllImport("AVProLiveCamera")]
	public static extern int GetWidth(int index);

	[DllImport("AVProLiveCamera")]
	public static extern int GetHeight(int index);

	[DllImport("AVProLiveCamera")]
	public static extern float GetFrameRate(int index);

	[DllImport("AVProLiveCamera")]
	public static extern long GetFrameDurationHNS(int index);

	[DllImport("AVProLiveCamera")]
	public static extern int GetFormat(int index);

	[DllImport("AVProLiveCamera")]
	private static extern bool GetDeviceFormat(int index, StringBuilder format);

	public static string GetDeviceFormat(int deviceIndex)
	{
		string result = "Unknown";
		StringBuilder stringBuilder = new StringBuilder(512);
		if (GetDeviceFormat(deviceIndex, stringBuilder))
		{
			result = stringBuilder.ToString();
		}
		return result;
	}

	[DllImport("AVProLiveCamera")]
	public static extern bool IsFrameTopDown(int index);

	[DllImport("AVProLiveCamera")]
	public static extern uint GetLastFrame(int index);

	[DllImport("AVProLiveCamera")]
	public static extern bool SetActive(int index, bool active);

	[DllImport("AVProLiveCamera")]
	public static extern bool IsNextFrameReadyForGrab(int index);

	[DllImport("AVProLiveCamera")]
	public static extern int GetLastFrameUploaded(int handle);

	[DllImport("AVProLiveCamera")]
	public static extern bool UpdateTextureGL(int index, int textureID);

	[DllImport("AVProLiveCamera")]
	public static extern bool GetFramePixels(int index, IntPtr buffer, int bufferIndex, int bufferWidth, int bufferHeight);

	[DllImport("AVProLiveCamera")]
	public static extern void SetTexturePointer(int index, int bufferIndex, IntPtr texturePtr);

	[DllImport("AVProLiveCamera")]
	public static extern bool GetFrameAsColor32(int index, IntPtr bufferPtr, int bufferWidth, int bufferHeight);

	[DllImport("AVProLiveCamera")]
	public static extern float GetCaptureFrameRate(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	public static extern uint GetCaptureFramesDropped(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	public static extern void SetFrameBufferSize(int deviceIndex, int read, int write);

	[DllImport("AVProLiveCamera")]
	public static extern long GetLastFrameBufferedTime(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	public static extern IntPtr GetLastFrameBuffered(int deviceIndex);

	[DllImport("AVProLiveCamera")]
	public static extern IntPtr GetFrameFromBufferAtTime(int deviceIndex, long time);
}
