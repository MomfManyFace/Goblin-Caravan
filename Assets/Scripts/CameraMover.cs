using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float speed = 1;
    public float newSpeed = 2;
    public float zoomSpeed = 1;
    [SerializeField]
    Vector2 zoomMaxMin;

    Camera thisCam;

    // Start is called before the first frame update
    void Start()
    {
        thisCam = GetComponent<Camera>();
    }

    private void Update()
    {
        thisCam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if(thisCam.orthographicSize < zoomMaxMin.x)
        {
            thisCam.orthographicSize = zoomMaxMin.x;

        }
        else if (thisCam.orthographicSize > zoomMaxMin.y)
        {
            thisCam.orthographicSize = zoomMaxMin.y;

        }



    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(!Input.GetButton("Run"))
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed, Input.GetAxisRaw("Vertical") * speed);

        else
            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * newSpeed, Input.GetAxisRaw("Vertical") * newSpeed);
        

        




    }
}
