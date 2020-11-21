using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAir : Building
{
    public AAir()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = aair;
    }
    public override void setTargets(List<Utilities.Pair_FichaInt> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {

            if (_list[i].i == 0) targets.Add(_list[i].f);
            if (_list[i].i == 1) targets.Add(_list[i].f);
            if (_list[i].i == 2) targets.Add(_list[i].f);
            if (_list[i].i == 3) targets.Add(_list[i].f);
            if (_list[i].i == 4) targets.Add(_list[i].f);
            if (_list[i].i == 5) targets.Add(_list[i].f);

            
        }
        //Debug.Log(targets.Count);
    }
}
