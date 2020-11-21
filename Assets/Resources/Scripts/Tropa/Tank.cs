using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Troop
{
    public Tank()
    {
        type = troopType.TANK;
        health = Constants.Entity.Troop.Tank.health;
    }

}