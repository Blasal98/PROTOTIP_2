using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : Entity {
    protected bool local;
    protected Ficha()
    {
        if(!gameObject.GetComponent<SpriteRenderer>())
            gameObject.AddComponent<SpriteRenderer>();
                


    }
}
