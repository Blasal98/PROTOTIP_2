using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity 
{
    protected GameObject gameObject;
    protected Entity()
    {
        gameObject = new GameObject();
        gameObject.transform.position = new Vector3(0,0, gameObject.transform.position.z);
        gameObject.AddComponent<SpriteRenderer>();

    }
    public GameObject getObject()
    {
        return gameObject;
    }
}
