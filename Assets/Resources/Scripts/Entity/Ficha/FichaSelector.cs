using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaSelector
{
    private Vector2 _position;
    private GameObject _gameObject;
    private Trigger _trigger;

    public FichaSelector(GameObject _parent)
    {
        gameObject = new GameObject("Ficha");
        gameObject.AddComponent<PolygonCollider2D>();

        Vector2[] points = new Vector2[6];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(Constants.Entity.Ficha.wU / 2, 0);
        points[2] = new Vector2(Constants.Entity.Ficha.wU * 3 / 4, -Constants.Entity.Ficha.hU / 2);
        points[3] = new Vector2(Constants.Entity.Ficha.wU / 2, -Constants.Entity.Ficha.hU);
        points[4] = new Vector2(0, -Constants.Entity.Ficha.hU);
        points[5] = new Vector2(-Constants.Entity.Ficha.wU / 4, -Constants.Entity.Ficha.hU / 2);
        gameObject.GetComponent<PolygonCollider2D>().points = points;
        gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(-Constants.Entity.Ficha.wU / 4, Constants.Entity.Ficha.hU / 2);

        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.AddComponent<Trigger>();
        _trigger = gameObject.GetComponent<Trigger>();

        gameObject.transform.SetParent(_parent.transform);
    }
    

    public Vector2 position
    {
        get { return _position; }
        set {
            _position = value;
            gameObject.transform.position = new Vector3(position.x, position.y, gameObject.transform.position.z);
        }
    }
    public GameObject gameObject
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }

    public bool isTriggered
    {
        get { return _trigger.isTriggered; }
        set { _trigger.isTriggered = value; }
    }
}
