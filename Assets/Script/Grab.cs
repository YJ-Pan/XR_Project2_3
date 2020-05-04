using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Grab : MonoBehaviour
{
    public SteamVR_Behaviour_Boolean steamVR_Behaviour_Boolean;
    public Transform righthand;
    GameObject moveObject;
    bool isGrab;
    bool firstGrab = false;

    // Start is called before the first frame update
    void Start()
    {
        moveObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        isGrab = SteamVR_Input.GetState("default", "GrabPinch", steamVR_Behaviour_Boolean.inputSource);
        if (isGrab & moveObject != null)
        {     
            moveObject.transform.position = righthand.position;
            moveObject.transform.rotation = righthand.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            moveObject = other.gameObject;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            moveObject = null;
        }
    }
}
