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

    public override void setTargets(List<Ficha> _list)
    {
        for(int i = 0; i< _list.Count; i++)
        {
            if(_list != null)
            {
                switch (sprite_index)
                {
                    case 0:
                        if (i == 0) targets.Add(_list[0]);
                        if (i == 1) targets.Add(_list[1]);
                        if (i == 4) targets.Add(_list[4]);
                        if (i == 5) targets.Add(_list[5]);
                        break;
                    case 1:
                        if (i == 0) targets.Add(_list[0]);
                        if (i == 2) targets.Add(_list[2]);
                        if (i == 3) targets.Add(_list[3]);
                        if (i == 5) targets.Add(_list[5]);
                        break;
                    case 2:
                        if (i == 1) targets.Add(_list[1]);
                        if (i == 2) targets.Add(_list[2]);
                        if (i == 3) targets.Add(_list[3]);
                        if (i == 4) targets.Add(_list[4]);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
