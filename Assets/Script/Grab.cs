using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

public class Grab : MonoBehaviour
{
    public SteamVR_Behaviour_Boolean steamVR_Behaviour_Boolean;
    public ItemManger itemManger;
    public Transform righthand_pickaxe;
    public Transform righthand_crowbar;
    public GameObject car;
    public AudioSource radio;
    Transform originalPos;
    GameObject moveObject;
    bool isGrab;
    bool firstGrab = false;
    bool menu;

    float MP;
    public Text MPText;

    // Start is called before the first frame update
    void Start()
    {
        moveObject = null;
        MP = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (radio.clip.name == "大悲咒1" && radio.isPlaying)
        {
            MP += 0.1f;
        }
        isGrab = SteamVR_Input.GetState("default", "GrabPinch", steamVR_Behaviour_Boolean.inputSource);
        if (isGrab && moveObject != null && MP > 0.1f)
        {
            MP -= 0.1f;
            if (itemManger.VrItem.m_Object == moveObject.gameObject)
            {
                itemManger.VrItem.m_type = ItemManger.ItemType.None;
                itemManger.VrItem.m_Object = null;
            }

            if (moveObject.name == "crowbar")
            {
                moveObject.transform.position = righthand_crowbar.position;
                moveObject.transform.rotation = righthand_crowbar.rotation;
            }
            else
            {
                moveObject.transform.position = righthand_pickaxe.position;
                moveObject.transform.rotation = righthand_pickaxe.rotation;
            }
        }
        else if(MP < 100.0f)
        {
            MP += 0.001f;
        }


        menu = SteamVR_Input.GetState("default", "UseItem", steamVR_Behaviour_Boolean.inputSource);
        if (menu && moveObject != null && MP > 0.1f)
        {
            if (moveObject.name == "crowbar")
            {
                MP -= 0.1f;
                car.GetComponent<Rigidbody>().useGravity = false;
                car.GetComponent<Rigidbody>().isKinematic = true;
                car.GetComponent<BoxCollider>().enabled = false;
                car.transform.rotation = righthand_crowbar.rotation;
            }
        }
        else
        {
            car.GetComponent<Rigidbody>().useGravity = true;
            car.GetComponent<Rigidbody>().isKinematic = false;
            car.GetComponent<BoxCollider>().enabled = true;
        }

        if (MP < 0.0f)
        {
            MP = 0.0f;
        }

        MPText.text = "" + MP;
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
