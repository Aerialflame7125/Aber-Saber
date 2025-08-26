using ReplayDecoder.BLReplay;
using System;
using System.IO;
using System.Windows.Forms;
using UnityEngine; // Assuming this is linked correctly in your Unity project

namespace ReplayDecoder
{
    static class Program
    {
        [STAThread]
        public static void Main()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "BS Open Replay |*.bsor";
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            Stream stream = null; // Initialize to null
            try
            {
                stream = File.Open(ofd.FileName, FileMode.Open);
                byte[] replayData;

                // --- Performance Measurement (these loops are problematic for stream position) ---
                // These loops will consume the stream.
                // For accurate performance measurement of the *CopyTo* operation,
                // you would need to reopen or reset the stream before each measurement block.
                // As they are, they are effectively moving the stream's position to the end.

                DateTime start = DateTime.Now;
                for (int i = 0; i < 10000; i++)
                {
                    // To truly test CopyTo performance multiple times on the same data,
                    // you'd need to seek the stream back to the beginning here.
                    // For now, let's assume this is just for
                    // measuring the overhead of creating MemoryStream and ToArray.
                    // It's still moving the stream's position.
                    using (var ms = new MemoryStream()) // No need for initial capacity if you're copying the whole stream
                    {
                        stream.Position = 0; // Reset stream position for each copy operation
                        stream.CopyTo(ms);
                        // long length = ms.Length; // Not used
                        replayData = ms.ToArray();
                    }
                }
                DateTime end = DateTime.Now;

                // Reset stream for subsequent performance tests
                stream.Position = 0;
                DateTime start2 = DateTime.Now;
                for (int i = 0; i < 10000; i++)
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.Position = 0; // Reset stream position
                        stream.CopyTo(ms);
                        // long length = ms.Length;
                        replayData = ms.ToArray();
                    }
                }
                DateTime end2 = DateTime.Now;

                // Reset stream for subsequent performance tests
                stream.Position = 0;
                DateTime start3 = DateTime.Now;
                for (int i = 0; i < 10000; i++)
                {
                    using (var ms = new MemoryStream())
                    {
                        stream.Position = 0; // Reset stream position
                        stream.CopyTo(ms);
                        // long length = ms.Length;
                        replayData = ms.ToArray();
                    }
                }
                DateTime end3 = DateTime.Now;

                // Concatenate the strings correctly for Debug.Log
                Debug.Log(string.Format(" {0} ms | {1} ms | {2} ms",
                    (end - start).TotalMilliseconds,
                    (end2 - start2).TotalMilliseconds,
                    (end3 - start3).TotalMilliseconds));

                // --- Core Logic for Decoding Replay ---

                // IMPORTANT: Reset the stream's position to the beginning before reading the *entire* file for decoding.
                stream.Position = 0;

                int arrayLength = (int)stream.Length;
                byte[] buffer = new byte[arrayLength];
                int bytesRead = stream.Read(buffer, 0, arrayLength);

                // It's good practice to check if bytesRead matches arrayLength
                if (bytesRead != arrayLength)
                {
                    Debug.LogError("Failed to read the entire file. Expected " + arrayLength + " bytes, but read " + bytesRead + " bytes.");
                    return;
                }

                // Debug.Log(buffer) will print the type "byte[]", not the content.
                // If you want to see the content, you'd need to convert it to a string representation.
                // For example: Debug.Log(BitConverter.ToString(buffer));
                Debug.Log("Buffer length: " + buffer.Length); // Log the length to confirm it's not empty.

                Replay replay = BLReplay.ReplayDecoder.Decode(buffer); // Line 71

                // Check if replay is null after decoding, as Decode might return null on failure
                if (replay == null)
                {
                    Debug.LogError("Replay decoding failed. The 'replay' object is null.");
                    return;
                }

                int score1 = replay.info.score;
                int score2 = ScoreCalculator.CalculateScoreFromReplay(replay);
                ScoreStatistic statistic = ReplayStatisticUtils.ProcessReplay(replay);
                int score3 = statistic.WinTracker.TotalScore;

                var velocityStats = Bakery.VelocityStats(replay);

                // For breakpoint
                Debug.Log(score1 + "   " + score2 + "   " + score3);
            }
            catch (Exception ex)
            {
                Debug.LogError("An error occurred: " + ex.Message + "\nStack Trace:\n" + ex.StackTrace);
            }
            finally
            {
                // Ensure the stream is closed, even if an error occurs
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose(); // Dispose of the stream
                }
            }
        }
    }
}