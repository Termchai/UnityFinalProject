using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Statistic : NetworkBehaviour {

    public static string playerName;

    public enum Team:int {
        Red = 0,
        Blue = 1
    }

    [SyncVar(hook = "OnChangeRedPlayerCount")]
    public int redPlayerCount = 0;

    [SyncVar(hook = "OnChangeBluePlayerCount")]
    public int bluePlayerCount = 0;

    [SyncVar(hook = "OnChangeRedCount")]
    public int redCount = 0;

    [SyncVar(hook = "OnChangeBlueCount")]
    public int blueCount = 0;

    void OnChangeRedPlayerCount(int c) {
        this.redPlayerCount = c;
    }

    void OnChangeBluePlayerCount(int c) {
        this.bluePlayerCount = c;
    }

    void OnChangeRedCount(int c) {
        this.redCount = c;
    }

    void OnChangeBlueCount(int c) {
        this.blueCount = c;
    }

    [Command]
    public void CmdAddPlayer(Team team) {
        if (isServer) {
            if (team == Team.Red) {
                redPlayerCount++;
            }
            else {
                bluePlayerCount++;
            }
        }
    }

    [Command]
    public void CmdRemovePlayer(Team team) {
        if (isServer) {
            if (team == Team.Red) {
                redPlayerCount--;
            }
            else {
                bluePlayerCount--;
            }
        }
    }

    [Command]
    public void CmdIncreaseRedCount() {
        if (isServer) {
            redCount++;
        }
    }

    [Command]
    public void CmdDecreaseRedCount() {
        if (isServer) {
            redCount--;
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
            blueCount--;
        }
    }

    public int GetRedCount() {
        return redCount;
    }

    public int GetBlueCount() {
        return blueCount;
    }

    public int GetRedPlayer() {
        return redPlayerCount;
    }

    public int GetBluePlayer() {
        return bluePlayerCount;
    }


    void Update() {
        Debug.Log(bluePlayerCount + ":" + redPlayerCount);
    }
}
