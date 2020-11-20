using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATank : Building
{
    private int sprite_index;
    public ATank()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = atank0;
        sprite_index = 0;
    }

    public override void nextSprite()
    {
        if (sprite_index == 0)
        {
            sprite_index++;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank1;
        }
        else if (sprite_index == 1)
        {
            sprite_index++;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank2;
        }
        else if (sprite_index == 2)
        {
            sprite_index = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank0;
        }


    }
    public override void previousSprite()
    {
        if (sprite_index == 0)
        {
            sprite_index = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank2;
        }
        else if (sprite_index == 1)
        {
            sprite_index--;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank0;
        }
        else if (sprite_index == 2)
        {
            sprite_index--;
            gameObject.GetComponent<SpriteRenderer>().sprite = atank1;
        }


    }
}
