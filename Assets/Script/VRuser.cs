using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRuser : MonoBehaviour
{
    public GameObject car;
    public GameObject user;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newpos = car.transform.position;
        newpos.y += 15;
        user.transform.position = newpos;
    }
}
