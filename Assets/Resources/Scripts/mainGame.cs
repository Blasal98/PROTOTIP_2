﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mainGame : MonoBehaviour
{


    private Camera mainCamera = null;
    private float mainCameraWidth;
    private float mainCameraHeight;

    [SerializeField] private GameObject cursorObject = null;
    private Vector3 cursorPositionUI = new Vector3(0, 0, 0);
    private Vector3 cursorPositionWorld = new Vector3(0, 0, 0);
    private GameObject cursorCollider = null;


    [SerializeField] private GameObject HUD;
    [SerializeField] private GameObject PAUSE;

    Map mainMap;

    bool pause = false;
    bool ended = false;
    bool victory = false;


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

       
    }
















    // Update is called once per frame0
    void Update()
    {
        cursorMovement();
        //Debug.Log(fixa.getObject().GetComponent<Trigger>().getTriggered());
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

    

    


    public void exitGame()
    {
        Application.Quit();
    }


    
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
