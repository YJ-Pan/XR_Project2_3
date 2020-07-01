﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManger : MonoBehaviour
{
    public enum ItemType
    {
        pickaxe,
        crowbar,
        shield,
        firstaid,
        None
    }

    public struct ItemFormat
    {
        public ItemType m_type;
        public GameObject m_Object;
    }

    public List<ItemFormat> myItem;
    public ItemFormat VrItem;
    public Grab grab;

    public GameObject car;

    public AudioClip load;

    [Header("Radio")]
    public GameObject radio;
    public GameObject radioUI;

    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        myItem = new List<ItemFormat>();
        VrItem = new ItemFormat();
        VrItem.m_type = ItemType.None;
        VrItem.m_Object = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (itemCount < 5)
            {
                itemCount++;
            }
            else
            {
                itemCount = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            if (itemCount < myItem.Count)
            {
                if(myItem[itemCount].m_Object.name == "Shield")
                {
                    car.GetComponent<drive>().setShieldLife(5.0f);
                    myItem.RemoveAt(itemCount);
                }
                else if (myItem[itemCount].m_Object.name == "firstaid")
                {
                    car.GetComponent<drive>().heal();
                    myItem.RemoveAt(itemCount);
                }
                else if(VrItem.m_type != ItemType.None)
                {
                    ItemFormat tmp = new ItemFormat();
                    tmp = VrItem;
                    VrItem = myItem[itemCount];
                    myItem[itemCount] = tmp;

                    myItem[itemCount].m_Object.SetActive(false);
                    VrItem.m_Object.SetActive(true);
                }
                else
                {
                    VrItem = myItem[itemCount];
                    myItem.RemoveAt(itemCount);
                    VrItem.m_Object.SetActive(true);
                }
            }
        }

        if(VrItem.m_Object != null)
        {
            VrItem.m_Object.transform.position = 
                new Vector3(car.transform.position.x, car.transform.position.y+15.0f, car.transform.position.z);
            VrItem.m_Object.transform.rotation = car.transform.rotation;
        }
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            if (other.gameObject == VrItem.m_Object)
            {                
                return;
            }
            else if(other.gameObject == grab.moveObject)
            {
                grab.moveObject = null;
            }

            GetComponent<AudioSource>().PlayOneShot(load);

            ItemFormat tmp = new ItemFormat();
            tmp.m_Object = other.gameObject;

            if (other.name== "Pickaxe")
            {
                tmp.m_type = ItemType.pickaxe;
                myItem.Add(tmp);
            }
            else if (other.name == "crowbar")
            {
                tmp.m_type = ItemType.crowbar;
                myItem.Add(tmp);
            }
            else if (other.name == "Shield")
            {
                tmp.m_type = ItemType.shield;
                myItem.Add(tmp);
            }
            else if (other.name == "firstaid")
            {
                tmp.m_type = ItemType.firstaid;
                myItem.Add(tmp);
            }
            else if(other.name == "Radio")
            {
                radio.SetActive(true);
                radioUI.SetActive(true);
            }
            

            other.gameObject.SetActive(false);
        }
    }
}
