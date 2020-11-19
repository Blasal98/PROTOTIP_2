using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop
{
    public enum troopType { SOLDIER,CAR,TANK,PLANE }
    protected troopType _type;
    private int _health;

   

    public Troop()
    {

    }

    public int health
    {
        get { return _health; }
        set { _health = value; }
    }
    public troopType type
    {
        get { return _type; }
        set { _type = value; }
    }
}
