public static class BLConstants
{
    // Adjust these as needed for your specific setup
    public const string BEATLEADER_API_URL = "https://api.beatleader.net/";
    public const string SIGNIN_WITH_TICKET = BEATLEADER_API_URL + "signin";
    public const int MaintenanceStatus = 503; // Example HTTP status code for maintenance
    public const int Unauthorized = 401; // Example HTTP status code for unauthorized
    // Dynamically build this if possible, or set a sensible default
    // For standalone, this might be simpler than the C++ version
    public static readonly string USER_AGENT = "BeatLeader/1.0.0 (BeatSaber/0.11.1) (Oculus)";
}

// Plugin.Log (or just use UnityEngine.Debug.Log directly)
// If you have a custom logging setup, ensure it's accessible.
// For this example, I'll use UnityEngine.Debug.Log
/*
namespace BeatLeader.API
{
    public static class Plugin
    {
        public static class Log
        {
            public static void Info(string message) => UnityEngine.Debug.Log($"[BeatLeader] INFO: {message}");
            public static void Debug(string message) => UnityEngine.Debug.Log($"[BeatLeader] DEBUG: {message}");
            public static void Error(string message) => UnityEngine.Debug.LogError($"[BeatLeader] ERROR: {message}");
            public static void Critical(string message) => UnityEngine.Debug.LogError($"[BeatLeader] CRITICAL: {message}");
        }
    }
}
*/