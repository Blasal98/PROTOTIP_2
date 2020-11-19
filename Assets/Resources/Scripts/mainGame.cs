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
    }
















    // Update is called once per frame0
    void Update()
    {
        cursorMovement();

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
                if (!mainMap.created) //si no esta creat el mapa
                {
                    if (clicked_left)
                    {
                        mainMap.selectFicha();
                        clicked_left = false;
                    }
                }
                else if (mainMap.justCreated) //una interaccio al crearlo
                {

                    hud.PATH.GetComponent<Button>().interactable = true;
                    hud.SKIP.GetComponent<Button>().interactable = true;
                    mainMap.justCreated = false;
                    hud.switchTroopsAndBuildings();
                }
                else if (!turnEnded)//si ja esta creat el mapa i turn no ha acabat
                {


                    turnTimeLeft -= Time.deltaTime;
                    if (turnTimeLeft <= 0) turnEnded = true;
                }
                else //acaba turno
                {
                    localPlayer.money += localPlayer.moneyXTurn;
                    othersPlayer[0].money += othersPlayer[0].moneyXTurn;
                    turnIndex++;
                    turnEnded = false;
                    turnTimeLeft = Constants.General.timeXTurn;
                }
            }
        }
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

   

    #endregion
}
