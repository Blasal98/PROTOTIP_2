﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Building
{
    private int sprite_index;
    public Sniper()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sniper0;
        sprite_index = 0;
    }

    public override void nextSprite()
    {
        if (sprite_index == 0)
        {
            sprite_index=1;
            gameObject.GetComponent<SpriteRenderer>().sprite = sniper1;
        }
        else if (sprite_index == 1)
        {
            sprite_index=0;
            gameObject.GetComponent<SpriteRenderer>().sprite = sniper0;
        }


    }
    public override void previousSprite()
    {
        nextSprite();

    }

    public override void setTargets(List<Utilities.Pair_FichaInt> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            switch (sprite_index)
            {
                case 0:
                    if (_list[i].i == 0) targets.Add(_list[i].f);
                    if (_list[i].i == 3) targets.Add(_list[i].f);
                    if (_list[i].i == 4) targets.Add(_list[i].f);

                    break;
                case 1:
                    if (_list[i].i == 1) targets.Add(_list[i].f);
                    if (_list[i].i == 2) targets.Add(_list[i].f);
                    if (_list[i].i == 5) targets.Add(_list[i].f);
                    break;
                default:
                    break;
            }
            
        }
        Debug.Log(_list.Count);
    }

}