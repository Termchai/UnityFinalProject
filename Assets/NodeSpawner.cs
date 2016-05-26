using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NodeSpawner : NetworkBehaviour {

    public GameObject nodePrefab;
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
            }
        }
    }
}
