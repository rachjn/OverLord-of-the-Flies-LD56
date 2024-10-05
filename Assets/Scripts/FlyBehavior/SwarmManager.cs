using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwarmManager : MonoBehaviour
{

    [SerializeField]
    private float swarmRadiusBase;
    [SerializeField]
    private float swarmRadiusIncRate;
    [SerializeField]
    private float swarmLookahead;

    private int swarmSize;
    public int SwarmSize{
        get { return swarmSize; }
        set { 
            swarmSize = value; 
            swarmRadius = swarmRadiusBase + swarmRadiusIncRate*value;
        }
    }

    [SerializeField]
    private float swarmRadius;
    public float SwarmRadius{
        get { return swarmRadius; }
    }
    private Rigidbody2D parentRb;

    // Start is called before the first frame update
    void Start()
    {
        SwarmSize = 0;
        parentRb = GetComponentInParent<Rigidbody2D>();
        Debug.Log(parentRb);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(swarmRadius, swarmRadius, swarmRadius) * 2;
        transform.localPosition = parentRb.velocity * swarmLookahead;
    }
}
