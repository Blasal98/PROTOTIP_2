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
                    Utilities.Maths.mapping(106 * (3 / 4), 0, Constants.General.scrnDfltWidth, -Constants.General.scrnDfltWidthU / 2, Constants.General.scrnDfltWidthU / 2) 
                    + Utilities.Maths.mapping(Constants.Map.Local.w_separation,0,1920, 0, Constants.General.scrnDfltWidthU) * (i+1),
                    Utilities.Maths.mapping(1080 - 700 + 50, 0, Constants.General.scrnDfltHeight, Constants.General.scrnDfltHeightU / 2, -Constants.General.scrnDfltHeightU / 2) - Constants.Entity.Ficha.hU * j
                    );



                if (!(i % 2 == 0 && j == Constants.Map.h - 1)) {
                    if(i % 2 == 0)
                        auxVec = new Vector2(auxVec.x, auxVec.y - Utilities.Maths.mapping(Constants.Entity.Ficha.hPix / 2, 0, 1920, 0, Constants.General.scrnDfltWidthU));
                    map[i][j] = new FichaSelector(mapFolder);
                    map[i][j].position = auxVec;
                }
                    

            }
        }
    }
    

}

