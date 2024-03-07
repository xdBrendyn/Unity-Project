using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("Resources")]
    public Transform CameraPos;
    public Transform Orientation;

    [Header("Settings")]
    public float SensX;
    public float SensY;
    public Vector2 RangeOfMotion; // Locks how far up/down the camera can look (IDK what the fuck to name this)

    float XRotation;
    float YRotation;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update() {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * SensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * SensY;

        YRotation += mouseX;
        XRotation -= mouseY;
        XRotation = Mathf.Clamp(XRotation, -RangeOfMotion.x, -RangeOfMotion.y);

        transform.rotation = Quaternion.Euler(XRotation, YRotation, 0);
        Orientation.rotation = Quaternion.Euler(0, YRotation, 0);

        transform.position = CameraPos.position; // Put the "MoveCamera" line here
    }
}
