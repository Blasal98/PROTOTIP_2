using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Trigger : MonoBehaviour
{
    private bool _isTriggered;

    private void Start()
    {
        isTriggered = false;
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isTriggered = true;
        Debug.Log("g");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isTriggered = false;
    }

    public bool isTriggered
    {
        get { return _isTriggered; }
        set { _isTriggered = value; }
    }
}

