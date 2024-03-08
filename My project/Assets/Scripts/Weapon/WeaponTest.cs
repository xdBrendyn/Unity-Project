using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponTest : MonoBehaviour {
    public PlayerInput Controls;
    public float fireCooldown = 0.5f;

    Camera PlayerCamera;
    InputAction Shoot;
    bool IsFiring;
    bool CanFire = true;

    private void Awake() {
        GetCamera();

        gameObject.tag = Controls.gameObject.tag;

        Shoot = Controls.actions.FindAction("Fire");
        Shoot.started += ctx => IsFiring = true;
        Shoot.canceled += ctx => IsFiring = false;
    }

    private void Update() {
        if (PlayerCamera != null && IsFiring && CanFire) {
            Fire();
            StartCoroutine(FireCooldown());
        }
    }

    void Fire() {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit)) {
            if (hit.collider.CompareTag(gameObject.tag)) {
                Debug.Log("Hit itself.");
            } else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast")) {
                Debug.Log("Hit on Ignore Raycast layer.");
            } else {
                Debug.Log("Hit on layer: " + LayerMask.LayerToName(hit.collider.gameObject.layer));
            }
        }
    }

    void GetCamera() {
        PlayerCamera = transform.parent.GetComponentInParent<Camera>();
    }

    IEnumerator FireCooldown() {
        CanFire = false;
        yield return new WaitForSeconds(fireCooldown);
        CanFire = true;
    }
}
