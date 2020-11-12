using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Map
{
    private FichaSelector[][] selectorMap;
    private Ficha[][] localMap;
    private List<Ficha[][]> othersMap;

    private GameObject mapFolder;

    public Map() {

        selectorMap = new FichaSelector[Constants.Map.w][];
        for (int i = 0; i < Constants.Map.w; i++) { selectorMap[i] = new FichaSelector[Constants.Map.h]; }
        localMap = new Ficha[Constants.Map.w][];
        for (int i = 0; i < Constants.Map.w; i++) { localMap[i] = new Ficha[Constants.Map.h]; }
        othersMap = new List<Ficha[][]>();
        othersMap.Add(new Ficha[Constants.Map.w][]);
        for (int i = 0; i < Constants.Map.w; i++) { othersMap[0][i] = new Ficha[Constants.Map.h]; }

        mapFolder = new GameObject("mapFolder");

        for (int i = 0; i < Constants.Map.w; i++)
        {
            for(int j = 0; j < Constants.Map.h; j++)
            {
                Vector2 auxVecLocal = new Vector2(
                    Utilities.Maths.mapping(Constants.Entity.Ficha.wPix * (3 / 4), 0, Constants.General.scrnDfltWidth, -Constants.General.scrnDfltWidthU / 2, Constants.General.scrnDfltWidthU / 2) 
                    + Utilities.Maths.mapping(Constants.Map.Local.w_separation,0,Constants.General.scrnDfltWidth, 0, Constants.General.scrnDfltWidthU) * (i+1),
                    Utilities.Maths.mapping(Constants.General.scrnDfltHeight - Constants.General.PhotoShop.h_player + Constants.Entity.Ficha.hPix/2, 0, Constants.General.scrnDfltHeight, Constants.General.scrnDfltHeightU / 2, -Constants.General.scrnDfltHeightU / 2) 
                    - Constants.Entity.Ficha.hU * j
                    );
                Vector2 auxVecOthers = new Vector2(
                    Utilities.Maths.mapping(Constants.Entity.Ficha_Pequeña.wPix * (3 / 4), 0, Constants.General.scrnDfltWidth, -Constants.General.scrnDfltWidthU / 2, Constants.General.scrnDfltWidthU / 2)
                    + Utilities.Maths.mapping(Constants.Map.Others.w_separation, 0, Constants.General.scrnDfltWidth, 0, Constants.General.scrnDfltWidthU) * (i + 1),
                    Utilities.Maths.mapping(Constants.Entity.Ficha_Pequeña.hPix /2, 0, Constants.General.scrnDfltHeight, Constants.General.scrnDfltHeightU / 2, -Constants.General.scrnDfltHeightU / 2) 
                    - Constants.Entity.Ficha_Pequeña.hU * j
                    );


                if (!(i % 2 == 0 && j == Constants.Map.h - 1)) {
                    if(i % 2 == 0) { 
                        auxVecLocal = new Vector2(auxVecLocal.x, auxVecLocal.y - Utilities.Maths.mapping(Constants.Entity.Ficha.hPix / 2, 0, Constants.General.scrnDfltWidth, 0, Constants.General.scrnDfltWidthU));
                        auxVecOthers = new Vector2(auxVecOthers.x, auxVecOthers.y - Utilities.Maths.mapping(Constants.Entity.Ficha.hPix / 4, 0, Constants.General.scrnDfltWidth, 0, Constants.General.scrnDfltWidthU));
                    }
                    selectorMap[i][j] = new FichaSelector(mapFolder);
                    selectorMap[i][j].position = auxVecLocal;
                    localMap[i][j] = new FichaVacia(mapFolder,true);
                    localMap[i][j].position = auxVecLocal;
                    othersMap[0][i][j] = new FichaVacia(mapFolder,false);
                    othersMap[0][i][j].position = auxVecOthers;
                }
                    

            }
        }
    }
    public void update() {
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (selectorMap[i][j].gameObject.GetComponent<Trigger>().isTriggered)
                {
                    localMap[i][j].gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0);
                }
                else
                {
                    localMap[i][j].gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
                }
            }
        }
    }
}
