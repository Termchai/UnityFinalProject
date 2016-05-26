using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Node : NetworkBehaviour {

    public Statistic statistic;

    [SyncVar(hook = "OnChangeColorId")]
    private Color color = new Color(0.8f,0.8f,0.8f);

    public void Hit(Color color) {
        if (!isServer)
            return;
        this.color = color;
    }

    void OnChangeColorId(Color color) {
        if (GetComponent<SpriteRenderer>().color == color) return;

        if (isServer) {
            if (GetComponent<SpriteRenderer>().color == GlobalData.colorsList[0]) {
                statistic.CmdDecreaseRedCount();
            }
            else if (GetComponent<SpriteRenderer>().color == GlobalData.colorsList[1]) {
                statistic.CmdDecreaseBlueCount();
            }

            if (color == GlobalData.colorsList[0]) {
                statistic.CmdIncreaseRedCount();
            }
            else if (color == GlobalData.colorsList[1]) {
                statistic.CmdIncreaseBlueCount();
            }
        }

        GetComponent<SpriteRenderer>().color = color;
    }
    public override void OnStartClient() {
        GetComponent<SpriteRenderer>().color = color;
    }
}