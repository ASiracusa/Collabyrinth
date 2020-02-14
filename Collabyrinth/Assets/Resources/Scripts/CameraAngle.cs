using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAngle : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 0.05f;

        if (Input.GetKey("w"))
        {
            transform.Translate(speed, 0, speed);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(-speed, 0, speed);
        }

        if (Input.GetKey("s"))
        {
            transform.Translate(-speed, 0, -speed);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(speed, 0, -speed);
        }
    }
}
