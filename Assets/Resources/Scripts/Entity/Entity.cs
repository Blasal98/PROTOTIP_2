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
        gameObject.transform.position = new Vector3(0,0,0);
        gameObject.AddComponent<SpriteRenderer>();
        _position = new Vector2(0,0);

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
