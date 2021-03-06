﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    bool visible;
    //TROOPS
    public GameObject TROOPS;

    public GameObject SOLDIER_BUTTON;
    public GameObject SOLDIER_PRICE;

    public GameObject CAR_BUTTON;
    public GameObject CAR_PRICE;

    public GameObject TANK_BUTTON;
    public GameObject TANK_PRICE;

    public GameObject PLANE_BUTTON;
    public GameObject PLANE_PRICE;

    //BUILDINGS
    public GameObject BUILDINGS;

    public GameObject TRINCHERA_BUTTON;
    public GameObject TRINCHERA_PRICE;

    public GameObject SNIPER_BUTTON;
    public GameObject SNIPER_PRICE;

    public GameObject ANTITANK_BUTTON;
    public GameObject ANTITANK_PRICE;

    public GameObject ANTIAIR_BUTTON;
    public GameObject ANTIAIR_PRICE;

    //CITY 
    public GameObject MAIN;

    public GameObject FCARS_BUTTON;
    public GameObject FCARS_PRICE;

    public GameObject FTANKS_BUTTON;
    public GameObject FTANKS_PRICE;

    public GameObject FPLANES_BUTTON;
    public GameObject FPLANES_PRICE;

    public GameObject MBLINDAJE_BUTTON;
    public GameObject MBLINDAJE_PRICE;

    public GameObject MCAMMO_BUTTON;
    public GameObject MCAMMO_PRICE;

    public GameObject MAIR_BUTTON;
    public GameObject MAIR_PRICE;

    //OTHERS
    public GameObject PAUSE_SCREEN;
    public GameObject RESTART;
    public GameObject SKIP;

    //INFO
    public GameObject MONEY;
    public GameObject MONEYXTURN;
    public GameObject TURN;
    public GameObject TURN_TIME_LEFT;
    public GameObject ENEMY_HEALTH;
    public GameObject LOCAL_HEALTH;
    public GameObject ENEMY_ATTACK;
    public GameObject LOCAL_ATTACK;

    public GameObject WIN;
    public GameObject LOOSE;

    //PROPERTYS
    public GameObject BUILDINGS_PROPERTIES;
    public GameObject BUILDINGS_CAMMO;
    public GameObject BUILDINGS_ARMOR;
    public GameObject BUILDINGS_X2;

    public GameObject TROOPS_PROPERTIES;
    public GameObject TROOPS_NOTHING;
    public GameObject TROOPS_CAMMO;
    public GameObject TROOPS_ARMOR;
    public GameObject TROOPS_BOTH;
    public GameObject TROOP_TEXT;

    public GameObject INFO_PROPERTIES;

    public GameObject DIFFICULTY_SCREEN;

    // Start is called before the first frame update
    void Start()
    {

        visible = true;

        SKIP.GetComponent<Button>().interactable = false;

        SOLDIER_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Soldier.cost.ToString() + "$";
        CAR_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Car.cost.ToString() + "$";
        TANK_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Tank.cost.ToString() + "$";
        PLANE_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Plane.cost.ToString() + "$";

        TRINCHERA_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.Trinchera.cost.ToString() + "$";
        SNIPER_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.Sniper.cost.ToString() + "$";
        ANTITANK_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.AntiTank.cost.ToString() + "$";
        ANTIAIR_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.AntiAir.cost.ToString() + "$";

        FCARS_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "CarFactory: " + Constants.Entity.City.FCars_Cost.ToString() + "$";
        FTANKS_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "TankFactory: " + Constants.Entity.City.FTanks_Cost.ToString() + "$";
        FPLANES_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "PlaneFactory: " + Constants.Entity.City.FPlanes_Cost.ToString() + "$";

        MCAMMO_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "CammoUpgrade: " + Constants.Entity.City.Milloras.ToString() + "$";
        MBLINDAJE_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "ArmorUpgrade: " + Constants.Entity.City.Milloras.ToString() + "$";
        MAIR_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = "AirUpgrade: " + Constants.Entity.City.Milloras.ToString() + "$";

        ENEMY_HEALTH.GetComponent<TMPro.TextMeshProUGUI>().text = "H: " + Constants.Player.starting_health.ToString() ;
        LOCAL_HEALTH.GetComponent<TMPro.TextMeshProUGUI>().text = "H: " + Constants.Player.starting_health.ToString() ;
        ENEMY_ATTACK.GetComponent<TMPro.TextMeshProUGUI>().text = "A: " + Constants.Player.starting_attack.ToString();
        LOCAL_ATTACK.GetComponent<TMPro.TextMeshProUGUI>().text = "A: " + Constants.Player.starting_attack.ToString();

        switchButtonsVisibility(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchButtonsVisibility(Player _p)
    {
        if (visible)
        {
            SOLDIER_BUTTON.GetComponent<Button>().interactable = false;
            CAR_BUTTON.GetComponent<Button>().interactable = false;
            TANK_BUTTON.GetComponent<Button>().interactable = false;
            PLANE_BUTTON.GetComponent<Button>().interactable = false;

            TRINCHERA_BUTTON.GetComponent<Button>().interactable = false;
            SNIPER_BUTTON.GetComponent<Button>().interactable = false;
            ANTITANK_BUTTON.GetComponent<Button>().interactable = false;
            ANTIAIR_BUTTON.GetComponent<Button>().interactable = false;
            
            FCARS_BUTTON.GetComponent<Button>().interactable = false;
            FTANKS_BUTTON.GetComponent<Button>().interactable = false;
            FPLANES_BUTTON.GetComponent<Button>().interactable = false;

            MCAMMO_BUTTON.GetComponent<Button>().interactable = false;
            MBLINDAJE_BUTTON.GetComponent<Button>().interactable = false;
            MAIR_BUTTON.GetComponent<Button>().interactable = false;

            visible = false;
        }
        else
        {

            SOLDIER_BUTTON.GetComponent<Button>().interactable = true;

            TRINCHERA_BUTTON.GetComponent<Button>().interactable = true;
            SNIPER_BUTTON.GetComponent<Button>().interactable = true;
            ANTITANK_BUTTON.GetComponent<Button>().interactable = true;
            ANTIAIR_BUTTON.GetComponent<Button>().interactable = true;

            if (_p == null)
            {
                FCARS_BUTTON.GetComponent<Button>().interactable = true;
                FTANKS_BUTTON.GetComponent<Button>().interactable = true;
                FPLANES_BUTTON.GetComponent<Button>().interactable = true;

                MCAMMO_BUTTON.GetComponent<Button>().interactable = true;
                MBLINDAJE_BUTTON.GetComponent<Button>().interactable = true;
                MAIR_BUTTON.GetComponent<Button>().interactable = true;
            }
            else
            {
                if (!_p.FCar) FCARS_BUTTON.GetComponent<Button>().interactable = true;
                if (!_p.FTank) FTANKS_BUTTON.GetComponent<Button>().interactable = true;
                if (!_p.FPlane) FPLANES_BUTTON.GetComponent<Button>().interactable = true;

                if (!_p.MCamo) MCAMMO_BUTTON.GetComponent<Button>().interactable = true;
                if (!_p.MArmor) MBLINDAJE_BUTTON.GetComponent<Button>().interactable = true;
                if (!_p.MX2) MAIR_BUTTON.GetComponent<Button>().interactable = true;

                if (_p.FCar) CAR_BUTTON.GetComponent<Button>().interactable = true;
                if (_p.FTank) TANK_BUTTON.GetComponent<Button>().interactable = true;
                if (_p.FPlane) PLANE_BUTTON.GetComponent<Button>().interactable = true;
            }
            visible = true;
        }
    }
}
