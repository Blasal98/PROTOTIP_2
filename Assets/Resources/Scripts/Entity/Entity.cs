using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity 
{
    protected GameObject _gameObject;
    protected Vector2 _position;
    protected Entity()
    {
        gameObject = new GameObject();
        gameObject.transform.position = new Vector3(0,0, gameObject.transform.position.z);
        gameObject.AddComponent<SpriteRenderer>();

    }
    ~Entity()
    {
        Object.Destroy(gameObject);
    }
    public GameObject getObject()
    {
        return gameObject;
    }
    public Vector2 position
    {
        get { return _position; }
        set
        {
            _position = value;
            gameObject.transform.position = new Vector3(position.x, position.y, gameObject.transform.position.z);
        }
    }
    public GameObject gameObject
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }
}
