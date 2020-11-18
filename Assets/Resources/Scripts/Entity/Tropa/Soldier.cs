using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Troop
{
    public Soldier()
    {
        gameObject.name = "Soldier";
        gameObject.GetComponent<SpriteRenderer>().sprite =
            UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Tierra/FichaEnd"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);
    }
   
}
