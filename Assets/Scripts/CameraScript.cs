using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public GameObject target;
    private Vector3 destination;
    public float speed = 10f;

    private float boostSpeed = 0f;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (target) {
            Vector3 destination = new Vector3(target.transform.position.x, target.transform.position.y, -10);

            boostSpeed = (destination - transform.position).magnitude * 2;

            transform.position = Vector3.MoveTowards(transform.position, destination, (speed + boostSpeed) * Time.deltaTime);
        }
    }

}
