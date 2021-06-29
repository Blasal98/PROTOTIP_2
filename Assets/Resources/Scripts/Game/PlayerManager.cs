using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject mapPrefab;
    public MapManager map;

    private GameManager gM;

    void Start()
    {
        gM = GameObject.Find("GameManager").GetComponent<GameManager>();
        map = Instantiate(mapPrefab, transform).GetComponent<MapManager>();
    }

    void Update()
    {
        
    }
}
