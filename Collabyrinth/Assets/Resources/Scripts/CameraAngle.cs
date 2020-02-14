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
            if (transform.position.x >= -0.2f && transform.position.x <= 8.1f && transform.position.z >= 0.98f && transform.position.z <= 13.08f)
            {
                transform.Translate(speed, 0, speed);
            }
            else { transform.position = new Vector3(7f, transform.position.y, 12f); }
        }

        if (Input.GetKey("a"))
        {
            if (transform.position.x >= -0.2f && transform.position.x <= 8.1f && transform.position.z >= 0.98f && transform.position.z <= 13.08f) if (transform.position.x > -0.2f && transform.position.x < 8.1f && transform.position.z > 0.98f && transform.position.z < 13.08f)
            {
                transform.Translate(-speed, 0, speed);
            }
                else { transform.position = new Vector3(0f, transform.position.y, 12f); }

        }

        if (Input.GetKey("s"))
        {
            if (transform.position.x >= -0.2f && transform.position.x <= 8.1f && transform.position.z >= 0.98f && transform.position.z <= 13.08f)
            {
                transform.Translate(-speed, 0, -speed);
             }
            else { transform.position = new Vector3(-0f, transform.position.y, 1f); }

        }

        if (Input.GetKey("d"))
        {
            if (transform.position.x >= -0.2f && transform.position.x <= 8.1f && transform.position.z >= 0.98f && transform.position.z <= 13.08f)
            {
                transform.Translate(speed, 0, -speed);
            }
            else
            {
                transform.position = new Vector3(7f, transform.position.y, 1f);
            }
        }

        
    }
}
