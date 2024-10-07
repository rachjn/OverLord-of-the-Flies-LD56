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

    public bool Attacking;
    public bool Retreating;

    [SerializeField]
    public float IdleRadius 
    {
        get { return Retreating? idleRadiusBase : idleRadiusBase + (float)Math.Sqrt(idleRadiusIncRate * SwarmSize); }
    }

    public float LassoRadius 
    {
        get { return Retreating ? idleRadiusBase : 1.5f*(idleRadiusBase + (float)Math.Sqrt(idleRadiusIncRate * SwarmSize)); }
    }
    private Rigidbody2D parentRb;
    private string team;
    private string enemyTeam;

    // private Color player1Color;

    // Start is called before the first frame update
    void Start()
    {
        team = tag;
        enemyTeam = (CompareTag("Player1")) ? "Player2" : "Player1";

        if (enemySwarm == null)
        {
            foreach (var controller in FindObjectsOfType<SwarmManager>())
            {
                if (controller.CompareTag(enemyTeam))
                {
                    enemySwarm = controller;
                }
            }
        }

        parentRb = GetComponentInParent<Rigidbody2D>();
        addFly(transform.parent.gameObject);
        Debug.Log(parentRb);
    }

    // Update is called once per frame
    void Update()
    {
        if (Attacking)
        {
            transform.position = enemySwarm.transform.position;
        }
        else if (Retreating)
        {
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.localPosition = parentRb.velocity * swarmLookahead;
        }
        transform.localScale = new Vector3(IdleRadius, IdleRadius, IdleRadius) * 2;
    }

    public HashSet<GameObject> GetEnemyFlies()
    {
        return (enemySwarm == null) ? new HashSet<GameObject>() : enemySwarm.Flies;
    }

    public void OnDestroy()
    {
        // when we die, lose the game
        GameManager.Instance.WinGame(enemyTeam);

        foreach(var g in GameObject.FindGameObjectsWithTag(enemyTeam))  
        {
            Destroy(g);
        }
        foreach(var g in GameObject.FindGameObjectsWithTag(team))  
        {
            Destroy(g);
        }
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
