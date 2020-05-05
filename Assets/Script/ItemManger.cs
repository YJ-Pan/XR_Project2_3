using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManger : MonoBehaviour
{
    public enum ItemType
    {
        pickaxe,
        crowbar,
        shield,
        None
    }

    public struct ItemFormat
    {
        public ItemType m_type;
        public GameObject m_Object;
    }

    public List<ItemFormat> myItem;
    public ItemFormat VrItem;

    public Transform carPos;

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
                if(VrItem.m_type != ItemType.None)
                {
                    ItemFormat tmp = new ItemFormat();
                    tmp = VrItem;
                    VrItem = myItem[itemCount];
                    myItem[itemCount] = tmp;

                    myItem[itemCount].m_Object.SetActive(false);
                }
                else
                {
                    VrItem = myItem[itemCount];
                    myItem.RemoveAt(itemCount);
                }

                VrItem.m_Object.SetActive(true);
            }
        }

        if(VrItem.m_Object != null)
        {
            VrItem.m_Object.transform.position = new Vector3(carPos.position.x, carPos.position.y+15.0f, carPos.position.z);
            VrItem.m_Object.transform.rotation = carPos.transform.rotation;
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
            else if(other.name == "Radio")
            {
                radio.SetActive(true);
                radioUI.SetActive(true);
            }

            other.gameObject.SetActive(false);
        }
    }
}
