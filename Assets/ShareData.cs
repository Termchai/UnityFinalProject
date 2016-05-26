using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ShareData :NetworkBehaviour {

    GameObject[] players;

    void Update() {
        if (!isServer) return;

        //players = GameObject.FindGameObjectsWithTag("Player");
    }
}

