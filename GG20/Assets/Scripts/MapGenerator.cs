using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject tile;

    private float Width = 20;
    private float Height = 20;

    public float heightScale = 0.1f;
    public float clrMin = 0.4f;

    public float mergeVal = 0.05f;

    public float animTimeSec = 1f;

    private List<List<Tile>> tileList = null;

    private int currMap = 1;


    private Color heightPosClr = new Color(0.8f, 0.2f, 0.1f);
    private Color heightNegClr = new Color(0, 0, 1);

    private Color treePosClr = new Color(0, 1, 0);
    private Color treeNegClr = new Color(0.4f, 0.4f, 0.4f);

 

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
        updateMap();
    }

    public void updateMap()
    {
        if (currMap == 0)
        {
            setTiles(GridManager.Singleton.TypeToDiffuse[typeof(Height)], GridManager.Singleton.TypeToDiffuse[typeof(Height)], heightPosClr, heightNegClr);
        }
        else if (currMap == 1)
        {
            setTiles(GridManager.Singleton.TypeToDiffuse[typeof(Height)], GridManager.Singleton.TypeToDiffuse[typeof(Tree)], treePosClr, treeNegClr);
        }
    }

    public void updateTiles(DiffuseAble heightLst, DiffuseAble valLst, Color clrPos, Color clrNeg)
    {
        //float heightMult = (lst.Count + lst[0].L.Count) / 2 * heightScale;
        
        for (int x = 0; x < GridManager.Singleton.Width; x++)
        {

            for (int y = 0; y < GridManager.Singleton.Height; y++)
            {
                Tile currTile = tileList[x][y];

                currTile.posClr = clrPos;
                currTile.negClr = clrNeg;

                //Tile tileHandler = currTile.GetComponent<Tile>();

                float currHeight = heightLst.GetValueWithOutEffects(x, y);
                float currVal = valLst.GetValueWithEffects(x, y);

                //float currHeight = mapHeight(currVal);
                //Debug.Log("2gen tile " + x + "," + y + " val: " + currVal);

                currTile.setHeight(currHeight);
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

    public void setTiles(DiffuseAble heightLst, DiffuseAble valLst, Color clrPos, Color clrNeg)
    {
        if (tileList == null)
        {
            genTiles(heightLst, heightPosClr, heightNegClr);
        }

        updateTiles(heightLst, valLst, clrPos, clrNeg);
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
