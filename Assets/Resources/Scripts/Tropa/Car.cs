using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Troop
{
    public Car(propertyType _p)
    {
        type = troopType.CAR;
        health = Constants.Entity.Troop.Car.health;
        p_type = _p;
    }

}