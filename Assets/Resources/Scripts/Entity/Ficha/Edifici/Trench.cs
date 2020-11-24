using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trench : Building
{
    
    public Trench()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = trench0;
        sprite_index = 0;
        damage = Constants.Entity.Building.Trinchera.dmg;
        troopTypes.Add(Troop.troopType.SOLDIER);
        troopTypes.Add(Troop.troopType.CAR);

        bType = BuildingType.TRENCH;
    }

    public Trench(int _sIndex)
    {
        switch (_sIndex)
        {
            case 0:
                gameObject.GetComponent<SpriteRenderer>().sprite = trench0;
                break;
            case 1:
                gameObject.GetComponent<SpriteRenderer>().sprite = trench1;
                break;
            case 2:
                gameObject.GetComponent<SpriteRenderer>().sprite = trench2;
                break;
        }

        damage = Constants.Entity.Building.Trinchera.dmg;
        troopTypes.Add(Troop.troopType.SOLDIER);
        troopTypes.Add(Troop.troopType.CAR);

        bType = BuildingType.TRENCH;
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

    public override void setTargets(List<Utilities.Pair_FichaInt> _list)
    {
        for(int i = 0; i< _list.Count; i++)
        {

            switch (sprite_index)
            {
                case 0:
                    if (_list[i].i == 0) targets.Add(_list[i].f);
                    if (_list[i].i == 1) targets.Add(_list[i].f);
                    if (_list[i].i == 4) targets.Add(_list[i].f);
                    if (_list[i].i == 5) targets.Add(_list[i].f);
                    break;
                case 1:
                    if (_list[i].i == 0) targets.Add(_list[i].f);
                    if (_list[i].i == 2) targets.Add(_list[i].f);
                    if (_list[i].i == 3) targets.Add(_list[i].f);
                    if (_list[i].i == 5) targets.Add(_list[i].f);
                    break;
                case 2:
                    if (_list[i].i == 1) targets.Add(_list[i].f);
                    if (_list[i].i == 2) targets.Add(_list[i].f);
                    if (_list[i].i == 3) targets.Add(_list[i].f);
                    if (_list[i].i == 4) targets.Add(_list[i].f);
                    break;                                   
                default:
                    break;
            }
            
        }
        //for(int i = 0;i< targets.Count; i++)
        //{
        //    Debug.Log(targets[i].i + " " +  targets[i].j);
        //}
    }
}
