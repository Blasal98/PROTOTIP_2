using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AAir : Building
{
    public AAir()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = aair;
    }
    public override void setTargets(List<Ficha> _list)
    {
        for (int i = 0; i < _list.Capacity; i++)
        {
            if (_list[i] != null)
            {
                if (i == 0) targets.Add(_list[0]);
                if (i == 1) targets.Add(_list[1]);
                if (i == 2) targets.Add(_list[2]);
                if (i == 3) targets.Add(_list[3]);
                if (i == 4) targets.Add(_list[4]);
                if (i == 5) targets.Add(_list[5]);

            }
        }
    }
}
