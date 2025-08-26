using UnityEngine;
using System;
using System.Linq;

public class FirstPersonController : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private float moveSpeed = 5.0f;

    [Header("Following GameObject")]
    [SerializeField] private GameObject objectToFollow;
    [SerializeField] private Vector3 followOffset = new Vector3(0, 0, 0);

    private float cameraPitch = 0.0f;
    private bool fpfcEnabled = false;

    void Start()
    {
        // Check for -fpfc launch parameter
        string[] args = Environment.GetCommandLineArgs();
        fpfcEnabled = args.Contains("-fpfc");

        if (fpfcEnabled)
        {
            Debug.Log("First Person Camera mode enabled via launch parameter");

            // Lock and hide cursor when in first person mode
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Make sure we have a camera reference
            if (playerCamera == null)
                playerCamera = Camera.main;
        }
        else
        {
            Debug.Log("First Person Camera mode disabled. Use -fpfc launch parameter to enable.");
            // Disable this component if FPFC is not enabled
            this.enabled = false;
        }
    }

    void Update()
    {
        if (!fpfcEnabled)
            return;

        // Handle camera rotation
        UpdateMouseLook();

        // Handle movement
        UpdateMovement();

        // Update the following object's position
        UpdateFollowingObject();
    }

    void UpdateMouseLook()
    {
        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Adjust pitch for vertical rotation
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        // Apply rotations
        playerCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void UpdateMovement()
    {
        // Get input for movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        moveDirection.Normalize();

        // Apply movement
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    void UpdateFollowingObject()
    {
        if (objectToFollow != null)
        {
            // Make the object follow the camera with the specified offset
            objectToFollow.transform.position = playerCamera.transform.position +
                                               playerCamera.transform.rotation * followOffset;

            // Match the rotation of the camera
            objectToFollow.transform.rotation = playerCamera.transform.rotation;
        }
    }
}