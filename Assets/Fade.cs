using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public static bool active = false;

    public RectTransform gameName;

    public GameObject[] fadeObject;

    private float time = 0.6f;
	
	// Update is called once per frame
	void Update () {
	    if(active)
        {
            gameName.Translate(new Vector3(0, 0.5f, 0));

            time -= Time.deltaTime;

            if(time <= 0)
            {
                for (int i = 0; i < fadeObject.Length; i++)
                {
                    fadeObject[i].SetActive(true);
                }
                Destroy(gameObject);
            }
        }
	}
}
