using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera();
    }

    void Camera()
    {
        if (Input.GetKey(KeyCode.D))                // -->
            transform.Translate(0.05f, 0, 0);   
        if (Input.GetKey(KeyCode.A))                // <--
            transform.Translate(-0.05f, 0, 0);
    }
}
