using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{

    [SerializeField]
    private float idleRadiusBase;
    [SerializeField]
    private float idleRadiusIncRate;
    [SerializeField]
    private float swarmLookahead;

    [SerializeField]
    private SwarmManager enemySwarm;

    private HashSet<GameObject> flies = new HashSet<GameObject>();
    public HashSet<GameObject> Flies 
    {
        get { return flies; }
    }
    public int SwarmSize
    {
        get { return flies.Count; }
    }

    [SerializeField]
    public float IdleRadius 
    {
        get { return idleRadiusBase + idleRadiusIncRate*SwarmSize; }
    }

    public float LassoRadius 
    {
        get { return 2*(idleRadiusBase + idleRadiusIncRate*SwarmSize); }
    }
    private Rigidbody2D parentRb;

    // Start is called before the first frame update
    void Start()
    {
        parentRb = GetComponentInParent<Rigidbody2D>();
        addFly(transform.parent.gameObject);
        Debug.Log(parentRb);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(IdleRadius, IdleRadius, IdleRadius) * 2;
        transform.localPosition = parentRb.velocity * swarmLookahead;
    }

    public HashSet<GameObject> GetEnemyFlies()
    {
        return (enemySwarm == null) ? new HashSet<GameObject>() : enemySwarm.Flies;
    }

    public bool addFly(GameObject fly)
    {
        if (fly.CompareTag(tag))
        {
            return flies.Add(fly);
        }
        return false;
    }
    public void removeFly(GameObject fly)
    {
        flies.Remove(fly);
    }
}
