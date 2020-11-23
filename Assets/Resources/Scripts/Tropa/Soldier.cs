using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Troop
{
    public Soldier()
    {
        type = troopType.SOLDIER;
        health = Constants.Entity.Troop.Soldier.health;
    }
   
}
