﻿using System.Collections;
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

    public int path_index;
    public GameObject textTroops;
    public GameObject textMoney;

    #region constructor
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

        textTroops = new GameObject();
        textTroops.AddComponent<TMPro.TextMeshPro>();
        textTroops.GetComponent<TMPro.TextMeshPro>().color = Color.red;
        textTroops.GetComponent<TMPro.TextMeshPro>().fontSize = 4;
        textTroops.GetComponent<TMPro.TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Center;
        textTroops.transform.position = new Vector3(textTroops.transform.position.x, textTroops.transform.position.y + Constants.Entity.Building.Animation.offset, Constants.Layers.zResult);
        textTroops.transform.SetParent(gameObject.transform);


        textMoney = new GameObject();
        textMoney.AddComponent<TMPro.TextMeshPro>();
        textMoney.GetComponent<TMPro.TextMeshPro>().color = Color.green;
        textMoney.GetComponent<TMPro.TextMeshPro>().fontSize = 4;
        textMoney.GetComponent<TMPro.TextMeshPro>().alignment = TMPro.TextAlignmentOptions.Center;
        textMoney.transform.position = new Vector3(textMoney.transform.position.x, textMoney.transform.position.y, Constants.Layers.zResult);
        textMoney.transform.SetParent(gameObject.transform);


    }
    #endregion
    public override GameObject getTroopText()
    {
        return textTroops;
    }
    public override GameObject getMoneyText()
    {
        return textMoney;
    }
    public override int getPathIndex()
    {
        return path_index;
    }
    public override void setPathIndex(int i)
    {
        path_index = i;
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

        textTroops.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + +Constants.Entity.Building.Animation.offset, Constants.Layers.zResult);
        textMoney.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, Constants.Layers.zResult);
        textTroops.GetComponent<TMPro.TextMeshPro>().text = "";
        textMoney.GetComponent<TMPro.TextMeshPro>().text = "";

        bool isCammo, isArmor, isBoth;

        isCammo = isArmor = isBoth = false;
        if(soldierCount > 0)
        {
            soldierIndicator.SetActive(true);
            if(soldierCount > 9) soldierIndicator.GetComponent<SpriteRenderer>().sprite = soldier_sprites[10];
            else soldierIndicator.GetComponent<SpriteRenderer>().sprite = soldier_sprites[soldierCount];
            for(int i = 0; i < _troops.Count; i++)
            {
                if(_troops[i].type == Troop.troopType.SOLDIER)
                {
                    if (_troops[i].p_type == Troop.propertyType.CAMMO) isCammo = true;
                    if (_troops[i].p_type == Troop.propertyType.ARMOR) isArmor = true;
                    if (_troops[i].p_type == Troop.propertyType.BOTH) isBoth = true;
                }
            }
            if (isBoth) soldierIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.bothColor;
            else if (isCammo && isArmor) soldierIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cam_armColor;
            else if (isCammo) soldierIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cammoColor;
            else if (isArmor) soldierIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.armorColor;
            else soldierIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.nothingColor;

        }
        else
        {
            soldierIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        isCammo = isArmor = isBoth = false;
        if (carCount > 0)
        {
            carIndicator.SetActive(true);
            if (carCount > 9) carIndicator.GetComponent<SpriteRenderer>().sprite = car_sprites[10];
            else carIndicator.GetComponent<SpriteRenderer>().sprite = car_sprites[carCount];
            for (int i = 0; i < _troops.Count; i++)
            {
                if (_troops[i].type == Troop.troopType.CAR)
                {
                    if (_troops[i].p_type == Troop.propertyType.CAMMO) isCammo = true;
                    if (_troops[i].p_type == Troop.propertyType.ARMOR) isArmor = true;
                    if (_troops[i].p_type == Troop.propertyType.BOTH) isBoth = true;
                }
            }
            if (isBoth) carIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.bothColor;
            else if (isCammo && isArmor) carIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cam_armColor;
            else if (isCammo) carIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cammoColor;
            else if (isArmor) carIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.armorColor;
            else carIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.nothingColor;
        }
        else
        {
            carIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        isCammo = isArmor = isBoth = false;
        if (tankCount > 0)
        {
            tankIndicator.SetActive(true);
            if (tankCount > 9) tankIndicator.GetComponent<SpriteRenderer>().sprite = tank_sprites[10];
            else tankIndicator.GetComponent<SpriteRenderer>().sprite = tank_sprites[tankCount];
            for (int i = 0; i < _troops.Count; i++)
            {
                if (_troops[i].type == Troop.troopType.TANK)
                {
                    if (_troops[i].p_type == Troop.propertyType.CAMMO) isCammo = true;
                    if (_troops[i].p_type == Troop.propertyType.ARMOR) isArmor = true;
                    if (_troops[i].p_type == Troop.propertyType.BOTH) isBoth = true;
                }
            }
            if (isBoth) tankIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.bothColor;
            else if (isCammo && isArmor) tankIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cam_armColor;
            else if (isCammo) tankIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cammoColor;
            else if (isArmor) tankIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.armorColor;
            else tankIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.nothingColor;
        }
        else
        {
            tankIndicator.SetActive(false);
        }
        /////////////////////////////////////////////////////////////////////////
        isCammo = isArmor = isBoth = false;
        if (planeCount > 0)
        {
            planeIndicator.SetActive(true);
            if (planeCount > 9) planeIndicator.GetComponent<SpriteRenderer>().sprite = plane_sprites[10];
            else planeIndicator.GetComponent<SpriteRenderer>().sprite = plane_sprites[planeCount];
            for (int i = 0; i < _troops.Count; i++)
            {
                if (_troops[i].type == Troop.troopType.PLANE)
                {
                    if (_troops[i].p_type == Troop.propertyType.CAMMO) isCammo = true;
                    if (_troops[i].p_type == Troop.propertyType.ARMOR) isArmor = true;
                    if (_troops[i].p_type == Troop.propertyType.BOTH) isBoth = true;
                }
            }
            if (isBoth) planeIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.bothColor;
            else if (isCammo && isArmor) planeIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cam_armColor;
            else if (isCammo) planeIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.cammoColor;
            else if (isArmor) planeIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.armorColor;
            else planeIndicator.GetComponent<SpriteRenderer>().color = Constants.Entity.Troop.nothingColor;
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

    public override GameObject indicator(Troop.troopType _t)
    {
        switch (_t)
        {
            case Troop.troopType.SOLDIER:
                return soldierIndicator;
            case Troop.troopType.CAR:
                return carIndicator;
            case Troop.troopType.TANK:
                return tankIndicator;
            case Troop.troopType.PLANE:
                return planeIndicator;
        }
        return null;
    }
    #endregion
}
