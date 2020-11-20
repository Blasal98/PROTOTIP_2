﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Ficha
{
    public enum BuildingType { TRENCH,SNIPER,ATANK,AAIR, COUNT}

    protected static Sprite trench0, trench1, trench2;
    protected static Sprite sniper0, sniper1;
    protected static Sprite atank0, atank1, atank2;
    protected static Sprite aair;

    protected Building()
    {
        if(aair == null)
        {
            trench0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            trench1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            trench2 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/trench2"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            sniper0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/sniper0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            sniper1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/sniper1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            atank0 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank0"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            atank1 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank1"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
            atank2 = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/atank2"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

            aair = UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Edificio/aair"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
        }
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Constants.Layers.zBuilding);
    }
    public virtual void nextSprite() { }
    public virtual void previousSprite() { }
}
