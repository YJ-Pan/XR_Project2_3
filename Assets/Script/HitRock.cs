using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitRock : MonoBehaviour
{
    public AudioClip knock;
    public AudioClip broken;

    GameObject rock;
    float hitPointHP;

    // Start is called before the first frame update
    void Start()
    {
        rock = null;
        hitPointHP = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Rock"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(knock);
            if(rock != other.gameObject)
            {
                rock = other.gameObject;
                hitPointHP = 10.0f;
            }
            else
            {
                hitPointHP -= 1.0f;
                if(hitPointHP < 0.0f)
                {
                    gameObject.GetComponent<AudioSource>().PlayOneShot(broken);
                    Destroy(other.gameObject);
                    rock = null;
                    hitPointHP = 10.0f;
                }
            }
        }
    }
}
