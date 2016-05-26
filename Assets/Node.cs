using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Node : NetworkBehaviour {

    [SyncVar(hook = "OnChangeColorId")]
    public int colorId = -1;

    public RectTransform healthBar;

    public void Hit(int colorId) {
        if (!isServer)
            return;
        Debug.Log("NODE HIT : " + colorId);
        this.colorId = colorId;
    }

    void OnChangeColorId(int colorId) {
        GetComponent<SpriteRenderer>().color = GlobalData.colorsList[colorId];
    }
}