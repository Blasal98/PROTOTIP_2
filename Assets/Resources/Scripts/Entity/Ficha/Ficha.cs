﻿using System.Collections;
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

    public virtual GameObject indicator(Troop.troopType _t) { return null; }
    public virtual void addTroopToFicha(Troop _t) { }
    public virtual void killTroopOfFicha(int index) { }
    public virtual void countTroops() { }
    public virtual void updateFicha() { }
    public virtual List<Troop> getTroops() { return null; }
    public virtual void setTroops(List<Troop> _t) { }
    public virtual void setTargets(List<Utilities.Pair_FichaInt> _list) { }
    public virtual List<Ficha> getTargets() { return null; }
    public virtual bool isTargetable() { return false; }
    public virtual List<bool> getUpgrades() { return null; }
    public virtual void setUpgrades(int i) { }
    public virtual Building.BuildingType getBuildingType() { return 0; }
    public virtual int getPathIndex() { return 0; }
    public virtual void setPathIndex(int i) { }
    public virtual GameObject getTroopText() { return null; }
    public virtual GameObject getMoneyText() { return null; }
}
