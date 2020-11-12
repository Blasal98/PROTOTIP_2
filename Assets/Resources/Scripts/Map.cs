using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Map
{
    private FichaSelector[][] map;
    private List<Vector2[][]> othersMap;

    private GameObject mapFolder;

    public Map() {
        map = new FichaSelector[Constants.Map.w][];
        for (int i = 0; i < Constants.Map.w; i++) { map[i] = new FichaSelector[Constants.Map.h]; }

        mapFolder = new GameObject("mapFolder");

        for (int i = 0; i < Constants.Map.w; i++)
        {
            for(int j = 0; j < Constants.Map.h; j++)
            {

                Vector2 auxVec = new Vector2(
                    Utilities.Maths.photoshopTOunity(Constants.Map.Local.x_min) + Constants.Entity.Ficha.wU * i,
                    Utilities.Maths.photoshopTOunity(Constants.Map.Local.y_max) - Constants.Entity.Ficha.hU * j
                    );

                map[i][j] = new FichaSelector(mapFolder);
                map[i][j].position = auxVec;

            }
        }
    }
    

}

