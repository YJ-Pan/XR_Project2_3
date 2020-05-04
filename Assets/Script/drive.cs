using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    public GameObject car;
    public float maxSpeed = 5.0f;

    float speed = 0.0f;
    Vector3 direction;

    float oilAmount; // 0 ~ 100;

    bool engineFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        direction = car.transform.forward;
        oilAmount = 80.0f;
        InvokeRepeating("oilConsume", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        direction = car.transform.forward;
       
        // go ahead
        if (Input.GetKey(KeyCode.UpArrow) && oilAmount > 0.0f)
        {
            if (speed < maxSpeed)
            {
                speed += 0.05f;
            }
        }// go back
        else if (Input.GetKey(KeyCode.DownArrow) && oilAmount > 0.0f)
        {
            if (speed > -maxSpeed)
            {
                speed -= 0.05f;
            }
        }
        else// auto decrease speed
        {
            if (speed > 0.01)
            {
                speed -= 0.01f;
            }
            else if (speed < -0.01)
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

        car.GetComponent<AudioSource>().volume = speed / maxSpeed;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetOil()
    {
        return oilAmount;
    }

    private void oilConsume()
    {
        if (oilAmount > 0.0f)
        {
            oilAmount -= speed * 0.02f;
        }
    }
}
