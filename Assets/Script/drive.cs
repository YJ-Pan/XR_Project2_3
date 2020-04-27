using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    public GameObject car;
    public float maxSpeed = 5.0f;

    float speed = 0.0f;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        direction = car.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        // go ahead
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (speed < maxSpeed)
            {
                speed += 0.05f;
            }
        }// go back
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (speed > -maxSpeed)
            {
                speed -= 0.05f;
            }
        }// auto decrease speed
        else
        {
            if(speed > 0.01)
            {
                speed -= 0.01f;
            }
            else if(speed < -0.01)
            {
                speed += 0.01f;
            }
            else
            {
                speed = 0.0f;
            }
        }

        // brakes
        if (Input.GetKey(KeyCode.Space))
        {
            float foward = speed > 0.0f ? 1.0f : -1.0f;

            if (System.Math.Abs(speed) > 0.01)
            {
                speed = System.Math.Abs(speed) - 0.1f;
                if (speed < 0.0f)
                {
                    speed = 0.0f;
                }
                else
                {
                    speed = speed * foward;
                }
            }
            
        }

        // turn right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = Quaternion.Euler(0, 1f * speed / (maxSpeed*1.5f), 0) * direction;
            car.transform.Rotate(new Vector3(0f, 1f * speed / (maxSpeed*1.5f), 0f));
        }// turn left
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = Quaternion.Euler(0, -1f * speed / (maxSpeed*1.5f), 0) * direction;
            car.transform.Rotate(new Vector3(0f, -1f * speed / (maxSpeed*1.5f), 0f));
        }


        if (System.Math.Abs(speed) > 0.01) { 

            car.transform.Translate(direction * Time.deltaTime * speed, Space.World);
        }

         
    }
}
