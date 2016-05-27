using UnityEngine;
using System.Collections;

public class ScreenLayout : MonoBehaviour {

    [Range(0, 100)]
    public float x;
    [Range(0, 100)]
    public float y;
    [Range(0, 100)]
    public float width;
    [Range(0, 100)]
    public float heigth;

	// Use this for initialization
	void Start () {


        RectTransform rectTransform = GetComponent<RectTransform>();

        float newWidth = width / 100f * Screen.width;
        float newHeight = heigth / 100f * Screen.height;

        float newX = x / 100f * Screen.width - newWidth / 2f;
        float newY = y / 100f * Screen.height - newWidth / 2f;

        new Rect(newX, newY, newWidth, newHeight);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
