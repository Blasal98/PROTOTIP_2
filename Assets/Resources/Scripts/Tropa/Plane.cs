using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Troop
{
    public Plane()
    {
        type = troopType.PLANE;
        health = Constants.Entity.Troop.Plane.health;
    }

}
