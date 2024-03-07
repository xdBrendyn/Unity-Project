using UnityEngine;

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

    void Awake()
    {
        PlayerCamera = Camera.main;
    }

    void Update()
    {
        Move(); 
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal") * MovementSpeed;
        float z = Input.GetAxis("Vertical") * MovementSpeed;
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

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            movement *= SprintSpeed;
        }

        RB.velocity = new(movement.x, RB.velocity.y, movement.z);

        if (Input.GetKey(KeyCode.Space) && Grounded)
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
