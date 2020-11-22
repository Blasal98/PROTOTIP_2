﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : Tierra
{
    public Start(GameObject _parent, bool _local)
    {
        if (!_local) { gameObject.transform.localScale = new Vector3(Constants.Entity.Ficha_Pequeña.scaleX, Constants.Entity.Ficha_Pequeña.scaleY, 1); }
        gameObject.name = "Start";
        gameObject.GetComponent<SpriteRenderer>().sprite =
            UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Tierra/FichaStart"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
    }
}
