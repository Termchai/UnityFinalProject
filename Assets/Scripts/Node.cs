using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Node : NetworkBehaviour {

    [SyncVar(hook = "OnChangeColorId")]
    private Color color = new Color(0.8f,0.8f,0.8f);

    public void Hit(Color color) {
        if (!isServer)
            return;
        this.color = color;
    }

    void OnChangeColorId(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }
    public override void OnStartClient() {
        GetComponent<SpriteRenderer>().color = color;
    }
}