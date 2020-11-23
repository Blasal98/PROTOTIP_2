using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int _id;
    int _money;
    int _moneyXTurn;
    List<Troop> _troops;
    List<Building> _buildings;
    List<IEnumerator> _shootings;
    int _health;
    int _attack;

    public bool FCar, FTank, FPlane, MCamo, MArmor, MAir;

    public Player(){}
    
    public Player(int _id)
    {
        money = Constants.Player.starting_money;
        moneyXTurn = Constants.Player.starting_moneyXTurn;
        id = _id;
        health = Constants.Player.starting_health;
        attack = Constants.Player.starting_attack;

        FCar = FTank = FPlane = MCamo = MArmor = MAir = false;

        troops = new List<Troop>();
        buildings = new List<Building>();
        shootings = new List<IEnumerator>();
    }
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public int money
    {
        get { return _money; }
        set { _money = value; }
    }
    public int moneyXTurn
    {
        get { return _moneyXTurn; }
        set { _moneyXTurn = value; }
    }
    public List<Troop> troops
    {
        get { return _troops; }
        set { _troops = value; }
    }
    public List<Building> buildings
    {
        get { return _buildings; }
        set { _buildings = value; }
    }
    public int health
    {
        get { return _health; }
        set { _health = value; }
    }
    public int attack
    {
        get { return _attack; }
        set { _attack = value; }
    }
    public List<IEnumerator> shootings
    {
        get { return _shootings; }
        set { _shootings = value; }
    }
}
