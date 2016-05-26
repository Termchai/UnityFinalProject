using UnityEngine;
using System.Collections;

public class MouseHandler : MonoBehaviour
{
    public GameObject  circle;
    public enum Shape { Circle, Rectangle, Star, Bar, Pentagon, Triangle };

    public Shape shapeState;
    public GameObject currentObject;

    void Start()
    {
        shapeState = Shape.Circle;
        currentObject = (GameObject)Instantiate(circle, Camera.main.ScreenPointToRay(Input.mousePosition).origin, Quaternion.identity);
    }
    
    void Update()
    {
        if (currentObject != null && currentObject.transform.position.x < 5)
        {
            currentObject.transform.position = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
        }

        if (Input.GetMouseButtonDown(0))    
        {

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            }

            currentObject = null;

            Debug.Log("Pressed left click.");
            Debug.Log(Input.mousePosition);


        }

        if (Input.GetMouseButtonDown(1))
            Debug.Log("Pressed right click.");

        if (Input.GetMouseButtonDown(2))
            Debug.Log("Pressed middle click.");

    }
}
