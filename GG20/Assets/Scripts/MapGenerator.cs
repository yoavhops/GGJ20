using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tile;
    public GameObject carbonPoint;

    private float Width = 20;
    private float Height = 20;

    public float heightScale = 0.1f;
    public float clrMin = 0.4f;

    public float mergeVal = 0.05f;

    public float animTimeSec = 1f;

    private List<List<Tile>> tileList = null;

    private int currMap = 1;


    public Color heightPosClr = new Color(0.8f, 0.2f, 0.1f);
    public Color heightNegClr = new Color(0, 0, 1);

    public Color treePosClr = new Color(0, 1, 0);
    public Color treeNegClr = new Color(0.4f, 0.4f, 0.4f);

    public Color saltPosClr = new Color(0.8f, 0.8f, 0.8f);
    public Color saltNegClr = new Color(0.3f, 0.2f, 0.1f);

    public Color temperaturePosClr = new Color(1, 0, 0);
    public Color temperatureNegClr = new Color(1, 0, 1);

    public Color nutrientsPosClr = new Color(0.3f, 0.5f, 0);
    public Color nutrientsNegClr = new Color(0, 0.5f, 0.3f);

    public Color hydrationPosClr = new Color(0.3f, 0.5f, 0);
    public Color hydrationNegClr = new Color(0, 0.5f, 0.3f);


    DiffuseAble heightMap, treeCityMap, saltMap, temperatureMap, nutrientsMap, hydrationMap;


    public void setHydrationMap()
    {
        changeSource(5);
    }
    public void setNutrientsMap()
    {
        changeSource(4);
    }
    public void setTemperatureMap()
    {
        changeSource(3);
    }
    public void setSaltMap()
    {
        changeSource(2);
    }
    public void setTreeMap()
    {
        changeSource(1);
    }
    public void setHeightMap()
    {
        changeSource(0);
    }
    private void changeSource(int src)
    {
        currMap = src;
        updateMap(false);
    }

    public void updateMap(bool setHeight)
    {
        updateLists();
        if (currMap == 0)
        {
            setTiles(heightMap, heightMap, heightPosClr, heightNegClr, 0, setHeight);
        }
        else if (currMap == 1)
        {
            setTiles(heightMap, treeCityMap, treePosClr, treeNegClr, 2, setHeight);
        }
        else if (currMap == 2)
        {
            setTiles(heightMap, saltMap, saltPosClr, saltNegClr, 1, setHeight);
        }
        else if (currMap == 3)
        {
            setTiles(heightMap, temperatureMap, temperaturePosClr, temperatureNegClr, 1, setHeight);
        }
        else if (currMap == 4)
        {
            setTiles(heightMap, nutrientsMap, nutrientsPosClr, nutrientsNegClr, 1, setHeight);
        }
        else if (currMap == 5)
        {
            setTiles(heightMap, hydrationMap, hydrationPosClr, hydrationNegClr, 1, setHeight);
        }
    }

    public void updateTiles(DiffuseAble heightLst, DiffuseAble valLst, Color clrPos, Color clrNeg, int clrMergeOption, bool setHeight)
    {
        //float heightMult = (lst.Count + lst[0].L.Count) / 2 * heightScale;
        
        for (int x = 0; x < GridManager.Singleton.Width; x++)
        {

            for (int y = 0; y < GridManager.Singleton.Height; y++)
            {
                Tile currTile = tileList[x][y];

                currTile.posClr = clrPos;
                currTile.negClr = clrNeg;

                currTile.clrMergeOption = clrMergeOption;

                //Tile tileHandler = currTile.GetComponent<Tile>();

                float currHeight = heightLst.GetValueWithOutEffects(x, y);
                float currHeightVal = heightLst.GetValueWithEffects(x, y); // for color
                float currVal = valLst.GetValueWithEffects(x, y);

                float currCityVal = treeCityMap.GetValueWithEffects(x,y);
                if (currCityVal > 0)
                {
                    //currCityVal *= -1;
                    currCityVal = currCityVal * Time.deltaTime / 200;
                    if (Random.Range(0f,1f) < currCityVal)
                    {
                        //Debug.Log("POINT POP AT: " + x + "," + y);
                        GameObject carbonPointObj = Instantiate(carbonPoint);
                        carbonPointObj.transform.position = new Vector3(currTile.transform.position.x, currTile.transform.lossyScale.y, currTile.transform.position.z);

                    }
                }

                //float currHeight = mapHeight(currVal);
                //Debug.Log("2gen tile " + x + "," + y + " val: " + currVal);

                if (setHeight)
                {
                    currTile.setHeight(currHeight);
                }
                currTile.setHeightVal(currHeightVal);
                currTile.setVal(currVal);
                //currTile.setColor(currClr);
            }
        }
    }

    public void genTiles(DiffuseAble lst, Color clrPos, Color clrNeg)
    {
        tileList = new List<List<Tile>>();
        for (int x=0; x<GridManager.Singleton.Width; x++)
        {
            tileList.Add(new List<Tile>());
            for (int y=0; y<GridManager.Singleton.Height; y++)
            {
                GameObject currTile = Instantiate(tile);
                Tile tileHandler = currTile.GetComponent<Tile>();
                tileHandler.hPosClr = clrPos;
                tileHandler.hNegClr = clrNeg;
                tileHandler.heightScale = (GridManager.Singleton.Width + GridManager.Singleton.Height) / 2 * heightScale;
                tileHandler.animTime = animTimeSec;
                tileHandler.clrMin = clrMin;
                tileHandler.mergeVal = mergeVal;
                currTile.transform.parent = transform;
                tileHandler.setPos(new Vector3(x, 0, y));
                tileList[x].Add(tileHandler);
            }
        }
    }

    public void setTiles(DiffuseAble heightLst, DiffuseAble valLst, Color clrPos, Color clrNeg, int clrMergeOption, bool setHeight)
    {
        if (tileList == null)
        {
            genTiles(heightLst, heightPosClr, heightNegClr);
        }

        updateTiles(heightLst, valLst, clrPos, clrNeg, clrMergeOption, setHeight);
    }

    private void updateLists()
    {
        heightMap = GridManager.Singleton.TypeToDiffuse[typeof(Height)];
        treeCityMap = GridManager.Singleton.TypeToDiffuse[typeof(Tree)];
        saltMap = GridManager.Singleton.TypeToDiffuse[typeof(SaltLevels)];
        temperatureMap = GridManager.Singleton.TypeToDiffuse[typeof(Temperature)];
        nutrientsMap = GridManager.Singleton.TypeToDiffuse[typeof(Nutrients)];
        hydrationMap = GridManager.Singleton.TypeToDiffuse[typeof(Hydration)];
    }

    // Start is called before the first frame update
    void Start()
    {
        /*
        List<List<float>> tiles = new List<List<float>>();
        for (var x = 0; x < Width; x++)
        {
            tiles.Add(new List<float>());
            for (var y = 0; y < Height; y++)
            {
                tiles[x].Add(Random.Range(-1f, 1f));
            }
        }
        */

        //genTiles(GridManager.Singleton.Grid, new Color(0.8f, 0.2f, 0.1f), new Color(0,0,1));
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateTile
        for (int x = 0; x < tileList.Count; x++)
        {

            for (int y = 0; y < tileList[x].Count; y++)
            {
                Tile currTile = tileList[x][y];
                currTile.UpdateTile(Time.deltaTime);
            }
        }
    }
}
