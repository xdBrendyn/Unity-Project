using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Moves whatever gameObject it is attached to
/// </summary>
public class Movement : MonoBehaviour
{

    // ==============================
    // 
    //  The Amazing Movement Script
    //  
    // ==============================

    [Header("Resources")]
    public Rigidbody RB;

    [Header("Settings")]
    [Min(0.1f)] public float MovementSpeed;
    [Min(1.1f)] public float SprintSpeed; // Multiplier of the original MovementSpeed
    [Min(0.1f)] public float JumpStrength;
    

    Camera PlayerCamera;
    bool Grounded;
    bool Jumped;
    bool Sprinting;
    Vector2 MovementInput = Vector2.zero;

    void Awake()
    {
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        Move(); 
    }

    // Switched to the new input system to set up local multiplayer
    #region InputReading

    public void OnMove(InputAction.CallbackContext context) 
    {
        MovementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Jumped = context.action.triggered;
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        Sprinting = context.action.triggered;
    }

    #endregion

    void Move()
    {
        float x = MovementInput.x * MovementSpeed;
        float z = MovementInput.y * MovementSpeed;
        Vector3 cameraForward;
        Vector3 cameraRight;
        Vector3 movement;

        cameraForward = PlayerCamera.transform.forward;
        cameraRight = PlayerCamera.transform.right;
        cameraForward.y = 0f;
        cameraRight.y = 0f;
        cameraForward.Normalize();
        cameraRight.Normalize();

        movement = (cameraForward * z + cameraRight * x);

        if (Sprinting && Grounded)
        {
            movement *= SprintSpeed;
        }

        RB.velocity = new(movement.x, RB.velocity.y, movement.z);

        if (Jumped && Grounded)
        {   
            RB.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
            Grounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!Grounded && collision.gameObject.layer.Equals(3))
        {
            Grounded = true;
        }
    }
}
