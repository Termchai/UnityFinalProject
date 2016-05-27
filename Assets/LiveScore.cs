using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LiveScore : MonoBehaviour {

    public Statistic statistic;
    private Text text;

    public enum Team {
        Red,
        Blue
    };

    public Team team;

    public enum StatCount {
        Player,
        Node
    };

    public StatCount statCount;

    // Use this for initialization
    void Start() {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update() {
        if (statCount == StatCount.Node) {
            if (team == Team.Blue)
                text.text = "" + statistic.GetBlueCount();
            else
                text.text = "" + statistic.GetRedCount();
        }
        if (statCount == StatCount.Player) {
            if (team == Team.Blue)
                text.text = statistic.GetBluePlayer() + " PLAYERS";
            else
                text.text = statistic.GetRedPlayer() + " PLAYERS";
        }
    }
}
