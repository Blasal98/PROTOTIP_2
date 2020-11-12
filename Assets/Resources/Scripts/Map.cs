using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Map
{
    private List<FichaSelector> map;
    private List<List<FichaSelector>> othersMap;

    public Map() {
        for(int i = 0; i < Constants.Map.w; i++)
        {
            for(int j = 0; j < Constants.Map.h; j++)
            {
                map.Add(new FichaSelector(i, j, true));
            }
        }
    }

}

