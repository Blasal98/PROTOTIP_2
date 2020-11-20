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
}
