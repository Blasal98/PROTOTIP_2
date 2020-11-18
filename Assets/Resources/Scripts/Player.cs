using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    int _id;
    int _money;

    public Player()
    {

    }
    public Player(int _id,int _mny)
    {
        money = _mny;
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
}
