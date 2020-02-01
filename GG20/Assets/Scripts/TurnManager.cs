using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public float TreeOffset = 1f;
    public float TreeTime = 1f;
    private float currentTreeTime;

    public float ZonamiTime = 0.6f;
    private float currentZonamiTime;

    private MapGenerator mapGen;

    void Start()
    {
        currentZonamiTime = ZonamiTime;
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

            //mapGen.setTiles(GridManager.Singleton.TypeToDiffuse[typeof(Tree)].Grid, new Color(0.8f, 0.2f, 0.1f), new Color(0, 0, 1));
        }

        currentZonamiTime -= Time.deltaTime;
        if (currentZonamiTime < 0)
        {
            currentZonamiTime += ZonamiTime;

            var zonami = new GameObject().AddComponent<Zonami>();
            zonami.Init(Random.Range(0, GridManager.Singleton.Width), Random.Range(0, GridManager.Singleton.Height));
            GridManager.Singleton.TypeToDiffuse[typeof(Height)].AddEffect(zonami);
        }


        mapGen.updateMap(false);

    }
}
