using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra : Ficha
{
    private List<Troop> troops;

    protected Tierra()
    {

        type = Ficha_Type.TIERRA;
        troops = new List<Troop>();
    }
}
