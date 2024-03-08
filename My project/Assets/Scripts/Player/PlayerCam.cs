using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    [Header("Resources")]
    public Transform CameraPos;
    public Transform Orientation;

    [Header("Settings")]
    public float SensX;
    public float SensY;
    public Vector2 VerticalRotationLimits; // Locks how far up/down the camera can look - Not sure if I named it any better

    float XRotation;
    float YRotation;
    Vector2 CameraInput;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Switched to the new input system to set up local multiplayer
    #region InputReading

    public void OnLook(InputAction.CallbackContext context) 
    {
        CameraInput = context.ReadValue<Vector2>();
    }

    #endregion

    void Update() {
        float mouseX = CameraInput.x * Time.deltaTime * SensX;
        float mouseY = CameraInput.y * Time.deltaTime * SensY;

        YRotation += mouseX;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -VerticalRotationLimits.x, -VerticalRotationLimits.y);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);

        transform.position = CameraPos.position; // Put the "MoveCamera" line here
    }
}
