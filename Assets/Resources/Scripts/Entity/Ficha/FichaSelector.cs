using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FichaSelector
{
    private Vector2 _position;
    private GameObject _gameObject;

    public FichaSelector(int _i, int _j, bool local)
    {
        gameObject = new GameObject("Ficha" + _i + _j);
        gameObject.AddComponent<PolygonCollider2D>();

        Vector2[] points = new Vector2[6];
        points[0] = new Vector2(0, 0);
        points[1] = new Vector2(Constants.Entity.Ficha.w / 2, 0);
        points[2] = new Vector2(Constants.Entity.Ficha.w * 3 / 4, -Constants.Entity.Ficha.h / 2);
        points[3] = new Vector2(Constants.Entity.Ficha.w / 2, -Constants.Entity.Ficha.h);
        points[4] = new Vector2(0, -Constants.Entity.Ficha.h);
        points[5] = new Vector2(-Constants.Entity.Ficha.w / 4, -Constants.Entity.Ficha.h / 2);
        gameObject.GetComponent<PolygonCollider2D>().points = points;
        gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(-Constants.Entity.Ficha.w / 4, Constants.Entity.Ficha.h / 2);

        gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        gameObject.AddComponent<Trigger>();

        if (local)
        {

        }
    }
    public Vector2 position
    {
        get { return _position; }
        set { _position = value; }
    }
    public GameObject gameObject
    {
        get { return _gameObject; }
        set { _gameObject = value; }
    }
}
