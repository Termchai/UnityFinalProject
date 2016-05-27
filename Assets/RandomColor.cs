using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomColor : MonoBehaviour {

    Text text;
    float r, g, b;
    int rC, gC, bC;
    bool isLoading = false;

    public float loadTime = 3f;
    public float speed = 1f;
    // Use this for initialization
    void Start() {
        r = 1;
        g = 0;
        b = 0;

        rC = 0;
        gC = 0;
        bC = 1;
        text = gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (r >= 1f && g >= 1f) {
            gC = -1;
            rC = 0;
            bC = 0;
        }

        if (r >= 1f && b >= 1f) {
            rC = -1;
            bC = 0;
            gC = 0;
        }

        if (b >= 1f && g >= 1f) {
            bC = -1;
            gC = 0;
            rC = 0;
        }

        if (r >= 1f && b <= 0 && g <= 0) {
            bC = +1;
            rC = 0;
            g = 0;
            gC = 0;
        }

        if (g >= 1f && r <= 0 && b <= 0) {
            rC = +1;
            gC = 0;
            b = 0;
            bC = 0;
        }

        if (b >= 1f && g <= 0 && r <= 0) {
            gC = +1;
            bC = 0;
            r = 0;
            rC = 0;
        }

        r += rC * Time.deltaTime;
        g += gC * Time.deltaTime;
        b += bC * Time.deltaTime;

        Color color = new Color(r, g, b);


        text.color = color;
    }
}
