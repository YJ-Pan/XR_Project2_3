using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject bomb;
    public Transform target;
    public float force = 100.0f;

    Vector3 distance;
    float speed = 2.0f;

    // Use this for initialization
    void Start () {
        StartCoroutine("Die");
    }

    // Update is called once per frame
    void Update () {
        distance = target.position - bomb.transform.position;
        if (distance.y < 0.0f)
        {
            bomb.transform.position += speed * distance.normalized;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            other.GetComponent<drive>().setHP(3.0f);
            Destroy(bomb);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(20.0f);
        Destroy(bomb);
    }
}
