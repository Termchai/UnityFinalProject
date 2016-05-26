using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Statistic : NetworkBehaviour {

    [SyncVar(hook = "OnChangeRedCount")]
    private int redCount = 0;

    [SyncVar(hook = "OnChangeBlueCount")]
    private int blueCount = 0;

    //[SyncVar(hook = "OnChangeBlueCount")]
    //public List<int> scoreboard = new List<int>();

    void OnChangeRedCount(int count) {
        redCount = count;
    }

    void OnChangeBlueCount(int count) {
        blueCount = count;
    }

    [Command]
    public void CmdIncreaseRedCount() {
        Debug.Log("CmdIncreaseRedCount");
        if (isServer) {
            redCount++;
        }
    }

    [Command]
    public void CmdDecreaseRedCount() {
        if (isServer) {
            redCount++;
        }
    }

    [Command]
    public void CmdIncreaseBlueCount() {
        if (isServer) {
            blueCount++;
        }
    }

    [Command]
    public void CmdDecreaseBlueCount() {
        if (isServer) {
            blueCount++;
        }
    }

    public int GetRedCount() {
        return redCount;
    }

    public int GetBlueCount() {
        return blueCount;
    }

    void Update() {
        Debug.Log("Stat: " + redCount + ":" + blueCount);
    }
}
