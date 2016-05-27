using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MapSpawner : NetworkBehaviour
{

    public GameObject nodePrefab;
    public GameObject wallsPrefab;
    public GameObject statistic;
    public int height;
    public int width;

    private Node[] nodes;

    public static string nodeColorString;

    public override void OnStartServer()
    {

        nodes = new Node[height * width];

        Debug.Log(nodeColorString);
        NetworkServer.Spawn(statistic);

        char[] nodeColor = nodeColorString.ToCharArray();

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
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


                node.GetComponent<Node>().statistic = statistic.GetComponent<Statistic>();
                    
                if (nodeColor[height * i + j] == '1' || (i < 4 && j < 4)) 
                {
                    node.GetComponent<Node>().Setup(GlobalData.colorsList[0]);
                }
                else if (nodeColor[height * i + j] == '2' || (i > 10 && j > 10))
                {
                    node.GetComponent<Node>().Setup(GlobalData.colorsList[1]);
                }

                node.GetComponent<Node>().SetListener(GetComponent<MapSpawner>());

                nodes[i * height + j] = node.GetComponent<Node>();
            }

        }
        var walls = (GameObject)Instantiate(wallsPrefab, new Vector3(), Quaternion.identity);
        NetworkServer.Spawn(walls);

        if (isServer)
        {
            //StartCoroutine(UpdateDataBase(2f));
        }
    }

    [Command]
    public void CmdUpdateDataBase()
    {
        if (!isServer) return;

        string nodeColors = "";

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (nodes[i * height + j].color == GlobalData.colorsList[0])
                {
                    nodeColors += 1;
                }
                else if (nodes[i * height + j].color == GlobalData.colorsList[1])
                {
                    nodeColors += 2;
                }
                else
                {
                    nodeColors += 0;
                }
            }
        }
        HttpManager hm = new HttpManager();
        hm.writeNodeItem(nodeColors);
    }
}
