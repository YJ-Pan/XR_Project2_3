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
    public GameObject VrItem;

    [Header("ItemImage")]
    public Texture none;
    public Texture pickaxe;
    public Texture crowbar;
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

    [Header("Music")]
    public AudioClip choose;
    public AudioClip enter;
    AudioSource audioSource;

    [Header("Radio")]
    public List<AudioClip> music;
    public AudioSource radio;
    public GameObject radioObject;
    public GameObject radioText;
    int radioCount = 0;
    bool radioStatus = false;

    float OldTime;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        carSpeed = 0.0f;
        oilMax = oilFull.GetComponent<RectTransform>().sizeDelta.x;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // radio
        if (Input.GetKeyUp(KeyCode.A) && radioObject.activeSelf)
        {
            if (radioStatus)
            {
                radio.Stop();
                radioText.GetComponent<Text>().text = music[radioCount].name;
            }
            else
            {
                radio.Play();
                radioText.GetComponent<Text>().text = "Now's playing : " + music[radioCount].name;
            }
            radioStatus = !radioStatus;
        }
        if (Input.GetKeyUp(KeyCode.S) && radioObject.activeSelf)
        {
            if (radioCount < music.Count - 1)
            {
                radioCount++;
            }
            else
            {
                radioCount = 0;
            }

            radio.clip = music[radioCount];
            radioText.GetComponent<Text>().text = music[radioCount].name;

            if (radioStatus)
            {
                radio.Play();
                radioText.GetComponent<Text>().text = "Now's playing : " + music[radioCount].name;
            }
        }

        // item
        if (Input.GetKeyUp(KeyCode.Z))
        {
            audioSource.PlayOneShot(choose); 
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            audioSource.PlayOneShot(enter);
        }

        int itemCount = itemManger.GetItemCount();
        Vector2 itemPos = new Vector2(0.0f, 0.0f);
        itemPos.x = m_item[itemCount].GetComponent<RectTransform>().anchoredPosition.x;
        itemPos.y = m_item[itemCount].GetComponent<RectTransform>().anchoredPosition.y;

        itemCursor.GetComponent<RectTransform>().anchoredPosition = itemPos;

        for (int i = 0; i < 6; i++)
        {
            if (itemManger.myItem.Count <= i)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = none;
                continue;
            }

            if(itemManger.myItem[i].m_type == ItemManger.ItemType.pickaxe)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = pickaxe;
            }
            else if (itemManger.myItem[i].m_type == ItemManger.ItemType.crowbar)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = crowbar;
            }
            else if (itemManger.myItem[i].m_type == ItemManger.ItemType.shield)
            {
                m_item[i].transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = shield;
            }
        }

        if (itemManger.VrItem.m_type == ItemManger.ItemType.pickaxe)
        {
            VrItem.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = pickaxe;
        }
        else if (itemManger.VrItem.m_type == ItemManger.ItemType.crowbar)
        {
            VrItem.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = crowbar;
        }
        else if (itemManger.VrItem.m_type == ItemManger.ItemType.shield)
        {
            VrItem.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = shield;
        }
        else
        {
            VrItem.transform.GetChild(0).gameObject.GetComponent<RawImage>().texture = none;
        }

        // speed
        carSpeed = car.GetComponent<drive>().GetSpeed();
        speedText.GetComponent<Text>().text = "" + (int)System.Math.Abs(carSpeed * 3.0f);

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
