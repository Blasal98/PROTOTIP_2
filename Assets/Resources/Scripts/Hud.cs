using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    //TROOPS
    public GameObject TROOPS;

    public GameObject SOLDIER_BUTTON;
    public GameObject SOLDIER_PRICE;
    public GameObject SOLDIER_QUANT;

    public GameObject CAR_BUTTON;
    public GameObject CAR_PRICE;
    public GameObject CAR_QUANT;

    public GameObject TANK_BUTTON;
    public GameObject TANK_PRICE;
    public GameObject TANK_QUANT;

    public GameObject PLANE_BUTTON;
    public GameObject PLANE_PRICE;
    public GameObject PLANE_QUANT;

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


    //OTHERS
    public GameObject PAUSE_SCREEN;
    public GameObject PATH;
    public GameObject SKIP;

    //INFO
    public GameObject MONEY; 
    public GameObject TURN;
    public GameObject TURN_TIME_LEFT;


    // Start is called before the first frame update
    void Start()
    {
        



        PATH.GetComponent<Button>().interactable = false;
        SKIP.GetComponent<Button>().interactable = false;

        SOLDIER_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Soldier.cost.ToString() + "$";
        CAR_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Car.cost.ToString() + "$";
        TANK_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Tank.cost.ToString() + "$";
        PLANE_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Troop.Plane.cost.ToString() + "$";

        TRINCHERA_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.Trinchera.cost.ToString() + "$";
        SNIPER_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.Sniper.cost.ToString() + "$";
        ANTITANK_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.AntiTank.cost.ToString() + "$";
        ANTIAIR_PRICE.GetComponent<TMPro.TextMeshProUGUI>().text = Constants.Entity.Building.AntiAir.cost.ToString() + "$";

        switchTroopsAndBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void switchTroopsAndBuildings()
    {
        if (SOLDIER_BUTTON.GetComponent<Button>().interactable)
        {
            SOLDIER_BUTTON.GetComponent<Button>().interactable = false;
            CAR_BUTTON.GetComponent<Button>().interactable = false;
            TANK_BUTTON.GetComponent<Button>().interactable = false;
            PLANE_BUTTON.GetComponent<Button>().interactable = false;

            TRINCHERA_BUTTON.GetComponent<Button>().interactable = false;
            SNIPER_BUTTON.GetComponent<Button>().interactable = false;
            ANTITANK_BUTTON.GetComponent<Button>().interactable = false;
            ANTIAIR_BUTTON.GetComponent<Button>().interactable = false;
        }
        else
        {
            SOLDIER_BUTTON.GetComponent<Button>().interactable = true;
            CAR_BUTTON.GetComponent<Button>().interactable = true;
            TANK_BUTTON.GetComponent<Button>().interactable = true;
            PLANE_BUTTON.GetComponent<Button>().interactable = true;

            TRINCHERA_BUTTON.GetComponent<Button>().interactable = true;
            SNIPER_BUTTON.GetComponent<Button>().interactable = true;
            ANTITANK_BUTTON.GetComponent<Button>().interactable = true;
            ANTIAIR_BUTTON.GetComponent<Button>().interactable = true;
        }
    }
}
