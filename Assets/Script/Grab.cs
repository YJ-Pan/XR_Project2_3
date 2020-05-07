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
    public AudioClip hit;
    Transform originalPos;
    GameObject moveObject;
    bool isGrab;
    bool firstGrab = false;
    bool menu;

    float MP;
    public Text MPText;
    public GameObject energy;
    public GameObject face1;
    public GameObject face2;

    // Start is called before the first frame update
    void Start()
    {
        moveObject = null;
        MP = 50.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (radio.clip.name == "大悲咒" && radio.isPlaying)
        {
            MP += 0.1f;
        }

        isGrab = SteamVR_Input.GetState("default", "GrabPinch", steamVR_Behaviour_Boolean.inputSource);
        if (isGrab && moveObject != null && MP > 0.1f)
        {
            MP -= 0.05f;
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
                MP -= 0.05f;
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

        if (MP > 100.0f)
        {
            MP = 100.0f;
        }

        if (MP > 33.333f)
        {
            face1.SetActive(true);
            face2.SetActive(false);
        }
        else
        {
            face1.SetActive(false);
            face2.SetActive(true);
        }

        energy.GetComponent<RectTransform>().sizeDelta = new Vector2(MP * 3.0f, 20);
        MPText.text = "" + (int)MP + "%";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            moveObject = other.gameObject;
        }
        else if (other.CompareTag("Bomb"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hit);
            if (MP > 0.5f)
            {
                Destroy(other.gameObject);
                MP -= 0.5f;
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            gameObject.GetComponent<AudioSource>().PlayOneShot(hit);
            if (MP > 3.0f)
            {
                Destroy(other.gameObject);
                MP -= 3.0f;
            }
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
