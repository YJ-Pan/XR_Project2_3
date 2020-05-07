using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject drone;
    public GameObject bomb;
    public Transform target;
    public float viewDis = 200.0f;
    public float speed = 20.0f;
    public float attackTime = 1.0f;
    public uint life = 1;
    public AudioClip gun;

    float distance;
    bool triggerAttack = false;
    bool shoot = false;
	
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            return;
        }

        distance = Vector3.Distance(drone.transform.position, target.position);
        if (distance > viewDis + 100.0f)
        {
            Destroy(drone);
            return;
        }

        if (distance < viewDis)
        {
            Vector3 LookPos = target.position;
            LookPos.y = drone.transform.position.y;
            drone.transform.LookAt(LookPos);
            distance = Vector3.Distance(drone.transform.position, LookPos);
            if (distance > 0.3f)
            {
                drone.transform.position += Mathf.Min(speed, distance) * drone.transform.forward;
                drone.transform.position +=
                    new Vector3(0.0f, target.position.y + 50.0f - drone.transform.position.y, 0.0f);
            }
            //distance = Vector3.Distance(drone.transform.position, LookPos);
            if (!triggerAttack && distance < 3.0f)
            {
                StartCoroutine("attackMod");
                triggerAttack = true;
            }
        }
        else
        {
            if (triggerAttack)
            {
                CancelInvoke("attackMod");
                triggerAttack = false;
            }
        }
    }

    IEnumerator attackMod()
    {
        Vector3 pos = drone.transform.position;
        pos.y -= 1.0f;
        GameObject newbomb = Instantiate(bomb, pos, Quaternion.identity);
        newbomb.GetComponent<Bomb>().target = target;
        //drone.GetComponent<AudioSource>().PlayOneShot(gun);
        yield return new WaitForSeconds(attackTime);
        triggerAttack = false;
    }

    void OnTriggerEnter(Collider collider)
    {
        
    }

}
