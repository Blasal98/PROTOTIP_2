using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainGame : MonoBehaviour
{

    #region Variables
    private Camera mainCamera = null;
    private float mainCameraWidth;
    private float mainCameraHeight;

    [SerializeField] private GameObject cursorObject = null;
    private Vector3 cursorPositionUI = new Vector3(0, 0, 0);
    private Vector3 cursorPositionWorld = new Vector3(0, 0, 0);
    private GameObject cursorCollider = null;

    [SerializeField] private GameObject HUD;
    private Hud hud;
 

    Map mainMap;

    private Player localPlayer;
    private List<Player> othersPlayer;
    private int turnIndex;
    private float turnTimeLeft;
    private bool turnEnded;

    bool pause;
    bool ended;
    bool victory;

    bool clicked_right;
    bool clicked_left;

    bool building;
    Building.BuildingType buildingType;


    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCameraHeight = mainCamera.orthographicSize * 2;
        mainCameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        Cursor.visible = false;
        mainCamera.transform.position = new Vector3(Constants.General.CameraStart_x, Constants.General.CameraStart_y, mainCamera.transform.position.z);
        
        cursorCollider = new GameObject("CursorCollider");
        cursorCollider.AddComponent<BoxCollider2D>();
        cursorCollider.GetComponent<BoxCollider2D>().size = new Vector2( 0.01f, 0.01f);
        cursorCollider.GetComponent<BoxCollider2D>().isTrigger = false;
        cursorCollider.AddComponent<Rigidbody2D>();
        cursorCollider.GetComponent<Rigidbody2D>().gravityScale = 0;

        mainMap = new Map();
  
        
        ended = false;
        victory = false;
        pause = false;

        clicked_right = false;
        clicked_left = false;

        localPlayer = new Player(-1);
        othersPlayer = new List<Player>();
        othersPlayer.Add(new Player(othersPlayer.Count));

        turnIndex = 0;
        turnTimeLeft = Constants.General.timeXTurn;
        turnEnded = false;

        hud = HUD.GetComponent<Hud>();

        GetComponentInChildren<Transform>().position = new Vector3(0,0,Constants.Layers.zBackGround);
        building = false;
        buildingType = Building.BuildingType.COUNT;
    }
















    // Update is called once per frame0
    void Update()
    {
        cursorMovement();
        //Debug.Log(Input.mouseScrollDelta); Vec2.y = 0/1/-1

        #region Input
        if (Input.GetMouseButtonDown(0))
        {
            clicked_left = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            clicked_left = false;
        }
        if (Input.GetMouseButtonDown(1))
        {
            clicked_right = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            clicked_right = false;
        }
        if (Input.GetKey(KeyCode.Space))
        {

        }
        #endregion

        if (!ended) //si no sa acabat la partida
        {
            if (!pause) //si no estem en pausa
            {
                mainMap.update();
                updateInfo();
                if (!mainMap.created) //si no esta creat el mapa-----------------------------------------------------------------------------------------------------------------------------------------
                {
                    if (clicked_left)
                    {
                        mainMap.selectFicha();
                        clicked_left = false;
                    }
                }
                else if (mainMap.justCreated) //una interaccio al crearlo-------------------------------------------------------------------------------------------------------------------------------
                {

                    hud.PATH.GetComponent<Button>().interactable = true;
                    hud.SKIP.GetComponent<Button>().interactable = true;
                    mainMap.justCreated = false;
                    hud.switchButtonsVisibility(null);
                    enemyStrategy();
                }
                else if (!turnEnded)//si ja esta creat el mapa i turn no ha acabat-----------------------------------------------------------------------------------------------------------------------
                {
                    if (building)
                    {
                        buildProcess();
                        if (clicked_left)
                        {
                            if (mainMap.setBuilding(localPlayer.buildings[localPlayer.buildings.Count - 1]))
                            {
                                building = false;
                                hud.switchButtonsVisibility(localPlayer);
                            }
                            clicked_left = false;
                        }

                    }

                    turnTimeLeft -= Time.deltaTime;
                    if (turnTimeLeft <= 0) turnEnded = true;

                    if (turnEnded && building)
                    {
                        buildCancel();
                        hud.switchButtonsVisibility(localPlayer);
                    }
                }
                else //acaba turno-----------------------------------------------------------------------------------------------------------------------------------------------------------------------
                {
                    


                    localPlayer.money += localPlayer.moneyXTurn;
                    othersPlayer[0].money += othersPlayer[0].moneyXTurn;
                    //Debug.Log(othersPlayer[0].money);

                    mainMap.nextFicha();

                    turnIndex++;
                    turnEnded = false;
                    turnTimeLeft = Constants.General.timeXTurn;

                    enemyStrategy();//aixo al final sempre -> enemic crea troops per seguent ronda
                }
            }
        }
    }



    private void enemyStrategy()
    {
        switch (turnIndex)
        {
            case 0:
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.CAR);
                addTroopEnemy(Troop.troopType.TANK);
                addTroopEnemy(Troop.troopType.PLANE);
                break;
        }
    }

    private void addTroopEnemy(Troop.troopType _t)
    {
        switch (_t)
        {
            case Troop.troopType.SOLDIER:
                othersPlayer[0].troops.Add(new Soldier());
                othersPlayer[0].money -= Constants.Entity.Troop.Soldier.cost;
                
                break;
            case Troop.troopType.CAR:
                othersPlayer[0].troops.Add(new Car());
                othersPlayer[0].money -= Constants.Entity.Troop.Car.cost;
                
                break;
            case Troop.troopType.TANK:
                othersPlayer[0].troops.Add(new Tank());
                othersPlayer[0].money -= Constants.Entity.Troop.Tank.cost;
                
                break;
            case Troop.troopType.PLANE:
                othersPlayer[0].troops.Add(new Plane());
                othersPlayer[0].money -= Constants.Entity.Troop.Plane.cost;
                
                break;
            default:
                break;
        }
        mainMap.getLocalPath[0].addTroopToFicha(othersPlayer[0].troops[othersPlayer[0].troops.Count - 1]);
        mainMap.getLocalPath[0].updateFicha();
    }


    private void buildProcess()
    {
        localPlayer.buildings[localPlayer.buildings.Count - 1].position = cursorPositionWorld;
        if (Input.mouseScrollDelta.y == 1)
        {
            localPlayer.buildings[localPlayer.buildings.Count - 1].nextSprite();
        }
        else if (Input.mouseScrollDelta.y == -1)
        {
            localPlayer.buildings[localPlayer.buildings.Count - 1].previousSprite();
        }
    }
    private void buildCancel()
    {
        switch (buildingType)
        {
            case Building.BuildingType.TRENCH:
                localPlayer.money += Constants.Entity.Building.Trinchera.cost;

                break;
            case Building.BuildingType.SNIPER:
                localPlayer.money += Constants.Entity.Building.Sniper.cost;

                break;
            case Building.BuildingType.ATANK:
                localPlayer.money += Constants.Entity.Building.AntiTank.cost;

                break;
            case Building.BuildingType.AAIR:
                localPlayer.money += Constants.Entity.Building.AntiAir.cost;

                break;

        }
        Destroy(localPlayer.buildings[localPlayer.buildings.Count - 1].gameObject);
        localPlayer.buildings.RemoveAt(localPlayer.buildings.Count - 1);
        building = false;
    }




    void cursorMovement()
    {
        Resolution auxResolution = Screen.currentResolution;
        cursorPositionWorld = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);
        float auxX = Utilities.Maths.mapping(cursorPositionWorld.x, mainCamera.transform.position.x - mainCameraWidth / 2, mainCamera.transform.position.x + mainCameraWidth / 2, -auxResolution.width / 2, auxResolution.width / 2);
        float auxY = Utilities.Maths.mapping(cursorPositionWorld.y, mainCamera.transform.position.y - mainCameraHeight / 2, mainCamera.transform.position.y + mainCameraHeight / 2, -auxResolution.height / 2, auxResolution.height / 2);
        cursorPositionUI = new Vector3(auxX, auxY, 0);
        cursorObject.transform.localPosition = cursorPositionUI + Constants.General.cursorCorrector;
        cursorCollider.transform.position = cursorPositionWorld;

    }

    #endregion

    #region Hud

    public void updateInfo()
    {
        hud.MONEY.GetComponent<TMPro.TextMeshProUGUI>().text = "Money: " + localPlayer.money.ToString() + "$";
        hud.MONEYXTURN.GetComponent<TMPro.TextMeshProUGUI>().text = "MoneyPerTurn: " + localPlayer.moneyXTurn.ToString() + "$";
        hud.TURN.GetComponent<TMPro.TextMeshProUGUI>().text = "Turn: " + turnIndex;
        hud.TURN_TIME_LEFT.GetComponent<TMPro.TextMeshProUGUI>().text = "Time Left: " + turnTimeLeft + "s";
        hud.LOCAL_HEALTH.GetComponent<TMPro.TextMeshProUGUI>().text = "H: " + localPlayer.health;
        hud.ENEMY_HEALTH.GetComponent<TMPro.TextMeshProUGUI>().text = "H: " + othersPlayer[0].health;

    }

    #endregion

    #region Buttons

    public void exitGame()
    {
        Application.Quit();
    }

    public void returnMenuBttn()
    {
        SceneManager.LoadScene(0);
    }


    public void pauseBttn()
    {
        pause = true;
        hud.PAUSE_SCREEN.SetActive(true);
    }
    public void resumeBttn()
    {
        pause = false;
        hud.PAUSE_SCREEN.SetActive(false);
    }
    public void switchPathVisibility()
    {
        mainMap.switchPathVisibility();
    }

    public void skipTurn()
    {
        turnTimeLeft = 0;
    }

    public void addTroop(int _t)
    {
        switch (_t)
        {
            case (int)Troop.troopType.SOLDIER:
                if(localPlayer.money >= Constants.Entity.Troop.Soldier.cost)
                {
                    localPlayer.troops.Add(new Soldier());
                    localPlayer.money -= Constants.Entity.Troop.Soldier.cost;
                    mainMap.getOthersPath[0][0].addTroopToFicha(localPlayer.troops[localPlayer.troops.Count-1]);
                }
                break;
            case (int)Troop.troopType.CAR:
                if (localPlayer.money >= Constants.Entity.Troop.Car.cost)
                {
                    localPlayer.troops.Add(new Car());
                    localPlayer.money -= Constants.Entity.Troop.Car.cost;
                    mainMap.getOthersPath[0][0].addTroopToFicha(localPlayer.troops[localPlayer.troops.Count - 1]);
                }
                break;
            case (int)Troop.troopType.TANK:
                if (localPlayer.money >= Constants.Entity.Troop.Tank.cost)
                {
                    localPlayer.troops.Add(new Tank());
                    localPlayer.money -= Constants.Entity.Troop.Tank.cost;
                    mainMap.getOthersPath[0][0].addTroopToFicha(localPlayer.troops[localPlayer.troops.Count - 1]);
                }
                break;
            case (int)Troop.troopType.PLANE:
                if (localPlayer.money >= Constants.Entity.Troop.Plane.cost)
                {
                    localPlayer.troops.Add(new Plane());
                    localPlayer.money -= Constants.Entity.Troop.Plane.cost;
                    mainMap.getOthersPath[0][0].addTroopToFicha(localPlayer.troops[localPlayer.troops.Count - 1]);
                }
                break;
            default:
                break;
        }
        mainMap.getOthersPath[0][0].updateFicha();
    }

    public void build(int _b)
    {
        switch (_b)
        {
            case (int)Building.BuildingType.TRENCH:
                if (localPlayer.money >= Constants.Entity.Building.Trinchera.cost)
                {
                    buildingType = Building.BuildingType.TRENCH;
                    localPlayer.money -= Constants.Entity.Building.Trinchera.cost;
                    building = true;
                    localPlayer.buildings.Add(new Trench());

                }
                break;
            case (int)Building.BuildingType.SNIPER:
                if (localPlayer.money >= Constants.Entity.Building.Sniper.cost)
                {
                    buildingType = Building.BuildingType.SNIPER;
                    localPlayer.money -= Constants.Entity.Building.Sniper.cost;
                    building = true;
                    localPlayer.buildings.Add(new Sniper());
   
                }
                break;
            case (int)Building.BuildingType.ATANK:
                if (localPlayer.money >= Constants.Entity.Building.AntiTank.cost)
                {
                    buildingType = Building.BuildingType.ATANK;
                    localPlayer.money -= Constants.Entity.Building.AntiTank.cost;
                    building = true;
                    localPlayer.buildings.Add(new ATank());
                    
                }
                break;
            case (int)Building.BuildingType.AAIR:
                if (localPlayer.money >= Constants.Entity.Building.AntiAir.cost)
                {
                    buildingType = Building.BuildingType.AAIR;
                    localPlayer.money -= Constants.Entity.Building.AntiAir.cost;
                    building = true;
                    localPlayer.buildings.Add(new AAir());

                }
                break;
            default:
                break;
        }
        if (building)
        {
            hud.switchButtonsVisibility(localPlayer);

        }
    }
    public void CityUpgrades(int _i)
    {
        switch (_i)
        {
            case 0:
                if (localPlayer.money >= Constants.Entity.City.FCars_Cost)
                {
                    localPlayer.money -= Constants.Entity.City.FCars_Cost;
                    localPlayer.FCar = true;
                    hud.FCARS_BUTTON.GetComponent<Button>().interactable = false;
                    hud.CAR_BUTTON.GetComponent<Button>().interactable = true;
                }
                break;
            case 1:
                if (localPlayer.money >= Constants.Entity.City.FTanks_Cost)
                {
                    localPlayer.money -= Constants.Entity.City.FTanks_Cost;
                    localPlayer.FTank = true;
                    hud.FTANKS_BUTTON.GetComponent<Button>().interactable = false;
                    hud.TANK_BUTTON.GetComponent<Button>().interactable = true;
                }
                break;
            case 2:
                if (localPlayer.money >= Constants.Entity.City.FPlanes_Cost)
                {
                    localPlayer.money -= Constants.Entity.City.FPlanes_Cost;
                    localPlayer.FPlane = true;
                    hud.FPLANES_BUTTON.GetComponent<Button>().interactable = false;
                    hud.PLANE_BUTTON.GetComponent<Button>().interactable = true;
                }
                break;
            case 3:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MCamo = true;
                    hud.MCAMMO_BUTTON.GetComponent<Button>().interactable = false;
                }
                break;
            case 4:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MArmor = true;
                    hud.MBLINDAJE_BUTTON.GetComponent<Button>().interactable = false;
                }
                break;
            case 5:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MAir = true;
                    hud.MAIR_BUTTON.GetComponent<Button>().interactable = false;
                }
                break;
        }
        
    }
    
    #endregion
}
