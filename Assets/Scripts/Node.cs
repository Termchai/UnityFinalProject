using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Node : NetworkBehaviour {

    public Statistic statistic;

    [SyncVar(hook = "OnChangeColorId")]
    public Color color = new Color(0.8f,0.8f,0.8f);

    private MapSpawner mapSpawner;

    public void Hit(Color color)
    {
        if (!isServer || color == this.color)
            return;
        Setup(color);
        mapSpawner.CmdUpdateDataBase();
    }

    public void Setup(Color color)
    {
        if (!isServer)
            return;
        this.color = color;
    }

    public void SetListener(MapSpawner m)
    {
        mapSpawner = m;
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