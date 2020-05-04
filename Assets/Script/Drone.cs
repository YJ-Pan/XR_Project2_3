using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject drone;
    public GameObject bomb;
    public Transform target;
    public float viewDis = 100.0f;
    public float speed = 0.1f;
    public float attackTime = 3.0f;
    public uint life = 1;

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
        
        if (IsTargetNear())
        {
            Vector3 LookPos = target.position;
            LookPos.y = drone.transform.position.y;
            drone.transform.LookAt(LookPos);
            if (!triggerAttack)
            {
                StartCoroutine("attackMod");
                triggerAttack = true;
            }
            drone.transform.position += speed * drone.transform.forward;
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
        Instantiate(bomb, pos, Quaternion.identity);
        yield return new WaitForSeconds(attackTime);
        triggerAttack = false;
    }

    bool IsTargetNear()
    {
        if (Vector3.Distance(drone.transform.position, target.position) < viewDis)
        {
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Bullet"))
        {
            life--;
        }
    }

}
