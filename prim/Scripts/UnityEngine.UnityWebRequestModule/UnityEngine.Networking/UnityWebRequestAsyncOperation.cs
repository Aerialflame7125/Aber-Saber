using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Networking;

[StructLayout(LayoutKind.Sequential)]
[NativeHeader("UnityWebRequestScriptingClasses.h")]
[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequestAsyncOperation.h")]
[UsedByNativeCode]
public class UnityWebRequestAsyncOperation : AsyncOperation
{
	public UnityWebRequest webRequest { get; internal set; }
}
