using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainGame : MonoBehaviour
{


    private Camera mainCamera = null;
    private float mainCameraWidth;
    private float mainCameraHeight;
    private float camVel_UtsXFrm = 0.2f;

    [SerializeField] private GameObject cursorObject = null;
    private Vector3 cursorPositionUI = new Vector3(0, 0, 0);
    private Vector3 cursorPositionWorld = new Vector3(0, 0, 0);
    //[SerializeField] private GameObject cursorCollider = null;


    
    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject PAUSE;

    int selectedBuilding;
    List<int> selectedHumans;

    bool villagerBuilding;
    bool targetSetting;

    bool pause = false;
    bool ended = false;
    bool victory = false;


    float initialZombieDelay;
    float initialZombieDelay_Count;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        mainCameraHeight = mainCamera.orthographicSize * 2;
        mainCameraWidth = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        Cursor.visible = false;
        mainCamera.transform.position = new Vector3(Constants.General.CameraStart_x, Constants.General.CameraStart_y, mainCamera.transform.position.z);

        

        //PAUSE.SetActive(false);
        
        ended = false;
        victory = false;
        pause = false;

        initialZombieDelay_Count = 0;
        initialZombieDelay = 30;
    }
















    // Update is called once per frame0
    void Update()
    {
        cursorMovement();
    }





  




    

    void cursorMovement()
    {
        Resolution auxResolution = Screen.currentResolution;
        cursorPositionWorld = new Vector3(mainCamera.ScreenToWorldPoint(Input.mousePosition).x, mainCamera.ScreenToWorldPoint(Input.mousePosition).y, 0);
        float auxX = mapping(cursorPositionWorld.x, mainCamera.transform.position.x - mainCameraWidth / 2, mainCamera.transform.position.x + mainCameraWidth / 2, -auxResolution.width / 2, auxResolution.width / 2);
        float auxY = mapping(cursorPositionWorld.y, mainCamera.transform.position.y - mainCameraHeight / 2, mainCamera.transform.position.y + mainCameraHeight / 2, -auxResolution.height / 2, auxResolution.height / 2);
        cursorPositionUI = new Vector3(auxX, auxY, 0);
        cursorObject.transform.localPosition = cursorPositionUI + Constants.General.cursorCorrector;

    }

    

    float mapping(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
    public void exitGame()
    {
        Application.Quit();
    }





    
    ///////////////////////////////////////////////////////////
    
    public void returnMenu()
    {
        SceneManager.LoadScene(0);
    }

    

    public void resume()
    {
        pause = false;
        PAUSE.SetActive(false);
    }

























    //LAYERS ORDENADES PELS HEALTHbars

}
