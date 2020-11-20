using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAir : Building
{
    public AAir()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = aair;
    }
}
