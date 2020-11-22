using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra : Ficha
{
    private List<Troop> _troops;
    private GameObject soldierIndicator;
    private GameObject carIndicator;
    private GameObject tankIndicator;
    private GameObject planeIndicator;

    private int soldierCount;
    private int carCount;
    private int tankCount;
    private int planeCount;


    public static List<Sprite> soldier_sprites;
    public static List<Sprite> car_sprites;
    public static List<Sprite> tank_sprites;
    public static List<Sprite> plane_sprites;

    protected Tierra()
    {
        if (soldier_sprites == null)
        {
            soldier_sprites = new List<Sprite>();
            car_sprites = new List<Sprite>();
            tank_sprites = new List<Sprite>();
            plane_sprites = new List<Sprite>();

            for (int i= 0; i < 11; i++)
            {
                soldier_sprites.Add(UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Troop/Soldier/soldier" + i.ToString()), new Rect(0, 0, Constants.Entity.Troop.wPix, Constants.Entity.Troop.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit));
                car_sprites.Add(UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Troop/Car/car" + i.ToString()), new Rect(0, 0, Constants.Entity.Troop.wPix, Constants.Entity.Troop.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit));
                tank_sprites.Add(UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Troop/Tank/tank" + i.ToString()), new Rect(0, 0, Constants.Entity.Troop.wPix, Constants.Entity.Troop.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit));
                plane_sprites.Add(UnityEngine.Sprite.Create(Resources.Load<Texture2D>("Images/Troop/Plane/plane" + i.ToString()), new Rect(0, 0, Constants.Entity.Troop.wPix, Constants.Entity.Troop.hPix), new Vector2(0.5f, 0.5f), Constants.Entity.imgDfltPiXUnit));
            }
        }

        soldierIndicator = new GameObject("SoldierIndicator");
        soldierIndicator.AddComponent<SpriteRenderer>();
        soldierIndicator.GetComponent<SpriteRenderer>().sprite = soldier_sprites[0];
        soldierIndicator.transform.SetParent(gameObject.transform);
        soldierIndicator.transform.position = new Vector3(soldierIndicator.transform.position.x - Constants.Entity.Troop.scale / 2.5f,
                                                          soldierIndicator.transform.position.y + Constants.Entity.Troop.scale / 2.5f, Constants.Layers.zFichaIndicator);
        soldierIndicator.transform.localScale = new Vector3(Constants.Entity.Troop.scale, Constants.Entity.Troop.scale, 1);

        carIndicator = new GameObject("CarIndicator");
        carIndicator.AddComponent<SpriteRenderer>();
        carIndicator.GetComponent<SpriteRenderer>().sprite = car_sprites[0];
        carIndicator.transform.SetParent(gameObject.transform);
        carIndicator.transform.position = new Vector3(carIndicator.transform.position.x + Constants.Entity.Troop.scale / 2.5f,
                                                      carIndicator.transform.position.y + Constants.Entity.Troop.scale / 2.5f, Constants.Layers.zFichaIndicator);
        carIndicator.transform.localScale = new Vector3(Constants.Entity.Troop.scale, Constants.Entity.Troop.scale, 1);

        tankIndicator = new GameObject("TankIndicator");
        tankIndicator.AddComponent<SpriteRenderer>();
        tankIndicator.GetComponent<SpriteRenderer>().sprite = tank_sprites[0];
        tankIndicator.transform.SetParent(gameObject.transform);
        tankIndicator.transform.position = new Vector3(tankIndicator.transform.position.x + Constants.Entity.Troop.scale / 2.5f,
                                                       tankIndicator.transform.position.y - Constants.Entity.Troop.scale / 2.5f, Constants.Layers.zFichaIndicator);
        tankIndicator.transform.localScale = new Vector3(Constants.Entity.Troop.scale, Constants.Entity.Troop.scale, 1);

        planeIndicator = new GameObject("PlaneIndicator");
        planeIndicator.AddComponent<SpriteRenderer>();
        planeIndicator.GetComponent<SpriteRenderer>().sprite = plane_sprites[0];
        planeIndicator.transform.SetParent(gameObject.transform);
        planeIndicator.transform.position = new Vector3(planeIndicator.transform.position.x - Constants.Entity.Troop.scale / 2.5f,
                                                        planeIndicator.transform.position.y - Constants.Entity.Troop.scale / 2.5f, Constants.Layers.zFichaIndicator);
        planeIndicator.transform.localScale = new Vector3(Constants.Entity.Troop.scale, Constants.Entity.Troop.scale, 1);

        soldierIndicator.SetActive(false);
        carIndicator.SetActive(false);
        tankIndicator.SetActive(false);
        planeIndicator.SetActive(false);

        type = Ficha_Type.TIERRA;
        _troops = new List<Troop>();

        soldierCount = 0;
        carCount = 0;
        tankCount = 0;
        planeCount = 0;
    }
    public override void addTroopToFicha(Troop _t)
    {
        if (_troops == null) _troops = new List<Troop>();
        switch (_t.type)
        {
            case Troop.troopType.SOLDIER:
                _troops.Add(_t);
                soldierCount++;
                break;
            case Troop.troopType.CAR:
                _troops.Add(_t);
                carCount++;
                break;
            case Troop.troopType.TANK:
                _troops.Add(_t);
                tankCount++;
                break;
            case Troop.troopType.PLANE:
                _troops.Add(_t);
                planeCount++;
                break;
            default:
                break;
        }
    }
    public override void killTroopOfFicha(int index)
    {
        _troops.RemoveAt(index);
        countTroops();
        updateFicha();
    }
    public override void countTroops()
    {
        soldierCount = 0;
        carCount = 0;
        tankCount = 0;
        planeCount = 0;


        if(_troops != null) { 
            for (int i = 0; i < _troops.Count; i++)
            {
                switch (_troops[i].type)
                {
                    case Troop.troopType.SOLDIER:
                        soldierCount++;
                        break;
                    case Troop.troopType.CAR:
                        carCount++;
                        break;
                    case Troop.troopType.TANK:
                        tankCount++;
                        break;
                    case Troop.troopType.PLANE:
                        planeCount++;
                        break;

                }
            }
        }

    }
    public override void updateFicha() {
        
        if(soldierCount > 0)
        {
            soldierIndicator.SetActive(true);
            if(soldierCount > 9) soldierIndicator.GetComponent<SpriteRenderer>().sprite = soldier_sprites[10];
            else soldierIndicator.GetComponent<SpriteRenderer>().sprite = soldier_sprites[soldierCount];
        }
        else
        {
            soldierIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        if (carCount > 0)
        {
            carIndicator.SetActive(true);
            if (carCount > 9) carIndicator.GetComponent<SpriteRenderer>().sprite = car_sprites[10];
            else carIndicator.GetComponent<SpriteRenderer>().sprite = car_sprites[carCount];
        }
        else
        {
            carIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        if (tankCount > 0)
        {
            tankIndicator.SetActive(true);
            if (tankCount > 9) tankIndicator.GetComponent<SpriteRenderer>().sprite = tank_sprites[10];
            else tankIndicator.GetComponent<SpriteRenderer>().sprite = tank_sprites[tankCount];
        }
        else
        {
            tankIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        if (planeCount > 0)
        {
            planeIndicator.SetActive(true);
            if (planeCount > 9) planeIndicator.GetComponent<SpriteRenderer>().sprite = plane_sprites[10];
            else planeIndicator.GetComponent<SpriteRenderer>().sprite = plane_sprites[planeCount];
        }
        else
        {
            planeIndicator.SetActive(false);
        }
    }

    #region get/set ers

    public override List<Troop> getTroops()
    {
        return _troops;
    }
    public override void setTroops(List<Troop> _t)
    {
        _troops = _t;
    }
    #endregion
}
