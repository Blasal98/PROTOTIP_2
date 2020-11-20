using System.Collections;
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

    public override void setTargets(List<Ficha> _list)
    {
        for (int i = 0; i < _list.Capacity; i++)
        {
            if (_list[i] != null)
            {
                switch (sprite_index)
                {
                    case 0:
                        if (i == 0) targets.Add(_list[0]);
                        if (i == 3) targets.Add(_list[3]);
                        if (i == 4) targets.Add(_list[4]);

                        break;
                    case 1:
                        if (i == 1) targets.Add(_list[1]);
                        if (i == 2) targets.Add(_list[2]);
                        if (i == 5) targets.Add(_list[5]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
