using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    #region VARIABLES

    private Vector3 startPosition;

    [Header("Prefabs")]
    public GameObject cellPrefab;
    public GameObject floorPrefab;

    public int width = 0;
    public int height = 0;

    private CellManager[][] cellMap;
    private GameObject floor;

    private GameManager gM;
    #endregion

    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (width > CONSTANTS.MAP.maxWidth || height > CONSTANTS.MAP.maxHeight || width % 2 == 0)
        {
            Debug.Log("MAP_DIMENSIONS_ERROR");
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            startPosition = transform.parent.transform.position;

            floor = Instantiate(floorPrefab, new Vector3(0, -0.01f, 0), Quaternion.identity, transform);
            floor.transform.localScale = new Vector3(10, 1, 10);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {

                    Vector3 spawnPosition = startPosition + new Vector3(i * UTILS.MATH.Hexagon_X_Displacement(CONSTANTS.CELL.radius), 0, j * UTILS.MATH.Hexagon_Z_Displacement(CONSTANTS.CELL.radius) * 2.0f);
                    Quaternion spawnQuaternion = Quaternion.identity;


                    if (!(i % 2 == 0 && j == height - 1))
                    {
                        if (i % 2 == 0)
                        {
                            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, spawnPosition.z + UTILS.MATH.Hexagon_Z_Displacement(CONSTANTS.CELL.radius));
                        }

                        Instantiate(cellPrefab, spawnPosition, spawnQuaternion, transform);
                    }
                }
            }
        }
    }

    void Update()
    {
        
    }

 
}
