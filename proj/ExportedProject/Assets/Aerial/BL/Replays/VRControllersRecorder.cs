using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; // Needed for InputTracking

namespace BL.Replays
{
    public class VRControllersRecorder : MonoBehaviour
    {
        // Public static instance for singleton pattern
        public static VRControllersRecorder Instance { get; private set; }

        [Header("Replay Metadata")]
        public string playerId = "2646159072086871";
        public string playerName = "Concreteeater";
        public string platform = "oculuspc";
        public string trackingSystem = "OpenXR";
        public string hmd = "Oculus Rift CV1";
        public string controller = "Oculus Touch Controller OpenXR";
        public string hash = "0F746AF53865A0C9598C826A7B525EF5A65646B6"; // map hash
        public string songName = "Hanavision";
        public string mapper = "VoltageO";
        public string difficulty = "ExpertPlus";
        public string mode = "Standard";
        public string environment = "TimbalandEnvironment";

        // Strict version values
        public string modVersion = "0.9.33";
        public string gameVersion = "0.11.1";

        // Runtime data
        private List<Frame> frames = new List<Frame>();
        private List<NoteEvent> notes = new List<NoteEvent>();
        public float startTime;
        private bool isRecording = false;

        private void Awake()
        {
            // Singleton setup: ensure only one instance exists
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional, but useful for scene transitions
        }

        private void Start()
        {
            Debug.Log("record");
            StartRecording();
        }

        private void Update()
        {
            if (!isRecording) return;

            // Capture frame
            Frame f = new Frame
            {
                time = Time.time - startTime,
                fps = (int)(1f / Time.deltaTime),
                head = CapturePose(InputTracking.GetLocalPosition(XRNode.Head), InputTracking.GetLocalRotation(XRNode.Head)),
                left = CapturePose(InputTracking.GetLocalPosition(XRNode.LeftHand), InputTracking.GetLocalRotation(XRNode.LeftHand)),
                right = CapturePose(InputTracking.GetLocalPosition(XRNode.RightHand), InputTracking.GetLocalRotation(XRNode.RightHand))
            };
            frames.Add(f);
        }

        public void StartRecording()
        {
            if (isRecording) return;
            frames.Clear();
            notes.Clear();
            startTime = Time.time;
            isRecording = true;
            Debug.Log("VR Replay Recording Started.");
        }

        public void RecordNoteEvent(NoteData noteData, NoteCutInfo noteCutInfo, float gameCurrentTime)
        {
            // 1. Declare and instantiate the new note event object
            NoteEvent newNoteEvent = new NoteEvent();
            newNoteEvent.eventTime = gameCurrentTime - startTime;
            newNoteEvent.spawnTime = noteData.time;

            // 2. Determine eventType based on noteCutInfo and noteData
            if (noteCutInfo != null)
            {
                if (noteCutInfo.allIsOK)
                {
                    newNoteEvent.eventType = 0; // Good cut
                    Debug.Log($"Note hit OK, Time: {newNoteEvent.spawnTime}");
                }
                else
                {
                    newNoteEvent.eventType = 1; // Bad cut
                    Debug.Log($"Note Bad Cut, Time: {newNoteEvent.spawnTime}");
                }
            }
            else // A note was missed
            {
                newNoteEvent.eventType = 2; // Miss
                Debug.Log($"Note Miss, Time: {newNoteEvent.spawnTime}");
            }

            // For bombs, the eventType should be 3 regardless of whether it was cut or not.
            if (noteData.noteType == NoteType.Bomb)
            {
                newNoteEvent.eventType = 3;
                Debug.Log($"NoteType BOMB, Time: {newNoteEvent.spawnTime}");
            }

            // 3. Encode noteID
            int scoringType = 0; // 0 for normal notes
            if (noteData.noteType == NoteType.Bomb) scoringType = 1;

            int lineIndex = noteData.lineIndex;
            int noteLayer = (int)noteData.noteLineLayer;
            int colorType = (int)noteData.noteType;
            int cutDir = (int)noteData.cutDirection;

            // Adjust cutDir for `None` to map to `Any` (usually 0) if needed
            if (cutDir == 9) cutDir = 0;

            newNoteEvent.noteID = scoringType * 10000 + lineIndex * 1000 + noteLayer * 100 + colorType * 10 + cutDir;

            // 4. Populate CutInfo for Good or Bad cuts only
            // This is where we create a new instance of the CutInfo class.
            if (newNoteEvent.eventType == 0 || newNoteEvent.eventType == 1)
            {
                // THIS IS THE CORRECTED LINE
                newNoteEvent.cutInfo = new CutInfo();

                newNoteEvent.cutInfo.speedOK = noteCutInfo.speedOK;
                newNoteEvent.cutInfo.directionOK = noteCutInfo.directionOK;
                newNoteEvent.cutInfo.saberTypeOK = noteCutInfo.saberTypeOK;
                newNoteEvent.cutInfo.wasCutTooSoon = noteCutInfo.wasCutTooSoon;
                newNoteEvent.cutInfo.saberSpeed = noteCutInfo.saberSpeed;
                newNoteEvent.cutInfo.saberDir = noteCutInfo.saberDir;
                newNoteEvent.cutInfo.saberType = (int)noteCutInfo.saberType;
                newNoteEvent.cutInfo.timeDeviation = noteCutInfo.timeDeviation;
                newNoteEvent.cutInfo.cutDirDeviation = noteCutInfo.cutDirDeviation;
                newNoteEvent.cutInfo.cutPoint = noteCutInfo.cutPoint;
                newNoteEvent.cutInfo.cutNormal = noteCutInfo.cutNormal;
                newNoteEvent.cutInfo.cutDistanceToCenter = noteCutInfo.cutDistanceToCenter;
                newNoteEvent.cutInfo.cutAngle = 0.0f;
                newNoteEvent.cutInfo.beforeCutRating = noteCutInfo.swingRating;
                newNoteEvent.cutInfo.afterCutRating = noteCutInfo.afterCutSwingRatingCounter?.rating ?? 0.0f;
            }

            // 5. Add the complete note event to the list
            notes.Add(newNoteEvent);
        }

        // Call this when the level ends
        public void StopAndSaveReplay(string path, int finalScore, float songEndTime)
        {
            if (!isRecording) return;
            isRecording = false;
            SaveReplay(path, finalScore, songEndTime);
            Debug.Log("Saving replay.");
        }

        private void SaveReplay(string path, int finalScore, float songEndTime)
        {
            using (BinaryWriter w = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                // Magic + version
                w.Write(0x442d3d69);
                w.Write((byte)1);

                // Section 0 Info
                w.Write((byte)0);
                WriteString(w, modVersion);
                WriteString(w, gameVersion);
                WriteString(w, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                WriteString(w, playerId);
                WriteString(w, playerName);
                WriteString(w, platform);
                WriteString(w, trackingSystem);
                WriteString(w, hmd);
                WriteString(w, controller);
                WriteString(w, hash);
                WriteString(w, songName);
                WriteString(w, mapper);
                WriteString(w, difficulty);
                w.Write(finalScore);
                WriteString(w, mode);
                WriteString(w, environment);
                WriteString(w, ""); // modifiers
                w.Write(24f); // jump distance default
                w.Write(false); // leftHanded
                w.Write(1.7f); // height default
                w.Write(0f); // startTime
                w.Write(songEndTime); // Fail time should be the total song duration
                w.Write(1f); // speed

                // Section 1 Frames
                w.Write((byte)1);
                w.Write(frames.Count);
                foreach (var f in frames) f.Write(w);

                // Section 2 Notes
                w.Write((byte)2);
                w.Write(notes.Count);
                foreach (var n in notes) n.Write(w);

                // Optional: Section 3 walls, Section 4 heights, Section 5 pauses
                w.Write((byte)3); w.Write(0);
                w.Write((byte)4); w.Write(0);
                w.Write((byte)5); w.Write(0);
            }
            Debug.Log("Replay saved to " + path);
        }

        // Helpers
        private void WriteString(BinaryWriter w, string s)
        {
            byte[] b = Encoding.UTF8.GetBytes(s);
            w.Write(b.Length);
            w.Write(b);
        }

        private PoseData CapturePose(Vector3 pos, Quaternion rot)
        {
            // Flip Z-axis for position
            Vector3 bsorPos = new Vector3(pos.x, pos.y, -pos.z);

            // Rotate the quaternion 180 degrees around the Y-axis
            Quaternion bsorRot = rot;

            return new PoseData
            {
                px = bsorPos.x,
                py = bsorPos.y,
                pz = bsorPos.z,
                rx = bsorRot.x,
                ry = bsorRot.y,
                rz = bsorRot.z,
                rw = bsorRot.w
            };
        }

        // Data structures
        [Serializable]
        public class CutInfo
        {
            public bool speedOK;
            public bool directionOK;
            public bool saberTypeOK;
            public bool wasCutTooSoon;

            public float saberSpeed;
            public Vector3 saberDir; // Use Vector3 for 3 floats
            public int saberType;
            public float timeDeviation;
            public float cutDirDeviation;
            public Vector3 cutPoint;
            public Vector3 cutNormal;
            public float cutDistanceToCenter;
            public float cutAngle;
            public float beforeCutRating;
            public float afterCutRating;

            public void Write(BinaryWriter w)
            {
                w.Write(speedOK);
                w.Write(directionOK);
                w.Write(saberTypeOK);
                w.Write(wasCutTooSoon);

                w.Write(saberSpeed);
                w.Write(saberDir.x); w.Write(saberDir.y); w.Write(saberDir.z);
                w.Write(saberType);
                w.Write(timeDeviation);
                w.Write(cutDirDeviation);
                w.Write(cutPoint.x); w.Write(cutPoint.y); w.Write(cutPoint.z);
                w.Write(cutNormal.x); w.Write(cutNormal.y); w.Write(cutNormal.z);
                w.Write(cutDistanceToCenter);
                w.Write(cutAngle);
                w.Write(beforeCutRating);
                w.Write(afterCutRating);
            }
        }

        // Update NoteEvent class to include CutInfo and write it conditionally
        [Serializable]
        public class NoteEvent
        {
            public int noteID;
            public float eventTime;
            public float spawnTime;
            public int eventType; // 0=good,1=bad,2=miss,3=bomb
            public CutInfo cutInfo; // Add a field for cut info

            public void Write(BinaryWriter w)
            {
                w.Write(noteID);
                w.Write(eventTime);
                w.Write(spawnTime);
                w.Write(eventType);

                // Conditional writing for CutInfo
                if (eventType == 0 || eventType == 1) // Good or Bad
                {
                    if (cutInfo != null)
                    {
                        cutInfo.Write(w);
                    }
                    else
                    {
                        Debug.LogError("Note didnt have cut info. This is a big problem.");
                    }
                }
            }
        }

        [Serializable]
        public class Frame
        {
            public float time;
            public int fps;
            public PoseData head, left, right;

            public void Write(BinaryWriter w)
            {
                w.Write(time);
                w.Write(fps);
                head.Write(w);
                left.Write(w);
                right.Write(w);
            }
        }

        [Serializable]
        public class PoseData
        {
            public float px, py, pz;
            public float rx, ry, rz, rw;

            public void Write(BinaryWriter w)
            {
                w.Write(px); w.Write(py); w.Write(pz);
                w.Write(rx); w.Write(ry); w.Write(rz); w.Write(rw);
            }
        }
    }
}
