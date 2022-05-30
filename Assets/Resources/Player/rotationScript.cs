using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotationScript : MonoBehaviour
{
    // public GameObject rotation_center;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");
        // if(mouse_x < 0){
        //     //Code for action on mouse moving left
        //     print("Mouse moved left");
        // }
        // if(Input.GetAxis("Mouse X") > 0){
        //     //Code for action on mouse moving right
        //     print("Mouse moved right");
        // }
        transform.Rotate(-mouse_y * 2, 0, 0, Space.Self);
        // transform.Rotate(mouse_y * 2, mouse_x * 2, 0, Space.World);
        // transform.rotation = (transform.rotation * Quaternion.Euler(mouse_y * 2, mouse_x * 2, 0)) * rotation_center.transform.rotation;
        // transform.localRotation = Quaternion.Euler(transform.localRotation.x + mouse_x,1,1);
    }
}
