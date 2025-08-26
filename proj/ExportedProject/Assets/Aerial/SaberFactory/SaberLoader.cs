using UnityEngine;
using System.IO;
using System.Reflection;

namespace Aerial.Saber
{
    public class SaberLoader : MonoBehaviour
    {
        private const string TrailWidthFile = "saberTrailWidth.txt";
        [SerializeField] private float defaultPodiumWidth = 0.06f; // Z scale if no file yet

        [Header("Saber Transforms")]
        public Transform leftController;
        public Transform rightController;

        [Space]
        [Header("Current Sabers")]
        public Transform currentLeftController;
        public Transform currentRightController;

        [Space]
        [Header("Podium")]
        public Transform podiumTransform;
        public float positionX;
        public float positionY;
        public float positionZ;
        public float rotationY;
        public float rotationX;
        public float rotationZ;
        public float scaleX;
        public float scaleY;
        public float scaleZ;
        string path;


        [Space]
        [Header("Saber Specific")]
        [SerializeField] private XWeaponTrailRenderer xWeaponTrailRendererPrefab;
        [SerializeField] private SaberTypeObject leftSaberType;
        [SerializeField] private SaberTypeObject rightSaberType;
        [SerializeField] private ColorManager colorManager;

        void Start()
        {
            CheckForSaber();
            DontDestroyOnLoad(this);
        }

        public void CheckForSaber()
        {
            if (!File.Exists(Path.Combine(Application.persistentDataPath, "saberSelection.txt")))
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, "saberSelection.txt"), "none");
                Debug.LogError("Selected Saber set to none.");
            }
            else
            {
                path = File.ReadAllText(Path.Combine(Application.persistentDataPath, "saberSelection.txt"));

                if (podiumTransform != null)
                {
                    LoadSelectedSaber(path, true);
                } 
                else
                {
                    LoadSelectedSaber(path, false);
                }
                
                Debug.LogError($"Selected Saber loaded from file: {path}");
            }
        }

        public void LoadSelectedSaber(string saberPath, bool podium = false)
        {
            if (string.IsNullOrEmpty(saberPath) || !File.Exists(saberPath))
            {
                Debug.LogError("[SaberLoader] No saber path found.");
                return;
            }

            var bundle = AssetBundle.LoadFromFile(saberPath);
            if (bundle == null)
            {
                Debug.LogError($"[SaberLoader] Failed to load saber bundle: {saberPath}");
                return;
            }

            // Find first prefab in this saber
            var prefabs = bundle.LoadAllAssets<GameObject>();
            if (prefabs.Length == 0)
            {
                Debug.LogError("[SaberLoader] No prefabs found in saber bundle!");
                bundle.Unload(false);
                return;
            }

            var saberPrefab = prefabs[0];
            Debug.LogError(prefabs[0].name);
            var saber = Instantiate(saberPrefab);
            if (!podium)
            {
                AttachToControllers(saber);
            }
            else
            {
                AttachToControllers(saber, podium, true, positionX, positionY, positionZ, rotationX, rotationY, rotationZ, scaleX, scaleY, scaleZ);
            }

            // Free unused assets but keep instances
            bundle.Unload(false);
        }

        private void AttachToControllers(GameObject saberRoot, bool podium = false,
                                 bool loadTrail = false,
                                 float x = 0.0f, float y = 0.0f, float z = 0.0f,
                                 float rx = 0.0f, float ry = 0.0f, float rz = 0.0f,
                                 float sx = 0.0f, float sy = 0.0f, float sz = 0.0f)
        {
            if (!podium)
            {
                var leftSaber = saberRoot.transform.Find("LeftSaber");
                var rightSaber = saberRoot.transform.Find("RightSaber");

                if (leftSaber != null && leftController != null)
                {
                    leftSaber.SetParent(leftController, false);
                    if (currentLeftController != null)
                        Destroy(currentLeftController.gameObject);

                    leftSaber.localPosition = new Vector3(0, 0, 0);
                    SetupTrail(leftSaber);
                    currentLeftController = leftSaber;
                }

                if (rightSaber != null && rightController != null)
                {
                    rightSaber.SetParent(rightController, false);
                    if (currentRightController != null)
                        Destroy(currentRightController.gameObject);

                    rightSaber.localPosition = new Vector3(0, 0, 0);
                    SetupTrail(rightSaber);
                    currentRightController = rightSaber;
                }
            }
            else
            {
                var leftSaber = saberRoot.transform.Find("LeftSaber");
                var rightSaber = saberRoot.transform.Find("RightSaber");

                var target = leftSaber != null ? leftSaber : rightSaber;

                if (target != null && podiumTransform != null)
                {
                    target.SetParent(podiumTransform, false);
                    target.localPosition = new Vector3(x, y, z);
                    target.localRotation = Quaternion.Euler(rx, ry, rz);
                    target.localScale = new Vector3(sx, sy, sz);

                    SetupTrail(target, true);
                    if (currentLeftController != null)
                        Destroy(currentLeftController.gameObject);
                    if (currentRightController != null)
                        Destroy(currentRightController.gameObject);
                }
                else
                {
                    Debug.LogError("[SaberLoader] Could not attach saber to podium.");
                }
            }

            Debug.Log("[SaberLoader] Attached saber(s) to new parent.");
        }

        private void SetupTrail(Transform saber, bool podium = false)
        {
            var trailData = saber.GetComponent<CustomSaber.CustomTrail>();

            if (trailData == null)
            {
                Debug.LogWarning($"[SaberLoader] No CustomTrail data on {saber.name}");
                return;
            }

            // Fallback path (no material): keep your existing SaberWeaponTrail setup
            if (trailData.TrailMaterial == null)
            {
                Debug.Log($"[SaberLoader] No TrailMaterial for {saber.name}, adding SaberWeaponTrail.");

                var saberTrail = saber.gameObject.GetComponent<SaberWeaponTrail>();
                if (saberTrail == null)
                    saberTrail = saber.gameObject.AddComponent<SaberWeaponTrail>();

                saberTrail.SetTrailRender(xWeaponTrailRendererPrefab);
                saberTrail.SetPointEnd(trailData.PointStart != null ? trailData.PointStart : saber);

                if (trailData.PointEnd != null)
                {
                    saberTrail.SetPointStart(trailData.PointEnd);
                }
                else
                {
                    var endObj = new GameObject("TrailEnd");
                    endObj.transform.SetParent(saber);
                    endObj.transform.localPosition = new Vector3(0f, 0.05f, 0f);
                    saberTrail.SetPointStart(endObj.transform);
                }

                saberTrail.SetMF(20);
                saberTrail.SetGran(60);
                saberTrail.SetSFF(4);

                if (trailData.colorType == CustomSaber.ColorType.LeftSaber)
                    saberTrail.SetSaberType(leftSaberType);
                else if (trailData.colorType == CustomSaber.ColorType.RightSaber)
                    saberTrail.SetSaberType(rightSaberType);

                saberTrail.SetColorManager(colorManager);
                saberTrail.SetColor(new Color32(255, 255, 255, 64));
                return;
            }

            // --- Normal TrailRenderer path if TrailMaterial is present ---
            Transform tip = trailData.PointStart != null ? trailData.PointStart : saber;  // trail "tip"
            Transform bottom = trailData.PointEnd;                                        // trail "bottom"

            // Ensure we have a bottom point one way or another
            if (bottom == null)
            {
                var endObj = new GameObject("TrailBottom");
                endObj.transform.SetParent(saber);
                // Place a little down the saber as a sensible default
                endObj.transform.localPosition = new Vector3(0f, -Mathf.Max(0.05f, trailData.Length * 0.01f), 0f);
                bottom = endObj.transform;
            }

            // Build/ensure a TrailRenderer (it will be disabled for podium)
            var trail = tip.GetComponent<TrailRenderer>();
            if (trail == null) trail = tip.gameObject.AddComponent<TrailRenderer>();

            trail.time = Mathf.Max(0.05f, trailData.Length * 0.01f);
            trail.startWidth = 0.1f;
            trail.endWidth = 0.0f;
            trail.material = trailData.TrailMaterial;

            Color c;
            switch (trailData.colorType)
            {
                case CustomSaber.ColorType.LeftSaber: c = colorManager.colorA; break;
                case CustomSaber.ColorType.RightSaber: c = colorManager.colorB; break;
                case CustomSaber.ColorType.CustomColor:
                default: c = trailData.TrailColor; break;
            }
            trail.startColor = c;
            trail.endColor = new Color(c.r, c.g, c.b, 0f);

            if (podium)
            {
                // Disable the dynamic TrailRenderer for podium showcase
                trail.emitting = false;
                trail.enabled = false;

                // Read or create saved width (Z scale)
                float zWidth = GetSavedTrailWidth();

                // Create a static 3D strip between tip and bottom
                BuildPodiumTrail(tip, bottom, zWidth, trailData.TrailMaterial);

                Debug.Log($"[SaberLoader] Podium trail built between tip '{tip.name}' and bottom '{bottom.name}', Z width {zWidth} (saved).");
            }

            Debug.Log($"[SaberLoader] Trail set up for {saber.name} with color {c}");
        }

        // --- NEW: create the static 3D trail object for podium ---
        private void BuildPodiumTrail(Transform tip, Transform bottom, float zWidth, Material trailMat)
        {
            // Compute length and direction from tip to bottom in world space
            Vector3 tipPos = tip.position;
            Vector3 bottomPos = bottom.position;
            Vector3 dir = (bottomPos - tipPos);
            float length = dir.magnitude;

            if (length <= 0.0001f)
            {
                Debug.LogWarning("[SaberLoader] Podium trail length is zero; skipping.");
                return;
            }

            // Create a cube and parent it to the tip so its "top" is at the tip
            GameObject strip = GameObject.CreatePrimitive(PrimitiveType.Cube);
            strip.name = "PodiumTrailMesh";
            strip.transform.SetParent(tip, worldPositionStays: true);

            // Remove collider
            var col = strip.GetComponent<Collider>();
            if (col != null) Destroy(col);

            // Align local +X to dir
            strip.transform.rotation = Quaternion.FromToRotation(Vector3.right, dir.normalized);

            // Position center at half the distance along +X so its near end (at tip) touches the tip
            strip.transform.position = tipPos + dir.normalized * (length * 0.5f);

            // Scale:
            //   X = distance (tip → bottom)
            //   Y = fixed thickness 0.02
            //   Z = saved width (file-backed)
            strip.transform.localScale = new Vector3(length, 0.02f, Mathf.Max(0.0001f, zWidth));

            // Use trail material if available, otherwise a default
            var mr = strip.GetComponent<MeshRenderer>();
            if (mr != null && trailMat != null)
            {
                mr.sharedMaterial = trailMat;
            }
        }

        // --- NEW: file-backed width (Z scale) ---
        private float GetSavedTrailWidth()
        {
            try
            {
                string file = Path.Combine(Application.persistentDataPath, TrailWidthFile);
                if (File.Exists(file))
                {
                    var txt = File.ReadAllText(file).Trim();
                    if (float.TryParse(txt, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float val))
                    {
                        return Mathf.Max(0f, val);
                    }
                }

                // If not found or parse failed, save default and return it
                SaveTrailWidth(defaultPodiumWidth);
                return defaultPodiumWidth;
            }
            catch
            {
                // In case of any IO/parse issue, fall back to default
                return defaultPodiumWidth;
            }
        }

        // Call this from an options UI if you want to let players change it
        public void SaveTrailWidth(float width)
        {
            try
            {
                string file = Path.Combine(Application.persistentDataPath, TrailWidthFile);
                File.WriteAllText(file, width.ToString(System.Globalization.CultureInfo.InvariantCulture));
            }
            catch { /* ignore */ }
        }
    }
}

