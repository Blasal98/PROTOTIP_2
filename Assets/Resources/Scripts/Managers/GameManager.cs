using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region PUBLIC VARIABLES

    [Header("Prefabs")]
    public GameObject playerPrefab;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera upperCamera;
    public Camera cityCamera;

    [Header("Players")]
    public int maxPlayers = 2;

    [HideInInspector]
    public List<PlayerManager> players;

    [HideInInspector]
    public List<Vector3> playerPositions;

    #endregion

    #region PRIVATE VARIABLES

    private float playerDistanceError = 3;

    private GameObject GameFolder;

    #endregion

    #region PRIVATE_METHODS

    void Start()
    {
        GameFolder = GameObject.Find("-----GAME-----");
        playerPositions = new List<Vector3>();
        for (int i = 0; i < maxPlayers; i++)
            playerPositions.Add(new Vector3(0, 0, i * CONSTANTS.CELL.radius * CONSTANTS.MAP.maxHeight * playerDistanceError));

        players = new List<PlayerManager>();
        for (int i = 0; i < maxPlayers; i++)
            players.Add(Instantiate(playerPrefab,playerPositions[i],Quaternion.identity,GameFolder.transform).GetComponent<PlayerManager>());
    }

    void Update()
    {

    }

    #endregion


}
