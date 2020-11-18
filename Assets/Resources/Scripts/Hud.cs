using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{

    public GameObject PAUSE_SCREEN; //GAMEOBJECTS
    public GameObject PATH;
    public GameObject SKIP;

    public GameObject MONEY; //TMPRO
    public GameObject TURN;
    public GameObject TURN_TIME_LEFT;


    // Start is called before the first frame update
    void Start()
    {
        PATH.GetComponent<Button>().interactable = false;
        SKIP.GetComponent<Button>().interactable = false;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
