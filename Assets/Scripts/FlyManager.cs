using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random=UnityEngine.Random;

public class FlyManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private SwarmManager swarmManager;
    private GameObject swarmObject;
    private AIPath pather;
    private Transform swarmCenter;
    private bool inRadius = false;

    void Start()
    {
        pather = GetComponent<AIPath>();
        // swarmManager = swarmObject.GetComponent<SwarmManager>();
        swarmObject = swarmManager.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        swarmCenter = swarmObject.transform;
        
        // simple following player
        // if (Vector2.Distance(pather.destination, swarmCenter.position) > swarmManager.SwarmRadius)
        if (Vector2.Distance(pather.destination, swarmCenter.position) > swarmManager.SwarmRadius || Random.Range(0, 100) < 3)
        {
            Vector3 randOffset = Random.insideUnitCircle;
            pather.destination = swarmCenter.position + (randOffset * swarmManager.SwarmRadius);
        }

        if (!inRadius && Vector2.Distance(transform.position, swarmCenter.position) < swarmManager.SwarmRadius)
        {
            swarmManager.SwarmSize++;
            inRadius = true;
        }
        if (inRadius && Vector2.Distance(transform.position, swarmCenter.position) > swarmManager.SwarmRadius)
        {
            swarmManager.SwarmSize--;
            inRadius = false;
        }
    }
    void OnDestroy()
    {
        if (inRadius)
        {
            swarmManager.SwarmSize--;
        }
    }
}
