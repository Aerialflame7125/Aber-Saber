using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;

public class VerboseLogger : MonoBehaviour
{
    [Header("Logger Settings")]
    [SerializeField] private bool showLogs = true;
    [SerializeField] private bool showWarnings = true;
    [SerializeField] private bool showErrors = true;
    [SerializeField] private int maxLogEntries = 100;
    [SerializeField] private KeyCode toggleKey = KeyCode.BackQuote; // ` key

    // Window settings
    private bool showLogWindow = false;
    private Rect logWindowRect = new Rect(20, 20, 500, 400);
    private Vector2 scrollPosition;

    // Log storage
    private List<LogEntry> logEntries = new List<LogEntry>();
    private Dictionary<LogType, Color> logTypeColors = new Dictionary<LogType, Color>
    {
        { LogType.Log, Color.white },
        { LogType.Warning, new Color(1f, 0.9f, 0.4f) },
        { LogType.Error, new Color(1f, 0.5f, 0.5f) },
        { LogType.Exception, new Color(1f, 0.3f, 0.3f) },
        { LogType.Assert, new Color(1f, 0.3f, 0.6f) }
    };

    // GUI styles
    private GUIStyle logStyle;
    private GUIStyle headerStyle;
    private GUIStyle entryStyle;
    private GUIStyle toggleButtonStyle;

    private bool verboseModeEnabled = false;

    private struct LogEntry
    {
        public string Message;
        public LogType Type;
        public DateTime Time;

        public LogEntry(string message, LogType type)
        {
            Message = message;
            Type = type;
            Time = DateTime.Now;
        }
    }

    private void Awake()
    {
        // Check command line arguments for --verbose
        string[] args = Environment.GetCommandLineArgs();
        verboseModeEnabled = args.Contains("--verbose");

        if (verboseModeEnabled)
        {
            Debug.Log("[VerboseLogger] Verbose mode enabled via launch parameter.");
            showLogWindow = true;
        }

        // Register for log messages
        Application.logMessageReceived += HandleLog;

        // Set this to DontDestroyOnLoad so it persists between scene changes
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        // Unregister from log messages to prevent memory leaks
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Create a new log entry
        LogEntry entry = new LogEntry(logString, type);

        // Add stack trace for errors and exceptions
        if (type == LogType.Error || type == LogType.Exception)
        {
            entry.Message += "\n" + stackTrace;
        }

        // Add to our log list
        logEntries.Add(entry);

        // Keep the list size under control
        if (logEntries.Count > maxLogEntries)
        {
            logEntries.RemoveAt(0);
        }

        // Auto-scroll to bottom in the next frame
        scrollPosition.y = float.MaxValue;
    }

    private void Update()
    {
        // Toggle log window with the configured key
        if (Input.GetKeyDown(toggleKey))
        {
            showLogWindow = !showLogWindow;
        }
    }

    private void OnGUI()
    {
        if (!showLogWindow)
        {
            /*
            // Show a small indicator button that the logger is active
            if (GUI.Button(new Rect(Screen.width - 100, 10, 90, 30), "Show Logs"))
            {
                showLogWindow = true;
            }
            */
            return;
        }

        InitializeGUIStyles();

        // Main log window
        logWindowRect = GUILayout.Window(123456, logWindowRect, DrawLogWindow, "Verbose Logger");
    }

    private void InitializeGUIStyles()
    {
        if (logStyle == null)
        {
            logStyle = new GUIStyle(GUI.skin.label);
            logStyle.fontSize = 12;
            logStyle.wordWrap = true;

            headerStyle = new GUIStyle(GUI.skin.box);
            headerStyle.fontSize = 12;
            headerStyle.alignment = TextAnchor.MiddleCenter;
            headerStyle.fontStyle = FontStyle.Bold;

            entryStyle = new GUIStyle(GUI.skin.box);
            entryStyle.alignment = TextAnchor.UpperLeft;
            entryStyle.wordWrap = true;
            entryStyle.richText = true;

            toggleButtonStyle = new GUIStyle(GUI.skin.button);
            toggleButtonStyle.padding = new RectOffset(2, 2, 2, 2);
        }
    }

    private void DrawLogWindow(int windowID)
    {
        GUILayout.BeginVertical();

        // Header with controls
        GUILayout.BeginHorizontal(GUI.skin.box);

        // Toggle buttons for log types
        GUI.color = showLogs ? Color.green : Color.gray;
        if (GUILayout.Toggle(showLogs, "Info", toggleButtonStyle) != showLogs)
            showLogs = !showLogs;

        GUI.color = showWarnings ? Color.yellow : Color.gray;
        if (GUILayout.Toggle(showWarnings, "Warnings", toggleButtonStyle) != showWarnings)
            showWarnings = !showWarnings;

        GUI.color = showErrors ? Color.red : Color.gray;
        if (GUILayout.Toggle(showErrors, "Errors", toggleButtonStyle) != showErrors)
            showErrors = !showErrors;

        GUI.color = Color.white;

        // Clear and close buttons
        if (GUILayout.Button("Clear", GUILayout.Width(60)))
        {
            logEntries.Clear();
        }

        if (GUILayout.Button("Close", GUILayout.Width(60)))
        {
            showLogWindow = false;
        }

        GUILayout.EndHorizontal();

        // Log entries scroll view
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUI.skin.box);

        foreach (LogEntry entry in logEntries)
        {
            // Filter by type
            if ((entry.Type == LogType.Log && !showLogs) ||
                (entry.Type == LogType.Warning && !showWarnings) ||
                ((entry.Type == LogType.Error || entry.Type == LogType.Exception || entry.Type == LogType.Assert) && !showErrors))
            {
                continue;
            }

            // Select color based on log type
            GUI.color = logTypeColors.ContainsKey(entry.Type) ? logTypeColors[entry.Type] : Color.white;

            // Display timestamp and log type
            GUILayout.BeginHorizontal();
            GUILayout.Label($"[{entry.Time.ToString("HH:mm:ss")}] [{entry.Type}]", logStyle, GUILayout.Width(150));

            // Display message (with word wrap)
            GUILayout.Label(entry.Message, logStyle);
            GUILayout.EndHorizontal();

            GUI.color = Color.white;

            // Add a separator line
            GUILayout.Box("", GUILayout.Height(1), GUILayout.ExpandWidth(true));
        }

        GUILayout.EndScrollView();

        GUILayout.EndVertical();

        // Make the window draggable
        GUI.DragWindow();
    }
}