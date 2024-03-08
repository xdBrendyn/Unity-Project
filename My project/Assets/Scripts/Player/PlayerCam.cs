using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerCam : MonoBehaviour {
    [Header("Resources")]
    public Transform CameraPos;
    public Transform Orientation;
    public PlayerInput playerInput;

    [Header("Settings")]
    public float SensX;
    public float SensY;
    public Vector2 VerticalRotationLimits;

    float XRotation;
    float YRotation;
    Vector2 CameraInput;
    bool UsingGamepad;

    void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        CheckInputDeviceType();
        InputUser.onChange += OnInputUserChange;
    }

    #region InputReading

    public void OnLook(InputAction.CallbackContext context) {
        CameraInput = context.ReadValue<Vector2>();
    }

    private void OnDestroy() {
        InputUser.onChange -= OnInputUserChange;
    }

    private void CheckInputDeviceType() {
        if (playerInput.currentControlScheme == "Controller") {
            UsingGamepad = true;
            Debug.Log("Gamepad connected for user: " + playerInput.playerIndex);
        } else {
            UsingGamepad = false;
            Debug.Log("Keyboard/Mouse connected for user: " + playerInput.playerIndex);
        }
    }

    private void OnInputUserChange(InputUser user, InputUserChange change, InputDevice device) {
        if (change == InputUserChange.ControlSchemeChanged) {
            CheckInputDeviceType();
        }
    }

    #endregion

    void Update() {
        float mouseX = (UsingGamepad ? CameraInput.x : Input.GetAxisRaw("Mouse X")) * SensX * Time.deltaTime;
        float mouseY = (UsingGamepad ? CameraInput.y : Input.GetAxisRaw("Mouse Y")) * SensY * Time.deltaTime;

        YRotation += mouseX;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -VerticalRotationLimits.x, -VerticalRotationLimits.y);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);

        transform.position = CameraPos.position;
    }
}
