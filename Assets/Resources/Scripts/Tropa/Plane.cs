using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Troop
{
    public Plane(propertyType _p)
    {
        type = troopType.PLANE;
        health = Constants.Entity.Troop.Plane.health;
        p_type = _p;
    }

}
