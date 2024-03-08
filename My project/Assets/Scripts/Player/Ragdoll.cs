using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour {
    public bool ToggleRagdoll;

    List<Rigidbody> Rigidbodies = new List<Rigidbody>();

    private void Awake() {
        GetRigidbodies(transform);
    }

    private void LateUpdate() {
        ToggleRagdollPhysics();
    }

    void GetRigidbodies(Transform currentTransform) {
        // Check if the current transform has a Rigidbody component
        Rigidbody rigidbody = currentTransform.GetComponent<Rigidbody>();

        if (rigidbody != null && rigidbody.transform != this.transform) {
            Rigidbodies.Add(rigidbody);
        }

        // Iterate through all child transforms
        foreach (Transform child in currentTransform) {
            GetRigidbodies(child);
        }
    }

    void ToggleRagdollPhysics() {
        // Disable ragdoll physics for each Rigidbody
        foreach (Rigidbody rb in Rigidbodies) {
            rb.isKinematic = !ToggleRagdoll;
        }
    }
}


