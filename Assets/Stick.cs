using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Stick : NetworkBehaviour {

    [SyncVar(hook = "OnChangeColorId")]
    private Color color = Color.gray;

    enum State {
        Shrinking,
        Stretching,
        Idle,
        Linked
    };

    public float range = 3f;
    public float speed = 100f;
    public int id;
    public Player player;

    private State state = State.Stretching;

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
    public void CmdSetColor(Color color) {
        if(!isServer) return;
        this.color = color;
    }

    void OnChangeColorId(Color color) {
        GetComponent<SpriteRenderer>().color = color;
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(state);
        if (state == State.Idle) {
            Destroy(gameObject);
        }
        else if (state == State.Shrinking) {
            transform.localScale += new Vector3(0, -0.1f * Time.deltaTime * speed, 0);
            if (transform.localScale.y <= 0) {
                transform.localScale = new Vector3(transform.localScale.x, 0, transform.localScale.z);
                state = State.Idle;
            }
        }
        else if (state == State.Stretching) {
            transform.localScale += new Vector3(0, 0.1f * Time.deltaTime * speed, 0);
            if (transform.localScale.y >= range) {
                transform.localScale = new Vector3(transform.localScale.x, range, transform.localScale.z);
                state = State.Shrinking;
            }
        }
    }

    public void Shrink() {
        Debug.Log("Shrink");
        state = State.Shrinking;
    }

    public void Stretch() {
        Debug.Log("Stretch");
        state = State.Stretching;
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("HIT " + other);

        if (other.CompareTag("Wall")) {
            Debug.Log("Hit Wall");
            Shrink();
        }
        if (other.CompareTag("Node") && other.transform.position != transform.position) {
            Debug.Log("Hit Node 1-" + player);
            Debug.Log("Hit Node 2-" + other.gameObject.GetComponent<Node>());
            other.gameObject.GetComponent<Node>().Hit(player.color);

            Vector3 target = other.transform.position;
            Vector3 from = player.transform.position;

            Vector3 relativePos = target - from;

            //player.Move(other.transform.position);

            if (relativePos.x != 0) {
                transform.rotation = Quaternion.LookRotation(-relativePos);
                transform.position = other.transform.position;
                transform.Rotate(new Vector3(0, 1, 0), -90);
                transform.Rotate(new Vector3(0, 0, 1), -90);



                if (transform.rotation.eulerAngles.y < -175 || transform.rotation.eulerAngles.y > 175) {
                    transform.Rotate(new Vector3(0, 1, 0), 180);
                }

                transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z));
            }
            else {
                transform.rotation = new Quaternion();
                if (relativePos.y > 0f)
                    transform.Rotate(new Vector3(0, 0, 1), 180);
                transform.position = other.transform.position;
            }

            Shrink();
        }
    }

    public bool isIdle() {
        return state == State.Idle;
    }
}
