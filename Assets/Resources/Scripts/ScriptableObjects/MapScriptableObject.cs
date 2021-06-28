using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapScriptableObject", menuName = "ScriptableObjects", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public GameObject base_Prefab;
    public GameObject base_base_unfilled_Prefab;
    public GameObject base_base_filled_Prefab;

}
