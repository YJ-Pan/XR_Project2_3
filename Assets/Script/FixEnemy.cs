using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixEnemy : MonoBehaviour
{
    public GameObject enemy;
    public GameObject bomb;
    public Transform target;
    public float viewDis = 100.0f;
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
            LookPos.y = enemy.transform.position.y;
            enemy.transform.LookAt(LookPos);
            if (!triggerAttack)
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
        Vector3 pos = enemy.transform.position + enemy.transform.forward * 3;
        Instantiate(bomb, pos, enemy.transform.rotation);
        yield return new WaitForSeconds(attackTime);
        triggerAttack = false;
    }

    bool IsTargetNear()
    {
        if (Vector3.Distance(enemy.transform.position, target.position) < viewDis)
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
