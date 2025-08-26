using System;
using System.Runtime.InteropServices;

public static class WinConsole
{
    public const int AttachParent = -1;

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool AllocConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool FreeConsole();

    [DllImport("kernel32.dll", SetLastError = true)]
    static extern bool AttachConsole(int dwProcessId);

    public static void Initialize(int processId)
    {
        // Detach any existing console
        FreeConsole();

        // Try attaching to parent or specific process
        if (!AttachConsole(processId))
        {
            // If attach fails, create a new console
            AllocConsole();
        }

        // Redirect standard output/input/error
        try
        {
            Console.SetOut(new System.IO.StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Console.SetError(new System.IO.StreamWriter(Console.OpenStandardError()) { AutoFlush = true });
            Console.SetIn(new System.IO.StreamReader(Console.OpenStandardInput()));
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogWarning("Failed to redirect console streams: " + e);
        }
    }
}
