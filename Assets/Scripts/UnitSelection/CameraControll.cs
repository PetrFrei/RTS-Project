using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    public float moveSpeed = 20f;
    public float scrollSpeed = 500f;
    public float rotate = 100f;

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = Screen.height;
        screenWidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        MouseMove();
        KeyMove();
        ScrollMove();
        ScrollRotate();
        
    }

    void MouseMove()
    {
        //WIP
    }

    void ScrollRotate()
    {
        Vector3 origin = transform.eulerAngles;
        Vector3 destination = origin;
        if(Input.GetMouseButton(2))
        {
            destination.x -= Input.GetAxis("Mouse Y") * rotate;
            destination.y += Input.GetAxis("Mouse X") * rotate; 
        }

        if(destination!=origin)
        {
            transform.eulerAngles = Vector3.MoveTowards(origin, destination, Time.deltaTime*rotate);
        }
        
    }

    void KeyMove()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        transform.position = new Vector3(transform.position.x + (y*Time.deltaTime*moveSpeed), transform.position.y, transform.position.z + (-x * Time.deltaTime * moveSpeed));
    }

    void ScrollMove()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(0, 0, scroll * Time.deltaTime*scrollSpeed);
    }

}
