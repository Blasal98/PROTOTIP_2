using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class Trigger : MonoBehaviour
{
    private bool isTriggered;
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
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isTriggered = false;
    }
    public bool getTriggered()
    {
        return isTriggered;
    }
}

