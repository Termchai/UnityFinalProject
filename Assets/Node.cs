using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Node : NetworkBehaviour {

    [SyncVar(hook = "OnChangeColorId")]
    private Color color = Color.gray;

    public void Hit(Color color) {
        if (!isServer)
            return;
        this.color = color;
    }

    void OnChangeColorId(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }
}