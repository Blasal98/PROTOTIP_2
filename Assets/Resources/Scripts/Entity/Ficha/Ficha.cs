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
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Constants.Layers.zFicha);

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

    public virtual void addTroopToFicha(Troop _t) { }
    public virtual void countTroops() { }
    public virtual void updateFicha() { }
    public virtual List<Troop> getTroops() { return null; }
    public virtual void setTroops(List<Troop> _t) { }
}
