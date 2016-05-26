using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MapSpawner : NetworkBehaviour {

    public GameObject nodePrefab;
    public GameObject wallsPrefab;
    public int height;
    public int width;

    public override void OnStartServer() {
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                var spawnPosition = new Vector3(
                    i * 2,
                    j * 2,
                    0.0f);

                var spawnRotation = Quaternion.Euler(
                    0.0f,
                    0.0f,
                    0.0f);

                var node = (GameObject)Instantiate(nodePrefab, spawnPosition, spawnRotation);
                NetworkServer.Spawn(node);

                if (i < 4 && j < 4) {
                    node.GetComponent<Node>().Hit(GlobalData.colorsList[0]);
                }
                else if (i > 10 && j > 10) {
                    node.GetComponent<Node>().Hit(GlobalData.colorsList[1]);
                }
            }
        }
        var walls = (GameObject)Instantiate(wallsPrefab, new Vector3(), Quaternion.identity);
        NetworkServer.Spawn(walls);
    }
}
