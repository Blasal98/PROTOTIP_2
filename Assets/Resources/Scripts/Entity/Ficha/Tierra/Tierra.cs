using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra : Ficha
{
    private List<Troop> troops;
    private GameObject soldierIndicator;
    private GameObject carIndicator;
    private GameObject tankIndicator;
    private GameObject planeIndicator;

    public static List<Sprite> soldier_sprites;
    public static List<Sprite> car_sprites;
    public static List<Sprite> tank_sprites;
    public static List<Sprite> plane_sprites;
    public static bool spritesCreated = false;

    protected Tierra()
    {
        if (!spritesCreated)
        {
            soldier_sprites = new List<Sprite>();

            for(int i= 0; i < 10; i++)
            {
                soldier_sprites.Add(UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Troop/Soldier/soldier" + i.ToString()), new Rect(0, 0, Constants.Entity.Ficha.wPix, Constants.Entity.Ficha.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit));
            }
        }

        soldierIndicator = new GameObject("Indicator"); soldierIndicator.AddComponent<SpriteRenderer>();
        soldierIndicator.transform.SetParent(gameObject.transform);







        type = Ficha_Type.TIERRA;
        troops = new List<Troop>();
    }
}
