using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpManager : MonoBehaviour
{

    string uri = "https://infinite-gorge-23096.herokuapp.com/";
    void Start()
    {
        //Debug.Log("Test Http Manager");


        //string a = "";
        //for (int i = 0;  i < 400; i++)
        //    a += "0";

        //writeNodeItem(a);
        //getNodeItem();
    }

    public void getNodeItem()
    {
        HTTP.Request someRequest = new HTTP.Request("get", uri + "users");
        someRequest.Send((request) => {
            Debug.Log("getNodeCallback");
            JSONObject thing = new JSONObject(request.response.Text);
            //for (int i = 0; i < thing.list.Count; i++)
            //{
            Debug.Log(thing);
            string nodeColorRaw = thing.list[1].ToString().Replace('\"', ' ');
            string nodeColor = nodeColorRaw.Substring(1,nodeColorRaw.Length-2);
            Debug.Log(nodeColor);
            Debug.Log(nodeColor.Length);

            //}
        });
    }

    public void writeNodeItem(string nodeColors)
    {
        Hashtable data = new Hashtable();
        data.Add("Session", "1");
        data.Add("NodeColorString", nodeColors);

        HTTP.Request theRequest = new HTTP.Request("post", uri + "users", data);
        theRequest.Send((request) => {


            Hashtable result = request.response.Object;
            if (result == null)
            {
                Debug.LogWarning("Could not parse JSON response!");

                return;
            }
            Debug.Log(request.response.Text);

        });
    }


}