using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : Entity {

    protected bool local;
    public enum Ficha_Type { TIERRA, EDIFICIO, VACIO}
    private Ficha_Type _type;

    private int _i, _j;
    protected Ficha()
    {


    }
    public int i
    {
        get { return _i; }
        set { _i = value; }
    }
    public int j
    {
        get { return _j; }
        set { _j = value; }
    }
    public Ficha_Type type
    {
        get { return _type; }
        set { _type = value; }
    }
}
