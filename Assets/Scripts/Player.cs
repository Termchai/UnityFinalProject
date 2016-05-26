using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    public float rotatingSpeed = 150f;
    public GameObject arrow;
    public GameObject stickPrefabs;
    public GameObject hiligth;

    [SyncVar]
    public Color color;
    public int playerId;

    [SyncVar]
    private GameObject stick;

    void Start() {
        playerId = (int)GetComponent<NetworkIdentity>().netId.Value;

        color = GlobalData.colorsList[playerId % 2];
        SetColor(color);

        if (isLocalPlayer) {
            arrow.GetComponent<SpriteRenderer>().sortingOrder = 5;
            hiligth.SetActive(true);
            hiligth.GetComponent<SpriteRenderer>().sortingOrder = 4;

            //set camera
            CameraScript camera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
            camera.target = gameObject;

            //spawn position
            transform.position = new Vector3(0, 0, 0);
        }

    }
    void SetColor(Color color) {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++) {
            if (srs[i].name != "Hilight")
                srs[i].color = color;
        }
    }

    void Update() {
        if (isLocalPlayer) {
            if (stick == null) {
                Rotating();
            }
            if (Input.GetKeyDown(KeyCode.Space) && stick == null) {
                CmdShoot(GetComponent<NetworkIdentity>().netId.Value, transform.rotation);
                arrow.SetActive(false);
            }

            if (stick == null) {
                arrow.SetActive(true);
            }
        }

    }

    [Command]
    void CmdShoot(uint netId, Quaternion rotation) {
        Player[] players = GameObject.FindObjectsOfType<Player>();
        Player player = null;
        for (int i = 0; i < players.Length; i++) {
            if (players[i].GetComponent<NetworkIdentity>().netId.Value == netId) {
                player = players[i];
            }
        }

        var go = (GameObject)Instantiate(
            stickPrefabs,
            transform.position,
            Quaternion.identity);
        go.GetComponent<SpriteRenderer>().color = color;
        this.stick = go;
        Stick stick = go.GetComponent<Stick>();

        NetworkServer.Spawn(go);

        RpcSetStick(go);
        stick.CmdRotate(rotation);
        stick.CmdSetPlayer(playerId);
        stick.CmdSetColor(color);
    }

    [ClientRpc]
    public void RpcKill() {
        if (isLocalPlayer)
            Debug.Log("Dead");
    }

    [ClientRpc]
    void RpcSetStick(GameObject stick) {
        if (isLocalPlayer)
            this.stick = stick;
    }

    [ClientRpc]
    public void RpcMove(Vector3 destination) {
        if (isLocalPlayer) {
            transform.position = destination;
        }
    }

    void Rotating() {
        transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * rotatingSpeed);
    }

    [Command]
    void CmdProvideColorToServer(Color c) {
        color = c;
    }

    [ClientCallback]
    void TransmitColor() {
        if (isLocalPlayer) {
            CmdProvideColorToServer(color);
        }
    }

    public override void OnStartClient() {
        StartCoroutine(UpdateColor(1.5f));
    }

    IEnumerator UpdateColor(float time) {

        float timer = time;

        while (timer > 0) {
            timer -= Time.deltaTime;

            TransmitColor();
            if (!isLocalPlayer)
                SetColor(color);
            yield return null;
        }
    }
}
