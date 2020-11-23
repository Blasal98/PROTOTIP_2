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
    bool won;
    bool victory;

    bool clicked_right;
    bool clicked_left;

    bool building;
    Building.BuildingType buildingType;

    #endregion

    #region Methods

    #region start
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
        won = false;
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
    #endregion

    #region update



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

                    
                    hud.SKIP.GetComponent<Button>().interactable = true;
                    mainMap.justCreated = false;
                    hud.switchButtonsVisibility(null);
                    turnIndex = 0;
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

                    //Debug.Log();

                    localPlayer.money += localPlayer.moneyXTurn;
                    othersPlayer[0].money += othersPlayer[0].moneyXTurn;

                    mainMap.nextFicha();
                    stopRoutines();
                    defense();
                    defenseCity();
                    attackCity();
                    if (localPlayer.health <= 0 || othersPlayer[0].health <= 0) ended = true;
                    if (othersPlayer[0].health <= 0) won = true;

                    turnIndex++;
                    turnEnded = false;
                    turnTimeLeft = Constants.General.timeXTurn;

                    enemyStrategy();//aixo al final sempre -> enemic crea troops per seguent ronda
                    //Debug.Log(othersPlayer[0].troops.Count);
                }
            }
        }
        else
        {
            if (won) hud.WIN.SetActive(true);
            else hud.LOOSE.SetActive(true);
        }
    }
    #endregion

    #region enemyIA
    private void enemyStrategy()
    {
        switch (turnIndex)
        {
            case 0:
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.CAR);
                //addTroopEnemy(Troop.troopType.CAR);
                addTroopEnemy(Troop.troopType.TANK);
                addTroopEnemy(Troop.troopType.PLANE);
                break;
            case 1:
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.SOLDIER);
                addTroopEnemy(Troop.troopType.CAR);
                //addTroopEnemy(Troop.troopType.CAR);
                addTroopEnemy(Troop.troopType.TANK);
                addTroopEnemy(Troop.troopType.PLANE);
                addBuildingEnemy(Building.BuildingType.TRENCH, 3, 3, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 4, 3, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 5, 4, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 6, 4, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 4, 1, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 5, 2, 2);
                addBuildingEnemy(Building.BuildingType.TRENCH, 6, 2, 2);

                break;
            case 2:
                //addTroopEnemy(Troop.troopType.SOLDIER);
                //addTroopEnemy(Troop.troopType.SOLDIER);
                //addTroopEnemy(Troop.troopType.SOLDIER);
                //addTroopEnemy(Troop.troopType.SOLDIER);
                //addTroopEnemy(Troop.troopType.SOLDIER);
                //addTroopEnemy(Troop.troopType.CAR);
                //addTroopEnemy(Troop.troopType.CAR);

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
    private void addBuildingEnemy(Building.BuildingType _t, int i,int j, int sprite_index)
    {
        switch (_t)
        {
            case Building.BuildingType.TRENCH:
                othersPlayer[0].buildings.Add(new Trench());
                break;
            case Building.BuildingType.SNIPER:
                othersPlayer[0].buildings.Add(new Sniper());
                break;
            case Building.BuildingType.ATANK:
                othersPlayer[0].buildings.Add(new ATank());
                break;
            case Building.BuildingType.AAIR:
                othersPlayer[0].buildings.Add(new AAir());
                break;
        }
        othersPlayer[0].buildings[othersPlayer[0].buildings.Count - 1].sprite_index = sprite_index;
        mainMap.setBuildingEnemy(othersPlayer[0].buildings[othersPlayer[0].buildings.Count - 1], i, j);
    }
    #endregion

    #region build
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
    #endregion

    #region atk/df
    void attackCity()
    {
        for (int i = 0; i < mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops().Count; i++) 
        {
            localPlayer.health -= mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops()[i].health;
        }
        for (int i = 0; i < mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops().Count; i++)
        {
            othersPlayer[0].health -= mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops()[i].health;
        }
    }

    void defenseCity()
    {
        if(mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops() != null) {
            if(mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops().Count > 0) { 
                int rand = Random.Range(0, mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops().Count);
                mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops()[rand].health -= localPlayer.attack;

                switch (mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops()[rand].type)
                {
                    case Troop.troopType.SOLDIER:
                        localPlayer.shootings.Add(shootRoutine(mainMap.getLocalPath[Constants.Map.path_size - 1].indicator(Troop.troopType.SOLDIER), localPlayer.shootings.Count));

                        break;
                    case Troop.troopType.CAR:
                        localPlayer.shootings.Add(shootRoutine(mainMap.getLocalPath[Constants.Map.path_size - 1].indicator(Troop.troopType.CAR), localPlayer.shootings.Count));

                        break;
                    case Troop.troopType.TANK:
                        localPlayer.shootings.Add(shootRoutine(mainMap.getLocalPath[Constants.Map.path_size - 1].indicator(Troop.troopType.TANK), localPlayer.shootings.Count));

                        break;
                    case Troop.troopType.PLANE:
                        localPlayer.shootings.Add(shootRoutine(mainMap.getLocalPath[Constants.Map.path_size - 1].indicator(Troop.troopType.PLANE), localPlayer.shootings.Count));
                        
                        break;
                }
                StartCoroutine(localPlayer.shootings[localPlayer.shootings.Count - 1]);

                if (mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops()[rand].health <= 0)
                {

                    localPlayer.troops.Remove(mainMap.getLocalPath[Constants.Map.path_size - 1].getTroops()[rand]);
                    mainMap.killTroop(Constants.Map.path_size - 1, rand, true);
                }
            }
        }
        if (mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops() != null)
        {
            if (mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops().Count > 0)
            {
                int rand = Random.Range(0, mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops().Count);
                mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops()[rand].health -= othersPlayer[0].attack;

                switch (mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops()[rand].type)
                {
                    case Troop.troopType.SOLDIER:
                        othersPlayer[0].shootings.Add(shootRoutine(mainMap.getOthersPath[0][Constants.Map.path_size - 1].indicator(Troop.troopType.SOLDIER), othersPlayer[0].shootings.Count));

                        break;
                    case Troop.troopType.CAR:
                        othersPlayer[0].shootings.Add(shootRoutine(mainMap.getOthersPath[0][Constants.Map.path_size - 1].indicator(Troop.troopType.CAR), othersPlayer[0].shootings.Count));

                        break;
                    case Troop.troopType.TANK:
                        othersPlayer[0].shootings.Add(shootRoutine(mainMap.getOthersPath[0][Constants.Map.path_size - 1].indicator(Troop.troopType.TANK), othersPlayer[0].shootings.Count));

                        break;
                    case Troop.troopType.PLANE:
                        othersPlayer[0].shootings.Add(shootRoutine(mainMap.getOthersPath[0][Constants.Map.path_size - 1].indicator(Troop.troopType.PLANE), othersPlayer[0].shootings.Count));

                        break;
                }
                StartCoroutine(othersPlayer[0].shootings[othersPlayer[0].shootings.Count - 1]);

                if (mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops()[rand].health <= 0)
                {

                    othersPlayer[0].troops.Remove(mainMap.getOthersPath[0][Constants.Map.path_size - 1].getTroops()[rand]);
                    mainMap.killTroop(Constants.Map.path_size - 1, rand, false);
                }
            }
        }
    }

    void defense()
    {
        for(int i = 0; i < localPlayer.buildings.Count; i++) //per cada edifici
        {
            if(localPlayer.buildings[i].targets.Count > 0) //si tenen targets
            {
                bool gotFirst = false;
                int first = -1;
                int killIndex = -1;
                for(int j = mainMap.getLocalPath.Count-2; j > 0; j--) //del target final al primer per trobar el primer target
                {
                    if (gotFirst) break;
                    for (int k = 0; k < localPlayer.buildings[i].targets.Count; k++) //recorre targets per mirar si coincideix amb el blucle superior
                    {
                        if (gotFirst) break;
                        if (mainMap.getLocalPath[j] == localPlayer.buildings[i].targets[k] && localPlayer.buildings[i].targets[k].getTroops().Count > 0) //coincideix i hi ha tropes
                        {
                            for (int g = 0; g < localPlayer.buildings[i].targets[k].getTroops().Count; g++) //recorrem les tropes
                            {
                                if (gotFirst) break;
                                for(int h = 0; h < localPlayer.buildings[i].troopTypes.Count; h++) //recorrem els tipus que pot atacar el edifici
                                {
                                    if (gotFirst) break;
                                    if (localPlayer.buildings[i].targets[k].getTroops()[g].type == localPlayer.buildings[i].troopTypes[h]) //si coincideix algu se fini
                                    {
                                        gotFirst = true;
                                        first = k;
                                        killIndex = j;
                                    }
                                }

                            }
                           
                        }
                    }
                    
                }
                if (gotFirst) {

                    List<Utilities.Pair_TroopInt> auxList = new List<Utilities.Pair_TroopInt>();
                    for (int j = 0; j < localPlayer.buildings[i].targets[first].getTroops().Count; j++) 
                    {
                        for (int k = 0; k < localPlayer.buildings[i].troopTypes.Count; k++) 
                        {
                            if (localPlayer.buildings[i].targets[first].getTroops()[j].type == localPlayer.buildings[i].troopTypes[k])
                                auxList.Add(new Utilities.Pair_TroopInt(localPlayer.buildings[i].targets[first].getTroops()[j], j));
                        }
                    }
                    int rand = Random.Range(0, auxList.Count);
                    localPlayer.buildings[i].targets[first].getTroops()[auxList[rand].i].health -= localPlayer.buildings[i].damage;

                    switch (localPlayer.buildings[i].targets[first].getTroops()[auxList[rand].i].type)
                    {
                        case Troop.troopType.SOLDIER:
                            localPlayer.shootings.Add(shootRoutine(localPlayer.buildings[i].targets[first].indicator(Troop.troopType.SOLDIER), localPlayer.shootings.Count));
                              
                            break;
                        case Troop.troopType.CAR:
                            localPlayer.shootings.Add(shootRoutine(localPlayer.buildings[i].targets[first].indicator(Troop.troopType.CAR), localPlayer.shootings.Count));
                      
                            break;
                        case Troop.troopType.TANK:
                            localPlayer.shootings.Add(shootRoutine(localPlayer.buildings[i].targets[first].indicator(Troop.troopType.TANK), localPlayer.shootings.Count));
                                
                            break;
                        case Troop.troopType.PLANE:
                            localPlayer.shootings.Add(shootRoutine(localPlayer.buildings[i].targets[first].indicator(Troop.troopType.PLANE), localPlayer.shootings.Count));
                                
                            break;
                    }
                    StartCoroutine(localPlayer.shootings[localPlayer.shootings.Count - 1]);

                    if (localPlayer.buildings[i].targets[first].getTroops()[auxList[rand].i].health <= 0)
                    {

                        localPlayer.troops.Remove(localPlayer.buildings[i].targets[first].getTroops()[auxList[rand].i]);
                        mainMap.killTroop(killIndex, auxList[rand].i, true);
                    }
                    
                }
            }
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        for (int i = 0; i < othersPlayer[0].buildings.Count; i++) //per cada edifici
        {
            if (othersPlayer[0].buildings[i].targets.Count > 0) //si tenen targets
            {
                bool gotFirst = false;
                int first = -1;
                int killIndex = -1;
                for (int j = mainMap.getOthersPath[0].Count - 2; j > 0; j--) //del target final al primer per trobar el primer target
                {
                    if (gotFirst) break;
                    for (int k = 0; k < othersPlayer[0].buildings[i].targets.Count; k++) //recorre targets per mirar si coincideix amb el blucle superior
                    {
                        if (gotFirst) break;
                        if (mainMap.getOthersPath[0][j] == othersPlayer[0].buildings[i].targets[k] && othersPlayer[0].buildings[i].targets[k].getTroops().Count > 0) //coincideix i hi ha tropes
                        {
                            for (int g = 0; g < othersPlayer[0].buildings[i].targets[k].getTroops().Count; g++) //recorrem les tropes
                            {
                                if (gotFirst) break;
                                for (int h = 0; h < othersPlayer[0].buildings[i].troopTypes.Count; h++) //recorrem els tipus que pot atacar el edifici
                                {
                                    if (gotFirst) break;
                                    if (othersPlayer[0].buildings[i].targets[k].getTroops()[g].type == othersPlayer[0].buildings[i].troopTypes[h]) //si coincideix algu se fini
                                    {
                                        gotFirst = true;
                                        first = k;
                                        killIndex = j;
                                    }
                                }

                            }

                        }
                    }

                }
                if (gotFirst)
                {

                    List<Utilities.Pair_TroopInt> auxList = new List<Utilities.Pair_TroopInt>();
                    for (int j = 0; j < othersPlayer[0].buildings[i].targets[first].getTroops().Count; j++)
                    {
                        for (int k = 0; k < othersPlayer[0].buildings[i].troopTypes.Count; k++)
                        {
                            if (othersPlayer[0].buildings[i].targets[first].getTroops()[j].type == othersPlayer[0].buildings[i].troopTypes[k])
                                auxList.Add(new Utilities.Pair_TroopInt(othersPlayer[0].buildings[i].targets[first].getTroops()[j], j));
                        }
                    }

                    int rand = Random.Range(0, auxList.Count);
                    othersPlayer[0].buildings[i].targets[first].getTroops()[auxList[rand].i].health -= othersPlayer[0].buildings[i].damage;

                    switch (othersPlayer[0].buildings[i].targets[first].getTroops()[auxList[rand].i].type)
                    {
                        case Troop.troopType.SOLDIER:
                            othersPlayer[0].shootings.Add(shootRoutine(othersPlayer[0].buildings[i].targets[first].indicator(Troop.troopType.SOLDIER), othersPlayer[0].shootings.Count));

                            break;
                        case Troop.troopType.CAR:
                            othersPlayer[0].shootings.Add(shootRoutine(othersPlayer[0].buildings[i].targets[first].indicator(Troop.troopType.CAR), othersPlayer[0].shootings.Count));

                            break;
                        case Troop.troopType.TANK:
                            othersPlayer[0].shootings.Add(shootRoutine(othersPlayer[0].buildings[i].targets[first].indicator(Troop.troopType.TANK), othersPlayer[0].shootings.Count));

                            break;
                        case Troop.troopType.PLANE:
                            othersPlayer[0].shootings.Add(shootRoutine(othersPlayer[0].buildings[i].targets[first].indicator(Troop.troopType.PLANE), othersPlayer[0].shootings.Count));

                            break;
                    }
                    StartCoroutine(othersPlayer[0].shootings[othersPlayer[0].shootings.Count - 1]);

                    if (othersPlayer[0].buildings[i].targets[first].getTroops()[auxList[rand].i].health <= 0)
                    {

                        othersPlayer[0].troops.Remove(othersPlayer[0].buildings[i].targets[first].getTroops()[auxList[rand].i]);
                        mainMap.killTroop(killIndex, auxList[rand].i, false);
                    }
                    
                }
            }
        }
    }
    #endregion

    #region cursor
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
        hud.LOCAL_ATTACK.GetComponent<TMPro.TextMeshProUGUI>().text = "A: " + localPlayer.attack;
        hud.ENEMY_ATTACK.GetComponent<TMPro.TextMeshProUGUI>().text = "A: " + othersPlayer[0].attack;

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
    public void restartGame()
    {
        SceneManager.LoadScene(1);
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
                    localPlayer.moneyXTurn += Constants.Entity.City.FCars_Plus;
                }
                break;
            case 1:
                if (localPlayer.money >= Constants.Entity.City.FTanks_Cost)
                {
                    localPlayer.money -= Constants.Entity.City.FTanks_Cost;
                    localPlayer.FTank = true;
                    hud.FTANKS_BUTTON.GetComponent<Button>().interactable = false;
                    hud.TANK_BUTTON.GetComponent<Button>().interactable = true;
                    localPlayer.moneyXTurn += Constants.Entity.City.FTanks_Plus;
                }
                break;
            case 2:
                if (localPlayer.money >= Constants.Entity.City.FPlanes_Cost)
                {
                    localPlayer.money -= Constants.Entity.City.FPlanes_Cost;
                    localPlayer.FPlane = true;
                    hud.FPLANES_BUTTON.GetComponent<Button>().interactable = false;
                    hud.PLANE_BUTTON.GetComponent<Button>().interactable = true;
                    localPlayer.moneyXTurn += Constants.Entity.City.FPlanes_Plus;
                }
                break;
            case 3:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MCamo = true;
                    hud.MCAMMO_BUTTON.GetComponent<Button>().interactable = false;
                    localPlayer.attack += Constants.Entity.City.MCammo;
                }
                break;
            case 4:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MArmor = true;
                    hud.MBLINDAJE_BUTTON.GetComponent<Button>().interactable = false;
                    localPlayer.attack += Constants.Entity.City.MArmor;
                }
                break;
            case 5:
                if (localPlayer.money >= Constants.Entity.City.Milloras)
                {
                    localPlayer.money -= Constants.Entity.City.Milloras;
                    localPlayer.MAir = true;
                    hud.MAIR_BUTTON.GetComponent<Button>().interactable = false;
                    localPlayer.attack += Constants.Entity.City.MAir;
                }
                break;
        }
        
    }

    #endregion

    #region CoRoutines

    private void stopRoutines()
    {
        for(int i = 0; i < localPlayer.shootings.Count; i++)
        {
            StopCoroutine(localPlayer.shootings[i]);
        }
        for (int i = 0; i < othersPlayer[0].shootings.Count; i++)
        {
            StopCoroutine(othersPlayer[0].shootings[i]);
        }
        for (int j = 0; j < Constants.Map.path_size; j++)
        {
            mainMap.getLocalPath[j].indicator(Troop.troopType.SOLDIER).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getLocalPath[j].indicator(Troop.troopType.CAR).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getLocalPath[j].indicator(Troop.troopType.TANK).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getLocalPath[j].indicator(Troop.troopType.PLANE).GetComponent<SpriteRenderer>().color = Color.white;

            mainMap.getOthersPath[0][j].indicator(Troop.troopType.SOLDIER).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getOthersPath[0][j].indicator(Troop.troopType.CAR).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getOthersPath[0][j].indicator(Troop.troopType.TANK).GetComponent<SpriteRenderer>().color = Color.white;
            mainMap.getOthersPath[0][j].indicator(Troop.troopType.PLANE).GetComponent<SpriteRenderer>().color = Color.white;
        }
        localPlayer.shootings.Clear();
        othersPlayer[0].shootings.Clear();
    }

    private IEnumerator shootRoutine(GameObject troop, int index)
    {
        Color startColor = new Color(1, 1, 1, 1);
        Color endColor = new Color(1, 0, 0, 1);
        Color nowColor = startColor;

        float duration = Constants.Entity.Building.Animation.duration;
        int steps = Constants.Entity.Building.Animation.steps;
        float acumulator = 0;

        while (acumulator < duration)
        {
            troop.GetComponent<SpriteRenderer>().color = nowColor;
            acumulator += duration / steps;
            float mapped = Utilities.Maths.mapping(acumulator, 0, duration, 0, 1);
            nowColor = Utilities.Maths.lerpColor(startColor, endColor, mapped);
            yield return new WaitForSeconds(duration / steps);
        }
       
        troop.GetComponent<SpriteRenderer>().color = startColor;
        //if(localPlayer.shootings.Count > index) localPlayer.shootings.RemoveAt(index);
        yield return null;
    }

    #endregion
}
