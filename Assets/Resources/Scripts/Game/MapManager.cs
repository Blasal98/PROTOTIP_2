using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    #region VARIABLES

    public Vector3 startPosition;

    public MapScriptableObject mapScrObj;

    private CellManager[][] cellMap;

    #endregion

    void Start()
    {
        for (int i = 0; i < mapScrObj.width; i++)
        {
            for (int j = 0; j < mapScrObj.height; j++)
            {

                Vector3 spawnPosition = new Vector3(i * 10, 0, j * 10);
                Quaternion spawnQuaternion = new Quaternion(0, 0, 0, 1);

                //Instantiate(.cell_base_unfilled_Prefab,spawnPosition,spawnQuaternion,transform);


                if (!(i % 2 == 0 && j == Constants.Map.h - 1))
                {
                    if (i % 2 == 0)
                    {
                        
                    }
                }
            }
        }
    }

    void Update()
    {
        
    }
}
