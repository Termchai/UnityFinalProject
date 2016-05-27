using UnityEngine;
using System.Collections;

public class HttpBegin : MonoBehaviour {

	// Use this for initialization
	void Start () {
        HttpManager hm = new HttpManager();
        hm.getNodeItem();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
