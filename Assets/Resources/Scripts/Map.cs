using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Map
{
    #region Variables
    private FichaSelector[][] selectorMap;
    private Ficha[][] localMap;
    private List<Ficha[][]> othersMap;
    private List<Ficha> localPath;
    private List<List<Ficha>> othersPath;

    private int pathCount;

    private GameObject mapFolder;
    private GameObject pathFolder;
    private bool _created;
    #endregion

    #region Map Initialization
    public Map() {
        created = false;
        pathCount = 0;

        localPath = new List<Ficha>();
        othersPath = new List<List<Ficha>>();
        othersPath.Add(new List<Ficha>());

        selectorMap = new FichaSelector[Constants.Map.w][];
        for (int i = 0; i < Constants.Map.w; i++) { selectorMap[i] = new FichaSelector[Constants.Map.h]; }
        localMap = new Ficha[Constants.Map.w][];
        for (int i = 0; i < Constants.Map.w; i++) { localMap[i] = new Ficha[Constants.Map.h]; }
        othersMap = new List<Ficha[][]>();
        othersMap.Add(new Ficha[Constants.Map.w][]);
        for (int i = 0; i < Constants.Map.w; i++) { othersMap[0][i] = new Ficha[Constants.Map.h]; }

        mapFolder = new GameObject("mapFolder");
        pathFolder = new GameObject("pathFolder");

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
                    localMap[i][j].i = i; localMap[i][j].j = j;
                    othersMap[0][i][j] = new FichaVacia(mapFolder,false);
                    othersMap[0][i][j].position = auxVecOthers;
                    othersMap[0][i][j].i = i; othersMap[0][i][j].j = j;
                }
                    

            }
        }
        
    }
    public void selectFicha()
    {
        bool onlyOne = false;
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1) && !onlyOne) //condicio basica del mapa + nomes una escollida
                {
                    if (selectorMap[i][j].gameObject.GetComponent<Trigger>().isTriggered) // esta trigerejada pel ratoli
                    {
                        if (pathCount < Constants.Map.path_size && localMap[i][j].type == Ficha.Ficha_Type.VACIO) // te que estar vuida i tenir path disponible
                        {
                            bool auxBool = true;
                            if (pathCount > 0) //
                            {
                                auxBool = isTouching(localPath[localPath.Count - 1], localMap[i][j]);
                                
                            }
                            if (auxBool)
                            {

                                Vector2 auxPos = localMap[i][j].position; //guardes posicio, destrueixes objecte
                                Object.Destroy(localMap[i][j].gameObject);
                                if (pathCount == 0) //decideixes quin nou objecte el substituira
                                {
                                    localMap[i][j] = new Start(pathFolder, true);
                                }
                                else if (pathCount == Constants.Map.path_size - 1)
                                {
                                    localMap[i][j] = new End(pathFolder, true);
                                }
                                else
                                {
                                    localMap[i][j] = new Camino(pathFolder, true);
                                }
                                localMap[i][j].position = auxPos; //actualitzes posicio e index
                                localMap[i][j].i = i; localMap[i][j].j = j;
                                localPath.Add(localMap[i][j]); //afegeix a Path
                                pathCount++;

                                onlyOne = true;
                            }
                        }
                        
                    }
                }
            }
        }
        if (pathCount == Constants.Map.path_size)//aqui predeterminem el del enemic
        { 
            Vector2 auxPos = othersMap[0][1][3].position; Object.Destroy(othersMap[0][1][3].gameObject);
            othersMap[0][1][3] = new Start(pathFolder, false); othersPath[0].Add(othersMap[0][1][3]); othersMap[0][1][3].position = auxPos;

            auxPos = othersMap[0][2][2].position; Object.Destroy(othersMap[0][2][2].gameObject);
            othersMap[0][2][2] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][2][2]); othersMap[0][2][2].position = auxPos;

            auxPos = othersMap[0][3][2].position; Object.Destroy(othersMap[0][3][2].gameObject);
            othersMap[0][3][2] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][3][2]); othersMap[0][3][2].position = auxPos;

            auxPos = othersMap[0][4][2].position; Object.Destroy(othersMap[0][4][2].gameObject);
            othersMap[0][4][2] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][4][2]); othersMap[0][4][2].position = auxPos;

            auxPos = othersMap[0][5][3].position; Object.Destroy(othersMap[0][5][3].gameObject);
            othersMap[0][5][3] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][5][3]); othersMap[0][5][3].position = auxPos;

            auxPos = othersMap[0][6][3].position; Object.Destroy(othersMap[0][6][3].gameObject);
            othersMap[0][6][3] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][6][3]); othersMap[0][6][3].position = auxPos;

            auxPos = othersMap[0][7][4].position; Object.Destroy(othersMap[0][7][4].gameObject);
            othersMap[0][7][4] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][7][4]); othersMap[0][7][4].position = auxPos;

            auxPos = othersMap[0][8][4].position; Object.Destroy(othersMap[0][8][4].gameObject);
            othersMap[0][8][4] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][8][4]); othersMap[0][8][4].position = auxPos;

            auxPos = othersMap[0][9][4].position; Object.Destroy(othersMap[0][9][4].gameObject);
            othersMap[0][9][4] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][9][4]); othersMap[0][9][4].position = auxPos;

            auxPos = othersMap[0][10][3].position; Object.Destroy(othersMap[0][10][3].gameObject);
            othersMap[0][10][3] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][10][3]); othersMap[0][10][3].position = auxPos;

            auxPos = othersMap[0][11][3].position; Object.Destroy(othersMap[0][11][3].gameObject);
            othersMap[0][11][3] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][11][3]); othersMap[0][11][3].position = auxPos;

            auxPos = othersMap[0][12][2].position; Object.Destroy(othersMap[0][12][2].gameObject);
            othersMap[0][12][2] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][12][2]); othersMap[0][12][2].position = auxPos;

            auxPos = othersMap[0][13][3].position; Object.Destroy(othersMap[0][13][3].gameObject);
            othersMap[0][13][3] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][13][3]); othersMap[0][13][3].position = auxPos;

            auxPos = othersMap[0][13][4].position; Object.Destroy(othersMap[0][13][4].gameObject);
            othersMap[0][13][4] = new Camino(pathFolder, false); othersPath[0].Add(othersMap[0][13][4]); othersMap[0][13][4].position = auxPos;

            auxPos = othersMap[0][13][5].position; Object.Destroy(othersMap[0][13][5].gameObject);
            othersMap[0][13][5] = new End(pathFolder, false); othersPath[0].Add(othersMap[0][13][5]); othersMap[0][13][5].position = auxPos;

            for(int i = 0; i< Constants.Map.path_size; i++)
            {
                localPath[i].gameObject.transform.SetParent(pathFolder.transform);
                othersPath[0][i].gameObject.transform.SetParent(pathFolder.transform);
            }


            created = true;
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

    public bool isTouching(Ficha _f1, Ficha _f2)
    {
        if(_f2.i == _f1.i || _f2.i == _f1.i + 1 || _f2.i == _f1.i - 1)
        {
            if(_f1.i % 2 == 0)
            {
                if (_f2.j == _f1.j || _f2.j == _f1.j + 1 || (_f2.j == _f1.j - 1 && _f2.i == _f1.i)) return true;
            }
            else
            {
                if (_f2.j == _f1.j || _f2.j == _f1.j - 1 || (_f2.j == _f1.j + 1 && _f2.i == _f1.i)) return true;
            }
        }

        return false;
    }

    #region properties
    public bool created
    {
        get { return _created; }
        set { _created = value; }
    }
    #endregion
}
