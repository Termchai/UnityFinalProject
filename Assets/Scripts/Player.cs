using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {

    public float rotatingSpeed = 150f;
    public GameObject arrow;
    public GameObject stickPrefabs;

    [SyncVar]
    private Color color;
    private int playerId;

    private State st;

    enum State {
        Shooting,
        Moving,
        Idle,
        Controllable
    };

    void Start() {
        if (isLocalPlayer) {
            st = State.Idle;
            //set camera
            CameraScript camera = GameObject.Find("Main Camera").GetComponent<CameraScript>();
            camera.target = gameObject;

            playerId = (int)GetComponent<NetworkIdentity>().netId.Value;

            color = GlobalData.colorsList[playerId % 2];
            SetColor(color);

            //spawn position
            transform.position = new Vector3(0, 0, 0);
        }

    }
    void SetColor(Color color) {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < srs.Length; i++) {
            srs[i].color = color;
        }
    }

    void Update() {
        if (isLocalPlayer) {
            if (st == State.Idle) {
                arrow.SetActive(true);
                Rotating();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {

            CmdShoot(GetComponent<NetworkIdentity>().netId.Value);
            arrow.SetActive(false);
            st = State.Shooting;
        }
    }

    [Command]
    void CmdShoot(uint netId) {
        Player[] players = GameObject.FindObjectsOfType<Player>();
        Player player = null;
        for (int i = 0; i < players.Length; i++) {
            if (players[i].GetComponent<NetworkIdentity>().netId.Value == netId) {
                player = players[i];
            }
        }
        // Create the Bullet from the Bullet Prefab
        //GameObject bullet;
        //if(colorId =)
        var go = (GameObject)Instantiate(
            stickPrefabs,
            transform.position,
            Quaternion.identity);
        //stick = go.GetComponent<Stick>();
        //stick.id = this.id;

        NetworkServer.Spawn(go);
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
