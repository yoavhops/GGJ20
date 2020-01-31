using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float TreeOffset = 1f;
    public float TreeTime = 1f;
    private float currentTreeTime = 1f;

    private MapGenerator mapGen;

    void Start()
    {
        currentTreeTime = TreeTime + TreeOffset;
        mapGen = GameObject.Find("/Map").GetComponent<MapGenerator>();
    }
    // Update is called once per frame
    void Update()
    {
        currentTreeTime -= Time.deltaTime;

        if (currentTreeTime <= 0)
        {
            currentTreeTime += TreeTime;
            GridManager.Singleton.TypeToDiffuse[typeof(Tree)].FullDiffuse();

            mapGen.updateMap();
            //mapGen.setTiles(GridManager.Singleton.TypeToDiffuse[typeof(Tree)].Grid, new Color(0.8f, 0.2f, 0.1f), new Color(0, 0, 1));
        }

    }
}
