using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManger : MonoBehaviour
{
    public enum ItemType
    {
        pickaxe,
        shield
    }

    public struct ItemFormat
    {
        public ItemType m_type;
        public GameObject m_Object;
    }

    public List<ItemFormat> myItem;

    private int itemCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        myItem = new List<ItemFormat>();
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
    }

    public int GetItemCount()
    {
        return itemCount;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            ItemFormat tmp = new ItemFormat();
            tmp.m_Object = other.gameObject;

            if (other.name== "Pickaxe")
            {
                tmp.m_type = ItemType.pickaxe;
                myItem.Add(tmp);
            }
            else if (other.name == "Shield")
            {
                tmp.m_type = ItemType.shield;
                myItem.Add(tmp);
            }

            other.gameObject.SetActive(false);
        }
    }
}
