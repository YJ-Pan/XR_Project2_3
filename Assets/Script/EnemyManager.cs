using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject drone;
    public Transform carPos;
    public float InstantiateTime = 10.0f;
    public UIManager uiManager;
    public GameManager gameManager;

    GameObject childDrone;
    bool instantiateOn = false;

    // Start is called before the first frame update
    void Start()
    {
        childDrone = null;
        //instantiateOn = true;
        //StartCoroutine("InstantiateDrone");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.chapter == GameManager.Chapter.Play)
        {
            if (childDrone == null && !instantiateOn)
            {
                instantiateOn = true;
                StartCoroutine("InstantiateDrone");
            }
        }

        if (gameManager.chapter == GameManager.Chapter.End)
        {
            StopCoroutine("InstantiateDrone");
            if (childDrone != null)
            {
                Destroy(childDrone);
            }
        }
    }

    public void startGenerateEnemy()
    {
        instantiateOn = true;
        StartCoroutine("InstantiateDrone");
    }

    IEnumerator InstantiateDrone()
    {
        yield return new WaitForSeconds(InstantiateTime);
        Vector3 pos = carPos.position;
        pos.y += 50.0f;
        pos.x += Random.Range(-100.0f, 100.0f);
        pos.z += Random.Range(-100.0f, 100.0f);
        childDrone = Instantiate(drone, pos, Quaternion.identity);
        childDrone.GetComponent<Drone>().target = carPos;
        instantiateOn = false;
    }
}
