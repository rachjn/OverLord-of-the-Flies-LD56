using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMovement : MonoBehaviour
{

     private void Start()
    {
        StartMovement();
    }
    public void StartMovement() {
        transform.LeanMoveLocal(new Vector2(7.1003f, -4.0399f), .5f).setEaseOutQuart().setLoopPingPong();

    }
}
