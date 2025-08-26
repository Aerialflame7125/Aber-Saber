using UnityEngine;
using System;
using System.IO;

public class GameLogger : MonoBehaviour
{
    private string logFilePath;
    private StreamWriter logWriter;

    void OnEnable()
    {
        // Determine the root game directory and create the Logs folder if it doesn't exist.
        // Application.dataPath points to the Assets folder in the editor, or the _Data folder in a build.
        // Path.GetDirectoryName moves up one level to the project root in editor, or the game executable directory in a build.
        string logDirectory = Path.Combine(Path.GetDirectoryName(Application.dataPath), "Logs");
        if (!Directory.Exists(logDirectory))
        {
            Directory.CreateDirectory(logDirectory);
        }

        // Create a unique log file name using the current date and time.
        string logFileName = $"_{DateTime.Now:yyyy.MM.dd}.log";
        logFilePath = Path.Combine(logDirectory, logFileName);

        // Open the log file for writing. 'true' in StreamWriter constructor means append mode.
        // AutoFlush ensures that messages are written to the file immediately.
        try
        {
            logWriter = new StreamWriter(logFilePath, true) { AutoFlush = true };
            // Subscribe to Unity's log message event.
            Application.logMessageReceived += HandleLog;
            logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss} | GameLogger] Logger initialized. Log file: {logFilePath}");
            DontDestroyOnLoad(this);
        }
        catch (Exception e)
        {
            // If the log file cannot be opened, log an error to Unity's console.
            Debug.LogError($"Failed to open log file: {e.Message}");
        }
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--verbose")
            {
                int processId = WinConsole.AttachParent;

                if (i + 1 < args.Length && int.TryParse(args[i + 1], out int parsedId))
                {
                    processId = parsedId;
                }

                WinConsole.Initialize(processId);

                // Optional: Redirect Unity logs to console
                Application.logMessageReceived += HandleLog;
            }
        }
        
    }

    void OnDisable()
    {
        // Unsubscribe from the log message event when the GameObject is disabled or destroyed.
        Application.logMessageReceived -= HandleLog;
        if (logWriter != null)
        {
            // Write a shutdown message and close the log file.
            logWriter.WriteLine($"[{DateTime.Now:HH:mm:ss} | GameLogger] Logger shut down.");
            logWriter.Close();
            logWriter.Dispose();
        }
    }

    // This method is called every time Unity logs a message.
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logWriter == null) return;

        System.Console.WriteLine($"[{type}] {logString}");
        if (type == LogType.Error || type == LogType.Exception)
            System.Console.WriteLine(stackTrace);

        string level = "";
        // Map Unity's LogType to your desired log level string.
        switch (type)
        {
            case LogType.Error:
            case LogType.Exception:
                level = "ERROR";
                break;
            case LogType.Warning:
                level = "WARNING";
                break;
            case LogType.Log:
            case LogType.Assert: // Treating Assert as INFO for general notices
                level = "INFO"; // Corresponds to "notice"
                break;
            default:
                level = "UNKNOWN";
                break;
        }

        // Attempt to extract a source from the log string, similar to your example.
        // This is a simple heuristic; Unity logs often don't explicitly state a "source" field in this format.
        // If not found, it defaults to "Unity".
        string source = "Unity";
        int pipeIndex = logString.IndexOf('|');
        int closingBracketIndex = logString.IndexOf(']');
        if (pipeIndex != -1 && closingBracketIndex != -1 && pipeIndex < closingBracketIndex)
        {
            string potentialSourcePart = logString.Substring(pipeIndex + 1, closingBracketIndex - (pipeIndex + 1)).Trim();
            if (!string.IsNullOrEmpty(potentialSourcePart) && !potentialSourcePart.Contains(" "))
            {
                source = potentialSourcePart;
            }
        }

        // Format the log message string.
        string formattedLog = $"[{level} @ {DateTime.Now:HH:mm:ss} | {source}] {logString}";
        logWriter.WriteLine(formattedLog);

        // Include stack trace for errors and exceptions.
        if (type == LogType.Error || type == LogType.Exception)
        {
            logWriter.WriteLine(stackTrace);
        }
    }
}