﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camino : Tierra
{
    public Camino(GameObject _parent, bool _local)
    {
        if (!_local) { gameObject.transform.localScale = new Vector3((float)42 / 78, 0.5f, 1); }
        gameObject.name = "Camino";
        gameObject.GetComponent<SpriteRenderer>().sprite = 
            UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Tierra/Camino"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
    }
}