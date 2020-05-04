using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject bomb;
    public float force = 100.0f;

	// Use this for initialization
	void Start () {
        bomb.GetComponent<Rigidbody>().AddForce(bomb.transform.forward * force);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            //other.GetComponent<drive>().live -= 1.0f;
        }
        Destroy(bomb);
    }
}
