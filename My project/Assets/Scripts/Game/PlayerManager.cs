using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour {
    const string PlayerTagPrefix = "Player";
    const string PlayerLayerPrefix = "Player";

    int currentPlayerCount = 0;

    public void OnPlayerJoined(PlayerInput playerInput) {
        currentPlayerCount++;

        string playerTag = PlayerTagPrefix + currentPlayerCount;
        playerInput.gameObject.tag = playerTag;

        int playerLayer = LayerMask.NameToLayer(PlayerLayerPrefix + currentPlayerCount);
        playerInput.gameObject.layer = playerLayer;

        SetChildTags(playerInput.gameObject, playerTag, playerLayer);
    }

    public void OnPlayerLeft() {
        currentPlayerCount--;
    }

    void SetChildTags(GameObject obj, string tag, int playerLayer) {
        if (obj.name == "Alpha_Joints" || obj.name == "Alpha_Surface") {
            obj.layer = playerLayer;
        }

        obj.tag = tag;
        foreach (Transform child in obj.transform) {
            SetChildTags(child.gameObject, tag, playerLayer);
        }
    }

}
