using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.Assertions;
using Random=UnityEngine.Random;

public class FlyManager : MonoBehaviour
{
    [SerializeField]
    private SwarmManager swarmManager;
    [SerializeField]
    private float aggroDistance;
    private GameObject swarmObject;
    private AIPath pather;
    private Transform swarmCenter;
    private bool inRadius = false;
    private GameObject enemyFly;
    private IAttack attack;

    [SerializeField]
    public float repathTime = 0.5f;
    private float repathCD;

    void Start()
    {
        if (!TryGetComponent(out attack))
        {
            Debug.LogWarning("No attack assigned to fly");
        }
        pather = GetComponent<AIPath>();
        swarmObject = swarmManager.gameObject;
        swarmManager.addFly(gameObject);
        enemyFly = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        swarmCenter = swarmObject.transform;
        repathCD -= Time.fixedDeltaTime;

        // retreat if too far
        if (Vector3.Distance(transform.position, swarmCenter.position) > swarmManager.LassoRadius)
        {
            pather.canMove = true;
            pather.destination = swarmCenter.position;
            // hack to disengage more effectively
            repathTime = 2f;
        }
        else
        {
            // 1) peridically check for enemies
            if (repathCD <= 0)
            {
                var enemyFlies = swarmManager.GetEnemyFlies();
                // find closest enemy fly
                GameObject closestFly = null;
                float closestDistance = 10000;
                foreach (GameObject fly in enemyFlies)
                {
                    Debug.Log(fly.name, fly);
                    if (fly == null) continue;
                    float distance = Vector3.Magnitude(fly.transform.position - transform.position);
                    if (distance < closestDistance && distance < aggroDistance)
                    {
                        closestFly = fly;
                        closestDistance = distance;
                    }
                }
                enemyFly = closestFly;

                if (enemyFly != null)
                {
                    pather.destination = enemyFly.transform.position;
                }
                repathCD = repathTime;
            }

            if (enemyFly != null)
            {
                if (Vector3.Distance(enemyFly.transform.position, transform.position) < attack.AttackRange && attack.Ready)
                {
                    pather.canMove = false;
                    attack.TryAttack(enemyFly);
                }
                else 
                {
                    pather.canMove = true;
                }
            }
            else if (Vector3.Distance(pather.destination, swarmCenter.position) > swarmManager.IdleRadius || Random.Range(0, 100) < 0.1)
            { 
                pather.canMove = true;
                Vector3 randOffset = Random.insideUnitCircle;
                pather.destination = swarmCenter.position + (randOffset * swarmManager.IdleRadius);
            }
        }
    }

    void OnDestroy()
    {
        swarmManager.removeFly(gameObject);
    }
}
