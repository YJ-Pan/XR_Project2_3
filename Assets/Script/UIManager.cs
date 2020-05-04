using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Item")]
    public ItemManger itemManger;
    public GameObject itemCursor;
    public List<GameObject> m_item;

    [Header("ItemImage")]
    public Texture pickaxe;
    public Texture shield;

    [Header("Speed")]
    public GameObject car;
    public GameObject speedText;
    float carSpeed;

    [Header("Oil")]
    public GameObject oilFull;
    public GameObject oil;
    public GameObject oilText;
    float oilMax;
    float oilAmount; // 0 ~ 100;

    float OldTime;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        carSpeed = 0.0f;

        oilMax = oilFull.GetComponent<RectTransform>().sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        // item
        if (Input.GetKeyUp(KeyCode.Z))
        {
            int itemCount = itemManger.GetItemCount();

            Vector2 itemPos = new Vector2(0.0f, 0.0f);
            itemPos.x = m_item[itemCount].GetComponent<RectTransform>().anchoredPosition.x;
            itemPos.y = m_item[itemCount].GetComponent<RectTransform>().anchoredPosition.y;

            itemCursor.GetComponent<RectTransform>().anchoredPosition = itemPos;
        }

        for (int i = 0; i < 6; i++)
        {
            if (itemManger.myItem.Count <= i)
            {
                break;
            }

            if(itemManger.myItem[i].m_type == ItemManger.ItemType.pickaxe)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = pickaxe;
            }
            else if(itemManger.myItem[i].m_type == ItemManger.ItemType.shield)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = shield;
            }
        }


        
        


        // speed
        carSpeed = car.GetComponent<drive>().GetSpeed();
        speedText.GetComponent<Text>().text = "" + (int)(carSpeed * 3.0f);

        // oil

        if (carSpeed > 0.0f) {

            OldTime = Time.time;
            currentTime = Time.time;
        }
        oilAmount = car.GetComponent<drive>().GetOil();
        oil.GetComponent<RectTransform>().offsetMax = new Vector2(-(oilMax - oilAmount * 3.0f), 10.0f);
        oilText.GetComponent<Text>().text = "" + (int)oilAmount + "%";

    }

}
