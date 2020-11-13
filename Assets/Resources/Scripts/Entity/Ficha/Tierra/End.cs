using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : Tierra
{
    public End(GameObject _parent, bool _local)
    {
        if (!_local) { gameObject.transform.localScale = new Vector3((float)42 / 78, 0.5f, 1); }
        gameObject.name = "End";
        gameObject.GetComponent<SpriteRenderer>().sprite =
            UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Ficha/Tierra/FichaEnd"), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit);

    }
}
