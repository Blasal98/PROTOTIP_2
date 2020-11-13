using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Map
{
    #region Variables
    private FichaSelector[][] selectorMap;
    private Ficha[][] localMap;
    private List<Ficha[][]> othersMap;

    private GameObject mapFolder;
    private bool _created;
    #endregion

    #region Map Initialization
    public Map() {
        created = false;
        
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
    #endregion

    public void update() {
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1))
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

    public void selectFicha()
    {
        bool selected = false;
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1) && !selected)
                { 
                    if (selectorMap[i][j].gameObject.GetComponent<Trigger>().isTriggered)
                    {
                        Vector2 auxPos = localMap[i][j].position;
                        Object.Destroy(localMap[i][j].gameObject);
                        localMap[i][j] = new Camino();
                        localMap[i][j].position = auxPos;
                        selected = true;
                    }
                }
            }
        }
    }

    #region properties
    public bool created
    {
        get { return _created; }
        set { _created = value; }
    }
    #endregion
}
