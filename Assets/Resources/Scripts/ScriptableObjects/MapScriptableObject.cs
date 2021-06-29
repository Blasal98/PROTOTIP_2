using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapScriptableObject", menuName = "ScriptableObjects/Maps", order = 1)]
public class MapScriptableObject : ScriptableObject
{
    public int width, height;
}
