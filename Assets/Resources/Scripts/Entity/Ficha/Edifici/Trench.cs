using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trench : Building
{
    private int sprite_index;
    public Trench()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trench0;
        sprite_index = 0;
    }

    public override void nextSprite()
    {
        if (sprite_index == 0) {
            sprite_index++;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench1;
        }
        else if (sprite_index == 1)
        {
            sprite_index++;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench2;
        }
        else if(sprite_index == 2)
        {
            sprite_index = 0;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench0;
        }


    }
    public override void previousSprite()
    {
        if (sprite_index == 0)
        {
            sprite_index = 2;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench2;
        }
        else if (sprite_index == 1)
        {
            sprite_index--;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench0;
        }
        else if (sprite_index == 2)
        {
            sprite_index--;
            gameObject.GetComponent<SpriteRenderer>().sprite = trench1;
        }


    }
}
