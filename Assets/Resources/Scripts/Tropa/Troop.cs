using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop
{
    enum troopType { SOLDIER,CAR,TANK,PLANE }
    troopType type;
    private int _health;

   

    public Troop()
    {

    }

    public int health
    {
        get { return _health; }
        set { _health = value; }
    }
}
