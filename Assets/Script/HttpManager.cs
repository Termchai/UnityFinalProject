using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HttpManager : MonoBehaviour {

    string uri = "https://infinite-gorge-23096.herokuapp.com/";
	void Start()
    {
        Debug.Log("Test Http Manager");
        //scanUser();
        writeUser();
    }

    public void scanUser()
    {
        HTTP.Request someRequest = new HTTP.Request("get", uri + "users");
        someRequest.Send((request) => {
            JSONObject thing = new JSONObject(request.response.Text);
            for(int i=0; i<thing.list.Count; i++)
            {
                Debug.Log(thing.list[i]["AndroidId"]);
            }
        });
    }

    public void writeUser()
    {
        Hashtable data = new Hashtable();
        data.Add("AndroidId", "test");
        data.Add("GCMToken", "test2");

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
