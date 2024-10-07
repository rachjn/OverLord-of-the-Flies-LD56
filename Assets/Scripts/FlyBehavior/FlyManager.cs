using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Assertions;
using Random=UnityEngine.Random;

public class FlyManager : MonoBehaviour
{
    [SerializeField]
    private SwarmManager swarmManager;
    [SerializeField]
    private float aggroDistance;

    [SerializeField]
    public float repathTime = 0.5f;
    private GameObject swarmObject;
    private AIPath pather;
    private Transform swarmCenter;
    private bool inRadius = false;
    private GameObject enemyFly;
    private IAttack attack;

    private float repathCD;
    
    private Rigidbody2D rb;

    void Start()
    {
        if (!TryGetComponent(out attack))
        {
            Debug.LogWarning("No attack assigned to fly");
        }
        if (!TryGetComponent(out rb))
        {
            Debug.LogWarning("No rigidbody assigned to fly");
        }
        pather = GetComponent<AIPath>();
        if (swarmManager == null)
        {
            // jank
            if (tag == "Player1" && tag != "Player2") tag = "Player1";
            // look for owner
            swarmManager = FindObjectsOfType<SwarmManager>().FirstOrDefault((s)=>s.tag == tag);
            if (swarmObject == null)
            {
                Debug.LogWarning("No valid swarm owner");
            }
        }
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
            pather.endReachedDistance = 0.1f;
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
                if (Vector3.Distance(enemyFly.transform.position, transform.position) < attack.AttackRange)
                {
                    // pather.canMove = false;
                    pather.endReachedDistance = attack.AttackRange;
                    if (attack.Ready)
                    {
                        attack.TryAttack(enemyFly);
                    }
                }
                else 
                {
                    pather.endReachedDistance = 0.1f;
                    // pather.canMove = true;
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

    public void ReceiveKnockback(Vector3 force)
    {
        StartCoroutine(doKnockback(force));
    }
    private IEnumerator doKnockback(Vector3 force)
    {
        yield return null;
        pather.enabled = false;
        rb.AddForce(force, ForceMode2D.Impulse);

        float startTime = Time.time;
        yield return new WaitUntil(() => rb.velocity.magnitude < 0.1f || Time.time - startTime > 0.5f);
        // yield return new WaitForSeconds(0.25f);
        rb.velocity = Vector3.zero;
        pather.enabled = true;
        yield return null;
    }

    void OnDestroy()
    {
        swarmManager.removeFly(gameObject);
    }
}
