using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drive : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject car;
    public float maxSpeed = 5.0f;
    public AudioSource skid;
    public UIManager uiManager;
    public AudioSource H;
    public AudioClip hit;
    public bool canDrive = false;

    float speed = 0.0f;
    Vector3 direction;

    public float oilAmount; // 0 ~ 100;
    float HP;
    float shieldLife;
    bool nearGasPump = false;

    // Start is called before the first frame update
    void Start()
    {
        direction = car.transform.forward;
        oilAmount = 80.0f;
        HP = 100.0f;
        shieldLife = 0.0f;
        InvokeRepeating("oilConsume", 0.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0.0f || uiManager.Win.activeSelf)
        {
            speed = 0.0f;
            skid.volume = 0.0f;
            car.GetComponent<AudioSource>().volume = 0.0f;
            return;
        }

        direction = car.transform.forward;

        if (canDrive)
        {
            // go ahead
            if (Input.GetKey(KeyCode.UpArrow) && oilAmount > 0.0f)
            {
                if (speed < maxSpeed)
                {
                    speed += 0.1f;
                }
            }// go back
            else if (Input.GetKey(KeyCode.DownArrow) && oilAmount > 0.0f)
            {
                if (speed > -maxSpeed)
                {
                    speed -= 0.1f;
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
                    speed = System.Math.Abs(speed) - 0.5f;
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

            if (Input.GetKeyDown(KeyCode.Space))
            {
                skid.Play();
            }
            else if (Input.GetKeyUp(KeyCode.Space) || speed <= 0.0f)
            {
                skid.Stop();
            }
            skid.volume = speed / maxSpeed;

            // turn right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                direction = Quaternion.Euler(0, 1f * speed / (maxSpeed * 1.2f), 0) * direction;
                car.transform.Rotate(new Vector3(0f, 1f * speed / (maxSpeed * 1.2f), 0f));
            }// turn left
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction = Quaternion.Euler(0, -1f * speed / (maxSpeed * 1.2f), 0) * direction;
                car.transform.Rotate(new Vector3(0f, -1f * speed / (maxSpeed * 1.2f), 0f));
            }


            if (System.Math.Abs(speed) > 0.01)
            {

                car.transform.Translate(direction * Time.deltaTime * speed, Space.World);
            }

            car.GetComponent<AudioSource>().volume = 0.9f * System.Math.Abs(speed / maxSpeed);

            if (Input.GetKey(KeyCode.X) && nearGasPump)
            {
                if (oilAmount < 100.0f)
                {
                    oilAmount += 1.0f;
                }
                else
                {
                    oilAmount = 100.0f;
                }
            }
        }
        
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetOil()
    {
        return oilAmount;
    }

    public float GetHP()
    {
        return HP;
    }

    public float GetShieldLife()
    {
        return shieldLife;
    }

    public void setHP(float sub)
    {
        H.PlayOneShot(hit);
        if (shieldLife > 0.0f)
            shieldLife -= 1.0f;
        else
            HP -= sub;

        if (HP < 0.0f)
            HP = 0.0f;
    }

    public void heal()
    {
        if (HP < 50.0f)
        {
            HP += 50.0f;
        }
        else
        {
            HP = 100.0f;
        }
    }

    public void setShieldLife(float add)
    {
        shieldLife += add;
    }

    private void oilConsume()
    {
        if (oilAmount > 0.0f)
        {
            oilAmount -= speed * 0.02f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GasPump"))
        {
            nearGasPump = true;
        }
        else if(other.CompareTag("Helicopter"))
        {
            uiManager.radio.Stop();
            //uiManager.Win.SetActive(true);
            //uiManager.winSong.Play();
            speed = 0.0f;
            skid.volume = 0.0f;
            car.GetComponent<AudioSource>().volume = 0.0f;

            gameManager.chapter = GameManager.Chapter.PrepareEnd;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GasPump"))
        {
            nearGasPump = false;
        }
    }
}
