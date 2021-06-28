using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Troop
{
    public Soldier(propertyType _p)
    {
        type = troopType.SOLDIER;
        health = Constants.Entity.Troop.Soldier.health;
        p_type = _p;
    }
   
}
