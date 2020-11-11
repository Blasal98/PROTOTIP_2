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

    private void OnTriggerEnter(Collider other)
    {
        isTriggered = true;
        Debug.Log("HOLAAA");
    }
    private void OnTriggerExit(Collider other)
    {
        isTriggered = false;
    }
    public bool getTriggered()
    {
        return isTriggered;
    }
}

