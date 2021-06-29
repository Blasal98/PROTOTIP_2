using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellScriptableObject", menuName = "ScriptableObjects/Cells", order = 1)]
public class CellScriptableObject : ScriptableObject
{
    public GameObject cell_Prefab;
    public GameObject cell_base_unfilled_Prefab;
    public GameObject cell_base_filled_Prefab;
}
