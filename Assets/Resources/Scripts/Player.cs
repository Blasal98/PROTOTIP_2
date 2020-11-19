using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int _id;
    int _money;
    int _moneyXTurn;


    public Player()
    {

    }
    public Player(int _id)
    {
        money = Constants.Player.starting_money;
        moneyXTurn = Constants.Player.starting_moneyXTurn;
        id = _id;
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
}
