﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Bullet : NetworkBehaviour {

    [SyncVar(hook = "OnChangeColorId")]
    private Color color = Color.gray;

    [SyncVar(hook = "OnChangeScale")]
    private Vector3 scale = new Vector3(0.2f, 0, 0);

    public float range = 0.1f;
    public float speed = 100f;
    public int id;
    public Player player;

    [Command]
    public void CmdSetPlayer(int id) {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++) {
            Player tmpPlayer = players[i].GetComponent<Player>();
            if (tmpPlayer.playerId == id) {
                player = tmpPlayer;
                break;
            }
        }
    }

    [Command]
    public void CmdRotate(Quaternion rotation) {
        transform.rotation = rotation;
    }

    [Command]
    public void CmdSetColor(Color color) {

        this.color = color;
    }

    void OnChangeColorId(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }

    void OnChangeScale(Vector3 scale) {
        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update() {
        if (!isServer) return;
        if (range <= 0) {
            Destroy(gameObject);
        }

        float move = speed * Time.deltaTime;
        range -= move;
        transform.position += transform.right * move;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!isServer) return;
        if (other.CompareTag("Wall") || (other.CompareTag("RedArea") && color == GlobalData.colorsList[1]) || (other.CompareTag("BlueArea") && color == GlobalData.colorsList[0])) {
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player")) {
            Player p = other.GetComponent<Player>();
            if (p.playerId != player.playerId) {
                p.RpcKill();
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Node") && other.transform.position != transform.position) {
            other.gameObject.GetComponent<Node>().Hit(color);
            Destroy(gameObject);
        }
    }
}
