using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Troop
{
    public Car()
    {
        type = troopType.CAR;
        health = Constants.Entity.Troop.Car.health;
    }

}