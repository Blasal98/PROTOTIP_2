using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Troop
{
    public Tank(propertyType _p)
    {
        type = troopType.TANK;
        health = Constants.Entity.Troop.Tank.health;
        p_type = _p;
    }

}