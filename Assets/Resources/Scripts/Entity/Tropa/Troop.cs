using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop : Entity
{
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
