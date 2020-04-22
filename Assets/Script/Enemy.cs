using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    public Transform target;
    public uint arsenalID = 0;
    public float viewDis = 15.0f;
    public float speed = 0.1f;
    public float attackTime = 3.0f;
    public uint life = 1;
    public float destroyTime = 120.0f;

    Actions actions;
    PlayerController controller;
    string[] arsenals = {"Sniper Rifle", "AK-74M"};

    bool triggerAttack = false;
    bool shoot = false;
	
    // Start is called before the first frame update
    void Start()
    {
        actions = enemy.GetComponent<Actions>();
        controller = enemy.GetComponent<PlayerController>();
        controller.SetArsenal(arsenals[arsenalID]);
        actions.Stay();
        //actions.Attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (life < 0)
        {
            destroyTime -= 1;
            if (destroyTime < 0.0f)
                Destroy(enemy);
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
            if(!shoot)
                enemy.transform.position += speed * enemy.transform.forward;
        }
        else
        {
            if (triggerAttack)
            {
                CancelInvoke("attackMod");
                triggerAttack = false;
            }
            actions.Stay();
        }
    }

    IEnumerator attackMod()
    {
        actions.Walk();
        yield return new WaitForSeconds(0.4f);
        actions.Run();
        shoot = false;
        yield return new WaitForSeconds(attackTime);
        shoot = true;
        actions.Attack();
        yield return new WaitForSeconds(1.2f);
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
            if (life < 1)
            {
                actions.Death();
            }
            else {
                actions.Damage();
            }
        }
    }

}
