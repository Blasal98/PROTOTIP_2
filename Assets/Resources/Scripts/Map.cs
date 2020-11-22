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
    private GameObject localPathFolder;
    private GameObject othersPathFolder;
    private GameObject localBuildingsFolder;
    private GameObject othersBuildingsFolder;

    private bool _created;
    private bool _justCreated;
    #endregion

    #region Map Initialization
    public Map() {
        created = false;
        justCreated = true;
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
        localPathFolder = new GameObject("localPath");
        localPathFolder.transform.SetParent(pathFolder.transform);
        othersPathFolder = new GameObject("othersPath");
        othersPathFolder.transform.SetParent(pathFolder.transform);

        localBuildingsFolder = new GameObject("localBuildings");
        othersBuildingsFolder = new GameObject("othersBuildings");

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

            localPathFolder.AddComponent<LineRenderer>();
            localPathFolder.GetComponent<LineRenderer>().startWidth = localPathFolder.GetComponent<LineRenderer>().endWidth = Constants.Map.Local.path_width;
            localPathFolder.GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            localPathFolder.GetComponent<LineRenderer>().startColor = new Color(0, 0, 1);
            localPathFolder.GetComponent<LineRenderer>().endColor = new Color(1, 1, 0);
            localPathFolder.GetComponent<LineRenderer>().positionCount = Constants.Map.path_size;

            othersPathFolder.AddComponent<LineRenderer>();
            othersPathFolder.GetComponent<LineRenderer>().startWidth = othersPathFolder.GetComponent<LineRenderer>().endWidth = Constants.Map.Others.path_width;
            othersPathFolder.GetComponent<LineRenderer>().material = new Material(Shader.Find("Sprites/Default"));
            othersPathFolder.GetComponent<LineRenderer>().startColor = new Color(0, 0, 1);
            othersPathFolder.GetComponent<LineRenderer>().endColor = new Color(1, 1, 0);
            othersPathFolder.GetComponent<LineRenderer>().positionCount = Constants.Map.path_size;

            Vector3[] localPoints = new Vector3[Constants.Map.path_size];
            Vector3[] othersPoints = new Vector3[Constants.Map.path_size];

            for (int i = 0; i< Constants.Map.path_size; i++)
            {
                localPath[i].gameObject.transform.SetParent(localPathFolder.transform);
                othersPath[0][i].gameObject.transform.SetParent(othersPathFolder.transform);

                localPoints[i] = new Vector3 (localPath[i].position.x, localPath[i].position.y, Constants.Layers.zPath);
                othersPoints[i] = new Vector3(othersPath[0][i].position.x, othersPath[0][i].position.y, Constants.Layers.zPath); 
            }
            localPathFolder.GetComponent<LineRenderer>().SetPositions(localPoints);
            othersPathFolder.GetComponent<LineRenderer>().SetPositions(othersPoints);



            created = true;
        }
    }
    #endregion

    #region Public Methods
    public void update() {
        Ficha auxFicha = null;
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1))
                {
                    
                    if (selectorMap[i][j].gameObject.GetComponent<Trigger>().isTriggered)
                    {
                        localMap[i][j].gameObject.GetComponent<SpriteRenderer>().color = Constants.Map.hoverFicha;
                        if (localMap[i][j].type == Ficha.Ficha_Type.EDIFICIO) auxFicha = localMap[i][j];
                    }
                    else
                    {
                        localMap[i][j].gameObject.GetComponent<SpriteRenderer>().color = Constants.Map.normalFicha;
                    }
                }
                

            }
        }
        if (auxFicha != null)
        {
            for (int k = 0; k < auxFicha.getTargets().Count; k++)
            {
                auxFicha.getTargets()[k].gameObject.GetComponent<SpriteRenderer>().color = Constants.Map.hoverFicha;
            }
        }
    }

    public bool isTouching(Ficha _f1, Ficha _f2)
    {
        if (_f1.i == _f2.i && _f1.j == _f2.j) return false;
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
    public List<Utilities.Pair_FichaInt> getTouchingCaminos(Ficha _b, bool local)
    {
        List<Utilities.Pair_FichaInt> returnList = new List<Utilities.Pair_FichaInt>();

        //if (local) { }
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1))
                {
                    if (local) { 
                        if (isTouching(_b, localMap[i][j]) && localMap[i][j].isTargetable())
                        {
                            if(_b.i % 2 == 0)
                            {
                                if(i == _b.i - 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 0));
                                }
                                else if (i == _b.i - 1 && j == _b.j + 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 1));
                                }
                                else if (i == _b.i + 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 4));
                                }
                                else if (i == _b.i + 1 && j == _b.j + 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 5));
                                }
                            }
                            else
                            {
                                if (i == _b.i - 1 && j == _b.j - 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 0));
                                }
                                else if (i == _b.i - 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 1));
                                }
                                else if (i == _b.i + 1 && j == _b.j - 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 4));
                                }
                                else if (i == _b.i + 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 5));
                                }
                            }
                            if(i == _b.i && j == _b.j - 1)
                            {
                                returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 2));
                            }
                            else if (i == _b.i && j == _b.j + 1)
                            {
                                returnList.Add(new Utilities.Pair_FichaInt(localMap[i][j], 3));
                            }
                        }
                    }
                    else
                    {
                        if (isTouching(_b, othersMap[0][i][j]) &&  othersMap[0][i][j].isTargetable())
                        {
                            if (_b.i % 2 == 0)
                            {
                                if (i == _b.i - 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 0));
                                }
                                else if (i == _b.i - 1 && j == _b.j + 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 1));
                                }
                                else if (i == _b.i + 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 4));
                                }
                                else if (i == _b.i + 1 && j == _b.j + 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 5));
                                }
                            }
                            else
                            {
                                if (i == _b.i - 1 && j == _b.j - 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 0));
                                }
                                else if (i == _b.i - 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 1));
                                }
                                else if (i == _b.i + 1 && j == _b.j - 1)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 4));
                                }
                                else if (i == _b.i + 1 && j == _b.j)
                                {
                                    returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 5));
                                }
                            }
                            if (i == _b.i && j == _b.j - 1)
                            {
                                returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 2));
                            }
                            else if (i == _b.i && j == _b.j + 1)
                            {
                                returnList.Add(new Utilities.Pair_FichaInt( othersMap[0][i][j], 3));
                            }
                        }
                    }
                }
            }
        }
        //for(int i = 0; i< returnList.Count; i++)
        //{
        //    Debug.Log(returnList[i].i);
        //}
        return returnList;
    }

    public void switchPathVisibility()
    {
        if (localPathFolder.GetComponent<LineRenderer>().enabled)
        {
            localPathFolder.GetComponent<LineRenderer>().enabled = false;
            othersPathFolder.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            localPathFolder.GetComponent<LineRenderer>().enabled = true;
            othersPathFolder.GetComponent<LineRenderer>().enabled = true;
        }
    }

    public void nextFicha()
    {
        List<Troop> auxListL = localPath[Constants.Map.path_size - 1].getTroops();
        localPath[Constants.Map.path_size - 1].setTroops(null);
        localPath[Constants.Map.path_size - 1].countTroops();
        List<Troop> auxListO = othersPath[0][Constants.Map.path_size - 1].getTroops();
        othersPath[0][Constants.Map.path_size - 1].setTroops(null);
        othersPath[0][Constants.Map.path_size - 1].countTroops();

        for (int i = Constants.Map.path_size - 1; i > 0 ; i--)
        {
            localPath[i].setTroops(localPath[i - 1].getTroops());
            localPath[i].countTroops();
            localPath[i].updateFicha();
            othersPath[0][i].setTroops(othersPath[0][i - 1].getTroops());
            othersPath[0][i].countTroops();
            othersPath[0][i].updateFicha();
        }
        localPath[0].setTroops(new List<Troop>());
        localPath[0].countTroops();
        localPath[0].updateFicha();
        othersPath[0][0].setTroops(new List<Troop>());
        othersPath[0][0].countTroops();
        othersPath[0][0].updateFicha();

        


        for (int i = 0; i < auxListL.Count; i++)
        {
            localPath[Constants.Map.path_size - 1].addTroopToFicha(auxListL[i]);
        }
        for (int i = 0; i < auxListO.Count; i++)
        {
            othersPath[0][Constants.Map.path_size - 1].addTroopToFicha(auxListO[i]);
        }
        localPath[Constants.Map.path_size - 1].updateFicha();
        othersPath[0][Constants.Map.path_size - 1].updateFicha();
    }

    public bool setBuilding(Building _b)
    {
        bool returnBool = false;
        for (int i = 0; i < Constants.Map.w; i++)
        {
            for (int j = 0; j < Constants.Map.h; j++)
            {
                if (!(i % 2 == 0 && j == Constants.Map.h - 1))
                {
                    if (selectorMap[i][j].gameObject.GetComponent<Trigger>().isTriggered && localMap[i][j].type == Ficha.Ficha_Type.VACIO)
                    {

                        bool auxBool = false;
                        for(int k = 1; k < Constants.Map.path_size-1; k++)
                        {
                            if (isTouching(localMap[i][j], localPath[k])) auxBool = true;
                        }
                        if (auxBool) { 

                            Object.Destroy(localMap[i][j].gameObject);
                            _b.position = localMap[i][j].position;
                            localMap[i][j] = _b;
                            localMap[i][j].i = i; localMap[i][j].j = j;
                            returnBool = true;

                            localMap[i][j].setTargets(getTouchingCaminos(localMap[i][j],true));
                            localMap[i][j].gameObject.transform.SetParent(localBuildingsFolder.transform);
                        }
                    }
                }
            }
        }
        return returnBool;
    }
    public void setBuildingEnemy(Building _b, int i, int j)
    {
        Object.Destroy(othersMap[0][i][j].gameObject);
        _b.position = othersMap[0][i][j].position;
        othersMap[0][i][j] = _b;
        othersMap[0][i][j].i = i; localMap[i][j].j = j;
        othersMap[0][i][j].setTargets(getTouchingCaminos(othersMap[0][i][j],false));
        othersMap[0][i][j].gameObject.transform.localScale = new Vector3(Constants.Entity.Ficha_Pequeña.scaleX, Constants.Entity.Ficha_Pequeña.scaleY, 1);
        othersMap[0][i][j].gameObject.transform.SetParent(othersBuildingsFolder.transform);
    }

    public void killTroop(int i_path, int i_troop, bool local)
    {
        if (local)
        {
            localPath[i_path].killTroopOfFicha(i_troop);
            
        }
        else
        {
            othersPath[0][i_path].killTroopOfFicha(i_troop);
        }
    }

    #endregion

    #region properties
    public bool created
    {
        get { return _created; }
        set { _created = value; }
    }
    public bool justCreated
    {
        get { return _justCreated; }
        set { _justCreated = value; }
    }
    public List<Ficha> getLocalPath
    {
        get { return localPath; }
    }
    public List<List<Ficha>> getOthersPath
    {
        get { return othersPath; }
    }
    #endregion
}
